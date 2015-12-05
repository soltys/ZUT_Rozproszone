using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MatrixService.Common;

namespace MatrixService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MatrixService" in both code and config file together.
    public class MatrixService : IMatrixService
    {
        public MatrixResponse MultiplyMatrix(MathMatrix a, MathMatrix b)
        {
            MatrixResponse multiplyMatrixResponse = new MatrixResponse();
            if (CheckMatrixData(a, b, ref multiplyMatrixResponse)) return multiplyMatrixResponse;

            var result = a.Multiply(b);

            return new MatrixResponse
            {
                Matrix = new MathMatrix()
               {
                   Columns = a.Rows,
                   Rows = b.Columns,
                   Data = result
               },
                Meta = new MetaResponse()
                {
                    Message = "Successful multiplication",
                    Status = 0,
                }
            };
        }

        public MatrixResponse MultiplyMatrixById(int aId, int bId)
        {
            string aMatrixFilePath;
            MatrixResponse matrixResponse = new MatrixResponse();
            if (ServiceActions.CheckIfMatrixIdExists(aId, out aMatrixFilePath, ref matrixResponse)) return matrixResponse;


            string bMatrixFilePath;
            if (ServiceActions.CheckIfMatrixIdExists(bId, out bMatrixFilePath, ref matrixResponse)) return matrixResponse;

            var matrixA = ServiceActions.GetMathMatrixFromFile(aMatrixFilePath);
            var matrixB = ServiceActions.GetMathMatrixFromFile(bMatrixFilePath);
            if (CheckMatrixData(matrixA, matrixB, ref matrixResponse)) return matrixResponse;

            var afterMultiply = matrixA.Multiply(matrixB);

            return new MatrixResponse
            {
                Matrix = new MathMatrix
                {
                    Rows = matrixA.Rows,
                    Columns = matrixB.Columns,
                    Data = afterMultiply
                },
                Meta = new MetaResponse
                {
                    Message = string.Format("Successful multiplication matrix with Id {0} and {1}", aId, bId),
                    Status = 0
                }
            };

        }


        

        public MatrixResponse GenerateMatrix(int rows, int columns)
        {
            if (rows <= 0)
            {
                return new MatrixResponse
                {
                    Meta = new MetaResponse
                    {
                        Message = "Rows less or equal zero",
                        Status = 1,
                    }
                };
            }
            if (columns <= 0)
            {
                return new MatrixResponse
                {
                    Meta = new MetaResponse
                    {
                        Message = "Columns less or equal zero",
                        Status = 1,
                    }
                };
            }


            Random random = new Random();
            var matrix = new int[rows][];
            for (int row = 0; row < rows; row++)
            {
                matrix[row] = new int[columns];
            }


            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    matrix[row][column] = random.Next(256);
                }
            }

            return new MatrixResponse
            {
                Matrix = new MathMatrix()
                {
                    Columns = columns,
                    Rows = rows,
                    Data = matrix
                },

                Meta = new MetaResponse
                {
                    Message = "Matrix created",
                    Status = 0,
                }
            };

        }

        public int GenerateMatrixWithId(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
            {
                return -1;
            }

            var matrixDirectoryPath = "./matrix";
            if (!Directory.Exists(matrixDirectoryPath))
            {
                Directory.CreateDirectory(matrixDirectoryPath);
            }
            var fileId = ServiceActions.GetFileId();

            var randomGenerator = new Random();
            var generatedOutput = "";
            using (StringWriter writer = new StringWriter())
            {
                writer.WriteLine("{0} {1}", rows, columns);
                for (int row = 0; row < rows; row++)
                {
                    for (int column = 0; column < columns; column++)
                    {
                        writer.Write(randomGenerator.Next(256) + " ");
                    }

                    writer.Write(Environment.NewLine);
                }

                generatedOutput = writer.ToString();
            }


            var matrixFilePath = Path.Combine(matrixDirectoryPath, fileId + ".txt");
            using (StreamWriter writer = new StreamWriter(matrixFilePath))
            {
                writer.Write(generatedOutput);
            }

            return fileId;
        }

        public MatrixResponse GetMatrixById(int id)
        {
            string filePath;
            MatrixResponse matrixResponse = new MatrixResponse();
            if (ServiceActions.CheckIfMatrixIdExists(id, out filePath, ref matrixResponse)) return matrixResponse;

            var mathMatrix = ServiceActions.GetMathMatrixFromFile(filePath);
            return new MatrixResponse
            {
                Matrix = mathMatrix,
                Meta = new MetaResponse
                {
                    Message = "Returned matrix with Id:" + id,
                    Status = 0
                }
            };


        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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



        private static bool CheckMatrixData(MathMatrix a, MathMatrix b, ref MatrixResponse multiplyMatrix)
        {
            //matrix check

            if (a.Columns != b.Rows)
            {
                {
                    multiplyMatrix = new MatrixResponse
                    {
                        Meta = new MetaResponse
                        {
                            Message = "Multiplication imposible",
                            Status = 1,
                        }
                    };
                    return true;
                }
            }

            //Declared zero or negative size
            if (a.Rows <= 0 || b.Rows <= 0 || a.Columns <= 0 || b.Columns <= 0)
            {
                {
                    multiplyMatrix = new MatrixResponse
                    {
                        Meta = new MetaResponse
                        {
                            Message = "Every matrix dimention need to be positive",
                            Status = 1,
                        }
                    };
                    return true;
                }
            }
            // Null Check
            if (a.Data == null || b.Data == null)
            {
                {
                    multiplyMatrix = new MatrixResponse
                    {
                        Meta = new MetaResponse
                        {
                            Message = "Data cannot be null",
                            Status = 1,
                        }
                    };
                    return true;
                }
            }

            //Row Check
            if (a.Data.Length != a.Rows || b.Data.Length != b.Rows)
            {
                {
                    multiplyMatrix = new MatrixResponse
                    {
                        Meta = new MetaResponse
                        {
                            Message = "Declared rows size not meet with data",
                            Status = 1,
                        }
                    };
                    return true;
                }
            }


            if (ColumnCheck(a) || ColumnCheck(a))
            {
                {
                    multiplyMatrix = new MatrixResponse
                    {
                        Meta = new MetaResponse
                        {
                            Message = "Declared column size not meet with data",
                            Status = 1,
                        }
                    };
                    return true;
                }
            }
            return false;
        }

        private static bool ColumnCheck(MathMatrix a)
        {
            bool columnCheck = false;
            for (int row = 0; row < a.Rows; row++)
            {
                if (a.Data[row].Length != a.Columns)
                {
                    return true;
                }
            }
            return false;
        }

        public MatrixResponse MultiplyMatrixById(int aId, int bId)
        {
            string aMatrixFilePath;
            MatrixResponse matrixResponse = new MatrixResponse();
            if (CheckIfMatrixIdExists(aId, out aMatrixFilePath, ref matrixResponse)) return matrixResponse;


            string bMatrixFilePath;
            if (CheckIfMatrixIdExists(bId, out bMatrixFilePath, ref matrixResponse)) return matrixResponse;

            var matrixA = GetMathMatrixFromFile(aMatrixFilePath);
            var matrixB = GetMathMatrixFromFile(bMatrixFilePath);
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
                    Message = string.Format("Successful multiplication matrix with Id {0} and {1}",aId, bId),
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
            var fileId = GetFileId();

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

        private static int GetFileId()
        {
            int id = 1;

            while (File.Exists(Path.Combine("./matrix", id + ".txt")))
            {
                id++;
            }

            return id;
        }

        public MatrixResponse GetMatrixById(int id)
        {
            string filePath;
            MatrixResponse matrixResponse = new MatrixResponse();
            if (CheckIfMatrixIdExists(id, out filePath, ref matrixResponse)) return matrixResponse;

            var mathMatrix = GetMathMatrixFromFile(filePath);
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

        private static bool CheckIfMatrixIdExists(int id, out string filePath, ref MatrixResponse matrixResponse)
        {
            filePath = Path.Combine("./matrix", id + ".txt");
            if (!File.Exists(filePath))
            {
                {
                    matrixResponse = new MatrixResponse
                    {
                        Meta = new MetaResponse
                        {
                            Message = string.Format("Matrix with set id {0} do not exist", id),
                            Status = -1
                        }
                    };
                    return true;
                }
            }
            return false;
        }

        private static MathMatrix GetMathMatrixFromFile(string filePath)
        {
            MathMatrix mathMatrix;
            using (StreamReader reader = new StreamReader(filePath))
            {
                var matrixSizeLine = reader.ReadLine();

                var size = matrixSizeLine.Split(' ');
                var rows = int.Parse(size[0]);
                var columns = int.Parse(size[1]);


                var matrix = new int[rows][];
                for (int row = 0; row < rows; row++)
                {
                    matrix[row] = new int[columns];
                }

                for (int row = 0; row < rows; row++)
                {
                    var rowLine = reader.ReadLine();
                    var columnTokens = rowLine.Split(' ');
                    for (int column = 0; column < columns; column++)
                    {
                        matrix[row][column] = int.Parse(columnTokens[column]);
                    }
                }
                mathMatrix = new MathMatrix()
                {
                    Rows = rows,
                    Columns = columns,
                    Data = matrix
                };
            }
            return mathMatrix;
        }
    }
}

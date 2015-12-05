using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Soltys.MatrixService.DTO;

namespace Soltys.GenerateMatrixService
{

    public class GenerateMatrixService : IGenerateMatrixService
    {

        public MatrixRes GenerateMatrix(int rows, int columns)
        {
            if (rows <= 0)
            {
                return new MatrixRes
                {
                    Meta = new MetaRes
                    {
                        Message = "Rows less or equal zero",
                        Status = 1,
                    }
                };
            }
            if (columns <= 0)
            {
                return new MatrixRes
                {
                    Meta = new MetaRes
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

            return new MatrixRes
            {
                Matrix = new MathMatrix()
                {
                    Columns = columns,
                    Rows = rows,
                    Data = matrix
                },

                Meta = new MetaRes
                {
                    Message = "Matrix created",
                    Status = 0,
                }
            };

        }
        static object synchronize = new object();
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
            int fileId;
            lock (synchronize)
            {
                fileId = ServiceActions.GetFileId();
            }


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
                writer.Flush();
            }


            var matrixFilePath = Path.Combine(matrixDirectoryPath, fileId + ".txt");
            using (StreamWriter writer = new StreamWriter(matrixFilePath))
            {
                writer.Write(generatedOutput);
                writer.Flush();
                writer.Close();
            }

            return fileId;
        }

        public MatrixRes GetMatrixById(int id)
        {
            string filePath;
            MatrixRes matrixRes = new MatrixRes();
            if (ServiceActions.CheckIfMatrixIdExists(id, out filePath, ref matrixRes)) return matrixRes;

            var mathMatrix = ServiceActions.GetMathMatrixFromFile(filePath);
            return new MatrixRes
            {
                Matrix = mathMatrix,
                Meta = new MetaRes
                {
                    Message = "Returned matrix with Id:" + id,
                    Status = 0
                }
            };


        }
    }
}

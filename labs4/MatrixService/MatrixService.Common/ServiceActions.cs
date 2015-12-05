using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Soltys.MatrixService.DTO
{
    public class ServiceActions
    {
        public static int GetFileId()
        {
            int id = 1;

            while (File.Exists(Path.Combine("./matrix", id + ".txt")))
            {
                id++;
            }

            return id;
        }


        public static bool CheckIfMatrixIdExists(int id, out string filePath, ref MatrixRes matrixRes)
        {
            filePath = Path.Combine("./matrix", id + ".txt");
            if (!File.Exists(filePath))
            {
                {
                    matrixRes = new MatrixRes
                    {
                        Meta = new MetaRes
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

        public static MathMatrix GetMathMatrixFromFile(string filePath)
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



        public static bool CheckMatrixData(MathMatrix a, MathMatrix b, ref MatrixRes multiplyMatrix)
        {
            //matrix check

            if (a.Columns != b.Rows)
            {
                {
                    multiplyMatrix = new MatrixRes
                    {
                        Meta = new MetaRes
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
                    multiplyMatrix = new MatrixRes
                    {
                        Meta = new MetaRes
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
                    multiplyMatrix = new MatrixRes
                    {
                        Meta = new MetaRes
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
                    multiplyMatrix = new MatrixRes
                    {
                        Meta = new MetaRes
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
                    multiplyMatrix = new MatrixRes
                    {
                        Meta = new MetaRes
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

        public static bool ColumnCheck(MathMatrix a)
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
    }
}

using Soltys.MatrixService.DTO;
using Soltys.Service.ServiceReference1;

namespace Soltys.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MatrixService" in both code and config file together.
    public class MatrixService : IMatrixService
    {
       
        public MatrixRes MultiplyMatrix(MathMatrix a, MathMatrix b)
        {
            MatrixRes multiplyMatrixRes = new MatrixRes();
            if (ServiceActions.CheckMatrixData(a, b, ref multiplyMatrixRes)) return multiplyMatrixRes;

            var result = a.Multiply(b);

            return new MatrixRes
            {
                Matrix = new MathMatrix()
               {
                   Columns = a.Rows,
                   Rows = b.Columns,
                   Data = result
               },
                Meta = new MetaRes()
                {
                    Message = "Successful multiplication",
                    Status = 0,
                }
            };
        }

        public MatrixRes MultiplyMatrixById(int aId, int bId)
        {

            GenerateMatrixServiceClient client = new GenerateMatrixServiceClient();
            MatrixRes matrixRes = new MatrixRes();
            var matrixA = client.GetMatrixById(aId).Matrix;
            var matrixB = client.GetMatrixById(bId).Matrix;

           
            if (ServiceActions.CheckMatrixData(matrixA, matrixB, ref matrixRes)) return matrixRes;

            var afterMultiply = matrixA.Multiply(matrixB);

            return new MatrixRes
            {
                Matrix = new MathMatrix
                {
                    Rows = matrixA.Rows,
                    Columns = matrixB.Columns,
                    Data = afterMultiply
                },
                Meta = new MetaRes
                {
                    Message = string.Format("Successful multiplication matrix with Id {0} and {1}", aId, bId),
                    Status = 0
                }
            };

        }

        public MatrixRes GenerateMatrix(int rows, int columns)
        {
            GenerateMatrixServiceClient client = new GenerateMatrixServiceClient();

            var response = client.GenerateMatrix(rows, columns);

            client.Close();
            return response;
        }

        public int GenerateMatrixWithId(int rows, int columns)
        {
            GenerateMatrixServiceClient client = new GenerateMatrixServiceClient();

            var response = client.GenerateMatrixWithId(rows, columns);

            client.Close();
            return response;
        }

        public MatrixRes GetMatrixById(int id)
        {
            GenerateMatrixServiceClient client = new GenerateMatrixServiceClient();
            var response = client.GetMatrixById(id);
            client.Close();
            return response;
        }
    }
}

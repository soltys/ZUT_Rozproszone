using System.ServiceModel;
using Soltys.MatrixService.DTO;

namespace Soltys.Service
{
    [ServiceContract]
    public interface IMatrixService
    {

        [OperationContract]
        MatrixRes MultiplyMatrix(MathMatrix a, MathMatrix b);

        [OperationContract]
        MatrixRes MultiplyMatrixById(int aId, int bId);

        [OperationContract]
        MatrixRes GenerateMatrix(int rows, int columns);

        [OperationContract]
        int GenerateMatrixWithId(int rows, int columns);

        [OperationContract]
        MatrixRes GetMatrixById(int id);

      


    }

 
    
}

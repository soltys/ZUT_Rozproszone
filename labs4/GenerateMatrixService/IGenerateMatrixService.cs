using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Soltys.MatrixService.DTO;

namespace Soltys.GenerateMatrixService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGenerateMatrixService" in both code and config file together.
    [ServiceContract]
    [ServiceKnownType(typeof(MatrixRes))]
    public interface IGenerateMatrixService
    {
        [OperationContract]
        MatrixRes GenerateMatrix(int rows, int columns);

        [OperationContract]
        int GenerateMatrixWithId(int rows, int columns);

        [OperationContract]
        MatrixRes GetMatrixById(int id);

    }

  
}

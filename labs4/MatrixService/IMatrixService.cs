using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MatrixService.Common;

namespace MatrixService
{
    [ServiceContract]
    public interface IMatrixService
    {

        [OperationContract]
        MatrixResponse MultiplyMatrix(MathMatrix a, MathMatrix b);

        [OperationContract]
        MatrixResponse MultiplyMatrixById(int aId, int bId);




    }

 
    
}

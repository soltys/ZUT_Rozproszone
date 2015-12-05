using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MatrixService.Common;

namespace GenerateMatrixService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public MatrixResponse GenerateMatrix(int rows, int columns)
        {
            throw new NotImplementedException();
        }

        public int GenerateMatrixWithId(int rows, int columns)
        {
            throw new NotImplementedException();
        }

        public MatrixResponse GetMatrixById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

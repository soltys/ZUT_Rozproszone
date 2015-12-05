using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MatrixService
{
    [ServiceContract]
    public interface IMatrixService
    {

        [OperationContract]
        MatrixResponse MultiplyMatrix(MathMatrix a, MathMatrix b);

        [OperationContract]
        MatrixResponse MultiplyMatrixById(int aId, int bId);

        [OperationContract]
        MatrixResponse GenerateMatrix(int rows, int columns);

        [OperationContract]
        int GenerateMatrixWithId(int rows, int columns);

        [OperationContract]
        MatrixResponse GetMatrixById(int id);



    }

    [DataContract]
    public class MathMatrix
    {
        [DataMember]
        public int Rows { get; set; }
        [DataMember]
        public int Columns { get; set; }
        [DataMember]
        public int[][] Data { get; set; }

        public int[][] Multiply(MathMatrix b)
        {
            int[][] result = MatrixCreate(Rows, b.Columns);

            for (int aRow = 0; aRow < Rows; ++aRow)
            {
                for (int bColumn = 0; bColumn < b.Columns; ++bColumn)
                {
                    for (int aColumn = 0; aColumn < Columns; ++aColumn)
                    {
                        result[aRow][bColumn] += Data[aRow][aColumn] * b.Data[aColumn][bColumn];
                    }
                }
            }
            return result;
        }

        public static int[][] MatrixCreate(int rows, int cols)
        {
            int[][] result = new int[rows][];
            for (int i = 0; i < rows; ++i)
            {
                result[i] = new int[cols];
            }
            return result;
        }
    }

    [DataContract]
    public class MatrixResponse
    {
        [DataMember]
        public MathMatrix Matrix { get; set; }
        [DataMember]
        public MetaResponse Meta { get; set; }
    }

    [DataContract]
    public class MetaResponse
    {
        private DateTime _time;

        public MetaResponse()
        {
            _time = DateTime.Now;
        }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public DateTime Time { get { return _time; } set { _time = value; } }
    }

    
}

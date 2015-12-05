using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Soltys.Service.Client.ServiceReference1;

namespace Soltys.Service.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MatrixServiceClient client = new MatrixServiceClient();

            int problemSize = 10;

            Task<int>[] generationTasks = new Task<int>[problemSize];

            for (int matrix = 0; matrix < problemSize; matrix++)
            {
                Console.WriteLine("Running {0} task", matrix);
                generationTasks[matrix] = client.GenerateMatrixWithIdAsync(10, 10);
            }

            Console.WriteLine("Waiting for service to complete generation of matrixes");
            Task<int>.WaitAll(generationTasks);
            Console.WriteLine("Generation done");

            List<Task> tasks = new List<Task>();
            for (int matrix = 0; matrix < problemSize - 1; matrix++)
            {
                var matrixA = generationTasks[matrix].Result;
                var matrixB = generationTasks[matrix + 1].Result;

                Console.WriteLine("Stating multiplication of {0} and  {1} matrixes, date: ", matrixA, matrixB, DateTime.Now);

                var multiplicationTask = client.MultiplyMatrixByIdAsync(matrixA, matrixB);
                multiplicationTask.ContinueWith((taskResult) =>
                {
                    var response = taskResult.Result;
                    Console.WriteLine("{0} with date: {1}, first row of computation {2}", response.Meta.Message, response.Meta.Time, string.Join(" ", response.Matrix.Data[0]));
                    
                });

                tasks.Add(multiplicationTask);
            }


            Console.WriteLine("Waiting to complete all multiplications");
            var taskArray = tasks.ToArray();
            Task.WaitAll(taskArray);
            
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

        }
    }
}

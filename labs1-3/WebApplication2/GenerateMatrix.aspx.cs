using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class GenerateMatrix : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var rowsQuery = Request.QueryString["rows"];
            var rows = int.Parse(rowsQuery);
            var columnQuery = Request.QueryString["columns"];
            var columns = int.Parse(columnQuery);
            var name = Request.QueryString["name"];

            var matrixDirectoryPath = Server.MapPath("~/matrix");
            if (!Directory.Exists(matrixDirectoryPath))
            {
                Directory.CreateDirectory(matrixDirectoryPath);
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
                        writer.Write(randomGenerator.Next() + " ");
                    }

                    writer.Write(Environment.NewLine);
                }

                generatedOutput = writer.ToString();
            }
            this.Output.Text = generatedOutput;

            var matrixFilePath = Path.Combine(matrixDirectoryPath, name + ".txt");
            using (StreamWriter writer = new StreamWriter(matrixFilePath))
            {
                writer.Write(generatedOutput);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class DoSthWithFiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.Text = "";
        }



        protected void Go_Click(object sender, EventArgs e)
        {
            Stream streamOne = null;
            Stream streamTwo = null;
            var path = Server.MapPath("~/App_Data/JoinFile.txt");
            try
            {
                streamOne = One.FileContent;
                streamTwo = Two.FileContent;

                using (StreamReader readerOne = new StreamReader(streamOne))
                using (StreamReader readerTwo = new StreamReader(streamTwo))
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(readerOne.ReadToEnd());
                    writer.Write(readerTwo.ReadToEnd());
                    
                }
                HyperLink1.Text = "Output";
                HyperLink1.NavigateUrl = "~/App_Data/JoinFile.txt";

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (streamOne != null
                )
                {
                    streamOne.Close();
                }
                if (streamTwo != null)
                {
                    streamTwo.Close();
                }

            }



        }
    }
}
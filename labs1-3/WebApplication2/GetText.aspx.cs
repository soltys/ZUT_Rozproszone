using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class GetText : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                var path = Server.MapPath("~/App_Data/JoinText.txt");
                this.Output.Text = string.Format("FL: {0}, LL:{1}, CWD:{2}", this.FirstLine.Text, this.LastLine.Text, path);
                
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(this.FirstLine.Text + this.LastLine.Text);
                }
            }
            
        }
    }
}
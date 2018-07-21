using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
namespace TextEditorWeb
{
    public partial class Editor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //hdText.Value = "sss";
        }

        protected void btnText_Click(object sender, EventArgs e)
        {
           // string content = hdText.Value;
           // Label1.Text = content;
           // Label1.Text = txtReText.Value;
            //txtReText.Text = hdText.Value;

           /* SqlConnection cc = new SqlConnection();
            try
            {

                cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
                cc.Open();
                string sql = "insert into members values(@user,@passw,@mail,@acc,@gende);";
                SqlCommand cmd = new SqlCommand(sql, cc);
                

                int i = cmd.ExecuteNonQuery();


            }
            catch (Exception ee)
            {
                Label1.Text = ee.Message;
            }
            finally
            {
                cc.Close();
            }
            */
        }
    }
}
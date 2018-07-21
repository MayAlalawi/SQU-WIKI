using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;

using System.Data;

using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection cc = new SqlConnection();
        try
        {
            string search = Request.QueryString["data"];
            cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
            cc.Open();
            string sql = "select * from  pages where pageName like '%" + search + "%'";
            SqlCommand cmd = new SqlCommand(sql, cc);
            SqlDataReader reader = cmd.ExecuteReader();
            Label l = new Label();
            l.Attributes["style"] = "color:#00ccff; font-weight:bold; font-size:20px";
            l.Text = "Search Result<br>";

            searcher.Controls.Add(l);
            while (reader.Read())
            {
                HyperLink a = new HyperLink();
                a.NavigateUrl = "Default.aspx?ptitle=" + reader[0].ToString();
                a.Text = reader[0].ToString() + "<br><br>";
                searcher.Controls.Add(a);

                //SearchResult.Text += reader[0];
            }


            reader.Close();

            /*  //searching in files
               sql = "select * from  UploadedFiles where FileName like('%'" + search + "'%';";
               cmd = new SqlCommand(sql, cc);
               reader = cmd.ExecuteReader();

              while (reader.Read())
              {
                  HyperLink a = new HyperLink();
                  a.Text = reader[0].ToString();
                  a.NavigateUrl = "Default.aspx?ptitle=" + reader[0].ToString();
                  searchres.Controls.Add(a);
              }


              reader.Close();*/
        }
        catch (Exception ee)
        {
            Label1.Text = ee.Message;
            
        }
        finally
        {
            cc.Close();
        } 

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
public partial class catagories : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        SqlConnection cc = new SqlConnection();
        try
        {
            cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
            cc.Open();
            string sql = "select pageName,lastmodification from pages where catagory='" + Request.QueryString["catName"] + "'";
            SqlCommand cmd = new SqlCommand(sql, cc);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TableRow r = new TableRow();
                TableCell c = new TableCell();
                HyperLink hr = new HyperLink();
                hr.Text=reader[0].ToString();
                hr.NavigateUrl = "Default.aspx?ptitle=" + reader[0].ToString();
                c.Controls.Add(hr);
                r.Controls.Add(c);
                TableCell cd = new TableCell();
                cd.Text = reader[1].ToString();
                r.Controls.Add(cd);
                Table1.Controls.Add(r);


            }

            reader.Close();

           
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
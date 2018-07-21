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


public partial class AddMedia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         SqlConnection cc = new System.Data.SqlClient.SqlConnection();
            cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
            cc.Open();
            string sql;
            
            SqlDataReader reader;
        sql="select location,FileName,description,f_type from UploadedFiles;";
        SqlCommand cmd=new SqlCommand(sql,cc);
        reader=cmd.ExecuteReader();
        TableCell[] cells = new TableCell[3];
        int m = 0;
        Table1.Controls.Clear();
        TableRow r = new TableRow();
        while (reader.Read())
        {
            
            TableCell c = new TableCell();
            string t=reader[3].ToString();
            t=t.ToLower();
            if (t == ".jpg" || t == ".jpeg" || t == ".gif" || t == ".ping" || t == ".png" || t == ".jpg" || t == ".bmp")
            {
                System.Web.UI.WebControls.Image i = new System.Web.UI.WebControls.Image();
                i.Width = 100;
                i.Height = 100;
                i.ImageUrl = reader[0].ToString();
                i.ToolTip = reader[1].ToString();
                c.Controls.Add(i);
            }
            else
            {
                HyperLink i = new HyperLink();
                i.Text = reader[1].ToString();
                i.NavigateUrl = "download.aspx?file=" + reader[0];
                c.Controls.Add(i);
            }
            Label l = new Label();
            l.Text = "<br>"+reader[2].ToString();
           
            
            c.Controls.Add(l);
           // cells[m++] = c;
            m++;
                r.Controls.Add(c);
           
            if(m>=2)
            {
                Table1.Controls.Add(r);
                r = new TableRow();
                m = 0;
            }
        } 
        
            
    }

    protected void MultipleFileUpload1_Click(object sender, EventArgs e)
    {

        HttpPostedFile myFile = filMyFile.PostedFile;
        string strFilename = Path.GetFileName(myFile.FileName);
        // Get size of uploaded file

        int nFileLen = myFile.ContentLength;

        for (int n = 0; n < nFileLen; n++)
        {
            //  oHttpPostedFile = oHttpFileCollection[n];

            myFile.SaveAs(Server.MapPath(strFilename));
        }
    }
    protected void upload_Click(object sender, EventArgs e)
    {
        HttpPostedFile myFile = filMyFile.PostedFile;
        string strFilename = Path.GetFileName(myFile.FileName);
        // Get size of uploaded file

        int nFileLen = myFile.ContentLength;

        for (int n = 0; n < nFileLen; n++)
        {
            //  oHttpPostedFile = oHttpFileCollection[n];

            myFile.SaveAs(Server.MapPath("upload/"+strFilename));
        }
        try
        {
            SqlConnection cc = new System.Data.SqlClient.SqlConnection();
            cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
            cc.Open();
            string sql;
            SqlCommand cmd;
            int reader;


            sql = "insert into UploadedFiles(FileName,location,mem_id,f_type,Uploadeddate,description) values(@fileN,@loc,@mem_id,@f_type,@date,@desc);";
            cmd = new System.Data.SqlClient.SqlCommand(sql, cc);
            cmd.Parameters.AddWithValue("@fileN", strFilename);
            cmd.Parameters.AddWithValue("@loc", "upload/" + strFilename);
            cmd.Parameters.AddWithValue("@mem_id", Session["username"]);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
          // Label1.Text= strFilename.Substring(strFilename.IndexOf('.'));
           cmd.Parameters.AddWithValue("@f_type", strFilename.Substring(strFilename.IndexOf('.')));
              cmd.Parameters.AddWithValue("@desc", desc.Text);
            reader = cmd.ExecuteNonQuery();
            cc.Close();
            desc.Text = "";
            Page_Load(sender, e);
            Label2.Text = "Your file is uploaded successfully";
        }
        catch (Exception ee)
        {
            Label1.Text = ee.Message;
        }
    }
}
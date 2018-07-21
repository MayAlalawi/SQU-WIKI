using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.Web.UI.HtmlControls;
public partial class Forms : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "";
       
            SqlConnection cc = new SqlConnection();
            Table1.Controls.Clear();
            try
            {
                cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
                cc.Open();
                string sql = "select * from Forms";
                SqlCommand cmd = new SqlCommand(sql, cc);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TableRow r = new TableRow();
                    TableCell c = new TableCell();
                    c.Width=300;
                    HyperLink hr = new HyperLink();
                    TableCell c2 = new TableCell();
                    Button delete = new Button();
                    delete.Click += new System.EventHandler(Delete_Click);
                    delete.ID =  reader[2].ToString();
                    delete.Text = "Delete";
                    delete.ToolTip = delete.ID;
                    delete.CssClass = "myButton";
                    TableCell c3 = new TableCell();
                    Button update = new Button();
                    update.CssClass = "myButton";
                    
                    update.Click += new System.EventHandler(update_Click);
                    update.ID = "update"+reader[2].ToString();
                    update.Text = "Update";
                    update.ToolTip = reader[2].ToString();

                    hr.Text = reader[0].ToString();
                    hr.NavigateUrl = "download.aspx?file=" + reader[2];
                    c.Controls.Add(hr);
                    c2.Controls.Add(delete);
                    c3.Controls.Add(update);
                    r.Controls.Add(c);
                    r.Controls.Add(c2);
                    r.Controls.Add(c3);
                    TableCell cd = new TableCell();
                    HyperLink download = new HyperLink();
                    download.Text = "Download";
                    download.NavigateUrl = "download.aspx?file=" + reader[2];
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


    protected void upload_Click(object sender, EventArgs e)
    {
        if (Session["username"] != null)
        {

            Label1.Text = "";

            HttpPostedFile myFile = filMyFile.PostedFile;
            string strFilename = Path.GetFileName(myFile.FileName);
            // Get size of uploaded file
            string filename = Server.MapPath("~\\forms\\" + strFilename);
            if ((!System.IO.File.Exists(filename)))
            {


                int nFileLen = myFile.ContentLength;

                for (int n = 0; n < nFileLen; n++)
                {
                    //  oHttpPostedFile = oHttpFileCollection[n];

                    myFile.SaveAs(Server.MapPath("forms\\" + strFilename));
                }
                try
                {
                    SqlConnection cc = new System.Data.SqlClient.SqlConnection();
                    cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
                    cc.Open();
                    string sql;
                    SqlCommand cmd;
                    int reader;


                    sql = "insert into Forms values(@fN,@fileN,@loc,@mem_id);";
                    cmd = new System.Data.SqlClient.SqlCommand(sql, cc);
                    cmd.Parameters.AddWithValue("@fileN", strFilename);
                    if (Fname.Text == "")
                        cmd.Parameters.AddWithValue("@fN", strFilename);
                    else
                        cmd.Parameters.AddWithValue("@fN", Fname.Text);
                    cmd.Parameters.AddWithValue("@loc", "forms/" + strFilename);
                    cmd.Parameters.AddWithValue("@mem_id", Session["username"]);

                    reader = cmd.ExecuteNonQuery();
                    cc.Close();

                    Page_Load(sender, e);
                    Label1.ForeColor = Color.Green;
                    Label1.Text = "Your form is uploaded successfully";
                    Fname.Text = "";
                }
                catch (Exception ee)
                {
                    Label1.ForeColor = Color.Red;
                    Label1.Text = ee.Message;
                }
            }
            else
            {

                // Response.Write("<script>alert('there is an already existing form with the same name .. Do you want to replace it ??  ');</script>");

                ClientScriptManager CSM = Page.ClientScript;
                if (!ReturnValue())
                {
                    string strconfirm = "<script>if(window.confirm('There is an already existing form with the same name .. Do you want to replace it ??')){}else{" + replace(sender,e) + ";}</script>";
                    CSM.RegisterClientScriptBlock(this.GetType(), "Confirm", strconfirm, false);
                }



            }
        }
        else
        {


            Label1.ForeColor = Color.Red;
            Label1.Text = "You must logIn before uploading any form";

        }

    }

    bool ReturnValue()
    {
        return false;
    }
    protected int replace(object sender, EventArgs e)
    {

        HttpPostedFile myFile = filMyFile.PostedFile;
        string strFilename = Path.GetFileName(myFile.FileName);
        // Get size of uploaded file
        string filename = Server.MapPath("~\\forms\\" + strFilename);


        //deleting the file 


        if ((System.IO.File.Exists(filename)))
        {
            System.IO.File.Delete(filename);

            try
            {
                SqlConnection cc = new SqlConnection();
                cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
                cc.Open();
                string sql;
                SqlCommand cmd;
                int reader;

                Label1.Text = "forms\\" + strFilename;
                sql = "delete from Forms where directory='" + "forms/" + strFilename + "'";
                cmd = new SqlCommand(sql, cc);

                reader = cmd.ExecuteNonQuery();
                cc.Close();

                Page_Load(sender, e);


                Label1.ForeColor = Color.Green;
                Label1.Text = reader.ToString();
                //Label1.Text = filename + " Deleted";
            }
            catch (Exception ee)
            {
                Label1.ForeColor = Color.Red;
                Label1.Text = ee.Message;
            }



        }





        

        //save the new file
            int nFileLen = myFile.ContentLength;

            for (int n = 0; n < nFileLen; n++)
            {
                //  oHttpPostedFile = oHttpFileCollection[n];

                myFile.SaveAs(Server.MapPath("forms\\" + strFilename));
            }
            try
            {
                SqlConnection cc = new System.Data.SqlClient.SqlConnection();
                cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
                cc.Open();
                string sql;
                SqlCommand cmd;
                int reader;


                sql = "insert into Forms values(@fN,@fileN,@loc,@mem_id);";
                cmd = new System.Data.SqlClient.SqlCommand(sql, cc);
                cmd.Parameters.AddWithValue("@fileN", strFilename);
                if (Fname.Text == "")
                    cmd.Parameters.AddWithValue("@fN", strFilename);
                else
                    cmd.Parameters.AddWithValue("@fN", Fname.Text);
                cmd.Parameters.AddWithValue("@loc", "forms/" + strFilename);
                cmd.Parameters.AddWithValue("@mem_id", Session["username"]);

                reader = cmd.ExecuteNonQuery();
                cc.Close();
                Label1.Text = "the file is replaced";
                Page_Load(sender, e);
                Label1.ForeColor = Color.Green;
                
                Fname.Text = "";
            }
            catch (Exception ee)
            {
                Label1.ForeColor = Color.Red;
                Label1.Text = ee.Message;
            }
        
        
        return 0;
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        Button a=(Button) sender;
        string fileName = Server.MapPath("~\\"+ a.ID);
         if ((System.IO.File.Exists(fileName)))
         {
             System.IO.File.Delete(fileName);

             try
             {
                 SqlConnection cc = new SqlConnection();
                 cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
                 cc.Open();
                 string sql;
                 SqlCommand cmd;
                 int reader;


                 sql = "delete from Forms where directory='"+a.ID+"'";
                 cmd = new SqlCommand(sql, cc);
                 
                 reader = cmd.ExecuteNonQuery();
                 cc.Close();

                 Page_Load(sender, e);
             
                 
                 Label1.ForeColor = Color.Green;
                Label1.Text = fileName + " Deleted";
             }
             catch (Exception ee)
             {
                 Label1.ForeColor = Color.Red;
                 Label1.Text = ee.Message;
             }


            
         } 
       // Page_Load(sender, e);
        
       
        

       // Label1.Text =a.ID +" Deleted";
    }

    protected void update_Click(object sender, EventArgs e)
    {
        Button b = (Button)sender;
        Label1.ForeColor = Color.Blue;
        Label1.Text = " ******* please upload a file with name ";
        Label1.ForeColor = Color.Green;
        Label1.Text += b.ID.Substring(12, b.ID.Length - 12);
        Label1.ForeColor = Color.Blue;
       Label1.Text +=" To be replaced with the privious form *************";
    }
}
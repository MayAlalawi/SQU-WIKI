using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
public partial class _Default : System.Web.UI.Page
{
    public string title;
    protected void Page_Load(object sender, EventArgs e)
    
    {
       //Label1.Text= Master.ptitle;
        
        if(Request.QueryString["ptitle"]!=null)
            title = Request.QueryString["ptitle"] ;
        else
            title="Home";
       this.Title = title;
       Label1.Text = "";
        //content display
       SqlConnection cc = new SqlConnection();
       try
       {
           cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
           cc.Open();
           string sql = "select * from pages where pageName='" + title + "'";
           
           SqlCommand cmd = new SqlCommand(sql, cc);
           SqlDataReader reader = cmd.ExecuteReader();

           try
           {
               while (reader.Read())
               {
                   //initialize a new instance of the System.IO.StreamReader class for the specified path and file
                   StreamReader sr = new StreamReader(reader[2].ToString());
                   string strLine;
                   while (sr.Peek() != -1)//as long as there is text to read
                   {
                       //read entire line
                       strLine = sr.ReadLine();

                       //add the line read to the list box
                       Label1.Text += strLine;
                   }
                   //close and dispose the stream
                   sr.Close();
                   sr.Dispose();

               }
           }
           catch (FileNotFoundException ex)//file not found
           {

           }
           catch (IOException ex)//file locked by another process
           {

           }
          
        /*   while (reader.Read())
           {
               Label1.Text = reader[2].ToString();//dispalying the content 
               //Label1.Text = Request.QueryString["ptitle"];
           }*/

           reader.Close();
       }
       catch (Exception ee)
       {
           /*if(Request.QueryString["error"]!=null)
               Label1.Text=Request.QueryString["error"].ToString();
           else*/
           Label1.Text = ee.Message;
       }
       finally
       {
           cc.Close();
       }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string url = "Editor.aspx?ptitle=" + title;
        Response.Redirect(url);
    }
}
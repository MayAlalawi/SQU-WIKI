using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
public partial class signUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void accessLevel(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedItem.Text == "Student")
        {
            mail.Text = "@student.squ.edu.om";
        }
        else
        {
            mail.Text = "@squ.edu.om";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
        SqlConnection cc = new SqlConnection();
        try
        {

            cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
            cc.Open();
            string sql = "insert into members values(@user,@passw,@mail,@acc,@gende);";
            SqlCommand cmd = new SqlCommand(sql, cc);
            
            cmd.Parameters.AddWithValue("@user", TextBoxUN.Text);
            cmd.Parameters.AddWithValue("@passw", TextBoxPass.Text);
            cmd.Parameters.AddWithValue("@mail", TextBoxEmail.Text + mail.Text);
            cmd.Parameters.AddWithValue("@acc", RadioButtonList1.SelectedIndex);
            cmd.Parameters.AddWithValue("@gende", DropDownList1.SelectedItem.Text);

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
        Label1.Text = "You are added successfully";
        Label1.ForeColor = Color.Green;
        Response.Redirect("Default.aspx?ptitle=Home" );
    }


    protected void ValidateUser(object sender, EventArgs e)
{
     string userName = TextBoxUN.Text;
        string pass = TextBoxPass.Text;
        string email = TextBoxEmail.Text + mail.Text;
        int access = RadioButtonList1.SelectedIndex;
        string gender = DropDownList1.SelectedItem.Text;
        SqlConnection cc = new SqlConnection();
        
    try
        {

            cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
            cc.Open();
            string sql = "select * from members";
            SqlCommand cmd = new SqlCommand(sql, cc);
            SqlDataReader reader = cmd.ExecuteReader();
        bool found=false;
            while (reader.Read())
            {
                if (reader[0].ToString() == userName)
                {
                    found=true;
                   ValidUser.Text="Used User name Please choose another username";
                    ValidUser.ForeColor=System.Drawing.Color.Red;
                    break;
                }
            }
        if(!found)
        {
            
            ValidUser.Text="Valid Name";
            ValidUser.ForeColor=System.Drawing.Color.Green;
        }
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
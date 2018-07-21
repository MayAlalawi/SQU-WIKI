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


public partial class MasterPage : System.Web.UI.MasterPage
{
    public string ptitle
    {
        get
        {
            return "Home";
        }

    }
    
    string[] news = new string[100];
   Boolean loginSettion = false; 
    String userNameSession = "u089645";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
      //  Session["username"] = "u089645";

        if (Session["login"] != null && Session["username"] != null)
        {
             loginSettion = (Boolean)Session["login"];
            // Label1.Text = loginSettion.ToString();
             userNameSession = Session["username"].ToString();
        }
        if (loginSettion == true)
        {
            
           
            userN.Visible = false;
            passw.Visible = false;
            username.Visible = false;
            password.Visible = false;
            login.Visible = false;
            signUp.Visible = false;
            usernamelogin.Text = "Welcom " + userNameSession + "! ";
            usernamelogin.Visible = true;
            logout.Visible = true;
            logout.Text = "Logout";


        }


       //display catagories on navigations
        MenuItem newMenuItem = new MenuItem("catagories");
        NavigationMenu.Items.Add(newMenuItem);
       SqlConnection cc = new SqlConnection();
        try
        {
            cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
            cc.Open();
            string sql = "select * from catagories";
            SqlCommand cmd = new SqlCommand(sql, cc);
            SqlDataReader reader = cmd.ExecuteReader();
           
            while (reader.Read())
            {
                MenuItem newchildItem = new MenuItem(reader[0].ToString());
                //catagory page 

                newchildItem.NavigateUrl = "catagories.aspx?catName=" + reader[0].ToString() ;
                
                newMenuItem.ChildItems.Add(newchildItem);
                
            }

            reader.Close();
            
            //displaying the recent changes

            sql = "select top(5) * from pages order by lastmodification desc";
            cmd = new SqlCommand(sql, cc);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                HyperLink title = new HyperLink();
                title.Text = reader[0].ToString();
               // title.ID = "title12";
                //sending page title to default page
                Session["ptitle"] = reader[0].ToString();
                title.NavigateUrl = "Default.aspx?ptitle="+reader[0].ToString();
                

                //lable to handle date and editor name
                Label l = new Label();
                DateTime t=(DateTime)reader[4];
                l.Text = "<br>" + reader[6] + "          "+t.ToShortDateString()+"<br><br>";
                recentCh.Controls.Add(title);
                recentCh.Controls.Add(l);
            }
        }
        catch (Exception ee)
        {
            Label1.Text =  ee.Message;
        }
        finally
        {
           cc.Close();
        }





        //reading news
        try
        {
            using (StreamReader sr = new StreamReader(Server.MapPath("~/TextFile.txt")))
            {
                string line;
             //   String line = sr.ReadToEnd();
                
               // Console.WriteLine(line);
                //Label4.Text = line;
                Label4.Text = "";
                int i = 0;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    news[i]=line;
                    Label4.Text += "<br><br>" + news[i];
                    i++;
                   
                }
              /*  int updateIndex= (int)Application["updateIndex"];
                //........................repeating
                if (updateIndex >= i)
                    updateIndex = 0;
                Label4.Text +="<br>"+ news[updateIndex];
               
                updateIndex += 1 ;
                Application["updateIndex"] = updateIndex;*/
               
            }
        }
        catch (Exception ee)
        {
           // Console.WriteLine("The file could not be read:");
           // Console.WriteLine(ee.Message);
            Label4.Text = ee.Message;
        }

       


    }


    protected void login_Click(object sender, EventArgs e)
    {
        string name = username.Text;
        string pass = password.Text;
        Session["username"] = name;
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
                if ((reader[0].ToString() == name) && (reader[1].ToString() == pass))
                {
                    found = true;
                    Session["login"] = true;
                    userN.Visible = false;
                    passw.Visible = false;
                    username.Visible = false;
                    password.Visible = false;
                    login.Visible = false;
                    signUp.Visible = false;
                    usernamelogin.Text = "Welcom " + name + "! ";
                    usernamelogin.Visible = true;
                    logout.Visible = true;
                    logout.Text = "Logout";
                    LableaddArticle.Text = "";

                }


            }
            if (!found)
            {
                Label1.Text = "Invalid Name";
                Session["login"] = false;

            }

            reader.Close();
        }
        catch (Exception ee)
        {
            Label1.Text =  ee.Message;
        }
        finally
        {
            cc.Close();
        }
    }
    protected void logout_Click(object sender, EventArgs e)
    {
        Session["login"] = false;
        usernamelogin.Visible = false;
        logout.Visible = false;

        userN.Visible = true;
        passw.Visible = true;
        username.Visible = true;
        password.Visible = true;
        login.Visible = true;
        signUp.Visible = true;

    }
    protected void signUp_Click(object sender, EventArgs e)
    {
        
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        bool islogedin;
        if (Session["login"] != null)
        {
            islogedin = (bool)Session["login"];

            if (islogedin)
            {
                Response.Redirect("Editor.aspx");
            }
            else
            {
                LableaddArticle.Text = "You must Loged in first";

            }
        }
        else
        {
            LableaddArticle.Text = "You must Loged in first";

        }
    }

    protected void Search_click(object sender, EventArgs e)
    {
        Response.Redirect("Search.aspx?data="+searchtxt.Text);//search for articles

      /* SqlConnection cc = new SqlConnection();
        try
        {
            string search = searchtxt.Text;
            cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
            cc.Open();
            string sql = "select * from  pages where pageName like '%"+search+"%'";
            SqlCommand cmd = new SqlCommand(sql, cc);
            SqlDataReader reader = cmd.ExecuteReader();
            Label l = new Label();
            l.Attributes["style"] = "color:red; font-weight:bold; font-size:20px";
            l.Text = "Search Result<br>";
            searchres.Controls.Add(l);
            while (reader.Read())
            {
                HyperLink a = new HyperLink();
                a.NavigateUrl = "Default.aspx?ptitle=" + reader[0].ToString();
                a.Text = reader[0].ToString()+"<br><br>";
                searchres.Controls.Add(a);
              
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
   /*     }
        catch (Exception ee)
        {
            Label1.Text = ee.Message;
        }
        finally
        {
            cc.Close();
        } */
    }


    protected void btnMedia_Click(object sender, EventArgs e)
    {

    }
}

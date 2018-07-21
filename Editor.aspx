<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="TextEditorWeb.Editor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    
       public string memid;
       string[] catagory = new string[100];
       int clength = 0;
       protected void Page_Load(object sender, EventArgs e)
       {
          //Session["username"] = "u089645"; 
           System.Data.SqlClient.SqlConnection cc = new System.Data.SqlClient.SqlConnection();
           string sql;
           System.Data.SqlClient.SqlCommand cmd;
           System.Data.SqlClient.SqlDataReader reader;
           cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
              
                   //loading the catagories
                   // string title = Request.QueryString["ptitle"];
                   // Label1.Text = title + "aaa";
                   if (!IsPostBack)
                   {
                       cc.Open();
                       sql = "select * from catagories";
                       cmd = new System.Data.SqlClient.SqlCommand(sql, cc);
                       reader = cmd.ExecuteReader();

                       ListItem lll = new ListItem("");
                       DropDownList1.Items.Add(lll);

                       while (reader.Read())
                       {
                           catagory[clength] = reader[0].ToString();
                           ListItem l = new ListItem(reader[0].ToString(), reader[0].ToString());
                           DropDownList1.Items.Add(l);
                           clength++;
                       }

                       ListItem ll = new ListItem("Add New Catagory");
                       DropDownList1.Items.Add(ll);
                       reader.Close();
                       cc.Close();
                   }
                   if (Request.QueryString["ptitle"] != null)
                   {
                       try
                       {
                           cc.Open();


                           if (!IsPostBack)
                           {
                               //display article page info
                               sql = "select content,catagory,member_id,title,accessLevel from pages where pageName='" + Request.QueryString["ptitle"] + "'";
                               pageN.Text = Request.QueryString["ptitle"];

                               cmd = new System.Data.SqlClient.SqlCommand(sql, cc);
                               reader = cmd.ExecuteReader();
                               while (reader.Read())
                               {
                                   // Label1.Text = title + "aaa";
                                   try
                                   {
                                       //initialize a new instance of the System.IO.StreamReader class for the specified path and file
                                       System.IO.StreamReader sr = new System.IO.StreamReader(reader[0].ToString());
                                       string strLine;
                                       while (sr.Peek() != -1)//as long as there is text to read
                                       {
                                           //read entire line
                                           strLine = sr.ReadLine();

                                           //add the line read to the list box
                                           txtEditor.Text += strLine;
                                       }
                                       //close and dispose the stream
                                       sr.Close();
                                       sr.Dispose();
                                   }
                                   catch (System.IO.FileNotFoundException ex)//file not found
                                   {

                                   }
                                   catch (System.IO.IOException ex)//file locked by another process
                                   {

                                   }
                                   
                                  // txtEditor.Text = reader[0].ToString();
                                   if (reader[1].ToString() != "")
                                   {
                                       for (int i = 0; i < clength; i++)
                                       {
                                           if (reader[1].ToString() == catagory[i])
                                           {

                                               DropDownList1.SelectedIndex = i + 1;
                                           }
                                       }
                                   }
                                   else
                                   {

                                       DropDownList1.SelectedIndex = 0;
                                   }
                                   title.Text = reader[3].ToString();
                                   memid = reader[2].ToString();
                                   if (reader[4].ToString() == "0")
                                   {
                                       access.SelectedIndex = 0;

                                   }
                                   else
                                   {
                                       access.SelectedIndex = 1;
                                   }
                               }

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
          
       
        protected void btnText_Click(object sender, EventArgs e)
        {
            
             //   string title = Request.QueryString["ptitle"].ToString();
            hdText.Value = txtEditor.Text;
            System.Data.SqlClient.SqlConnection cc = new System.Data.SqlClient.SqlConnection();
            if (pageN.Text == "")
                        {
                            Label2.Text = "You have to enter the page Name";
                        }
                       else
                        {
                try
                {
                
                    cc.ConnectionString = ConfigurationManager.ConnectionStrings["FYPproject"].ConnectionString;
                    cc.Open();
                    string sql;
                    System.Data.SqlClient.SqlCommand cmd;
                    int reader ;
                    
                    if (Request.QueryString["ptitle"] == null)//add page at the first time
                    {
                       
                            if (Session["login"] != null)
                        {
                            //add in pages table
                            sql = "insert into pages(pageName,accessLevel,title,content,lastmodification,member_id,catagory) values(@pname,@accessLevel,@ptitle,@content,@dateTime,@id,@cata);";
                            cmd = new System.Data.SqlClient.SqlCommand(sql, cc);
                            cmd.Parameters.AddWithValue("@pname", pageN.Text);
                            cmd.Parameters.AddWithValue("@ptitle", title.Text);
                                
                             //   writing the Content into File;

                            try
                            {
                                //initialize a new instance of the System.IO.StreamWriter class for the specified path and file
                                //if the file exists it will be appended to
                                //if the file does not exist it will be created        
                                System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath(".\\articles\\"+pageN.Text+".txt"), true);

                                //write text stream followed by line terminator
                                sw.WriteLine(txtEditor.Text);
                               

                                //close the stream
                                sw.Close();
                                sw.Dispose();
                                
                            }
                            catch (System.IO.IOException ex)//file locked by another process
                            {
                                
                            }



                            cmd.Parameters.AddWithValue("@content", Server.MapPath(".\\articles\\" + pageN.Text + ".txt"));
                           // cmd.Parameters.AddWithValue("@content", txtEditor.Text);
                            cmd.Parameters.AddWithValue("@accessLevel", access.SelectedItem.Value);
                            cmd.Parameters.AddWithValue("@dateTime", DateTime.Now.ToShortDateString());
                            cmd.Parameters.AddWithValue("@id", Session["username"].ToString());
                            if (DropDownList1.SelectedItem.Value == "Add New Catagory")
                                cmd.Parameters.AddWithValue("@cata", newcatagory.Text);
                            else
                                cmd.Parameters.AddWithValue("@cata", DropDownList1.SelectedItem.Value);

                            reader = cmd.ExecuteNonQuery();
                            //add in pages_history table
                            sql = "insert into pages_history(pageName,accessLevel,title,content,lastmodification,member_id,catagory) values(@pname,@accessLevel,@ptitle,@content,@dateTime,@id,@cata);";
                            cmd = new System.Data.SqlClient.SqlCommand(sql, cc);
                            cmd.Parameters.AddWithValue("@pname", pageN.Text);
                            cmd.Parameters.AddWithValue("@ptitle", title.Text);
                           

                            cmd.Parameters.AddWithValue("@content", Server.MapPath(".\\articles\\" + pageN.Text + ".txt"));
                          // cmd.Parameters.AddWithValue("@content", txtEditor.Text);
                            cmd.Parameters.AddWithValue("@accessLevel", access.SelectedItem.Value);
                            cmd.Parameters.AddWithValue("@dateTime", DateTime.Now.ToShortDateString());
                            cmd.Parameters.AddWithValue("@id", Session["username"].ToString());
                            if (DropDownList1.SelectedItem.Value == "Add New Catagory")
                                cmd.Parameters.AddWithValue("@cata", newcatagory.Text);
                            else
                                cmd.Parameters.AddWithValue("@cata", DropDownList1.SelectedItem.Value);
                            reader = cmd.ExecuteNonQuery();
                        }


                        else
                        {
                            Response.Redirect("default.aspx?ptitle=" + pageN.Text);
                        }
                    }

                    else //editing page
                    {
                        //add in pages table
                       
                        sql = "update pages set title=@ptitle,content=@content,accessLevel=@acc,lastmodification=@dateTime,editor_id=@edid,catagory=@cata where pageName='" + pageN.Text + "';";
                        cmd = new System.Data.SqlClient.SqlCommand(sql, cc);
                        cmd.Parameters.AddWithValue("@ptitle", title.Text);
                        try
                            {
                            System.IO.File.Delete(Server.MapPath(".\\articles\\"+Request.QueryString["ptitle"]+".txt"));
                                //initialize a new instance of the System.IO.StreamWriter class for the specified path and file
                                //if the file exists it will be appended to
                                //if the file does not exist it will be created        
                                System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath(".\\articles\\"+Request.QueryString["ptitle"]+".txt"), true);

                                //write text stream followed by line terminator
                                sw.WriteLine(hdText.Value);
                               

                                //close the stream
                                sw.Close();
                                sw.Dispose();
                                
                            }
                            catch (System.IO.IOException ex)//file locked by another process
                            {
                                
                            }

                        cmd.Parameters.AddWithValue("@content", Server.MapPath(".\\articles\\" + Request.QueryString["ptitle"] + ".txt"));
                       // cmd.Parameters.AddWithValue("@content", hdText.Value);
                        cmd.Parameters.AddWithValue("@acc", access.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@dateTime", DateTime.Now.ToShortDateString());
                        if (Session["username"] != null)
                        {
                            cmd.Parameters.AddWithValue("@edid", Session["username"].ToString());
                        }
                        else
                        {
                            //...........................
                            // ...............................................................
                            // getting the machine ip if the user is not register
                            // same for others
                            //     ...........................................
                            System.Net.IPHostEntry host;
                            string localIP = "?";
                            host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                            foreach (System.Net.IPAddress ip in host.AddressList)
                            {
                                if (ip.AddressFamily.ToString() == "InterNetwork")
                                {
                                    localIP = ip.ToString();
                                    cmd.Parameters.AddWithValue("@edid", localIP);
                                }
                            }

                        }
                       
                        if (DropDownList1.SelectedItem.Value == "Add New Catagory")
                            cmd.Parameters.AddWithValue("@cata", newcatagory.Text);
                        else
                            cmd.Parameters.AddWithValue("@cata", DropDownList1.SelectedItem.Value);
                        reader = cmd.ExecuteNonQuery();
                        //add in pages_history table
                        sql = "insert into pages_history(pageName,title,content,accessLevel,lastmodification,editor_id,catagory) values(@pname,@ptitle,@content,@acc,@dateTime,@id,@cata);";
                        cmd = new System.Data.SqlClient.SqlCommand(sql, cc);
                        cmd.Parameters.AddWithValue("@pname", pageN.Text);
                        cmd.Parameters.AddWithValue("@ptitle", title.Text);
                        
                        cmd.Parameters.AddWithValue("@content", Server.MapPath(".\\articles\\" + Request.QueryString["ptitle"] + ".txt"));
                        cmd.Parameters.AddWithValue("@acc", access.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@dateTime", DateTime.Now.ToShortDateString());
                        if (Session["username"] != null)
                        {
                            cmd.Parameters.AddWithValue("@id", Session["username"].ToString());
                        }
                        else
                        {
                            //...........................
                            // ...............................................................
                            // getting the machine ip if the user is not register
                            // same for others
                            //     ...........................................
                            System.Net.IPHostEntry host;
                            string localIP = "?";
                            host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                            foreach (System.Net.IPAddress ip in host.AddressList)
                            {
                                if (ip.AddressFamily.ToString() == "InterNetwork")
                                {
                                    localIP = ip.ToString();
                                    cmd.Parameters.AddWithValue("@id", localIP);
                                }
                            }

                        }
                       
                        
                        if (DropDownList1.SelectedItem.Value == "Add New Catagory")
                            cmd.Parameters.AddWithValue("@cata", newcatagory.Text);
                        else
                            cmd.Parameters.AddWithValue("@cata", DropDownList1.SelectedItem.Value);
                        reader = cmd.ExecuteNonQuery();
                        // Label1.Text = reader.ToString();
                    }
                    if (DropDownList1.SelectedItem.Value == "Add New Catagory" && newcatagory.Text != "")
                    {
                    sql="insert into catagories(catagory) values('"+newcatagory.Text+"')";
                    cmd = new System.Data.SqlClient.SqlCommand(sql, cc);
                    reader = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ee)
                {
                    Label1.Text = "Please choose another Page name .";
                    Label1.Text = ee.Message;
                    
                }
                finally
                {
                    cc.Close();
                }
           Response.Redirect("default.aspx?ptitle="+pageN.Text);
            }
        }
             /*txtEditor.Text = "<b>aaaa</b>";
             string content = txtEditor.Text;
             Label1.Text = content;*/


        protected void cancle(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx?ptitle=Home" );
        }

        protected void pageN_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void catagoryChanged(object sender, EventArgs e)
        {
            
            if (DropDownList1.SelectedItem.Value == "Add New Catagory")
            {
                newcatagory.Visible = true;
                //newcatagory.Text = DropDownList1.SelectedItem.Text;
            }
        }
    
    
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Text Editor With JQuery</title>
     <link rel="stylesheet" href="style.css" />
    <link href="CSS/demo.css" rel="stylesheet" type="text/css" />
    <link href="CSS/jquery-te-1.4.0.css" rel="stylesheet" type="text/css" />
   <style type="text/css">
   body
   {
   font-family:Arial,Helvetica,sans-serif;margin:0;background:#FBFEFD;color:#000;background:#FBFEFD;
   }
   
   </style>
</head>
<body>
<div id="wrap">
    <form id="form1" runat="server">
     <div class="Header">
          <div class="HeaderTitle">
                    <h1><a href="#"> &nbsp;</a></h1>
                 <h2>&nbsp;</h2>
          </div>
        </div>
    <div>
    <br /><br />
     Page Name : <asp:TextBox ID="pageN" runat="server" CssClass="tb5" 
            ontextchanged="pageN_TextChanged"></asp:TextBox><br />

        <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red"></asp:Label><br />
        Title : <asp:TextBox ID="title" runat="server" CssClass="tb5"></asp:TextBox>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         Catagory : 
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="catagoryChanged" >
        </asp:DropDownList>
         
         <asp:TextBox ID="newcatagory" runat="server" CssClass="tb5" Visible="false"></asp:TextBox><br /><br />
         Access Level : <asp:RadioButtonList ID="access" runat="server"> 
         <asp:ListItem Text="Doctors and students" Value="0"></asp:ListItem>
         <asp:ListItem Text="Doctors only" Value="1" Selected="True"></asp:ListItem>
        </asp:RadioButtonList>


        <asp:TextBox ID="txtEditor" TextMode="MultiLine" runat="server" CssClass="textEditor"
            onblur="Test()"></asp:TextBox>
        <asp:Button ID="btnText" runat="server" Text="Save" OnClick="btnText_Click" CssClass="myButton"/>


          <asp:Button ID="Button1" runat="server" Text="Cancle" OnClick="cancle" CssClass="myButton"/>


        <asp:HiddenField ID="hdText" runat="server"/>
        <asp:TextBox ID="txtReText" TextMode="MultiLine" runat="server" CssClass="textEditor1" Visible="false"></asp:TextBox>

        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
    </form>
    </div>
</body>




<script src="JS/jquery-1.10.2.min.js" type="text/javascript"></script>
<script src="JS/jquery-te-1.4.0.min.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    $('.textEditor1').jqte();
    $(".textEditor").jqte({ blur: function () {
          document.getElementById('<%=hdText.ClientID %>').value = document.getElementById('<%=txtEditor.ClientID %>').value;
        document.getElementById('<%=Label1.ClientID %>').Text= document.getElementById('<%=hdText.ClientID %>').value;
    }
    });
</script>
</html>

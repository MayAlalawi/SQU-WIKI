using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string fileName = "DownloadTextFile.txt";
        string s = Request.QueryString["file"];
        string fileName = s;
        //This method helps to download File from Server.
        DownLoadFileFromServer(fileName);
    }
    public static string ServerMapPath(string path)
    {
        return HttpContext.Current.Server.MapPath(path);
    }

    public static HttpResponse GetHttpResponse()
    {
        return HttpContext.Current.Response;
    }

    public static void DownLoadFileFromServer(string fileName)
    {

        //This is used to get Project Location.

        string filePath = ServerMapPath(fileName);
        //This is used to get the current response.

        HttpResponse res = GetHttpResponse();

        res.Clear();
        res.AppendHeader("content-disposition", "attachment; filename=" + filePath);

        res.ContentType = "application/octet-stream";

        res.WriteFile(filePath);

        res.Flush();

        res.End();

    }
   

}
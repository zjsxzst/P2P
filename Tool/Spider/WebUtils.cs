using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using System.Diagnostics;
using System.IO.Compression;
using System.Xml;

/// <summary>
/// 网络工具类。
/// </summary>
public sealed class WebUtils
{
    private X509Certificate _cert;
    private int _timeout = 100000;
    private CookieContainer _cookies = new CookieContainer();

    /// <summary>
    /// 请求与响应的超时时间
    /// </summary>
    public int Timeout
    {
        get { return this._timeout; }
        set { this._timeout = value; }
    }

    /// <summary>
    /// 读取或写入Cookies
    /// </summary>
    public CookieContainer Cookies
    {
        get { return this._cookies; }
        set { this._cookies = value; }
    }

    public X509Certificate Certificate
    {
        get { return this._cert; }
        set { this._cert = value; }
    }
    public int timestamp = 0;
    public int gettimestamp()
    {
        if (timestamp == 0)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            timestamp = (int)(DateTime.Now - startTime).TotalSeconds;
        }
        return timestamp;
    }
    public System.Text.Encoding Charset;



    /// <summary>
    /// 执行HTTP POST请求。
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="parameters">请求参数</param>
    /// <returns>HTTP响应</returns>
    public string DoPost(string url, IDictionary<string, string> parameters)
    {
        HttpWebRequest req = GetWebRequest(url, "POST");
        req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

        byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters));
        System.IO.Stream reqStream = req.GetRequestStream();
        reqStream.Write(postData, 0, postData.Length);
        reqStream.Close();
        Encoding encoding = Encoding.Default;
        HttpWebResponse rsp;
        try
        {
            rsp = (HttpWebResponse)req.GetResponse();
        }
        catch (WebException ex)
        {
            rsp = (HttpWebResponse)ex.Response;
        }

        if (!string.IsNullOrEmpty(rsp.CharacterSet))
            encoding = Encoding.GetEncoding(rsp.CharacterSet);
        if (Charset != null)
            encoding = Charset;

        string html = GetResponseAsString(rsp, encoding);

        if (rsp.StatusCode == HttpStatusCode.OK)
            return html;
        else
            throw new Exception(html);
    }

    public string DoPost(string url, string postdata)
    {
        HttpWebRequest req = GetWebRequest(url, "POST");
        req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

        byte[] postData = Encoding.UTF8.GetBytes(postdata);
        System.IO.Stream reqStream = req.GetRequestStream();
        reqStream.Write(postData, 0, postData.Length);
        reqStream.Close();

        Encoding encoding = Encoding.Default;
        HttpWebResponse rsp;
        try
        {
            rsp = (HttpWebResponse)req.GetResponse();
        }
        catch (WebException ex)
        {
            rsp = (HttpWebResponse)ex.Response;
        }

        if (!string.IsNullOrEmpty(rsp.CharacterSet))
            encoding = Encoding.GetEncoding(rsp.CharacterSet);
        if (Charset != null)
            encoding = Charset;

        string html = GetResponseAsString(rsp, encoding);

        if (rsp.StatusCode == HttpStatusCode.OK)
            return html;
        else
            throw new Exception(html);
    }

    /// <summary>
    /// 执行HTTP GET请求。
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="parameters">请求参数</param>
    /// <returns>HTTP响应</returns>
    public string DoGet(string url, IDictionary<string, string> parameters)
    {
        if (parameters != null && parameters.Count > 0)
        {
            if (url.Contains("?"))
            {
                url = url + "&" + BuildQuery(parameters);
            }
            else
            {
                url = url + "?" + BuildQuery(parameters);
            }
        }

        HttpWebRequest req = GetWebRequest(url, "GET");
        req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

        Encoding encoding = Encoding.Default;
        HttpWebResponse rsp;
        try
        {
            rsp = (HttpWebResponse)req.GetResponse();
        }
        catch (WebException ex)
        {
            rsp = (HttpWebResponse)ex.Response;
        }

        if (!string.IsNullOrEmpty(rsp.CharacterSet))
            encoding = Encoding.GetEncoding(rsp.CharacterSet);
        if (Charset != null)
            encoding = Charset;

        string html = GetResponseAsString(rsp, encoding);

        if (rsp.StatusCode == HttpStatusCode.OK)
            return html;
        else
            return "403";// throw new Exception(html);
    }

    public void DoGet(string url, IDictionary<string, string> parameters, Stream streamOut)
    {
        if (parameters != null && parameters.Count > 0)
        {
            if (url.Contains("?"))
            {
                url = url + "&" + BuildQuery(parameters);
            }
            else
            {
                url = url + "?" + BuildQuery(parameters);
            }
        }

        HttpWebRequest req = GetWebRequest(url, "GET");
        req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

        HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
        //Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
        //if (Charset != null) encoding = Charset;

        GetResponseAsStream(rsp, streamOut);
    }


    public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
    {
        if (errors == SslPolicyErrors.None)
            return true;
        return false;
    }

    public HttpWebRequest GetWebRequest(string url, string method)
    {
        HttpWebRequest req = null;

        req = (HttpWebRequest)WebRequest.Create(url);

        req.ServicePoint.Expect100Continue = false;
        req.Method = method;
        req.KeepAlive = true;
        req.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)";
        req.Timeout = this._timeout;
        req.CookieContainer = _cookies;

        Console.WriteLine(req.RequestUri);
        return req;
    }

    /// <summary>
    /// 把响应流转换为文本。
    /// </summary>
    /// <param name="rsp">响应流对象</param>
    /// <param name="encoding">编码方式</param>
    /// <returns>响应文本</returns>
    public string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
    {
        if (rsp.Cookies != null) _cookies.Add(rsp.Cookies);

        System.IO.Stream stream = null;
        StreamReader reader = null;

        try
        {
            // 以字符流的方式读取HTTP响应
            stream = rsp.GetResponseStream();
            reader = new StreamReader(stream, encoding);
            return reader.ReadToEnd();
        }
        finally
        {
            // 释放资源
            if (reader != null) reader.Close();
            if (stream != null) stream.Close();
            if (rsp != null) rsp.Close();
        }
    }

    private void InternalCopyTo(Stream destination, Stream streamOut, int bufferSize)
    {
        byte[] array = new byte[bufferSize];
        int count;
        while ((count = destination.Read(array, 0, array.Length)) != 0)
        {
            streamOut.Write(array, 0, count);
        }
    }

    public void GetResponseAsStream(HttpWebResponse rsp, Stream streamOut)
    {
        if (rsp.Cookies != null) _cookies.Add(rsp.Cookies);

        try
        {
            Stream sr = rsp.GetResponseStream();

            InternalCopyTo(sr, streamOut, 4096);
            // 以字符流的方式读取HTTP响应
            //sr.CopyTo(streamOut);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            // 释放资源
            if (rsp != null) rsp.Close();
        }
    }

    /// <summary>
    /// 组装GET请求URL。
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="parameters">请求参数</param>
    /// <returns>带参数的GET请求URL</returns>
    public string BuildGetUrl(string url, IDictionary<string, string> parameters)
    {
        if (parameters != null && parameters.Count > 0)
        {
            if (url.Contains("?"))
            {
                url = url + "&" + BuildQuery(parameters);
            }
            else
            {
                url = url + "?" + BuildQuery(parameters);
            }
        }
        return url;
    }

    /// <summary>
    /// 组装普通文本请求参数。
    /// </summary>
    /// <param name="parameters">Key-Value形式请求参数字典</param>
    /// <returns>URL编码后的请求数据</returns>
    public string BuildQuery(IDictionary<string, string> parameters)
    {
        StringBuilder postData = new StringBuilder();
        bool hasParam = false;

        IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
        while (dem.MoveNext())
        {
            string name = dem.Current.Key;
            string value = dem.Current.Value;
            // 忽略参数名或参数值为空的参数
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
            {
                if (hasParam)
                {
                    postData.Append("&");
                }

                postData.Append(name);
                postData.Append("=");
                postData.Append(UrlEncode(value));
                hasParam = true;
            }
        }
        Console.WriteLine(postData);
        return postData.ToString();
    }
    public static string UrlEncode(string str)
    {
        StringBuilder sb = new StringBuilder();
        byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.UTF8.GetBytes(str)
        for (int i = 0; i < byStr.Length; i++)
        {
            sb.Append(@"%" + Convert.ToString(byStr[i], 16));
        }

        return (sb.ToString());
    }

}
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace JS.Utilities
{
    public static class JS_HttpRequest
    {
        public static string Post(string url, string data, string contenType, Dictionary<string, string> headers)
        {
            string response = string.Empty;

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] _data = encoding.GetBytes(data);

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = contenType;

            if (headers != null)
            {
                foreach (var item in headers)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }

            request.ContentLength = _data.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(_data, 0, _data.Length);
            stream.Close();

            WebResponse wsResponse = request.GetResponse();
            using (var reader = new StreamReader(wsResponse.GetResponseStream()))
            {
                response = reader.ReadToEnd();
            }

            return response;
        }

        public static string Get(string url, Dictionary<string, object> headers, string contenType)
        {
            string response = string.Empty;

            string data = string.Empty;
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] _data = encoding.GetBytes(data);

            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = contenType;
            foreach (var item in headers)
            {
                request.Headers.Add(item.Key, item.Value.ToString());
            }
            request.ContentLength = _data.Length;

            WebResponse wsResponse = request.GetResponse();
            using (var reader = new StreamReader(wsResponse.GetResponseStream()))
            {
                response = reader.ReadToEnd();
            }

            return response;
        }

    }
}

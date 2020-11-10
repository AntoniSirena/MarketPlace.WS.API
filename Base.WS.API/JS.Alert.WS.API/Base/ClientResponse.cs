

namespace JS.Alert.WS.API.Base
{
    public class ClientResponse<T>
    {
        public ClientResponse()
        {
            Code = "000";
            Message = string.Empty;
            MessageDetail = new object();
        }

        public string Code { get; set; }
        public string Message { get; set; }
        public object MessageDetail { get; set; }
        public T Data { get; set; }
    }
}
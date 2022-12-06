namespace Restauracja.Web.Models
{
    public class ApiRequest
    {
        public string Url { get; set; }

        public object Data { get; set; }

        public ApiType ApiType { get; set; } = ApiType.GET;
    }

    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}

namespace StonksWebApi
{
    public class StonksWebApiOptions
    {
        public BasicAuthorizationOptions Authorization { get; set; } = new BasicAuthorizationOptions();
    }
}
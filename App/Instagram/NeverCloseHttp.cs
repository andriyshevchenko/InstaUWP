namespace App
{
    public class NeverCloseHttp : System.Net.Http.HttpClient
    {
        public NeverCloseHttp()
        {
            DefaultRequestHeaders.ConnectionClose = false;
        }
    }
}

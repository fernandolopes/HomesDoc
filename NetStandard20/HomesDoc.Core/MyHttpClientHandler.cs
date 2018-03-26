using System.Net.Http;

namespace HomesDoc.Core
{
    public class MyHttpClientHandler : HttpClientHandler
    {
        protected override void Dispose(bool disposing)
        { }

        public void MyDispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}

using System.Text;

namespace HomesDoc.Core
{
    public static class Funcoes
    {
        public static string GetImageType(byte[] buffer)
        {
            string headerCode = GetHeaderInfo(buffer).ToUpper();

            if (headerCode.StartsWith("FFD8FFE0"))
            {
                return "image/jpg";
            }
            else if (headerCode.StartsWith("49492A"))
            {
                return "image/tiff";
            }
            else if (headerCode.StartsWith("424D"))
            {
                return "image/bmp";
            }
            else if (headerCode.StartsWith("474946"))
            {
                return "image/gif";
            }
            else if (headerCode.StartsWith("89504E470D0A1A0A"))
            {
                return "image/png";
            }
            else if (headerCode.StartsWith("255044462D312"))
            {
                return "application/pdf";
            }
            else
            {
                return ""; //UnKnown
            }
        }

        public static string GetHeaderInfo(byte[] buffer)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in buffer)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}

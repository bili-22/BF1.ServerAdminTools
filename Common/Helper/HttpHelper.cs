namespace BF1.ServerAdminTools.Common.Helper
{
    public class HttpHelper
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> HttpClientGET(string url)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static async Task<bool> DownloadFile(string url, string saveDirectory)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                using (Stream stream = await response.Content.ReadAsStreamAsync())
                {
                    string extension = Path.GetFileName(response.RequestMessage.RequestUri.ToString());
                    using (FileStream fileStream = new FileStream(saveDirectory + extension, FileMode.CreateNew))
                    {
                        byte[] buffer = new byte[1024];
                        int readLength = 0;
                        int length;
                        while ((length = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                        {
                            readLength += length;
                            fileStream.Write(buffer, 0, length);
                        }

                        return true;
                    }
                }
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}

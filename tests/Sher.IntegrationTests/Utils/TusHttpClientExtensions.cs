using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sher.IntegrationTests.Utils
{
    public static class TusHelper
    {
        public static async Task<string> SendTusFileAsync(
            this HttpClient httpClient,
            string fileContents,
            string fileName = "file.txt",
            string parentDirectoryId = null)
        {
            var byteContent = Encoding.UTF8.GetBytes(fileContents);
            using var message = new HttpRequestMessage(HttpMethod.Post, "/api/file")
            {
                Content = new ByteArrayContent(byteContent)
            };
            message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/offset+octet-stream");

            var metadata = $"fileName {Convert.ToBase64String(Encoding.UTF8.GetBytes("file.txt"))}";

            if (parentDirectoryId is not null)
            {
                metadata += $",parentDirectoryId {Convert.ToBase64String(Encoding.UTF8.GetBytes(parentDirectoryId))}";
            }

            message.Headers.Add("Upload-Metadata", metadata);
            message.Headers.Add("Tus-Resumable", "1.0.0");
            message.Headers.Add("Upload-Length", byteContent.Length.ToString());

            var res = await httpClient.SendAsync(message);

            return res.Headers.Location!.OriginalString;
        }
    }
}
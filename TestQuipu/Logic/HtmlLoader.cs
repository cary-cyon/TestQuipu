using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestQuipu.Logic
{
    public class HtmlLoader
    {
        public async Task<string> LoadHtmlCodeAsync(string url)
        {

            if (!Regex.Match(url, "www.").Success)
            {
                url = "www." + url;
            }
            if (!Regex.Match(url, "http://").Success)
            {
                url = "http://" + url;
            }
            HttpClient client = new HttpClient();
            using HttpResponseMessage response = await client.GetAsync(url);
            using HttpContent content = response.Content;
            string pageContent = await content.ReadAsStringAsync();
            return pageContent;
        }
        public string LoadHtmlCode(string url)
        {
            if (!Regex.Match(url, "www.").Success)
            {
                url = "www." + url;
            }
            if (!Regex.Match(url, "http://").Success && !Regex.Match(url, "https://").Success)
            {
                url = "http://" + url;
            }
            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.GetAsync(url).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        return responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (HttpRequestException ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
                catch (TaskCanceledException ex)
                {
                    throw new TaskCanceledException(ex.Message);
                }
            }
        }
    }
}

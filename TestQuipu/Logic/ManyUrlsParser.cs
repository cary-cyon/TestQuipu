using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestQuipu.Model;
using TestQuipu.ViewModel;

namespace TestQuipu.Logic
{
    internal class ManyUrlsParser
    {
        private BackgroundWorker backgroundWorker;
        private LinkTagCounter linkTagCounter;
        private HtmlLoader htmlLoader;
        private AppViewModel viewModel;
        public ManyUrlsParser(AppViewModel vm)
        {
            viewModel = vm;
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += (o, e) => GetColOfLinkTags();
            backgroundWorker.RunWorkerCompleted += (o, e) => EndParse();
            linkTagCounter = new LinkTagCounter();
            htmlLoader = new HtmlLoader();
        }

        private void EndParse()
        {
            viewModel.CurrentUrl = "";
            viewModel.IsWorking = false;
            viewModel.ProgressVisible = Visibility.Collapsed;
        }

        private void GetColOfLinkTags()
        {
            List<UrlInfo> cols = new List<UrlInfo>();
            viewModel.IsWorking = true;
            viewModel.MaxProgerssbar = viewModel.Urls.Count;
            viewModel.CurrentProgerssbar = 0;
            viewModel.ProgressVisible = Visibility.Visible;
            var endFlag = false;
            foreach (UrlInfo url in viewModel.Urls)
            {
                if(backgroundWorker.CancellationPending == true)
                {
                    endFlag = true;
                    break;
                }
                viewModel.CurrentUrl = url.Url;
                try
                {
                    string html = htmlLoader.LoadHtmlCode(url.Url);
                    UrlInfo new_element =new UrlInfo(url.Url, linkTagCounter.CountLinkTags(html).ToString());
                    cols.Add(new_element);
                }
                catch (HttpRequestException ex)
                {
                    cols.Add(new UrlInfo(url.Url, "Url не доступна"));
                }
                catch(TaskCanceledException ex)
                {
                    cols.Add(new UrlInfo(url.Url, "обработка URL занимает слишком много времени"));
                }
                viewModel.CurrentProgerssbar = viewModel.CurrentProgerssbar + 1;

            }
            if (endFlag == false)
            {
                var max = -1;
                var index = 0;
                for(int i = 0; i < cols.Count; i++)
                {
                    int value;
                    try
                    {
                        value = Int32.Parse(cols[i].ColOfLinkTags);
                    }
                    catch
                    {
                        value = -1;
                    }
                    if (value > max)
                    {
                        max = value;
                        index = i;
                    }
                }
                cols[index].MaxValue = true;
                viewModel.Urls = cols;
            }
            else
            {
                MessageBox.Show("операция прервана");
            }
            
        }
        public void Parse()
        {
            backgroundWorker.RunWorkerAsync();
        }
        public void CanselParser()
        {
            backgroundWorker.CancelAsync();
            List<UrlInfo> pure_urls = new List<UrlInfo>();
            foreach(var url in viewModel.Urls)
            {
                pure_urls.Add(new UrlInfo(url.Url, ""));
            }
            viewModel.Urls = pure_urls;
        }
    }
}

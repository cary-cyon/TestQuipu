using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestQuipu.Interfaces;
using TestQuipu.Logic;
using TestQuipu.Model;

namespace TestQuipu.ViewModel
{
    internal class AppViewModel : INotifyPropertyChanged
    {
        private Visibility _visible;
        private Visibility _progeressVisible;
        private int _maxProgerssbar;
        private int _currentProgerssbar;
        private string _currentUrl;
        private RelayCommand _openFileWithUrlCommand;
        private RelayCommand _countLinksFromUrls;
        private RelayCommand _canselParsing;
        private IDialogService dialogService;
        private IFileService<UrlInfo> fileService;
        private List<UrlInfo> _urls;
        private ManyUrlsParser manyUrlsParser;
        private bool _isWorking;
        public bool IsWorking { get { return _isWorking; } set { _isWorking = value; OnPropertyChanged(nameof(IsWorking)); } }
        public List<UrlInfo> Urls
        {
            get { return _urls; }
            set { _urls = value; OnPropertyChanged(nameof(Urls)); }
        }
        public Visibility ProgressVisible
        {
            get { return _progeressVisible;  }
            set { _progeressVisible = value; OnPropertyChanged(nameof(ProgressVisible)); }
        }
        public int MaxProgerssbar
        {
            get { return _maxProgerssbar;}
            set { _maxProgerssbar = value; OnPropertyChanged(nameof(MaxProgerssbar));}
        }
        public int CurrentProgerssbar
        {
            get { return _currentProgerssbar;}
            set { _currentProgerssbar = value; OnPropertyChanged(nameof(CurrentProgerssbar)); }
        }
        public string CurrentUrl
        {
            get { return _currentUrl; }
            set { _currentUrl = value; OnPropertyChanged(nameof(CurrentUrl)); }
        }
        public RelayCommand CanselParsingCommand
        {
            get 
            {
                
                return _canselParsing ??
                    (_canselParsing = new RelayCommand(obj =>
                    {
                        manyUrlsParser.CanselParser();
                    }, obj => { return IsWorking; }
                    ));
            }
        }
        public RelayCommand OpenFileWithUrlCommand
        {
            get
            {
                return _openFileWithUrlCommand ??
                  (_openFileWithUrlCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              var urls = fileService.Open(dialogService.FilePath);
                              Urls = urls;
                              dialogService.ShowMessage("Файл открыт");
                              Visible = Visibility.Visible;
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }
        public RelayCommand CountLinksFromUrlsCommand
        {
            get
            {
                if(manyUrlsParser == null)
                {
                    manyUrlsParser = new ManyUrlsParser(this);
                }
                return _countLinksFromUrls ??
                    (_countLinksFromUrls = new RelayCommand(obj =>
                    {
                        IsWorking = true;
                        manyUrlsParser.Parse();
                    }, obj => { return !IsWorking; }
                    ));
            }
        }
        public Visibility Visible 
        {
            get { return _visible; }
            set {_visible = value; OnPropertyChanged(nameof(Visible));}
        }
        public AppViewModel()
        {
            IsWorking = false;
            Visible = Visibility.Hidden;
            dialogService = new DefaultDialogService();
            fileService = new UrlFileSetvice();
            ProgressVisible = Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

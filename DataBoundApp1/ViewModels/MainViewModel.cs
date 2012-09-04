using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using HtmlAgilityPack;
using DanielVaughan.WindowsPhone7Unleashed;
using System.Threading;
using Hardcodet.Silverlight.Util;



namespace DataBoundApp1
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {


            //AddMoreItems();
            if (!busy)
            {
                Busy = true;
                fetchMoreDataCommand = new DelegateCommand(
                    obj =>
                    {
                        /*if (busy)
                        {
                            return;
                        }
                        Busy = true;*/
                        ThreadPool.QueueUserWorkItem(
                            delegate
                            {
                                /* This is just to demonstrate a slow operation. */
                                //Thread.Sleep(3000);
                                /* We invoke back to the UI thread. 
                                    * Ordinarily this would be done 
                                    * by the Calcium infrastructure automatically. */
                               Deployment.Current.Dispatcher.BeginInvoke(
                                    delegate
                                    {
                                        AddMoreItems();
                                        Busy = false;
                                    });
                            });
                        //Busy = false;

                    });
            }
        }

        //this.Items = new ObservableCollection<ItemViewModel>();

        readonly ObservableCollection<ItemViewModel> items = new ObservableCollection<ItemViewModel>();
        string baseAddress = "http://www.joemonster.org/mg/displayimage.php?album=lastup&pos=";

        public ObservableCollection<ItemViewModel> Items
        {
            get
            {
                return items;
            }
        }

        void AddMoreItems()
        {
            //Busy = true;
            int start = items.Count;
            int end = start + 20;

            for (int i = start; i < end; i++)
            {
                HtmlWeb web = new HtmlWeb();
                //Encoding enc = CustomEncoding("iso-8859-2");
                CustomEncoding enc = new CustomEncoding();
                web.LoadCompleted += new EventHandler<HtmlDocumentLoadCompleted>(_DownLoadCompleted);
                web.LoadAsync(baseAddress + i, enc);

                /*HtmlWeb.LoadAsync(baseAddress + i, (s, args) =>
                {
                    if (args.Document != null)
                    {
                        
                        items.Add(new ItemViewModel()
                        {
                            Path = args.Document.DocumentNode.SelectSingleNode("//img[@class = \"image\"]").GetAttributeValue("src", "Błąd pobierania"),
                            Title = i + ": " + args.Document.DocumentNode.SelectSingleNode("//table[@class=\"maintable\"]//td[text()='\n']//td[@align=\"center\"]").FirstChild.InnerText.Trim(),
                            Album = args.Document.DocumentNode.SelectSingleNode("//table[@class=\"maintable\"]//td[text()='\n']//td[@align=\"center\"]//a").FirstChild.InnerText,
                            AlbumPath = args.Document.DocumentNode.SelectSingleNode("//table[@class=\"maintable\"]//td[text()='\n']//td[@align=\"center\"]").GetAttributeValue("a", string.Empty),
                            numer = i
                        });
                    }
                });*/
            }
            //Busy = false;
        }

        private void _DownLoadCompleted(object sender, HtmlDocumentLoadCompleted e)
        {
            //Busy = true;
            if (e.Error == null)
            {
                HtmlDocument doc = e.Document;
                System.Text.Encoding enc = doc.DeclaredEncoding;
                if (doc != null)
                    items.Add(new ItemViewModel()
                    {
                        Path = doc.DocumentNode.SelectSingleNode("//img[@class = \"image\"]").GetAttributeValue("src", "Błąd pobierania"),
                        Title = doc.DocumentNode.SelectSingleNode("//table[@class=\"maintable\"]//td[text()='\n']//td[@align=\"center\"]").FirstChild.InnerText.Trim(),
                        Album = doc.DocumentNode.SelectSingleNode("//table[@class=\"maintable\"]//td[text()='\n']//td[@align=\"center\"]//a").FirstChild.InnerText,
                        AlbumPath = doc.DocumentNode.SelectSingleNode("//table[@class=\"maintable\"]//td[text()='\n']//td[@align=\"center\"]").GetAttributeValue("a", string.Empty),
                        numer = 0
                    });
            }
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        //public ObservableCollection<ItemViewModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    //NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        /*public void LoadData()
        {
            string baseAddress = "http://www.joemonster.org/mg/displayimage.php?album=lastup&pos=";
            for (int i = 0; i < 10; i++)
            {
                HtmlWeb.LoadAsync(baseAddress + i, (s, args) =>
                {
                    if (args.Document != null)
                    {
                        this.Items.Add(new ItemViewModel()
                        {
                            Path = args.Document.DocumentNode.SelectSingleNode("//img[@class = \"image\"]").GetAttributeValue("src", "Błąd pobierania"),
                            Title = args.Document.DocumentNode.SelectSingleNode("//table[@class=\"maintable\"]//td[text()='\n']//td[@align=\"center\"]").FirstChild.InnerText.Trim(),
                            Album = args.Document.DocumentNode.SelectSingleNode("//table[@class=\"maintable\"]//td[text()='\n']//td[@align=\"center\"]//a").FirstChild.InnerText,
                            AlbumPath = args.Document.DocumentNode.SelectSingleNode("//table[@class=\"maintable\"]//td[text()='\n']//td[@align=\"center\"]").GetAttributeValue("a", string.Empty)
                        });
                    }
                });
            }
            //NotifyPropertyChanged("Items");
            this.IsDataLoaded = true;
        }*/

        readonly DelegateCommand fetchMoreDataCommand;

        public ICommand FetchMoreDataCommand
        {
            get
            {
                return fetchMoreDataCommand;
            }
        }

        bool busy;

        public bool Busy
        {
            get
            {
                return busy;
            }
            set
            {
                if (busy == value)
                {
                    return;
                }
                busy = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Busy"));
                //NotifyPropertyChanged("Busy");
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var tempEvent = PropertyChanged;
            if (tempEvent != null)
            {
                tempEvent(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
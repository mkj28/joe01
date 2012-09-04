using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using HtmlAgilityPack;

namespace DataBoundApp1
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();

            /*string source = "http://hg.joemonster.org//mg/albums/new/120824/01milego_dnia.jpg"; //add link to your image
            string source1 = "{Binding LineThree}";

            BitmapImage bitmapImage = new BitmapImage(new Uri(source, UriKind.Absolute));
            bitmapImage.DownloadProgress += new EventHandler<DownloadProgressEventArgs>(bitmapImage_DownloadProgress);
            image1.Source = bitmapImage;*/

            // The HtmlWeb class is a utility class to get the HTML over HTTP
            HtmlWeb htmlWeb = new HtmlWeb
            {
                //AutoDetectEncoding = false,
                //OverrideEncoding = Encoding.GetEncoding("iso-8859-7"),
            };

            // Creates an HtmlDocument object from an URL
            //Hap();
            //htmlWeb.LoadAsync("http://www.joemonster.org/mg/displayimage.php?album=lastup&pos=0", this._DownLoadCompleted);


        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
            {
                int index = int.Parse(selectedIndex);
                DataContext = App.ViewModel.Items[index];

            }
        }

        void bitmapImage_DownloadProgress(object sender, DownloadProgressEventArgs e)
        {
            progressBar1.Value = e.Progress;
        }

        public void Hap()
        {
            HtmlWeb.LoadAsync("http://www.joemonster.org/mg/displayimage.php?album=lastup&pos=0", _DownLoadCompleted);
        }

        private void _DownLoadCompleted(object sender, HtmlDocumentLoadCompleted e)
        {
            if (e.Error == null)
            {
                HtmlDocument doc = e.Document;
                System.Text.Encoding enc = doc.DeclaredEncoding;
                if (doc != null)
                {
                    string imgValue = doc.DocumentNode.SelectSingleNode("//img[@class = \"image\"]").GetAttributeValue("src", "0");
                    if (imgValue != null)
                    {
                        ContentText.Text += imgValue;

                        BitmapImage bitmapImage = new BitmapImage(new Uri(imgValue, UriKind.Absolute));
                        image1.Source = bitmapImage;
                    }
                    string title = doc.DocumentNode.SelectSingleNode("//table[@class=\"maintable\"]//td[text()='\n']//td[@align=\"center\"]").FirstChild.InnerText.Trim();

                    ContentText.Text = title;
                    string album = doc.DocumentNode.SelectSingleNode("//table[@class=\"maintable\"]//td[text()='\n']//td[@align=\"center\"]//a").FirstChild.InnerText;
                    string albumLink = doc.DocumentNode.SelectSingleNode("//table[@class=\"maintable\"]//td[text()='\n']//td[@align=\"center\"]").GetAttributeValue("a", "0");
                    ContentText.Text += " " + album + ": " + albumLink;
                }
            }
        }
    }
}
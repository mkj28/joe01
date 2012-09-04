using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DataBoundApp1
{
    public class ItemViewModel : INotifyPropertyChanged
    {

        private string _Path;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Path
        {
            get
            {
                return _Path;
            }
            set
            {
                if (value != _Path)
                {
                    _Path = value;
                    NotifyPropertyChanged("Path");
                }
            }
        }

        private string _Title;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (value != _Title)
                {
                    _Title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        private string _Album;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Album
        {
            get
            {
                return _Album;
            }
            set
            {
                if (value != _Album)
                {
                    _Album = value;
                    NotifyPropertyChanged("Album");
                }
            }
        }

        private string _AlbumPath;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string AlbumPath
        {
            get
            {
                return _AlbumPath;
            }
            set
            {
                if (value != _AlbumPath)
                {
                    _AlbumPath = value;
                    NotifyPropertyChanged("AlbumPath");
                }
            }
        }

        private int _numer;
        public int numer
        {
            get
            {
                return _numer;
            }
            set
            {
                if (value != _numer)
                {
                    _numer = value;
                    NotifyPropertyChanged("numer");
                }
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
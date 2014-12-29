using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofability.Models
{
    public class Article : INotifyPropertyChanged
    {
        private string _title;
        private string _description;
        private string _content;
        private string _source;
        private string _link;
        private string _date;
        private bool _read;
        private string _id;

        public string Title
        {
            get { return _title; }
            set { if (value != _title) { _title = value; NotifyPropertyChanged("Title"); } }
        }

        public string Description
        {
            get { return _description; }
            set { if (value != _description) { _description = value; NotifyPropertyChanged("Description"); } }
        }

        public string Content
        {
            get { return _content; }
            set { if (value != _content) { _content = value; NotifyPropertyChanged("Content"); } }
        }

        public string Source
        {
            get { return _source; }
            set { if (value != _source) { _source = value; NotifyPropertyChanged("Source"); } }
        }

        public string Link
        {

            get { return (_link == null) ? "http://www.sofability.com/" : _link; }
            set { if (value != _link) { _link = value; NotifyPropertyChanged("Link"); } }
        }

        public string Date
        {
            get { return _date; }
            set { if (value != _date) { _date = value; NotifyPropertyChanged("Date"); } }
        }

        public bool Read
        {
            get { return _read; }
            set { if (value != _read) { _read = value; NotifyPropertyChanged("Read"); } }
        }

        public string Id
        {
            get { return _id; }
            set { if(value != _id) { _id = value; NotifyPropertyChanged("Id"); } }
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

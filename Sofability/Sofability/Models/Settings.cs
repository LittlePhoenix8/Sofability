using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofability.Models
{
    public class Settings : INotifyPropertyChanged
    {
        private string _kindleEmail;
        private double _fontSize;
        private string _fontFamily;
        private List<string> _fontsList;

        public string KindleEmail
        {
            get { return _kindleEmail; }
            set { if (value != _kindleEmail) { _kindleEmail = value; NotifyPropertyChanged("KindleEmail"); } }
        }

        public double FontSize
        {
            get { return _fontSize; }
            set { if (value != _fontSize) { _fontSize = value; NotifyPropertyChanged("FontSize"); } }
        }

        public string FontFamily
        {
            get { return _fontFamily; }
            set { if (value != _fontFamily) { _fontFamily = value; NotifyPropertyChanged("FontFamily"); } }
        }

        public List<string> FontsList
        {
            get 
            {
                if ((_fontsList == null) || (_fontsList != null))
                {
                    _fontsList = new List<string>()
                    {
                        "Segoe WP", "Georgia", "Tahoma"
                    };
                }
                
                return _fontsList;
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

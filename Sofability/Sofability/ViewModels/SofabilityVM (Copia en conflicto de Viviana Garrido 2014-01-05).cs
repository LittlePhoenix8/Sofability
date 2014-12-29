using Sofability.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Sofability.Resources;
using RestSharp;
using RestSharp.Authenticators;
using System.Xml;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Windows;

namespace Sofability.ViewModels
{
    public class SofabilityVM : INotifyPropertyChanged
    {
        string CONSUMER_KEY = "copihuelabs";
        string CONSUMER_SECRET = "8aduHwU6AznxgGuru9yNU6xnfY9Je33m";
        string USERNAME = "copihuelabs";
        string PASSWORD = "chilehack";
        xmlReader xml;
        string idArt;
        RestClient c;
        public Dictionary<string, string> dict = new Dictionary<string, string>();
        public event EventHandler DataUpdated = delegate { };

        public ObservableCollection<Article> Pendings { get; private set; }

        public ObservableCollection<Article> Stars { get; private set; }

        public ObservableCollection<Article> Archived { get; private set; }

        public SofabilityVM()
        {
            this.Archived = new ObservableCollection<Article>();
            this.Stars = new ObservableCollection<Article>();
            this.Pendings = new ObservableCollection<Article>();
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            OAuthTest();
            this.IsDataLoaded = true;
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

        // LOGIN!
        public void OAuthTest()
        {
            var baseURL = "https://www.readability.com/api/rest/v1";
            c = new RestClient(baseURL);
            RestRequest resquest = new RestRequest("oauth/access_token", Method.POST);
            c.Authenticator = OAuth1Authenticator.ForClientAuthentication(CONSUMER_KEY, CONSUMER_SECRET, USERNAME, PASSWORD);

            c.ExecuteAsync(resquest, (response) =>
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show("Se ha producico un error.");
                }
                else if (response.ResponseStatus == ResponseStatus.Completed)
                {
                    string query = HttpUtility.UrlDecode(response.Content);
                    var items = query.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Split('='));

                    foreach (var item in items)
                    {
                        dict.Add(item[0], item[1]);
                    };
                }
                obtenerXml();
            });
        }

        public void obtenerXml()
        {
            string txtOAuthToken = dict["oauth_token"];
            string txtOAuthSecret = dict["oauth_token_secret"];

            c.Authenticator = OAuth1Authenticator.ForProtectedResource(CONSUMER_KEY, CONSUMER_SECRET, txtOAuthToken, txtOAuthSecret);
            RestRequest request = new RestRequest("/bookmarks", Method.GET);
            request.AddParameter("format", "xml");

            c.ExecuteAsync(request, (response) =>
            {
                string xmlArticulos = response.Content;
                xml = new xmlReader(xmlArticulos);
                obtenerListaArticulosArchivados();
                obtenerListaArticulosFavoritos();
                obtenerListaArticulosEnLectura();
            });
        }

        void obtenerListaArticulosArchivados()
        {
            List<bookmarks> listaArticulos = xml.listarArticulosArchivados();
            foreach (var item in listaArticulos)
            {
                this.Archived.Add(new Article() { Title = item.articulo.title, Description = item.articulo.dek, Source = item.articulo.domain, Content = item.articulo.excerpt });
            }
        }

        void obtenerListaArticulosFavoritos()
        {
            List<bookmarks> listaArticulos = xml.listarArticulosFavoritos();
            foreach (var item in listaArticulos)
            {
                idArt = item.articulo.id;
                this.Stars.Add(new Article() { Title = item.articulo.title, Description = item.articulo.dek, Source = item.articulo.domain, Content = item.articulo.excerpt });
            }
        }

        void obtenerListaArticulosEnLectura()
        {
            List<bookmarks> listaArticulos = xml.listarArticulosEnLectura();
            foreach (var item in listaArticulos)
            {
                this.Pendings.Add(new Article() { Title = item.articulo.title, Description = item.articulo.dek, Source = item.articulo.domain, Content = item.articulo.excerpt, Date = item.articulo.date_published.ToString("d MMM yyyy"), Id = item.articulo.id });
            }
            this.DataUpdated(this, null);
        }
    }
}

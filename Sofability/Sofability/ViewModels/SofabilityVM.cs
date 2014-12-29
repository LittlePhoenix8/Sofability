using RestSharp;
using RestSharp.Authenticators;
using Sofability.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;

namespace Sofability.ViewModels
{
    public class SofabilityVM : INotifyPropertyChanged
    {
        string CONSUMER_KEY = "copihuelabs";
        string CONSUMER_SECRET = "8aduHwU6AznxgGuru9yNU6xnfY9Je33m";
        public string USERNAME { get; set; }// = "copihuelabs";
        public string PASSWORD { get; set; }// = "chilehack";
        xmlReader xml;
        RestClient c;
        public Dictionary<string, string> dict = new Dictionary<string, string>();
        public event EventHandler DataUpdated = delegate { };
        public event EventHandler<vistaArticulo> ArticleLoaded = delegate { };
        public event EventHandler<ErrorStatus> ErrorOnConnect = delegate { };

        public ObservableCollection<Article> Pendings { get; private set; }

        public ObservableCollection<Article> Stars { get; private set; }

        public ObservableCollection<Article> Archived { get; private set; }

        public SofabilityVM()
        {
            this.Archived = new ObservableCollection<Article>();
            this.Stars = new ObservableCollection<Article>();
            this.Pendings = new ObservableCollection<Article>();

            var baseURL = "https://www.readability.com/api/rest/v1";
            c = new RestClient(baseURL);
        }

        public void Logout()
        {
            this.USERNAME = null;
            this.PASSWORD = null;
            this.dict.Clear();
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            // Si no está autenticado, se loguea
            if (dict.Count == 0)
                OAuthTest();
            else
                obtenerXml();

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
            RestRequest resquest = new RestRequest("oauth/access_token", Method.POST);
            c.Authenticator = OAuth1Authenticator.ForClientAuthentication(CONSUMER_KEY, CONSUMER_SECRET, USERNAME, PASSWORD);

            c.ExecuteAsync(resquest, (response) =>
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    this.Logout();
                    ErrorOnConnect(this, ErrorStatus.WrongUserPassword);
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorOnConnect(this, ErrorStatus.OAuth);
                }
                else
                {
                    string query = HttpUtility.UrlDecode(response.Content);
                    var items = query.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Split('='));

                    foreach (var item in items)
                    {
                        dict.Add(item[0], item[1]);
                    };
                    obtenerXml();
                }
            });
        }

        private void obtenerXml()
        {
            string txtOAuthToken = dict["oauth_token"];
            string txtOAuthSecret = dict["oauth_token_secret"];

            c.Authenticator = OAuth1Authenticator.ForProtectedResource(CONSUMER_KEY, CONSUMER_SECRET, txtOAuthToken, txtOAuthSecret);
            RestRequest request = new RestRequest("/bookmarks", Method.GET);
            request.AddParameter("format", "xml");

            c.ExecuteAsync(request, (response) =>
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorOnConnect(this, ErrorStatus.GetBookmarks);
                }
                else
                {
                    string xmlArticulos = response.Content;
                    xml = new xmlReader(xmlArticulos);
                    obtenerListaArticulosEnLectura();
                    obtenerListaArticulosArchivados();
                    obtenerListaArticulosFavoritos();
                    this.DataUpdated(this, null);
                }
            });
        }

        public void addBookmark(string URL)
        {
            string txtOAuthToken = dict["oauth_token"];
            string txtOAuthSecret = dict["oauth_token_secret"];

            c.Authenticator = OAuth1Authenticator.ForProtectedResource(CONSUMER_KEY, CONSUMER_SECRET, txtOAuthToken, txtOAuthSecret);
            RestRequest request = new RestRequest("/bookmarks", Method.POST);
            request.AddParameter("url", URL);

            c.ExecuteAsync(request, (response) =>
            {
                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    this.ErrorOnConnect(this, ErrorStatus.AddBookmark);
                }
                else
                {
                    App.SofabilityVM.LoadData();
                }
            });
        }

        void obtenerListaArticulosArchivados()
        {
            List<bookmarks> listaArticulos = xml.listarArticulosArchivados();
            foreach (var item in listaArticulos)
            {
                var index = 0;
                if (this.Archived.Count(b => b.Id == item.articulo.id) == 0)
                {
                    Article a = new Article() { Title = item.articulo.title, Description = item.articulo.dek, Source = item.articulo.domain, Content = item.articulo.excerpt, Date = item.articulo.date_published.ToString("d MMM"), Id = item.articulo.id };
                    this.Archived.Insert(index++, a);
                }
            }
        }

        void obtenerListaArticulosFavoritos()
        {
            List<bookmarks> listaArticulos = xml.listarArticulosFavoritos();
            var index = 0;
            foreach (var item in listaArticulos)
            {
                if (this.Stars.Count(b => b.Id == item.articulo.id) == 0)
                    this.Stars.Insert(index++, new Article() { Title = item.articulo.title, Description = item.articulo.dek, Source = item.articulo.domain, Content = item.articulo.excerpt, Date = item.articulo.date_published.ToString("d MMM"), Id = item.articulo.id });
            }
        }

        void obtenerListaArticulosEnLectura()
        {
            List<bookmarks> listaArticulos = xml.listarArticulosEnLectura();
            var index = 0;
            foreach (var item in listaArticulos)
            {
                if (this.Pendings.Count(b => b.Id == item.articulo.id) == 0)
                    this.Pendings.Insert(index++, new Article() { Title = item.articulo.title, Description = item.articulo.dek, Source = item.articulo.domain, Content = item.articulo.excerpt, Date = item.articulo.date_published.ToString("d MMM"), Id = item.articulo.id });
            }
        }

        public void obtenerArticulo(string idArticulo)
        {
            string txtOAuthToken = dict["oauth_token"];
            string txtOAuthSecret = dict["oauth_token_secret"];

            c.Authenticator = OAuth1Authenticator.ForProtectedResource(CONSUMER_KEY, CONSUMER_SECRET, txtOAuthToken, txtOAuthSecret);
            RestRequest request = new RestRequest("/articles/" + idArticulo, Method.GET);
            request.AddParameter("format", "xml");
            vistaArticulo verArticulo = new vistaArticulo();

            c.ExecuteAsync(request, (response) =>
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    this.ErrorOnConnect(this, ErrorStatus.GetBookmarks);
                }
                else
                {
                    string xmlArticulo = response.Content;
                    xml = new xmlReader(xmlArticulo);
                    verArticulo = xml.verArticulo();
                    this.ArticleLoaded(this, verArticulo);
                }
            });
        }
    }

    public enum ErrorStatus
    {
        WrongUserPassword, OAuth, GetBookmarks, AddBookmark, GetBookmark
    }
}
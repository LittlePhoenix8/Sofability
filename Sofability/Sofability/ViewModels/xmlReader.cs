using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Sofability.Models;

namespace Sofability.ViewModels
{
    class xmlReader
    {
        XElement datos;
        string reader;
        public xmlReader(string reader)
        {
            this.reader = reader;
            datos = XElement.Parse(reader);
        }

        public List<bookmarks> listarArticulosArchivados()
        {
            var datosXml = from item in datos.Elements("bookmarks").Elements("resource") select item;

            List<bookmarks> lista = new List<bookmarks>();

            foreach (XElement elemento in datosXml)
            {
                if (bool.Parse(elemento.Element("archive").Value.ToString()))
                {
                    bookmarks book = new bookmarks();
                    book.article_href = elemento.Element("article_href").Value.ToString();
                    article art = new article();
                    art.domain = elemento.Element("article").Element("domain").Value.ToString();
                    art.title = elemento.Element("article").Element("title").Value.ToString();
                    art.url = elemento.Element("article").Element("url").Value.ToString();
                    art.excerpt = elemento.Element("article").Element("excerpt").Value.ToString();
                    art.date_published = DateTime.Parse(elemento.Element("article").Element("date_published").Value.ToString());
                    art.id = elemento.Element("article").Element("id").Value.ToString();

                    book.articulo = art;

                    lista.Add(book);
                }
            }
            return lista;
        }

        public List<bookmarks> listarArticulosFavoritos()
        {
            var datosXml = from item in datos.Elements("bookmarks").Elements("resource") select item;

            List<bookmarks> lista = new List<bookmarks>();

            foreach (XElement elemento in datosXml)
            {
                if (bool.Parse(elemento.Element("favorite").Value.ToString()))
                {
                    bookmarks book = new bookmarks();
                    book.article_href = elemento.Element("article_href").Value.ToString();
                    article art = new article();
                    art.domain = elemento.Element("article").Element("domain").Value.ToString();
                    art.title = elemento.Element("article").Element("title").Value.ToString();
                    art.url = elemento.Element("article").Element("url").Value.ToString();
                    art.excerpt = elemento.Element("article").Element("excerpt").Value.ToString();
                    art.date_published = DateTime.Parse(elemento.Element("article").Element("date_published").Value.ToString());
                    art.id = elemento.Element("article").Element("id").Value.ToString();

                    book.articulo = art;

                    lista.Add(book);
                }
            }
            return lista;
        }

        public List<bookmarks> listarArticulosEnLectura()
        {
            var datosXml = from item in datos.Elements("bookmarks").Elements("resource") select item;

            List<bookmarks> lista = new List<bookmarks>();

            foreach (XElement elemento in datosXml)
            {
                if (bool.Parse(elemento.Element("archive").Value.ToString()))
                { }
                else
                {
                    bookmarks book = new bookmarks();
                    book.article_href = elemento.Element("article_href").Value.ToString();
                    article art = new article();
                    art.domain = elemento.Element("article").Element("domain").Value.ToString();
                    art.title = elemento.Element("article").Element("title").Value.ToString();
                    art.url = elemento.Element("article").Element("url").Value.ToString();
                    art.excerpt = elemento.Element("article").Element("excerpt").Value.ToString();
                    art.date_published = DateTime.Parse(elemento.Element("article").Element("date_published").Value.ToString());
                    art.id = elemento.Element("article").Element("id").Value.ToString();

                    book.articulo = art;

                    lista.Add(book);
                }
            }
            return lista;
        }
    }
}

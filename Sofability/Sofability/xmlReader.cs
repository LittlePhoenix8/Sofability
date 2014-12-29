using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Sofability.Models;

namespace Sofability
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
                    article art = new article();

                    try
                    {
                        book.article_href = elemento.Element("article_href").Value.ToString();
                        book.date_updated = DateTime.Parse(elemento.Element("date_updated").Value.ToString());
                        book.date_archived = DateTime.Parse(elemento.Element("date_archived").Value.ToString());
                        book.date_added = DateTime.Parse(elemento.Element("date_added").Value.ToString());
                        // elemento.Element("").Value;

                        art.domain = elemento.Element("article").Element("domain").Value.ToString();
                        art.title = elemento.Element("article").Element("title").Value.ToString();
                        art.url = elemento.Element("article").Element("url").Value.ToString();
                        art.excerpt = elemento.Element("article").Element("excerpt").Value.ToString();
                        art.date_published = DateTime.Parse(elemento.Element("article").Element("date_published").Value.ToString());
                        art.id = elemento.Element("article").Element("id").Value.ToString();

                        book.articulo = art;

                        lista.Add(book);
                    }
                    catch(FormatException)
                    {
                        book.article_href = elemento.Element("article_href").Value.ToString();
                        book.date_updated = DateTime.Now;
                        book.date_archived = DateTime.Now;
                        book.date_added = DateTime.Now;

                        art.domain = elemento.Element("article").Element("domain").Value.ToString();
                        art.title = elemento.Element("article").Element("title").Value.ToString();
                        art.url = elemento.Element("article").Element("url").Value.ToString();
                        art.excerpt = elemento.Element("article").Element("excerpt").Value.ToString();
                        art.date_published = DateTime.Now;
                        art.id = elemento.Element("article").Element("id").Value.ToString();

                        book.articulo = art;

                        lista.Add(book);
                    }
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
                    if (elemento.Element("article").Element("date_published").Value.ToString().Equals("None"))
                    {
                        art.date_published = DateTime.Now;
                    }
                    else
                    {
                        art.date_published = DateTime.Parse(elemento.Element("article").Element("date_published").Value.ToString());
                    }
                    art.id = elemento.Element("article").Element("id").Value.ToString();

                    book.articulo = art;

                    lista.Add(book);
                }
            }
            return lista;
        }

        public vistaArticulo verArticulo()
        {
            vistaArticulo vista = new vistaArticulo();

            vista.author = datos.Element("author").Value.ToString();
            vista.url = datos.Element("url").Value.ToString();
            vista.title = datos.Element("title").Value.ToString();
            vista.domain = datos.Element("domain").Value.ToString();
            vista.content = datos.Element("content").Value.ToString();
            DateTime variableTime = new DateTime();
            if (DateTime.TryParse(datos.Element("date_published").Value.ToString(), out variableTime))
            {
                vista.date_published = variableTime;
            }
            else
            {
                vista.date_published = DateTime.Now;
            }
            return vista;
        }
    }
}
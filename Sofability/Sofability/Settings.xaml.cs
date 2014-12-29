using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Sofability
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();
            this.DataContext = App.Settings;
        }

        //Live Tiles
        private void msgTile()
        {
            MessageBox.Show("Ya has creado este Live Tile");
        }

        private void pendientes_hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                ShellTile pendientesTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("Title=pendientes"));
                if (pendientesTile == null)
                {
                    StandardTileData dataPendientes = new StandardTileData();
                    dataPendientes.Title = "pendientes";
                    dataPendientes.BackgroundImage = new Uri("/Assets/Images/readinglist_icon.png", UriKind.Relative);
                    ShellTile.Create(new Uri("/MainPage.xaml?id=1", UriKind.Relative), dataPendientes);
                }
            }
            catch (InvalidOperationException)
            {
                msgTile();
            }

        }

        private void favoritos_hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                ShellTile favoritosTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("Title=favoritos"));
                if (favoritosTile == null)
                {
                    StandardTileData dataFavoritos = new StandardTileData();
                    dataFavoritos.Title = "favoritos";
                    dataFavoritos.BackgroundImage = new Uri("/Assets/Images/favorited_icon.png", UriKind.Relative);
                    ShellTile.Create(new Uri("/MainPage.xaml?id=2", UriKind.Relative), dataFavoritos);
                }
            }
            catch (InvalidOperationException)
            {
                msgTile();
            }
        }

        private void archivados_hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                ShellTile archivadosTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("Title=archivados"));
            if (archivadosTile == null)
            {
                StandardTileData dataArchivados = new StandardTileData();
                dataArchivados.Title = "archivados";
                dataArchivados.BackgroundImage = new Uri("/Assets/Images/archived_icon.png", UriKind.Relative);
                ShellTile.Create(new Uri("/MainPage.xaml?id=3", UriKind.Relative), dataArchivados);
            }
            }
            catch (InvalidOperationException)
            {
                msgTile();
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxText = "¿Deseas cerrar sesión?";
            var caption = "Cerrar sesión";
            var ans = MessageBox.Show(
                messageBoxText, caption, MessageBoxButton.OKCancel);

            if (ans == MessageBoxResult.OK)
            {
                App.SofabilityVM.Logout();
                this.Navigate("Login");
            }
        }
    }
}
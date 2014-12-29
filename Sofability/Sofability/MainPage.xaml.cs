using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sofability.Models;
using Sofability.Resources;
using Sofability.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Sofability
{
    public partial class MainPage : PhoneApplicationPage
    {
        string id;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            BuildLocalizedApplicationBar();

            // Load DataContext
            this.DataContext = App.SofabilityVM;
            
            // Deactivate the progress bar
            App.SofabilityVM.DataUpdated += SofabilityVM_DataUpdated;
           
        }

        void SofabilityVM_ErrorOnConnect(object sender, ErrorStatus e)
        {
            string message = "";
            string caption = "";
            switch (e)
            {
                case ErrorStatus.WrongUserPassword:
                    message = "Tu usuario o contraseña no es válida. Revisa tus credenciales e inténtalo nuevamente";
                    caption = "Problema en el login";
                    break;
                case ErrorStatus.OAuth:
                    message = "Hubo un problema en la autenticación con Readability";
                    caption = "Problema con el servidor";
                    break;
                case ErrorStatus.GetBookmarks:
                    message = "Hubo un problema al obtener los marcadores";
                    caption = "Problema con el servidor";
                    break;
                case ErrorStatus.AddBookmark:
                    message = "Hubo un problema al enviar tu marcador";
                    caption = "Problema con el servidor";
                    break;
            }
            MessageBox.Show(message, caption, MessageBoxButton.OK);

            this.ProgressBar.IsIndeterminate = false;

            if (e == ErrorStatus.WrongUserPassword)
                this.Navigate("Login");
        }

        void SofabilityVM_DataUpdated(object sender, EventArgs e)
        {
            this.ProgressBar.Visibility = Visibility.Visible;
            updateList();
        }

        private void updateList()
        {
            if (string.IsNullOrEmpty(id))
                id = "1";
            switch (id)
            {
                case "1":
                    this.ProgressBar.IsIndeterminate = true;
                    this.ArticlesList.DataContext = App.SofabilityVM.Pendings;
                    PendingsRect.Visibility = System.Windows.Visibility.Visible;
                    this.ProgressBar.Visibility = Visibility.Collapsed;
                    StarsRect.Visibility = System.Windows.Visibility.Collapsed;
                    ArchivedRect.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case "2":
                    this.ProgressBar.IsIndeterminate = true;
            
                    this.ArticlesList.DataContext = App.SofabilityVM.Stars;
                    PendingsRect.Visibility = System.Windows.Visibility.Collapsed;
                    this.ProgressBar.Visibility = Visibility.Collapsed;
                    StarsRect.Visibility = System.Windows.Visibility.Visible;
                    ArchivedRect.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case "3":
                    this.ProgressBar.IsIndeterminate = true;
                    this.ArticlesList.DataContext = App.SofabilityVM.Archived;
                    PendingsRect.Visibility = System.Windows.Visibility.Collapsed;
                    this.ProgressBar.Visibility = Visibility.Collapsed;
                    StarsRect.Visibility = System.Windows.Visibility.Collapsed;
                    ArchivedRect.Visibility = System.Windows.Visibility.Visible;
                    break;
            }
        }

        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            var updateButton = new ApplicationBarIconButton(new Uri("/Assets/Images/appbar.refresh.png", UriKind.Relative));
            updateButton.Text = AppResources.updateButton;
            updateButton.Click += updateButton_Click;
            ApplicationBar.Buttons.Add(updateButton);

            // Settings
            var settingsButton = new ApplicationBarIconButton(new Uri("/Assets/Images/appbar.settings.png", UriKind.Relative));
            settingsButton.Text = AppResources.settingsButton;
            settingsButton.Click += settingsButton_Click;
            ApplicationBar.Buttons.Add(settingsButton);

            //Add bookmark
            var addButton = new ApplicationBarIconButton(new Uri("/Assets/Images/appbar.add.png", UriKind.Relative));
            addButton.Text = AppResources.addBookmark;
            addButton.Click += addButton_Click;
            ApplicationBar.Buttons.Add(addButton);
        }

        void settingsButton_Click(object sender, EventArgs e)
        {
            this.Navigate("Settings");
        }

        void updateButton_Click(object sender, EventArgs e)
        {
            App.SofabilityVM.LoadData();
        }

        void addButton_Click(object sender, EventArgs e)
        {
            this.Navigate("AddBookmark");
        }

        private void Pendiente_Click(object sender, System.Windows.Input.MouseEventArgs e)
        {
            id = "1";
            updateList();
        }

        private void Destacados_Click(object sender, System.Windows.Input.MouseEventArgs e)
        {
            id = "2";
            updateList();
        }

        private void Archivados_Click(object sender, System.Windows.Input.MouseEventArgs e)
        {
            id = "3";
            updateList();
        }

        private void ArticlesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListBox;
            if (list.SelectedIndex < 0) return;

            var article = list.SelectedItem as Article;
            App.SelectedArticle = article;

            this.Navigate("ViewArticle", "Id", article.Id);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            App.SofabilityVM.ErrorOnConnect += SofabilityVM_ErrorOnConnect;

            if (!App.SofabilityVM.IsDataLoaded)
                App.SofabilityVM.LoadData();

            this.RemoveNavigationEntries();
            App.StorageSettings.Save();

            if (!NavigationContext.QueryString.TryGetValue("id", out id))
            {
                id = "1";
            }
            updateList();
        }

        private void Search_Click(object sender, System.Windows.Input.MouseEventArgs e)
        {
            string caption = "La búsqueda está deshabilitada";
            string message = "Nos disculapmos por no tener está caráteristica en estos momentos. Te prometemos que pronto estará disponible.";
            MessageBox.Show(message, caption, MessageBoxButton.OK);

            //this.Navigate("Search");
        }
    }
}
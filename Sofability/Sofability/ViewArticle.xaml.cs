using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sofability.Resources;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using Sofability.Models;
using Sofability.ViewModels;

namespace Sofability
{
    public partial class ViewArticle : PhoneApplicationPage
    {
        List<double> allowedFontSizes;
        List<string> allowedFontFamilies;
        vistaArticulo articulo;

        public ViewArticle()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            BuildLocalizedApplicationBar();

            allowedFontSizes = new List<double>{ 10.0f, 15.0f, 22.0f, 30.0f, 37.0f };
            allowedFontFamilies = App.Settings.FontsList;

            App.SofabilityVM.ArticleLoaded += SofabilityVM_ArticleLoaded;
        }

        void SofabilityVM_ArticleLoaded(object sender, vistaArticulo e)
        {
            articulo = e;
            App.SelectedArticle.Content = articulo.content;
            browser_ScriptNotify(null, null);
        }

        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            var textSizeButton = new ApplicationBarIconButton(new Uri("/Assets/Images/appbar.text.size.png", UriKind.Relative));
            textSizeButton.Text = AppResources.textSizeButton;
            textSizeButton.Click += textSizeButton_Click;
            ApplicationBar.Buttons.Add(textSizeButton);

            var changeFontButton = new ApplicationBarIconButton(new Uri("/Assets/Images/appbar.text.script.png", UriKind.Relative));
            changeFontButton.Text = AppResources.changeFontButton;
            changeFontButton.Click += changeFontButton_Click;
            ApplicationBar.Buttons.Add(changeFontButton);

            // Create a new menu item with the localized string from AppResources.
            var shareMenuItem = new ApplicationBarMenuItem(AppResources.shareMenuItem);
            shareMenuItem.Click += shareMenuItem_Click;
            ApplicationBar.MenuItems.Add(shareMenuItem);
        }

        void shareMenuItem_Click(object sender, EventArgs e)
        {
            this.Navigate("ShareArticle");
        }

        void textSizeButton_Click(object sender, EventArgs e)
        {
            var actual = App.Settings.FontSize;
            var i = this.allowedFontSizes.IndexOf(actual);
            i = (i == -1) ? this.allowedFontSizes.Count / 2 : i + 1;
            var next = this.allowedFontSizes[ (i % this.allowedFontSizes.Count) ];
            App.Settings.FontSize = next;

            browser_ScriptNotify(this, null);
        }

        void changeFontButton_Click(object sender, EventArgs e)
        {
            var actual = App.Settings.FontFamily;
            var i = this.allowedFontFamilies.IndexOf(actual) + 1;
            var next = this.allowedFontFamilies[ (i%this.allowedFontFamilies.Count) ];
            App.Settings.FontFamily = next;

            browser_ScriptNotify(this, null);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DataContext = App.SelectedArticle;
            string id_art = App.SelectedArticle.Id;
            App.SofabilityVM.obtenerArticulo(id_art);
        }

        private void browser_ScriptNotify(object sender, NotifyEventArgs e)
        {
            var fontFamily = App.Settings.FontFamily;
            var fontSize = App.Settings.FontSize.ToString() + "px; ";
            var response = new[]
            {
                App.SelectedArticle.Content,
                "body { text-align: left; " + 
                "color: Black; " + 
                "background-color: White; " + 
                "font-size: " + fontSize +
                "font-family: '"+fontFamily+"';  }\r\n" + 
                "a { color: inherit; text-decoration: none !important; }"
            };
            ArticleViewer.InvokeScript("getContentCallback", response);
        }

        bool firstTime = true;

        private void browser_Navigating(object sender, NavigatingEventArgs e)
        {
            if (firstTime)
                firstTime = false;
            else
                e.Cancel = true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace Sofability
{
    public partial class Login : PhoneApplicationPage
    {
        public Login()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // Hace que no sea posible volver a las páginas anteriores
            this.RemoveNavigationEntries();
        }

        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            var webBrowserTask = new WebBrowserTask();

            webBrowserTask.Uri = new Uri("https://readability.com/register", UriKind.Absolute);

            webBrowserTask.Show();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            App.SofabilityVM.USERNAME = this.Username.Text;
            App.SofabilityVM.PASSWORD = this.Password.Password;

            var mapper = App.RootFrame.UriMapper as UriMapper;
            mapper.UriMappings[0].MappedUri = new Uri("/MainPage.xaml", UriKind.Relative);
            
            this.Navigate("MainPage", "login", "true");
        }
    }
}
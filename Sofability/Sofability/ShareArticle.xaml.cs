using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System;

namespace Sofability
{
    public partial class ShareArticle : PhoneApplicationPage
    {
        public ShareArticle()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DataContext = App.SelectedArticle;
        }

        private void Kindle_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var emailComposeTask = new EmailComposeTask();
            var article = App.SelectedArticle;

            emailComposeTask.Subject = article.Title;
            emailComposeTask.Body = "https://www.readability.com/articles/" + article.Id;
            emailComposeTask.To = "el-mail-de-tu-kindle@kindle.com";

            emailComposeTask.Show();
            this.NavigationService.GoBack();
        }

        private void Facebook_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var shareLinkTask = new ShareLinkTask();
            var article = App.SelectedArticle;

            shareLinkTask.Title = article.Title;
            shareLinkTask.LinkUri = new Uri("https://www.readability.com/articles/" + article.Id, UriKind.Absolute);
            shareLinkTask.Message = "Mira este artículo. Leído en #Sofability";

            shareLinkTask.Show();
            this.NavigationService.GoBack();

        }
    }
}
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
    public partial class AddBookmark : PhoneApplicationPage
    {
        public AddBookmark()
        {
            InitializeComponent();
            this.DataContext = App.SofabilityVM;
        }

        

        private void btnAddBookmark_Click(object sender, RoutedEventArgs e)
        {
            App.SofabilityVM.addBookmark(txtURL.Text);
            this.NavigationService.GoBack();
        }

        

        
    }
}
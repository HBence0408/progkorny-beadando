using porgKorny_WPF_beadando.Model;
using porgKorny_WPF_beadando.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace porgKorny_WPF_beadando.View
{

    public partial class MarketView : Window
    {
        private MarketplaceViewModel viewModel;

        private string user;

        public string User {
            get => user;
            set
            {
                user = value;
                viewModel.SetUser(user);
            }
        }

        public MarketView(string u)
        {
            InitializeComponent();
            viewModel = new MarketplaceViewModel(u);
            DataContext = viewModel;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        //private void SearchBox_Changed(object sender, RoutedEventArgs e)
        //{

        //    if (DataContext is MarketplaceViewModel vm)
        //    {
        //        vm.SearchQuery = SearchTextBox.Text;
        //    }

        //}

        //private void AddAd_Click(object sender, RoutedEventArgs e)
        //{
        //    var dialog = new AddAdDialog(viewModel);
        //    dialog.ShowDialog();
        //}

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SearchOtherAds();
        }
        private void AddAd_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddAdWindow
            {
                Owner = this
            };

            if (addWindow.ShowDialog() == true)
            {
                if (int.TryParse(addWindow.PriceText, out int price))
                {
                    var newAd = new Ad
                    {
                        Title = addWindow.TitleText,
                        Description = addWindow.DescriptionText,
                        Price = price,
                        UserName = user
                    };

        viewModel.AddAd(newAd.Title,newAd.Description,newAd.Price);
                }
                else
                {
                    MessageBox.Show("Invalid price!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is MarketplaceViewModel vm)
            {
                vm.SearchQuery = SearchTextBox.Text;
            }
        }
    }
}

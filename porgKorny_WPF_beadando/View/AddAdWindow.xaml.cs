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
    /// <summary>
    /// Interaction logic for AddAdWindow.xaml
    /// </summary>
    public partial class AddAdWindow : Window
    {
        public string TitleText => TitleTextBox.Text;
        public string DescriptionText => DescriptionTextBox.Text;
        public string PriceText => PriceTextBox.Text;

        public AddAdWindow()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

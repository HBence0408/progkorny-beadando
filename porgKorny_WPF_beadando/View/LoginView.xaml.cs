using porgKorny_WPF_beadando.Services;
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
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
          DataContext = new LoginViewModel();
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

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            if (DataContext is LoginViewModel vm)
            {
                vm.Password = passwordTextBox.Password;
            }

        }

        private void UsernameBox_UsernamChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.Username = usernameTextBox.Text;
            }

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserDatabase.Instance.CheckUserCredentials(((LoginViewModel)DataContext).Username, ((LoginViewModel)DataContext).Password))
            {
                MessageBox.Show($"Welcome back {((LoginViewModel)DataContext).Username}", "Succesful Login", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Wrong username or password", "Login error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }
    }
}

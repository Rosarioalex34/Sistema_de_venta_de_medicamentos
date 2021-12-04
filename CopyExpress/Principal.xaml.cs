using CopyExpress.Views;
using System.Windows;
using System.Windows.Input;

namespace CopyExpress
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void TBShow(object sender, RoutedEventArgs e)
        {
            GridContent.Opacity = 0.5;
        }

        private void TBHide(object sender, RoutedEventArgs e)
        {
            GridContent.Opacity = 1;
        }

        private void PreviewMouseLeftBottonDownBG(object sender, MouseButtonEventArgs e)
        {
            BtnShowHide.IsChecked = false;
        }

        private void Maximizar(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Usuarios_Click(object sender, RoutedEventArgs e)
        {
            Content = new Usuarios();
        }

        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Informacion(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Esto es un prototipo de un sistema de farmacia o consultorio :)");
        }
    }
}

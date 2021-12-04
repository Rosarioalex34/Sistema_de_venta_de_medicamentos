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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CopyExpress.Views
{
    /// <summary>
    /// Lógica de interacción para MAntenimientoProductos.xaml
    /// </summary>
    public partial class MAntenimientoProductos : UserControl
    {
        public MAntenimientoProductos()
        {
            InitializeComponent();
        }

        private void Agregar_Producto(object sender, RoutedEventArgs e)
        {
            CRUBproductos ventana = new CRUBproductos();
            FrameProductos.Content = ventana;
            ventana.BtnCrearP.Visibility = Visibility.Visible;
        }
    }
}

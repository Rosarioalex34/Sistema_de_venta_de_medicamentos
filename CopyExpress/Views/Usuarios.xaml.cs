using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace CopyExpress.Views
{
    /// <summary>
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class Usuarios : UserControl
    {
        public Usuarios()
        {
            InitializeComponent();
            CargarDatos();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionDB"].ConnectionString);

        void CargarDatos()
        {
            con.Open();
            SqlCommand cnd = new SqlCommand("Select IdUsuarios, Nombres, Apellidos, Telefono, Correo, NombrePrivilegios from Usuarios Inner Join Privilegios on Usuarios.Privilegios=Privilegios.IdPrivilegios order by IdUsuarios ASC", con);
            SqlDataAdapter da = new SqlDataAdapter(cnd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridDatos.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void Agregar(object sender, RoutedEventArgs e)
        {
            CRUDUsuarios ventana = new CRUDUsuarios();
            FrameUsuarios.Content = ventana;
            ventana.BtnCrear.Visibility = Visibility.Visible;

        }

        private void Consultar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDUsuarios ventana = new CRUDUsuarios();
            ventana.idUsuarios = id;
            ventana.Consultar();
            FrameUsuarios.Content = ventana;
            ventana.Titulo.Text = "Consulta de Usuario";
            ventana.tbNombres.IsEnabled = false;
            ventana.tbApellidos.IsEnabled = false;
            ventana.tbDNI.IsEnabled = false;
            ventana.tbNIT.IsEnabled = false;
            ventana.tbFecha.IsEnabled = false;
            ventana.tbTelefono.IsEnabled = false;
            ventana.tbCorreo.IsEnabled = false;
            ventana.cbPrivilegios.IsEnabled = false;
            ventana.tbUsuario.IsEnabled = false;
            ventana.tbContraseña.IsEnabled = false;
            ventana.BtnSubir.IsEnabled = false;

        }

        private void Actualizar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDUsuarios ventana = new CRUDUsuarios();
            ventana.idUsuarios = id;
            ventana.Consultar();
            FrameUsuarios.Content = ventana;
            ventana.Titulo.Text = "Actualizar Usuario";
            ventana.tbNombres.IsEnabled = true;
            ventana.tbApellidos.IsEnabled = true;
            ventana.tbDNI.IsEnabled = true;
            ventana.tbNIT.IsEnabled = true;
            ventana.tbFecha.IsEnabled = true;
            ventana.tbTelefono.IsEnabled = true;
            ventana.tbCorreo.IsEnabled = true;
            ventana.cbPrivilegios.IsEnabled = true;
            ventana.tbUsuario.IsEnabled = true;
            ventana.tbContraseña.IsEnabled = true;
            ventana.BtnSubir.IsEnabled = true;
            ventana.btnModificar.Visibility = Visibility.Visible;
        }

        private void Eliminar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDUsuarios ventana = new CRUDUsuarios();
            ventana.idUsuarios = id;
            ventana.Consultar();
            FrameUsuarios.Content = ventana;
            ventana.Titulo.Text = "Eliminar Usuario";
            ventana.tbNombres.IsEnabled = false;
            ventana.tbApellidos.IsEnabled = false;
            ventana.tbDNI.IsEnabled = false;
            ventana.tbNIT.IsEnabled = false;
            ventana.tbFecha.IsEnabled = false;
            ventana.tbTelefono.IsEnabled = false;
            ventana.tbCorreo.IsEnabled = false;
            ventana.cbPrivilegios.IsEnabled = false;
            ventana.tbUsuario.IsEnabled = false;
            ventana.tbContraseña.IsEnabled = false;
            ventana.BtnSubir.IsEnabled = false;
            ventana.btnEliminar.Visibility = Visibility.Visible;

        }
    }
}

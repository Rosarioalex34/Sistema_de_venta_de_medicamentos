using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace CopyExpress
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        

        private void BotonIngresar_Click(object sender, RoutedEventArgs e)
        {
            login();
            //Principal p = new Principal();
            //p.ShowDialog();
            //this.Hide();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionDB"].ConnectionString);
        public void login()
        {
            try
            {
                con.Open();
                SqlCommand comando = new SqlCommand("SELECT USUARIO, CONTRASENIA FROM USUARIOS WHERE USUARIO = @vusuario AND contrasenia = @vcontrasenia", con);
                comando.Parameters.AddWithValue("@vusuario", TextUsuario.Text);
                comando.Parameters.AddWithValue("@vcontrasenia", Contraseña.Password);

                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    Principal p = new Principal();
                    p.ShowDialog();
                    this.Hide();
                }
                else
                {
                    if (TextUsuario.Text == "")
                    {
                        MessageBox.Show("Por favor ingrese su nombre de usuario");
                    }
                    else if (Contraseña.Password == "")
                    {
                        MessageBox.Show("Por favor ingrese su contraseña para continuar");
                    }
                    else
                    {
                        MessageBox.Show("Datos incorrectos");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void MostarContrasena(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
using Microsoft.Win32;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CopyExpress.Views
{
    /// <summary>
    /// Lógica de interacción para CRUDUsuarios.xaml
    /// </summary>
    public partial class CRUDUsuarios : Page
    {
        public CRUDUsuarios()
        {
            InitializeComponent();
            CargarCB();
        }

        readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionDB"].ConnectionString);
        public void CargarCB()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select NombrePrivilegios from Privilegios", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cbPrivilegios.Items.Add(dr["NombrePrivilegios"].ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Sucedio este error" + e.ToString());

            }
            finally
            {
                con.Close();
            }


        }
        #region CRUD (crear, leer, actualizar, eliminar)
        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new Usuarios();
        }
        public int idUsuarios;
        #region CREATE
        private void Crear(object sender, RoutedEventArgs e)
        {

            if (tbNombres.Text == "" || tbApellidos.Text == "" || tbDNI.Text == "" || tbNIT.Text == "" || tbCorreo.Text == "" || tbTelefono.Text == "" || tbFecha.Text == "" || cbPrivilegios.Text == "" || tbUsuario.Text == "" || tbContraseña.Text == "")
            {
                MessageBox.Show("Los campos no pueden estar vacios");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select IdPrivilegios from privilegios where NombrePrivilegios='" + cbPrivilegios.Text + "'", con);
                    var valor = cmd.ExecuteScalar();
                    int privilegio = (int)valor;
                    if (ImagenSubida == true)
                    {
                        SqlCommand com = new SqlCommand("Insert into Usuarios(nombres, apellidos, dni, nit, Fecha_Nac, " +
                            "telefono, correo, Privilegios, usuario, Img, contrasenia)" +
                            "Values( @nombres, @apellidos, @dni, @nit, @Fecha_Nac, @Telefono, @Correo,  @Privilegios, @usuario, @Img, @contrasenia)", con);

                        com.Parameters.Add("@nombres", SqlDbType.VarChar).Value = tbNombres.Text;
                        com.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = tbApellidos.Text;
                        com.Parameters.Add("@dni", SqlDbType.BigInt).Value = tbDNI.Text;
                        com.Parameters.Add("@nit", SqlDbType.BigInt).Value = tbNIT.Text;
                        com.Parameters.Add("@Fecha_Nac", SqlDbType.Date).Value = tbFecha.Text;
                        com.Parameters.Add("@Telefono", SqlDbType.BigInt).Value = (tbTelefono.Text);
                        com.Parameters.Add("@Correo", SqlDbType.VarChar).Value = tbCorreo.Text;
                        com.Parameters.Add("@Privilegios", SqlDbType.Int).Value = privilegio;
                        com.Parameters.Add("@usuario", SqlDbType.VarChar).Value = tbUsuario.Text;
                        com.Parameters.AddWithValue("@Img", SqlDbType.VarBinary).Value = data;
                        com.Parameters.Add("@contrasenia", SqlDbType.VarChar).Value =  tbContraseña.Text;
                        com.ExecuteNonQuery();
                        Content = new Usuarios();
                }
                else
                {
                    MessageBox.Show("Debe agregar una foto de perfil");
                }
                }
                catch (Exception x)
                {

                MessageBox.Show("Ocurrio el siguiente error" + x.ToString());
                }
                finally
                {
                con.Close();
                }

            }
        }
        #endregion
        #region LEER
        public void Consultar()
        {
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand("select * from Usuarios Inner Join Privilegios on Usuarios.Privilegios=Privilegios.IdPrivilegios where idUsuarios=" + idUsuarios, con);
                SqlDataReader rd = com.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                rd.Read();
                this.tbNombres.Text = rd["Nombres"].ToString();
                this.tbApellidos.Text = rd["Apellidos"].ToString();
                this.tbDNI.Text = rd["DNI"].ToString();
                this.tbNIT.Text = rd["NIT"].ToString();
                this.tbFecha.Text = rd["Fecha_Nac"].ToString();
                this.tbTelefono.Text = rd["Telefono"].ToString();
                this.tbCorreo.Text = rd["Correo"].ToString();
                this.cbPrivilegios.SelectedItem = rd["NombrePrivilegios"];
                this.tbUsuario.Text = rd["Usuario"].ToString();
                this.tbUsuario.Text = rd["Contrasenia"].ToString();
                rd.Close();

                //IMAGEN
                DataSet ds = new DataSet();
                SqlDataAdapter sqda = new SqlDataAdapter("select img from usuarios where IdUsuarios='" + idUsuarios + "'", con);
                sqda.Fill(ds);
                byte[] data = (byte[])ds.Tables[0].Rows[0][0];
                MemoryStream strm = new MemoryStream();
                strm.Write(data, 0, data.Length);
                strm.Position = 0;
                System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();
                Imagen.Source = bi;
                //IMAGEN
            }
            catch (Exception x)
            {

                MessageBox.Show("Ocurrio el siguiente error" + x.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        #endregion
        #region ACTUALIZAR
        private void Modificar(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand("select IdPrivilegios from Privilegios where NombrePrivilegios='" + cbPrivilegios.Text + "'", con);
                object valor = com.ExecuteScalar();
                int privilegios = (int)valor;

                if (tbNombres.Text == "" || tbApellidos.Text == "" || tbDNI.Text == "" || tbNIT.Text == "" || tbCorreo.Text == "" || tbTelefono.Text == "" || tbFecha.Text == "" || cbPrivilegios.Text == "" || tbUsuario.Text == "")
                {
                    MessageBox.Show("Los campos no pueden estar vacios");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("update usuarios set Nombres='" + tbNombres.Text + "',Apellidos='" + tbApellidos.Text +
                   "', DNI='" + Int64.Parse(tbDNI.Text) + "',NIT='" + Int64.Parse( tbNIT.Text )+ "',Fecha_Nac='" + tbFecha.Text + "',Telefono='" + Int64.Parse(tbTelefono.Text) +
                   "',Correo='" + tbCorreo.Text + "',Privilegios='" + privilegios + "',Usuario='" + tbUsuario.Text + "' where IdUsuarios='" + idUsuarios + "'", con);

                    cmd.ExecuteNonQuery();

                    if (ImagenSubida == true)
                    {
                        SqlCommand img = new SqlCommand("Update Usuarios set img=@img where IdUsuarios='" + idUsuarios + "'", con);
                        img.Parameters.AddWithValue("@img", SqlDbType.VarBinary).Value = data;
                        img.ExecuteNonQuery();

                    }
                }
                if (tbContraseña.Text != "")
                {
                    SqlCommand cmd = new SqlCommand(" update Usuarios set contrasenia='"+ tbContraseña.Text +"' where IdUsuarios='" + idUsuarios + "'", con);
                    cmd.ExecuteNonQuery();
                }
            con.Close();
            Content = new Usuarios();
            }
            catch (Exception x)
            {

                MessageBox.Show("Ocurrio el siguiente error " + x.ToString());
            }
            finally
            {

            }
        }
        private void Actualizar(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion
        #region ELIMINAR    

        private void Eliminar(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Usuarios where IdUsuarios= "+idUsuarios+"", con);
            cmd.ExecuteNonQuery();
            con.Close();
            Content = new Usuarios();

        }
        #endregion


        #endregion
        #region Imagen
        byte[] data;
        private bool ImagenSubida=false;
        private void Subir(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                data = new byte[fs.Length];
                fs.Read(data, 0, System.Convert.ToInt32(fs.Length));
                fs.Close();

                ImageSourceConverter imgs = new ImageSourceConverter();
                Imagen.SetValue(Image.SourceProperty, imgs.ConvertFromString(ofd.FileName.ToString()));
            }
            ImagenSubida = true;
        }
        #endregion
    }
}

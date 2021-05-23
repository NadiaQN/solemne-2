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
using System.IO;

namespace Ejercicio1
{
    /// <summary>
    /// Lógica de interacción para winPacientesMenores.xaml
    /// </summary>
    public partial class winPacientesMenores : Window
    {
        public winPacientesMenores()
        {
            InitializeComponent();
            CargarComboBox();
        }

        private void CargarComboBox()
        {
            string linea;
            string[] campo;

            try
            {
                FileStream f = new FileStream("pediatras.txt", FileMode.Open, FileAccess.Read);
                StreamReader fr = new StreamReader(f);

                while (!fr.EndOfStream)
                {
                    linea = fr.ReadLine();
                    campo = linea.Split(';');
                    this.cboPediatras.Items.Add(campo[0] + "-" + campo[1]);
                }

                fr.Close();
                f.Close();

                this.cboPediatras.SelectedIndex = 0;
            }
            catch (IOException ex)
            {
                MessageBox.Show("No se pudo abrir el archivo: " + ex.Message);
            }
        }

        public void MostrarPacientesMenores()
        {
            string cboNombrePediatra = this.cboPediatras.Text;
            string[] campoPediatra = cboNombrePediatra.Split('-');
            string codigoPediatra = campoPediatra[0];

            int meses;
            int cont = 0;

            try
            {
                FileStream f = new FileStream("Pacientes.txt", FileMode.Open, FileAccess.Read);
                StreamReader fr = new StreamReader(f);

                while(!fr.EndOfStream)
                {
                    string linea = fr.ReadLine();
                    string[] campos = linea.Split(';');
                    int.TryParse(campos[2], out meses);


                    if(codigoPediatra.Equals(campos[4]) && meses < 12)
                    {
                        cont++;
                    }
                }

                this.lblCantidadPacientes.Content = "La cantidad de pacientes menores a 12 meses es: " + cont;
            }
            catch (IOException ex)
            {
                MessageBox.Show("No se pudo abrir el archivo: " + ex.Message);
            }
        }

        public void Limpiar()
        {
            this.cboPediatras.SelectedIndex = 0;
            this.lblCantidadPacientes.Content = string.Empty;
        }

        private void btnMostrar_Click(object sender, RoutedEventArgs e)
        {
            MostrarPacientesMenores();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

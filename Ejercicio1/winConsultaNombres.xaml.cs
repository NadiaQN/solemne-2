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
    /// Lógica de interacción para winConsultaNombres.xaml
    /// </summary>
    public partial class winConsultaNombres : Window
    {
        public winConsultaNombres()
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

        public void MostrarPacientes()
        {
            this.lsvPacientes.Items.Clear();

            string cboNombrePediatra = this.cboPediatras.Text;
            string[] campo = cboNombrePediatra.Split('-');
            string codigoPediatra = campo[0];


            try
            {
                FileStream f = new FileStream("Pacientes.txt", FileMode.Open, FileAccess.Read);
                StreamReader fr = new StreamReader(f);

                while (!fr.EndOfStream)
                {
                    string linea = fr.ReadLine();
                    string[] campos = linea.Split(';');

                    if (codigoPediatra.Equals(campos[4]))
                    {
                        this.lsvPacientes.Items.Add(campos[1]);
                    }
                }

                fr.Close();
                f.Close();
            }
            catch (IOException ex) 
            {
                MessageBox.Show("Error al abrir el archivo: " + ex.Message);
            }
        }

        private void btnMostrar_Click(object sender, RoutedEventArgs e)
        {
            MostrarPacientes();
        }

        public void Limpiar()
        {
            this.lsvPacientes.Items.Clear();
            this.cboPediatras.SelectedIndex = 0;
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

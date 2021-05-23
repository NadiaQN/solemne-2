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
using System.Text.RegularExpressions;

namespace Ejercicio1
{
    /// <summary>
    /// Lógica de interacción para winAtencion.xaml
    /// </summary>
    public partial class winAtencion : Window
    {
        string archivo = "Pacientes.txt";

        public winAtencion()
        {
            InitializeComponent();
            CargarComboBox();
        }

        public void RegistrarPaciente(string arch)
        {
            string rut, nombre, meses, codigoTerapia, cboNombrePediatra, codigoPediatra, linea;
            string[] campo;

            rut = this.txtRut.Text;
            nombre = this.txtNombre.Text;
            meses = this.txtMeses.Text;
            codigoTerapia = this.txtCodigoTerapia.Text;

            cboNombrePediatra = this.cboPediatras.Text;
            campo = cboNombrePediatra.Split('-');
            codigoPediatra = campo[0];

            try
            {
                FileStream f = new FileStream(arch, FileMode.Append, FileAccess.Write);
                StreamWriter fw = new StreamWriter(f);

                linea = rut + ";" + nombre + ";" + meses + ";" + codigoTerapia + ";" + codigoPediatra;
                fw.WriteLine(linea);

                fw.Close();
                f.Close();

                MessageBox.Show("Paciente registrado con exito");
            }
            catch (IOException e)
            {
                MessageBox.Show("Error al abrir el archivo: " + e.Message);
            }
        }

        private void CargarComboBox()
        {
            string linea;
            string[] campo;

            try
            {
                FileStream f = new FileStream("pediatras.txt", FileMode.Open, FileAccess.Read);
                StreamReader fr = new StreamReader(f);

                while(!fr.EndOfStream)
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

        public bool ConsultarDatos(string rut)
        {
            string linea;
            string[] campos;
            bool existeRut = false;

            try
            {
                FileStream f = new FileStream("Pacientes.txt", FileMode.Open, FileAccess.Read);
                StreamReader fr = new StreamReader(f);

                while (!fr.EndOfStream)
                {
                    linea = fr.ReadLine();
                    campos = linea.Split(';');

                    if (rut.Equals(campos[0]))
                    {
                        existeRut = true;
                    }
                }

                fr.Close();
                f.Close();

            }
            catch (IOException e)
            {
                MessageBox.Show("Error al abrir el archivo: " + e.Message);
            }

            return existeRut;
        }

        public bool ValidarCodigoTerapia()
        {
            bool estaOK = false;
            string codigoTerapia = this.txtCodigoTerapia.Text;

            string patronCodigoTerapia = @"\A[A-Z]{3}\-[1-9]{2}\Z";

            if (Regex.IsMatch(codigoTerapia, patronCodigoTerapia))
            {
                estaOK = true;
            }
            return estaOK;
        }

        public void Limpiar()
        {
            this.txtRut.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtMeses.Text = string.Empty;
            this.txtCodigoTerapia.Text = string.Empty;
            this.cboPediatras.SelectedIndex = 0;
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            string rut = this.txtRut.Text;

            if (ConsultarDatos(rut))
            {
                MessageBox.Show("El paciente ya se ha atendido este mes");
            }
            else if (!ValidarCodigoTerapia())
            {
                MessageBox.Show("El codigo de terapia es incorrecto");
            }
            else
            {
                RegistrarPaciente(archivo);
            }
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

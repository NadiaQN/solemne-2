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

namespace Ejercicio1
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

        private void mmiAtencion_Click(object sender, RoutedEventArgs e)
        {
            winAtencion ventana = new winAtencion();
            ventana.Owner = this;
            ventana.ShowDialog();
        }

        private void mmiConsultarNombres_Click(object sender, RoutedEventArgs e)
        {
            winConsultaNombres ventana = new winConsultaNombres();
            ventana.Owner = this;
            ventana.ShowDialog();
        }

        private void mmiConsultarMenores_Click(object sender, RoutedEventArgs e)
        {
            winPacientesMenores ventana = new winPacientesMenores();
            ventana.Owner = this;
            ventana.ShowDialog();
        }

        private void mmiConsultarPromedio_Click(object sender, RoutedEventArgs e)
        {
            winPromedio ventana = new winPromedio();
            ventana.Owner = this;
            ventana.ShowDialog();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfGym.Test
{
    /// <summary>
    /// Interaction logic for Padre.xaml
    /// </summary>
    public partial class Padre : Window
    {
        public Padre()
        {
            InitializeComponent();
        }

        //Creamos una referencia a la Ventana Hija
        Hijo ag = new Hijo();

        //Creamos una pequeña estructura con la cual cargaremos nuestro dataGridView
        //que contendrá el nombre y apellido de una persona
        public struct MyData
        {
            public string nombre { set; get; }
            public string apellido { set; get; }
        }


        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ag.AgregarEventHandler += new RoutedEventHandler(ag_AgregarEventHandler);
                ag.ShowDialog();
            }
            catch
            {
                ag = new Hijo();
                ag.AgregarEventHandler += new RoutedEventHandler(ag_AgregarEventHandler);
                ag.ShowDialog();
            }
        }

        void ag_AgregarEventHandler(object sender, RoutedEventArgs e)
        {

            MyData datos = new MyData() { nombre = ag.Nombre.Text.ToString(), apellido = ag.Apellido.Text.ToString() };
            Informacion.Items.Add(datos);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ag.Close();
        }
    }
}

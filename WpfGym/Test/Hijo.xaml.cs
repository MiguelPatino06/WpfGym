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
    /// Interaction logic for Hijo.xaml
    /// </summary>
    public partial class Hijo : Window
    {
        public Hijo()
        {
            InitializeComponent();
        }

        //Creamos nuestro evento que dispararemos cuando el usuario
        //agregue un nuevo dato
        public event RoutedEventHandler AgregarEventHandler;
        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            //Validamos que haya ingresado al menos alguno de los dos campos (nombre, apellido).
            if (Nombre.Text != string.Empty || Apellido.Text != string.Empty)
            {
                if (AgregarEventHandler != null)
                {
                    //Disparamos el evento que hará que el dataGridView de la Ventana
                    //padre se actualice
                    AgregarEventHandler(sender, e);
                }
                //Reinicializamos los valores de los textBox
                Nombre.Text = string.Empty;
                Apellido.Text = string.Empty;
            }
        }
    }
}

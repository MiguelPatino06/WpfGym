using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfGym.Controls
{
    /// <summary>
    /// Interaction logic for GRButton.xaml
    /// </summary>
    public partial class GRButton : Button
    {
        public ImageSource ButtonImage
        {
            get { return mImage.Source; }
            set { mImage.Source = value; }
        }

        public double ButtonImageH
        {
            get { return mImage.Height; }
            set { mImage.Height = value; }
        }

        public double ButtonImageW
        {
            get { return mImage.Width; }
            set { mImage.Width = value; }
        }

        public Stretch ButtonImageStretch
        {
            get { return mImage.Stretch; }
            set { mImage.Stretch = value; }
        }

        public String ButtonText
        {
            get { return mLabel.Text; }
            set { mLabel.Text = value; }
        }

        public GRButton()
        {
            InitializeComponent();
        }
    }
}

using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfGym.Controls
{
    public partial class GRLogPhotoHome : UserControl
    {
        //public string SetPicture
        //{
        //    set
        //    {
        //        if (System.IO.File.Exists(value))
        //        {
        //            try
        //            {
        //                ImageRuta.Source = new BitmapImage(new Uri(value, UriKind.Absolute));
        //            }
        //            catch
        //            {
        //                ImageRuta.Source = new BitmapImage(new Uri("/Green Retail;componentComponent/Resources/ICON_ANIMO_BANNER_LOGO.png", UriKind.RelativeOrAbsolute));
        //            }
        //        }
        //    }
        //}

        public GRLogPhotoHome()
        {
            InitializeComponent();
        }
    }
}

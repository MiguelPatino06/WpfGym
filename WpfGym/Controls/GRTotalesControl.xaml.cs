using System.Globalization;
using System.Windows.Controls;

namespace WpfGym.Controls
{
    /// <summary>
    /// Interaction logic for GRTotalesControl.xaml
    /// </summary>
    public partial class GRTotalesControl : UserControl
    {
        public string etiqueta_totales
        {
            set { TextSaldo.Content = decimal.Parse(value).ToString("N2", CultureInfo.CreateSpecificCulture("en-US")); }
            get { return TextSaldo.Content.ToString(); }
        }
        public string etiqueta_TotalPago
        {
            set { Total_Pago.Content = decimal.Parse(value).ToString("N2", CultureInfo.CreateSpecificCulture("en-US")); }
            get { return Total_Pago.Content.ToString(); }
        }
        public string etiqueta_TotalImpuestos
        {
            set { Total_Impuesto.Content = decimal.Parse(value).ToString("N2", CultureInfo.CreateSpecificCulture("en-US")); }
            get { return Total_Impuesto.Content.ToString(); }
        }
        public string etiqueta_totalSubtotal
        {
            set { Total_Subtotal.Content = decimal.Parse(value).ToString("N2",CultureInfo.CreateSpecificCulture("en-US")); }
            get { return Total_Subtotal.Content.ToString(); }
        }
        public string etiqueta_numLineas
        {
            set { Total_NumLineas.Content = decimal.Parse(value).ToString("N2", CultureInfo.CreateSpecificCulture("en-US")); }
            get { return Total_NumLineas.Content.ToString(); }
        }
        public string etiqueta_totalDescuento
        {
            set { Total_Descuento.Content = decimal.Parse(value).ToString("N2", CultureInfo.CreateSpecificCulture("en-US")); }
            get { return Total_Descuento.Content.ToString(); }
        }

        public string etiqueta_totalDescuentoLineas
        {
            set { Total_descuento_Lineas.Content = decimal.Parse(value).ToString("N2", CultureInfo.CreateSpecificCulture("en-US")); }
            get { return Total_descuento_Lineas.Content.ToString(); }
        }

        public string etiqueta_NoTransaccion
        {
            set { No_Transaccion.Content = value; }
            get { return No_Transaccion.Content.ToString(); }
        }
        //Mod 19-08-2016
        public string etiqueta_SalesPerson
        {
            set { SalesPerson.Content = value; }
            get { return SalesPerson.Content.ToString(); }
        }
        //
        public string etiqueta_Fecha
        {
            set { Label_Fecha.Content = value; }
            get { return Label_Fecha.Content.ToString(); }
        }

        public string etiqueta_Diferencia
        {
            set { Diferencia.Content = decimal.Parse(value).ToString("N2", CultureInfo.CreateSpecificCulture("en-US")); }
            get { return Diferencia.Content.ToString(); }
        }

        public void resetControlTotales()
        {
            etiqueta_Diferencia = string.Empty;
            etiqueta_totales = string.Empty;
            etiqueta_TotalPago = string.Empty;
            etiqueta_TotalImpuestos = string.Empty;
            etiqueta_TotalImpuestos = string.Empty;
            etiqueta_totalSubtotal = string.Empty;
            etiqueta_numLineas = string.Empty;
            etiqueta_totalDescuento = string.Empty;
            etiqueta_totalDescuentoLineas = string.Empty;
            etiqueta_NoTransaccion = "No:";
            etiqueta_SalesPerson = "Vendedor:";

            etiqueta_Fecha = "Fecha:";
        }
        public GRTotalesControl()
        {
            InitializeComponent();
        }

    }
}

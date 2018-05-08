using GreenRetail.Bridge.Modules;
using System.Windows;

namespace Green_Retail.Control
{
    /// <summary>
    /// Interaction logic for GRDialogPasswordExit.xaml
    /// </summary>
    public partial class GRDialogPasswordExit : Window
    {
        public string Message
        {
            get { return MessageOut.Text; }
            set { MessageOut.Text = value; }
        }

        public GRDialogPasswordExit()
        {
            InitializeComponent();
            TextBoxUser.Focus();
        }

        private void ButtonAcept_Click(object sender, RoutedEventArgs e)
        {
            bool buser = false;
            bool bpassword = false;
            bool badmi = false;

            GRUser user = GreenRetail.Bridge.GreenRetailApplication.Instance.User;

            if (GreenRetail.Bridge.GreenRetailApplication.Instance.ExistByName(TextBoxUser.Text))
            {
                GreenRetail.Bridge.GreenRetailApplication.Instance.OperatorID = TextBoxUser.Text;
                buser = true;
            }
            if (GreenRetail.Bridge.GreenRetailApplication.Instance.UserAuthenticate(GreenRetail.Bridge.GreenRetailApplication.Instance.OperatorID, passwordBox.Password))
            {
                bpassword = true;
            }

            if (string.Equals(GreenRetail.Bridge.GreenRetailApplication.Instance.User.NameSufix.ToUpper().Trim(), "ADM"))
            {
                badmi = true;
            }

            if (!buser)
            {

                GRDialogError Var = new GRDialogError();
                Var.Message = "Error: ID no Existe";
                Var.ShowDialog();

            }else
            if (!bpassword)
            {
                GRDialogError Var = new GRDialogError();
                Var.Message = "Error: Clave Errada";
                Var.ShowDialog();
            }else
            if (!badmi)
            {
                GRDialogError Var = new GRDialogError();
                Var.Message = "Error: El Usuario no tiene privilegios para salir de la aplicación..";
                Var.ShowDialog();
            }

            GreenRetail.Bridge.GreenRetailApplication.Instance.UserAuthenticateTrust(user.UserId);//Volvemos a la sesion anterior

            if (buser && bpassword && badmi)
            {
                this.DialogResult = true;
            }
            else
            {
                this.DialogResult = false;
            }


        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

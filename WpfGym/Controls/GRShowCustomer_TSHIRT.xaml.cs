
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace WpfGym.Controls
{
    /// <summary>
    /// Interaction logic for GRShowCustomer_TSHIRT.xaml
    /// </summary>
    public partial class GRShowCustomer_TSHIRT : UserControl
    {
        #region "Attributes"
        //protected GreenRetail.Framework.Core.DataTypes.UnorderedMap<string, IGREventHandler> mEventHandlers;
        //protected Queue<IGREvent> mEventQueue;
        protected bool mMultiHandler;
       // private int TipoClienteSelected;
        #endregion   

        public GRShowCustomer_TSHIRT()
        {
            //mEventHandlers = new GreenRetail.Framework.Core.DataTypes.UnorderedMap<string, IGREventHandler>();
            //mEventQueue = new Queue<IGREvent>();
            mMultiHandler = false;
            InitializeComponent();
        }

        #region "Methods"
        //[XmlIgnore]
        //protected List<IGREventHandler> findEventHandlers(string eventName)
        //{
        //    List<IGREventHandler> HandlersList = (List<IGREventHandler>)mEventHandlers.select(eventName);
        //    return HandlersList;
        //}

        [XmlIgnore]
        public bool Multihandler
        {
            get
            {
                return mMultiHandler;
            }
            set
            {
                mMultiHandler = value;
            }
        }

        //[XmlIgnore]
        //public bool addEventListener(string eventName, IGREventHandler handler)
        //{
        //    bool res = false;

        //    //if (mEventHandlers.select(eventName).Count <= 0 || mMultiHandler)
        //    //{
        //    mEventHandlers.Add(new KeyValuePair<string, IGREventHandler>(eventName, handler));
        //    res = true;
        //    //}
        //    return res;
        //}

        //public int HandlerCount { get { return mEventHandlers.Count; } }
        //public int QueueCount { get { return mEventQueue.Count; } }

        ////[XmlIgnore]
        //public void disposeEvent(IGREvent evnt)
        //{
        //    mEventQueue.Enqueue(evnt);
        //    doEvents();

        //}

        ////[XmlIgnore]
        //public bool removeEventListener(string eventName, IGREventHandler handler)
        //{
        //    bool res = false;
        //    int index = 0;

        //    while ((index = mEventHandlers.getIndex(eventName)) > -1)
        //    {
        //        mEventHandlers.Remove(index);
        //        res = true;
        //    }
        //    return res;
        //}


        ////[XmlIgnore]
        //public void doEvents()
        //{
        //    IGREvent evnt;
        //    try
        //    {
        //        while (mEventQueue.Count > 0)
        //        {
        //            evnt = mEventQueue.Dequeue();
        //            try
        //            {
        //                string name = evnt.getName();
        //                ICollection<IGREventHandler> handlers = mEventHandlers.select(name);
        //                foreach (IGREventHandler hndlr in handlers)
        //                {
        //                    hndlr.doEvent(evnt);
        //                }
        //                evnt = null;
        //            }
        //            catch (Exception e)
        //            {
        //                GRDiagnostic.WriteLog("EXCEPCIÓN:" + e.Message);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }

        //}
        #endregion

        #region Properties
        public string TitleCustomerP
        {
            get { return (string)TitleCustomer.Content; }
            set { TitleCustomer.Content = value; }
        }

        public string TextNombreClienteP
        {
            get { return (string)TextNombreCliente.Content; }
            set { TextNombreCliente.Content = value; }
        }
        public string TextDNIP
        {
            get { return (string)TextDNI.Content; }
            set { TextDNI.Content = value; }
        }
        public string TextAddressP
        {
            get { return (string)TextAddress.Content; }
            set { TextAddress.Content = value; }
        }
        #endregion

        private void ButtonFindCustomer_Click(object sender, RoutedEventArgs e)
        {
           // this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(sender));
        }

        private void TextNombreCliente_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (POSClient.Instance.CustomerP!=null)
            //{
            //    GRChangeCustomer Var = new GRChangeCustomer();
            //    Var.ShowDialog();
            //    if (Var.DialogResult==true)
            //    {
            //        bool flagName = string.IsNullOrEmpty(Var.TextNameP);
            //        bool flagRuc = string.IsNullOrEmpty(Var.TextRUCP);
            //        bool flagAddress = string.IsNullOrEmpty(Var.TextAddressP);

            //        if (!(flagName) || !(flagRuc) || !(flagAddress))
            //        {
            //            POSClient.Instance.CustomerP.Name = Var.TextNameP;
            //            POSClient.Instance.CustomerP.VATRegistrationNo_ = Var.TextRUCP;
            //            POSClient.Instance.CustomerP.Address1 = Var.TextAddressP;
            //            POSClient.Instance.Set_Customer(POSClient.Instance.CustomerP);
            //            POSClient.Instance.UpdateDisplay();
            //        }
            //    }
            //}
        }

        private void ButtonFindEmployee_Click(object sender, RoutedEventArgs e)
        {
            //  if(POSClient.Instance.TransactionHeader!=null)
             //GreenRetail.Shell.GreenRetailShell.Instance.Navigate(new POS.POSFindEmployees());
           // GreenRetail.Shell.GreenRetailShell.Instance.Navigate(new POS.POSPrincipalCustomerAnimo());
        }

        //private void ComboBoxTipoCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    TipoClienteSelected = 0;

        //    switch (comboBoxTIPOCLIENTE.SelectedIndex)
        //    {
        //        case 0:
        //            TipoClienteSelected = 0;
        //            break;
        //        case 1:
        //            TipoClienteSelected = 1;
        //            break;
        //    }
        //}
    }
}

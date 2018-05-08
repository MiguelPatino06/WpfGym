
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace WpfGym.Views
{
    /// <summary>
    /// Interaction logic for POSPrincipal1.xaml
    /// </summary>
    public partial class POSPrincipal1 : Page
    {

        //private POSPrincipal appPrincipal;

        //public POSPrincipal MappPrincipal
        //{
        //    set { appPrincipal = value; }
        //    get { return appPrincipal; }
        //}

        #region "Attributes"
        //protected GreenRetail.Framework.Core.DataTypes.UnorderedMap<string, IGREventHandler> mEventHandlers;
        //protected Queue<IGREvent> mEventQueue;
        protected bool mMultiHandler;
        #endregion   

        public POSPrincipal1()
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
        //            catch (Exception ex)
        //            {
        //                GreenRetail.Framework.Core.Diagnostic.GRDiagnostic.WriteError(ex.Message);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        GreenRetail.Framework.Core.Diagnostic.GRDiagnostic.WriteError(ex.Message);
        //    }

        //}
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(sender));
        }

        private void Button_ClickButtonDeleteTransP(object sender, RoutedEventArgs e)
        {
            //POSClient.Instance.disposeEvent(new GreenRetail.Bridge.POS.Events.POSTransactionVoidEvent(POSClient.Instance.TransactionHeader, POSClient.Instance));
        }

        private void Button_ClickButtonFindItem(object sender, RoutedEventArgs e)
        {
            //if (POSClient.Instance.CustomerP == null)
            //{
            //    GRDialogError Var = new GRDialogError();
            //    Var.Message = "Debe asginar el cliente primero..";
            //    Var.ShowDialog();
            //}
            //else
            //{
            //    if (POSClient.Instance.TotalPaid > 0)
            //    {
            //        GRDialogError Var = new GRDialogError();
            //        Var.Message = "Operacion No Permitida: Transacción con pagos hechos..";
            //        Var.ShowDialog();
            //    }
            //    else
            //    {
            //        GreenRetail.Shell.GreenRetailShell.Instance.Navigate(new POSFindItem());
            //    }
            //}

        }

        private void Button_ClickButtonTransaccion(object sender, RoutedEventArgs e)
        {
           // GreenRetail.Shell.GreenRetailShell.Instance.Navigate(new POSFindTrans(true)); //true = normal; false = con filtro
        }

        private void Button_ClickDeletePayments(object sender, RoutedEventArgs e)
        {
            //if (POSClient.Instance.TransactionHeader != null)
            //{
            //    if (POSClient.Instance.SalesLineSelected != null)
            //    {
            //        GRCommentControl ControlComentario = new GRCommentControl();
            //        if (ControlComentario.ShowDialog() == true)
            //        {
            //            SalesLineRecords sltemp = new SalesLineRecords();
            //            POSClient.Instance.SalesLineSelected.Comment = ControlComentario.Comment;
            //            sltemp.UpdateSalesLines(POSClient.Instance.SalesLineSelected);
            //            POSClient.Instance.UpdateDisplay();
            //        }
            //    }
            }
            //if(POSClient.Instance.TransactionHeader!=null)
            //{
            //    GRDeletePayments ni = new GRDeletePayments();
            //    BackGround.Visibility = System.Windows.Visibility.Visible;
            //    if (ni.ShowDialog() == true)
            //    {
            //        POSClient.Instance.disposeEvent(new GreenRetail.Bridge.POS.Events.POSPaymentRemoveEvent(ni.pSalesPM, POSClient.Instance.TransactionHeader, POSClient.Instance));
            //    }
            //    BackGround.Visibility = System.Windows.Visibility.Hidden;
            //    }
            //else
            //{
            //    GRDialogError Var = new GRDialogError();
            //    Var.Message = "No existe Transaccion Cargada";
            //    Var.ShowDialog();
            //}

       

        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //   // BackGround.Visibility = System.Windows.Visibility.Hidden;
        //}

        //private void ButtonButtonCash_Click(object sender, RoutedEventArgs e)
        //{
        //    GRPayments ni = new GRPayments(PaymentsType.CPS_CASH);
        //    BackGround.Visibility = System.Windows.Visibility.Visible;

        //    if (POSClient.Instance.TransactionHeader != null)
        //    {
        //        if (POSClient.Instance.TransactionHeader.Document_Type != (int)TransactionType.TT_QUOTE)
        //        {
        //            if (POSClient.Instance.CustomerP != null)
        //            {
        //                if (ni.ShowDialog() == true)
        //                {
        //                    POSClient.Instance.disposeEvent(new GreenRetail.Bridge.POS.Events.POSPaymentAddEvent(ni.pSalesPM, POSClient.Instance.TransactionHeader, POSClient.Instance));

        //                    POSClient.Instance.disposeEvent(new GreenRetail.Bridge.POS.Events.POSProcessedTransactionPaymentEvent(ni.pSalesPM, POSClient.Instance.TransactionHeader, POSClient.Instance));
        //                }
        //            }
        //            else
        //            {
        //                GRDialogError Var = new GRDialogError();
        //                Var.Message = "No existe Cliente Asignado!";
        //                Var.ShowDialog();
        //            }
        //        }
        //        else
        //        {
        //            GRDialogError Var = new GRDialogError();
        //            Var.Message = "No se permiten pagos en las Cotizaciones";
        //            Var.ShowDialog();
        //        }
        //    }
        //    else
        //    {
        //        GRDialogError Var = new GRDialogError();
        //        Var.Message = "No existe Transaccion Cargada";
        //        Var.ShowDialog();
        //    }

        //    BackGround.Visibility = System.Windows.Visibility.Hidden;
        //}

        //private void ButtonButtonSuspend_Click(object sender, RoutedEventArgs e)
        //{
        //    if (POSClient.Instance.TransactionHeader != null)
        //    {
        //        if (POSClient.Instance.TransactionHeader.Document_Type != (int)TransactionType.TT_QUOTE)
        //        {
        //            if (!(POSClient.Instance.TransactionHeader.Status == (int)TransactionStatus.TS_RESERVEDWITH_PAY || POSClient.Instance.TransactionHeader.Status == (int)TransactionStatus.TS_RESERVEDWITHOUT_PAY))
        //            {
        //                POSClient.Instance.TransactionHeader.Status = (int)TransactionStatus.TS_STANDBY;
        //                SalesHeaderRecords SHRTemp = new SalesHeaderRecords();
        //                SHRTemp.UpdateSalesHeader(POSClient.Instance.TransactionHeader);
        //                POSClient.Instance.Reset();
        //            }
        //            else
        //            {
        //                GRDialogError Var = new GRDialogError();
        //                Var.Message = "No se puede suspender una reserva..";
        //                Var.ShowDialog();
        //            }
        //        }
        //        else
        //        {
        //            GRDialogError Var = new GRDialogError();
        //            Var.Message = "No se puede suspender una cotizacion..";
        //            Var.ShowDialog();
        //        }
        //    }
        //    else
        //    {
        //        GRDialogError Var = new GRDialogError();
        //        Var.Message = "No existe Transaccion en Transito: Error al suspender la Transaccion ";
        //        Var.ShowDialog();
        //    }
        //}

        //private void ButtonExit_Click(object sender, RoutedEventArgs e)
        //{
        //    GreenRetail.Bridge.GreenRetailApplication.Instance.IsRunning = false;
        //    App.Current.Shutdown();
        //}

        //private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        //{
        //    GreenRetail.Bridge.GreenRetailApplication.Instance.Reactor.disposeEvent(new GreenRetail.Bridge.POS.Events.POSMinimizeEvent(POSClient.Instance));
        ////    this.WindowState = WindowState.Minimized;
        //}


 
    }
}

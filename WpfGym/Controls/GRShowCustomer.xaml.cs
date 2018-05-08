using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace WpfGym.Controls
{
    /// <summary>
    /// Interaction logic for GRShowCustomer.xaml
    /// </summary>
    public partial class GRShowCustomer : UserControl
    {
        #region "Attributes"
        //protected GreenRetail.Framework.Core.DataTypes.UnorderedMap<string, IGREventHandler> mEventHandlers;
        //protected Queue<IGREvent> mEventQueue;
        protected bool mMultiHandler;
        #endregion   

        public GRShowCustomer()
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

        //[XmlIgnore]
        //public bool Multihandler
        //{
        //    get
        //    {
        //        return mMultiHandler;
        //    }
        //    set
        //    {
        //        mMultiHandler = value;
        //    }
        //}

        ////[XmlIgnore]
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
            get; set; }
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
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(sender));
        }

        private void ButtonFindCustomer_Click_1(object sender, RoutedEventArgs e)
        {
            var _window = new Index();
            _window.Show();
          

        }
    }
}

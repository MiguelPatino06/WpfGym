
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace WpfGym.Controls
{
    /// <summary>
    /// Interaction logic for GRPrincipalControl.xaml
    /// </summary>
    public partial class GRPrincipalControl : UserControl
    {
        private Frame mMainFrame;
    //    private String mActiveButton;

        public Frame MainFrame 
        { set { mMainFrame = value; }
          get { return mMainFrame;  }
        }

        #region "Attributes"
        //protected GreenRetail.Framework.Core.DataTypes.UnorderedMap<string, IGREventHandler> mEventHandlers;
        //protected Queue<IGREvent> mEventQueue;
        protected bool mMultiHandler;
        #endregion   

        public GRPrincipalControl()
        {
            //mEventHandlers = new GreenRetail.Framework.Core.DataTypes.UnorderedMap<string, IGREventHandler>();
            //mEventQueue = new Queue<IGREvent>();
            //mMultiHandler = false;
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
        //POSPrincipal mPrincipalPage;
        //public POSPrincipal PrincipalPage
        //{ 
        //  set { mPrincipalPage = value; }
        //  get { return mPrincipalPage;  }
        //}

        public void Initialze() //?????????????????????????
        {
            //PrincipalPage.PageFrameProperty = new POSPrincipal1();

            //MainFrame.Navigate(PrincipalPage.PageFrameProperty);
        }

        private void ButtonPrincipalControl_Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(sender));
        }


    }
}

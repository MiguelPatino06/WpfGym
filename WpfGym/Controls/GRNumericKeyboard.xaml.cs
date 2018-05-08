
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace WpfGym.Controls
{
    /// <summary>
    /// Lógica de interacción para GRNumericKeyboard.xaml
    /// </summary>
    public partial class GRNumericKeyboard : UserControl
    {
        #region "Attributes"
        //protected GreenRetail.Framework.Core.DataTypes.UnorderedMap<string, IGREventHandler> mEventHandlers;
        //protected Queue<IGREvent> mEventQueue;
        protected bool mMultiHandler;
        #endregion   


        public GRNumericKeyboard()
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

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(Button1));
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(Button2));
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(Button3));
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(Button4));
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(Button5));
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(Button6));
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(Button7));
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(Button8));
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(Button9));
        }

        private void ButtonC_Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(ButtonC));
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(ButtonOK));
        }

        private void ButtonDEL_Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(ButtonDEL));
        }

        private void Button__Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(Button_));
        }

        private void Button_coma_Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(Button_coma));
        }

        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            //this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(Button0));
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
        //    if (e.Key == Key.Enter || e.Key == Key.Return)
        //    {
        //        e.Handled = true;
        //        this.disposeEvent(new GreenRetail.Framework.Core.EventHandlerImp.GRActionEvent(ButtonOK));
        //    }
        }

    }
}

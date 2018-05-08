
using GreenRetail.Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using WpfGym;
//using Forms = System.Windows.Forms;

//using GreenRetail.AutoService.Animo.Resources;
using System.Windows;
//using GreenRetail.Store.Model;

namespace GreenRetail.Trainer.PowerClub
{
    public class GreenRetailAddin : GreenRetail.Framework.Core.Addins_Manager.GRAddin, GreenRetail.Framework.Core.Event_Manager.IGREventHandler
    {
        private Bridge.POS.POSClient mPosClient = null;


        private Index index;
        public GreenRetailAddin(GreenRetailApplication app)
            : base(app)
        {
            index = new Index();
        }

        // Shutdown Rutines
        public override bool Shutdown()
        {
            return true;
        }


        // Addin Entry point
        public override bool Startup()
        {
            GreenRetailApplication app = (GreenRetailApplication)ApplicationContext;
            mPosClient = Bridge.POS.POSClient.Instance;

            app.AddinManager.addAddin(this.mPath);

            //MessageBox.Show("Startup Addins");

            GreenRetail.Bridge.GreenRetailApplication.Instance.onAuthenticated += Instance_onAuthenticated;
            return true;
        }

        private void Instance_onAuthenticated(object sender, AuthenticationEventArgs auth)
        {
           
            index.Show();

        }

        public bool doEvent(Framework.Core.Event_Manager.IGREvent evnt)
        {
            Shutdown();
            return true;
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace wsPresenceAgentStatus
{
    public partial class sPresenceAgentService : ServiceBase
    {

        private Timer Schedular;
        private Form1 myForm;
        private bool _bShouldIBeRunning;

        public sPresenceAgentService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            clLog.CreateLog();
            clLog.Log("PresenceAgentSerice is starting.");
            _bShouldIBeRunning = true;
            this.ScheduleService();

        }

        public void ScheduleService()
        {
            if (_bShouldIBeRunning == true)
            {
                Schedular = new Timer(new TimerCallback(SchedularCallback));

                Schedular.Change(30000, Timeout.Infinite);

            }
        }

        private void SchedularCallback(Object e)
        {

            //clLog.Log("PresenceAgentSerice SchedularCallback");

            Thread thread = new Thread(new ThreadStart(RunPresence));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            while (thread.IsAlive)
            {
                System.Windows.Forms.Application.DoEvents();
            }

            this.ScheduleService();

        }

        private void RunPresence()
        {

            myForm = new Form1();
            myForm.bShouldIBeRunning = true;
            myForm.GetData();

        }

        protected override void OnStop()
        {

            _bShouldIBeRunning = false;
            myForm.bShouldIBeRunning = false;

            while (myForm.bAmIRunning == true)
            {
                System.Threading.Thread.Sleep(10);
            }

            clLog.Log("PresenceAgentSerice is stoping.");

        }

    }
}

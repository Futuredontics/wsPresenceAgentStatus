using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wsPresenceAgentStatus
{
     public partial class Form1 : Form
    {

        private bool _bShouldIBeRunning = true;
        private bool _bAmIRunning = false;

        public bool bAmIRunning
        {
            get
            {
                return _bAmIRunning;
            }
            set
            {
                _bAmIRunning = value;
            }
        }

        public bool bShouldIBeRunning
        {
            get
            {
                return _bShouldIBeRunning;
            }
            set
            {
                _bShouldIBeRunning = value;
            }

        }

        public Form1()
        {
            InitializeComponent();
        }

        public void GetData()
        {

            _bAmIRunning = true;

            try
            {

                this.axPresenceInterfaceX1.LineActive = -1;
                this.axPresenceInterfaceX1.ContactCode = -1;
                this.axPresenceInterfaceX1.Phone2 = "930000000";
                this.axPresenceInterfaceX1.ScheduledDate = Convert.ToDateTime("1/1/2001 0:00:00");
                this.axPresenceInterfaceX1.Comments = "";
                this.axPresenceInterfaceX1.ContactName = "";
                this.axPresenceInterfaceX1.CaptureCall = -1;
                this.axPresenceInterfaceX1.CaptureCallDateLimit = Convert.ToDateTime("1/1/2001 0:00:00");
                this.axPresenceInterfaceX1.EMailOutFrom = "";
                this.axPresenceInterfaceX1.EMailOutTo = "";
                this.axPresenceInterfaceX1.EMailOutSubject = "";
                this.axPresenceInterfaceX1.EMailOutMessage = "";
                this.axPresenceInterfaceX1.DoubleBuffered = true;
                this.axPresenceInterfaceX1.Enabled = false;
                this.axPresenceInterfaceX1.Visible = false;
                this.axPresenceInterfaceX1.CtlCursor = 0;
                this.axPresenceInterfaceX1.AllowEndContact = true;


                this.axPresenceInterfaceX1.Active();
                this.axPresenceInterfaceX1.ConnectToAllServices();
                this.axPresenceInterfaceX1.LoginAgent("112188", "");

                //Wait for ActiveX to load
                System.Threading.Thread.Sleep(30000);

                using (SqlConnection oCn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FDLAXSQLBI01"].ConnectionString))
                {
                    if (oCn.State != ConnectionState.Open)
                    {
                        oCn.Open();
                    }

                    while (_bShouldIBeRunning == true)
                    {

                        int iCurrentRunSecond = DateTime.Now.Second;

                        if (this.axPresenceInterfaceX1.RequestServiceAgents(22) == true)
                        {
                            DataTable tblAgents = new DataTable();

                            tblAgents.Columns.Add("ServiceAgentLogin", Type.GetType("System.Int32"));
                            tblAgents.Columns.Add("ServiceAgentStatus", Type.GetType("System.String"));

                            for (int x = 0; x < this.axPresenceInterfaceX1.ServiceAgentsCount(); x++)
                            {
                                DataRow newRow = tblAgents.NewRow();

                                newRow["ServiceAgentLogin"] = Convert.ToInt32(this.axPresenceInterfaceX1.ServiceAgentsLogin2(x));

                                switch (this.axPresenceInterfaceX1.ServiceAgentsStatus(x))
                                {
                                    case 0:
                                        newRow["ServiceAgentStatus"] = "Stopped";
                                        break;

                                    case 1:
                                        newRow["ServiceAgentStatus"] = "Available";
                                        break;

                                    case 2:
                                        newRow["ServiceAgentStatus"] = "Servicing contact";
                                        break;

                                    case 3:
                                        newRow["ServiceAgentStatus"] = "ACW";
                                        break;

                                    case 4:
                                        newRow["ServiceAgentStatus"] = "Servicing another service";
                                        break;
                                    default:
                                        newRow["ServiceAgentStatus"] = "Unknown";
                                        break;
                                }

                                tblAgents.Rows.Add(newRow);

                            }

                            using (SqlCommand oCmd = new SqlCommand("[dbo].[CallCenter_AgentStatus_Insert2]", oCn))
                            {
                                oCmd.CommandType = CommandType.StoredProcedure;

                                SqlParameter sParaServiceAgentLogin = oCmd.Parameters.AddWithValue("@AgentValues", tblAgents);
                                sParaServiceAgentLogin.SqlDbType = SqlDbType.Structured;
                                sParaServiceAgentLogin.TypeName = "CallCenter_Agents";

                                try
                                {
                                    oCmd.ExecuteNonQuery();
                                }
                                catch (Exception e)
                                {
                                    clLog.Log(e.ToString());
                                }
                            }

                        }

                        while (iCurrentRunSecond == DateTime.Now.Second)
                        {
                            System.Threading.Thread.Sleep(100);
                        }

                        iCurrentRunSecond = DateTime.Now.Second;

                    }

                    if (oCn.State != ConnectionState.Closed)
                    {
                        oCn.Close();
                    }

                }

            }
            catch (Exception e)
            {
                clLog.Log("" + e.ToString());
            }

            this.axPresenceInterfaceX1.Close();
            _bAmIRunning = false;

        }
    }
}

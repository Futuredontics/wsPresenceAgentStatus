﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace wsPresenceAgentStatus
{
    class clLog
    {
        public static void CreateLog()
        {
            if (EventLog.SourceExists("PresenceAgentStatus") == false)
            {
                EventLog.CreateEventSource("PresenceAgentStatus", "PresenceAgentStatus");
            }

            System.Threading.Thread.Sleep(1000);

        }

        public static void Log(string sLogEntry)
        {

            EventLog elEvent = new EventLog();
            elEvent.Source = "PresenceAgentStatus";
            elEvent.WriteEntry(sLogEntry);
            elEvent.Dispose();

        }
    }
}

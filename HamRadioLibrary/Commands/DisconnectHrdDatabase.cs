using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Reflection;
using HamRadioDeluxeProperties;
using HamRadioDeluxeDatabaseLibrary;

namespace HrdDisConnectDatabase
{

    [Cmdlet(VerbsCommunications.Disconnect, "HrdDatabase")]
    [OutputType(typeof(string))]
    public class DisconnectHrdDatabase : Cmdlet
    {

        protected override void ProcessRecord()
        {
            WriteVerbose("Disconnecting Database.");
            HamRadioDeluxeDatabase.DisconnectFromDatabase();
            //WriteObject("jolo");
        }
    }

}

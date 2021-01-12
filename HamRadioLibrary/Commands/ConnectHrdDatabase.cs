using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Reflection;
using HamRadioDeluxeProperties;
using HamRadioDeluxeDatabaseLibrary;

namespace HrdConnectDatabase
{

    [Cmdlet(VerbsCommunications.Connect, "HrdDatabase")]
    [OutputType(typeof(string))]
    public class ConnectHrdDatabase : Cmdlet
    {

        [Parameter(Position = 0, Mandatory = true)]
        public string Server { get; set; }
        [Parameter(Position = 1, Mandatory = true)]
        public Int16 Port { get; set; }
        [Parameter(Position = 2, Mandatory = true)]
        public string User { get; set; }
        [Parameter(Position = 3, Mandatory = true)]
        public string Password { get; set; }
        [Parameter(Position = 4, Mandatory = true)]
        public string Database { get; set; }

        protected override void ProcessRecord()
        {
            WriteVerbose("Start Opening Database.");

            if (HamRadioDeluxeDatabase.CheckDatabaseOpen())
            {
                WriteVerbose("Closing database, because it was open.");
                HamRadioDeluxeDatabase.DisconnectFromDatabase();
            }

            if (!HamRadioDeluxeDatabase.ConnectToDatabase(Server, Port, User, Password, Database))
            {
                WriteVerbose("Database open, checking for existing hrd table.");
                try
                {
                    if (!HamRadioDeluxeDatabase.ExistsTableHrdContacts())
                    {
                        WriteVerbose("Hrd table not found.");
                        throw new TABLEHRDCONTACTSException("Could not find table : TABLE_HRD_CONTACTS_V01");
                    }
                }
                catch (TABLEHRDCONTACTSException ex)
                {
                    WriteError(new ErrorRecord(ex, "Hrd Database Disconnected",
                                 ErrorCategory.ObjectNotFound, "TABLE_HRD_CONTACTS_V01"));
                    HamRadioDeluxeDatabase.DisconnectFromDatabase();
                }
            }
        }
    }

    public class TABLEHRDCONTACTSException : Exception
    {
        public TABLEHRDCONTACTSException(string message)
            : base(message)
        {
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Reflection;
using HamRadioDeluxeProperties;
using HamRadioDeluxeDatabaseLibrary;

namespace HrdDatabaseInfo
{

    [Cmdlet(VerbsCommon.Get, "HrdDatabaseInfo")]
    [OutputType(typeof(string))]
    public class GetHrdDatabaseInfo : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxeDatabase.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }

            WriteVerbose("Show database info.");
            HamRadioDeluxeDatabase.DatabaseInfo();

            StringBuilder sb = new StringBuilder();

            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var modulename = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            sb.AppendLine("Module name (dll) : " + modulename);
            sb.Append("Module version    : " + version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision);

            //if (version.Build > 0)
            //{
            //    sb.Append(" [patch " + version.Build + "]");
            //    sb.Append("Revision " + version.Revision);
            //    sb.Append(version.Build + "." + version.Revision);
            //}
            sb.AppendLine();

            Console.WriteLine();
            Console.Write(sb.ToString());
            //WriteObject("jolo");
        }
    }

}

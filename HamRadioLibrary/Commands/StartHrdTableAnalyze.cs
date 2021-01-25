using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Reflection;
using HamRadioDeluxeProperties;
using HamRadioDeluxeDatabaseLibrary;

namespace HrdTableAnalyze
{

        [Cmdlet(VerbsLifecycle.Start, "HrdTableAnalyze")]
    [OutputType(typeof(string))]
    public class StartHrdTableAnalyze : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxeDatabase.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            WriteVerbose("Start Analyze Database.");
            HamRadioDeluxeDatabase.AnalyzeHrdTable();
        }
    }

}

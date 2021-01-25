using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Reflection;
using HamRadioDeluxeProperties;
using HamRadioDeluxeDatabaseLibrary;

namespace HrdReportMode
{

    [Cmdlet(VerbsCommon.Get, "HrdReportMode")]
    [OutputType(typeof(string))]
    public class GetHrdReportMode : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxeDatabase.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            WriteVerbose("Fill the list.");
            HamRadioDeluxeDatabase.ReportMode();

            WriteVerbose("Reading list.");
            foreach (HRDProperties.ReportModeObjects ModeList in HamRadioDeluxeDatabase.HrdModeList)
            {
                WriteObject(new HRDProperties.ReportModeObjects
                {
                    MODE = ModeList.MODE,
                    SUBMODE = ModeList.SUBMODE,
                    WORKED = ModeList.WORKED
                });
            }
        }
    }


}

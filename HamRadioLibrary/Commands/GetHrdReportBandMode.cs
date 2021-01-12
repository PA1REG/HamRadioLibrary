using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Reflection;
using HamRadioDeluxeProperties;
using HamRadioDeluxeDatabaseLibrary;

namespace HrdReportBandMode
{

    [Cmdlet(VerbsCommon.Get, "HrdReportBandMode")]
    [OutputType(typeof(string))]
    public class GetHrdReportBandMode : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxeDatabase.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            WriteVerbose("Fill the list.");
            HamRadioDeluxeDatabase.ReportModeBand();

            WriteVerbose("Reading list.");
            foreach (HRDProperties.ReportBandModeObjects BandModeList in HamRadioDeluxeDatabase.HrdBandModeList)
            {
                WriteObject(new HRDProperties.ReportBandModeObjects
                {
                    BAND = BandModeList.BAND,
                    MODE = BandModeList.MODE,
                    SUBMODE = BandModeList.SUBMODE,
                    WORKED = BandModeList.WORKED
                });
            }
        }
    }


}

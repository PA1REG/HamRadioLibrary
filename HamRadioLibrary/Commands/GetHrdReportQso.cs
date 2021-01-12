using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Reflection;
using HamRadioDeluxeProperties;
using HamRadioDeluxeDatabaseLibrary;

namespace HrdReportQso
{

    [Cmdlet(VerbsCommon.Get, "HrdReportQso")]
    [OutputType(typeof(string))]
    public class GetHrdReportQso : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxeDatabase.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            WriteVerbose("Fill the list.");
            HamRadioDeluxeDatabase.ReportQso();

            WriteVerbose("Reading list.");
            foreach (HRDProperties.ReportQsoObjects QsoList in HamRadioDeluxeDatabase.HrdQsoList)
            {
                WriteObject(new HRDProperties.ReportQsoObjects
                {
                    WORKED = QsoList.WORKED,
                    OM = QsoList.OM,
                    SWL = QsoList.SWL
                });
            }
        }
    }



}

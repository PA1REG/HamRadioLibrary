using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using Microsoft.PowerShell.Commands;
using CsvLibrary;

namespace CsvCmdLets
{


    [Cmdlet(VerbsData.Convert, "CsvToAdif")]
    [OutputType(typeof(string))]
    public class GetAdifFileCmdlets : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string CsvFileName { get; set; }
        [Parameter(Position = 1, Mandatory = true)]
        public string AdifFileName { get; set; }
        [Parameter(Position = 2, Mandatory = false)]
        public SwitchParameter Swl { get; set; }
        [Parameter(Position = 3, Mandatory = false)]
        public SwitchParameter QslMsgCopyToComment { get; set; }
        [Parameter(Position = 4, Mandatory = false)]
        public SwitchParameter AllFields { get; set; }
 //       [Parameter(Position = 5, Mandatory = false)]
 //       public SwitchParameter ShowAllFields { get; set; }


        protected override void ProcessRecord()
        {
            //Boolean result = false;

            WriteVerbose("Input Csvfilename : " + CsvFileName);
            SessionState ss = new SessionState();
            Directory.SetCurrentDirectory(ss.Path.CurrentFileSystemLocation.Path);
            CsvFileName = Path.GetFullPath(CsvFileName);
            WriteVerbose("Output (Full) CsvFilename : " + CsvFileName);

            WriteVerbose("Input Adiffilename : " + AdifFileName);
            AdifFileName = Path.GetFullPath(AdifFileName);
            WriteVerbose("Output (Full) AdifFilename : " + AdifFileName);

            WriteVerbose("Check Csv file exists.");
            if (!File.Exists(CsvFileName))
            {
                WriteWarning("File not found : " + CsvFileName);
                return;
            }
            WriteVerbose("Reading and fill the list.");

            bool SwlStation = false;
            if (Swl)
            {
                SwlStation = true;
            }
            bool QslMsgComment = false;
            if (QslMsgCopyToComment)
            {
                QslMsgComment = true;
            }
            bool ShowAllFields = false;
            if (AllFields)
            {
                ShowAllFields = true;
            }

            CsvUtils.CsvReaderQso(CsvFileName, SwlStation, QslMsgComment, ShowAllFields);
            CsvUtils.AdiWriteQso(AdifFileName);

        }
    }

}


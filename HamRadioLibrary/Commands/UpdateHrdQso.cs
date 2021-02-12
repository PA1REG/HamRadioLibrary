using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Reflection;
using HamRadioDeluxeProperties;
using HamRadioDeluxeDatabaseLibrary;

namespace HrdQso
{

    [Cmdlet(VerbsData.Update, "HrdQso")]
    [OutputType(typeof(string))]
    public class UpdateHrdQso : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string Key { get; set; }
        [Parameter(Position = 1, Mandatory = true)]
        public string Operator { get; set; }
        [Parameter(Position = 2, Mandatory = false)]
        public SwitchParameter TitleCase { get; set; }

        protected override void BeginProcessing()
        {
            //_output = true;
        }


        Int32 RowsCounter = 0;
        protected override void ProcessRecord()
        {
            // Get-HrdSearch -Call pa3djy | Update-HrdQso
            if (!HamRadioDeluxeDatabase.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            Boolean ToTitleCase = false;
            if (TitleCase)
            {
                ToTitleCase = true;
            }
            WriteVerbose("Start Update Qso.");
            RowsCounter= RowsCounter+ HamRadioDeluxeDatabase.UpdateHrdQso(Key, Operator, ToTitleCase);
        }
        protected override void EndProcessing()
        {
            WriteObject(RowsCounter+" Qso's updated.");
        }
    }


    //[CmdletBinding(DefaultParameterSetName = "ViaPipeline", PositionalBinding =$false)]
    //param(
    // [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName=$true, DontShow=$true, ParameterSetName="ViaPipeline")]
    //    [string] $Key,

    // [Parameter(Mandatory=$false, Position = 0 )]
    //    [string[]] $name

}

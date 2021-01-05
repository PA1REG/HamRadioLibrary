using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using Microsoft.PowerShell.Commands;


namespace TestCmdLets
{



    //[Cmdlet(VerbsCommon.Get, "HrdTestFile")]
    //[OutputType(typeof(string))]
    //public class GethrdTestFileCmdlets : Cmdlet
    //{
    //    [Parameter(Position = 0, Mandatory = false)]
    //    public string FileName { get; set; }
    //    protected override void ProcessRecord()
    //    {
    //        WriteWarning("This a TEST cmdlet for development usage.");
    //        WriteWarning("Nothing here.....");




    //        //string current = Directory.GetCurrentDirectory();
    //        //WriteWarning(current);

    //        //SessionState ss = new SessionState();
    //        //Directory.SetCurrentDirectory(ss.Path.CurrentFileSystemLocation.Path);
    //        //string LiteralPath = FileName;
    //        //LiteralPath = Path.GetFullPath(LiteralPath);
    //        //WriteWarning(LiteralPath);


    //        //Boolean result = false;

    //        //            foreach ($curPath in ($Path | Where - Object {$_} | Resolve - Path | Convert - Path)) {
    //        //# test wether the input is a file
    //        //                if (Test - Path $curPath - PathType Leaf) {


    //        // $filePath = $(Resolve-Path $FileName).path
    //        //$Query =  [System.IO.File]::ReadAllText("$filePath")

    //        //     string path = SessionState.Path.GetUnresolvedProviderPathFromPSPath(FileName);
    //        //string t = getun


    //        //if (!File.Exists(FileName))
    //        //{
    //        //    WriteWarning("File not found : " + FileName);
    //        //    return;
    //        //}
    //    }
    //}

    //[Cmdlet(VerbsCommon.Get, "HrdTestPipeline")]
    //[OutputType(typeof(string))]
    //public class GethrdTestPipelineCmdlets : Cmdlet
    //{
    //    [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
    //    public object PipeLine { get; set; }
    //    protected override void ProcessRecord()
    //    {
    //        //WriteWarning("This a TEST cmdlet for development usage.");
    //        //WriteWarning("Nothing here.....");

    //        // Perform various Foo-related activities...

    //        Object pipelineObject = PipeLine.ToString();
    //        string p = PipeLine.ToString();

    //        WriteObject(pipelineObject);
    //        WriteObject(p);

    //        //foreach (var q in PipeLine)
    //        //{
    //        //    WriteObject(q);

    //        //}
    //    }

    //}






}



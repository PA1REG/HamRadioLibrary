$ErrorActionPreference = "Stop"

#Import-Module $PSScriptRoot/HamRadioLibrary.dll

function Start-HrdQsoCallBandMode {
  [CmdletBinding()]
  param
  (
    [Parameter(Position = 0, Mandatory = $true)]
    [String] $AdifFile,

    [Parameter(Position = 1, Mandatory = $false)]
    [String] $CsvFileOmContacts,

    [Parameter(Position = 2, Mandatory = $false)]
    [String] $AdifFileOmContacts
  )
  begin {
    Write-Verbose "Start Module : [$($MyInvocation.MyCommand)] *************************************"


    if (!$CsvFileOmContacts ) {
      $CsvFileOmContacts = ".\OmContacts.csv"
    }

    if (!$AdifFileOmContacts ) {
      $AdifFileOmContacts = ".\OmContacts.adi"
    }
    
    Write-Warning("Starting analyze, during analyse table statistics there is a lock (no read/write allowed) on the HrdLogbook table.")
    Start-HrdTableOptimize
    Write-Host("")

    Write-Host("Reading ADIF file : ""$($AdifFile)"", wait .............") -ForegroundColor Yellow
    $AdifRecords = Get-AdiReadFile -FileName $AdifFile
    $OmContacts = $AdifRecords
    Write-Host("Start comparing OM records with the HrdLogbook database") -ForegroundColor Yellow
    Write-Host("")

    $Id = 1
    $Activity = "Comparing records with the HrdLogbook database"
    $StatusBlock = "Reading ADIF file : $AdifFile"
    $RecordCounter = 0
    $TotalOmContacts = $OmContacts.Count

    $StartTime = Get-Date

    $AdifNotFoundArray = @()
    foreach ($OmContact in $OmContacts) { 
      Write-Progress -Id $Id -Activity $Activity -Status ($StatusBlock) -PercentComplete ($RecordCounter / $TotalOmContacts * 100)
      $RecordCounter++
      Write-Host("Doing: $($OmContact.Call)")
      $OmRecord = Get-HrdSearch -Call $OmContact.Call -Band $OmContact.Band -Mode $OmContact.Mode 
      if ($OmRecord) {
      }
      else {
        Write-Host("NOT Found : $($OmContact.Call)") -ForegroundColor Red
        $AdifNotFoundArray += $OmContact
      }
    }

    Write-Progress -Id $Id -Activity $Activity -Status "Ready" -Completed
    #Disconnect-HrdDatabase

    Write-Host("")
    Write-Host("Readed ADIF records : $($TotalAdifRecords)") -ForegroundColor Yellow
    Write-Host("Writed ADIF records : $($AdifNotFoundArray.Count)") -ForegroundColor Yellow
    Write-Host "Completed in $((Get-Date).Subtract($StartTime).Minutes) minute(s) and $((Get-Date).Subtract($StartTime).Seconds) second(s)"  -ForegroundColor Green  
    Write-Host("")
 
    Write-Host("Writing CSV file : ""$($CsvFileOmContacts)""") -ForegroundColor Yellow
    $AdifNotFoundArray | Export-Csv -Path $CsvFileOmContacts -Encoding ASCII -NoTypeInformation
    Write-Host("Writing ADIF file : ""$($AdifFileOmContacts)""") -ForegroundColor Yellow
    Convert-CsvToAdif -CsvFileName $CsvFileOmContacts -AdifFileName $AdifFileOmContacts -Swl -QslMsgCopyToComment

    Write-Verbose "End Module  : [$($MyInvocation.MyCommand)] *************************************"
  }
}


function Start-HrdSwlContacts {
  [CmdletBinding()]
  param
  (
    [Parameter(Position = 0, Mandatory = $true)]
    [String] $AdifFile,

    [Parameter(Position = 1, Mandatory = $false)]
    [String] $CsvFileSwlContacts,

    [Parameter(Position = 2, Mandatory = $false)]
    [String] $AdifFileSwlContacts
  )
  begin {
    Write-Verbose "Start Module : [$($MyInvocation.MyCommand)] *************************************"


    if (!$CsvFileSwlContacts ) {
      $CsvFileSwlContacts = ".\SwlContacts.csv"
    }

    if (!$AdifFileSwlContacts ) {
      $AdifFileSwlContacts = ".\SwlContacts.adi"
    }
    
    Write-Warning("Starting analyze, during analyse table statistics there is a lock (no read/write allowed) on the HrdLogbook table.")
    Start-HrdTableOptimize
    Write-Host("")

    Write-Host("Reading ADIF file : ""$($AdifFile)"", wait .............") -ForegroundColor Yellow
    $AdifRecords = Get-AdiReadFile -FileName $AdifFile
    $SwlContacts = $AdifRecords | Where-Object { $PSItem.QSLMSG -like "*wkd*" -or $PSItem.QSLMSG -like "*working*" -or $PSItem.QSLMSG -like "*wsl*" -or $PSItem.QSLMSG -like "*heard*" -or $PSItem.QSLMSG -like "*report*" }
    Write-Host("Start comparing SWL records with the HrdLogbook database") -ForegroundColor Yellow
    Write-Host("")

    $Id = 1
    $Activity = "Comparing records with the HrdLogbook database"
    $StatusBlock = "Reading ADIF file : $AdifFile"
    $RecordCounter = 0
    $TotalSwlContacts = $SwlContacts.Count

    $StartTime = Get-Date

    $AdifNotFoundArray = @()
    foreach ($SwlContact in $SwlContacts) { 
      Write-Progress -Id $Id -Activity $Activity -Status ($StatusBlock) -PercentComplete ($RecordCounter / $TotalSwlContacts * 100)
      $RecordCounter++
      Write-Host("Doing: $($SwlContact.Call)")
      $SwlRecord = Get-HrdSearch -Call $SwlContact.Call -Band $SwlContact.Band -Mode $SwlContact.Mode -Date $SwlContact.QsoDate -IncludeSwl
      if ($SwlRecord) {
      }
      else {
        Write-Host("NOT Found : $($SwlContact.Call)") -ForegroundColor Red
        $AdifNotFoundArray += $SwlContact
      }
    }

    Write-Progress -Id $Id -Activity $Activity -Status "Ready" -Completed
    #Disconnect-HrdDatabase

    Write-Host("")
    Write-Host("Readed ADIF records : $($TotalAdifRecords)") -ForegroundColor Yellow
    Write-Host("Writed ADIF records : $($AdifNotFoundArray.Count)") -ForegroundColor Yellow
    Write-Host "Completed in $((Get-Date).Subtract($StartTime).Minutes) minute(s) and $((Get-Date).Subtract($StartTime).Seconds) second(s)"  -ForegroundColor Green  
    Write-Host("")
 
    Write-Host("Writing CSV file : ""$($CsvFileSwlContacts)""") -ForegroundColor Yellow
    $AdifNotFoundArray | Export-Csv -Path $CsvFileSwlContacts -Encoding ASCII -NoTypeInformation
    Write-Host("Writing ADIF file : ""$($AdifFileSwlContacts)""") -ForegroundColor Yellow
    Convert-CsvToAdif -CsvFileName $CsvFileSwlContacts -AdifFileName $AdifFileSwlContacts -Swl -QslMsgCopyToComment

    Write-Verbose "End Module  : [$($MyInvocation.MyCommand)] *************************************"
  }
}



function Get-XlsReportQsoYearMode {
  [CmdletBinding()]
  param
  (
    [Parameter(Position = 0, Mandatory = $true)]
    [String] $ExcelFile,
    [Parameter(Position = 1, Mandatory = $false)]
    [Switch] $AutoOpen
 
  )
  begin {
    Write-Verbose "Start Module : [$($MyInvocation.MyCommand)] *************************************"
    
    $ModuleName = "ImportExcel"
    $ModuleExcel = Get-Module -Name $ModuleName
    if ($ModuleExcel) {
    }
    else {
      Write-Warning("Module NOT installed, run as Administrator first : Install-Module -Name ImportExcel and Import-Module -Name ImportExcel -Force") 
      Break
    }

    $Report = Get-HrdReportQsoYearMode
    $Report | Export-Excel -Path $ExcelFile
    Write-Host("Writing Excel file : ""$($ExcelFile)""") -ForegroundColor Yellow
  
    if ($AutoOpen) {
      # start Excel
      $excel = New-Object -comobject Excel.Application

      #open file
     
      $FilePath =  (Get-ChildItem -Path $ExcelFile).FullName 
      $Workbook = $Excel.Workbooks.Open($FilePath)

      #make it visible (just to check what is happening)
      $Excel.Visible = $true
   
    }

    Write-Verbose "End Module  : [$($MyInvocation.MyCommand)] *************************************"
  }
}

Export-ModuleMember -function Start-HrdQsoCallBandMode
Export-ModuleMember -function Start-HrdSwlContacts
Export-ModuleMember -function Get-XlsReportQsoYearMode

Export-ModuleMember -function Start-HrdQsoCallBandMode
Export-ModuleMember -function Start-HrdSwlContacts


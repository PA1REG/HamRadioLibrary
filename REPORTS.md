# Examples handling Reports:

 
## Report to .CSV file

```PowerShell
$rpt = Get-HrdReportQsoYearMode
$rpt | Export-Csv -Path ".\QsoReport.csv" -Encoding ASCII -NoTypeInformation
```

## Report to .TXT file

```PowerShell
$rpt = Get-HrdReportQsoYearMode
$rpt | Out-File -FilePath ".\QsoReport.txt"
```


## Report to .HTML file

```PowerShell
$rpt = Get-HrdReportQsoYearMode
$rpt | ConvertTo-Html | Out-File -FilePath ".\QsoReport.htm"
```

## Report to .XLSX file

### Install module ImportExcel (One time):

```PowerShell
Install-Module -Name ImportExcel
```

### Report:

```PowerShell
$rpt = Get-HrdReportQsoYearMode
$rpt | Export-Excel -Path ".\QsoReport.xlsx"
```


Return to [readme](./README.md).

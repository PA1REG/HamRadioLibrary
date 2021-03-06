# Examples for the HamRadioLibrary:


## Examples 1

Connect to the Ham Radio Deluxe database:

```PowerShell
Connect-HrdDatabase -Server localhost -Port 3306 -User PA1REG -Password 'MyScretPassword' -Database PA1REG
```

Search call OP4K:

```PowerShell
Get-HrdSearch -Call op4k
```

Search for calls starting with pa1:

```powershell
Get-HrdSearch -Call pa1*
```

Search for calls starting with pa1 and list all columns with autosize:

```powershell
Get-HrdSearch -Call pa1* | Format-Table -AutoSize
```

Compare ADI with database:

```powershell
Start-HrdQsoCallBandMode -AdifFile .\PA1REG_ADIF.adi -CsvFileOmContacts .\Found.csv -AdifFileOmContacts .\Found.adi
``` 

## Examples 2

Write result into variable pa1calls:
Show all calls which Mode=FT8 from variable pa1calls:

```powershell
$pa1calls = Get-HrdSearch -Call pa1*
$pa1calls | Where-Object { $PSItem.Mode -eq "FT8" }
``` 

Show all calls which Mode=FT8 and sort on freq:

```powershell
Get-HrdSearch -Call pa1* | Where-Object { $PSItem.Mode -eq "FT8" } | Sort-Object Freq
``` 


### EXAMPLE 3

Search for all Qso's from OP4K.
Show results.
Update selected calls with the operators correct name and make the first letters of the names capital.

```powershell
$Calls = Get-HrdSearch -Call OP4K 
$Calls
$Calls | Update-HrdQso -Operator "joe ziegelshiffer" -TitleCase
```

Run the first 2 commands, see the results and then run the last command.


Return to [readme](./README.md).

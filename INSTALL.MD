### How To Install, Update and Remove HamRadioLibrary:

 
## Installation using the [PowerShell Gallery](https://www.powershellgallery.com)

Open PowerShell as Administrator!

You can run the following commands to check the PowerShell cmdlets releases:

```PowerShell
Find-Module HamRadioLibrary -AllowPrerelease
```

You can run the following commands to install the PowerShell cmdlets:

```PowerShell
Install-Module HamRadioLibrary -AllowPrerelease
```

You can check the installed HamRadioLibrary version with the following command:

```powershell
Get-Module HamRadioLibrary -ListAvailable | Select-Object Name, Version | Sort-Object Version -Descending
```

# Importing module

```powershell
Import-Module -Name HamRadioLibrary -Force -AllowPrerelease
``` 

# Getting all available commands in the module

```powershell
Get-Command -Module HamRadioLibrary
``` 

# Installation Example

![alt text](https://github.com/PA1REG/HamRadioLibrary/blob/Reorganize/Screenshots/Installation.PNG)



## Update using the [PowerShell Gallery](https://www.powershellgallery.com)

To update the current installation:

```powershell
Install-Module HamRadioLibrary -Force -AllowPrerelease
``` 

# Update Example

![alt text](https://github.com/PA1REG/HamRadioLibrary/blob/Reorganize/Screenshots/Update.PNG)


## Remove module HamRadioLibrary. 

Uninstall the module:

```powershell
Uninstall-Module HamRadioLibrary -Force -AllVersions
``` 


Return to [readme](../README.md).

# How To Install, Update and Remove HamRadioLibrary:

 
 ## Installation HamRadioLibrary. 

Open PowerShell as Administrator!
Type : Find-Module HamRadioLibrary
( First time use asks for installing "NuGet provider", accept with "Y" )
Type : Install-Module HamRadioLibrary
Accept "Untrusted repository" with "Y"
Check if module is succesfully installed
Type : Get-Module -Name HamRadioLibrary -ListAvailable
Import module
Type : Import-Module -Name HamRadioLibrary -Force
Check if the commands are available
Type : Get-Command -Module HamRadioLibrary

![alt text](https://github.com/PA1REG/HamRadioLibrary/blob/Reorganize/Screenshots/Installation.PNG)


 ## Update new version of HamRadioLibrary. 

Open PowerShell as Administrator!
Check current version
Get-Module -Name HamRadioLibrary -ListAvailable
Check if new version is available
Find-Module HamRadioLibrary
New version, then
Type : Install-Module HamRadioLibrary -Force
Accept "Untrusted repository" with "Y"
Check if module is succesfully installed
Type : Get-Module -Name HamRadioLibrary -ListAvailable
Import module
Type : Import-Module -Name HamRadioLibrary -Force
Check if the commands are available
Type : Get-Command -Module HamRadioLibrary


![alt text](https://github.com/PA1REG/HamRadioLibrary/blob/Reorganize/Screenshots/Update.PNG)


## Remove module HamRadioLibrary. 

Uninstall the module.
Uninstall-Module HamRadioLibrary -Force -AllVersions


Return to [readme](../README.md).

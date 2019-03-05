# This is needed to do anything on your machine
# Allows you to run powershell
# only needs to happen once
Set-ExecutionPolicy UnRestricted

# Install PnP Powershell
# https://github.com/SharePoint/PnP-PowerShell
Install-Module SharePointPnPPowerShellOnline

# To Update it
Update-Module SharePointPnPPowerShellOnline

# connect to sharepoint online
Connect-PnPOnline -Url https://folkis2018.sharepoint.com/sites/David -Credentials folkis2018

Get-PnPWeb

$userField = Get-PnPField -Identity xUser

$userField.SchemaXml

Connect-PnPOnline -Url https://folkis2018.sharepoint.com/sites/David/ -Credentials folkis2018

Set-PnPTraceLog -On -Debug

Apply-PnPProvisioningTemplate -Path template.xml 
Apply-PnPProvisioningTemplate -Path taxonomy.xml 

Remove-PnPList -Identity "Fun List" -Force
Remove-PnPList -Identity "Lookup List" -Force


Connect-PnPOnline -Url https://folkis2018.sharepoint.com/sites/David/ -Credentials folkis


Add-PnPCustomAction -Title "ext4" -Name "ext4" -RegistrationType List -RegistrationId "100" -Location "ClientSideExtension.ListViewCommandSet.CommandBar" -ClientSideComponentId 83d668c7-a02c-43b7-b8e4-044395250117


Add-PnPSiteCollectionAppCatalog -Site "https://folkis2018.sharepoint.com/sites/David"

get-pnpcustomaction -Scope Site

Remove-PnPCustomAction -Identity 3124b5b2-ea4c-41c4-b5b2-32519555a56f -Scope Site

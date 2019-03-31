
Connect-PnPOnline -Url https://folkis2018-admin.sharepoint.com -Credentials folkis2018

$tsite = Get-PnPTenantSite -Url https://folkis2018.sharepoint.com/sites/David
$tsite.DenyAddAndCustomizePages = 1
$tsite.Update()
Invoke-PnPQuery

Connect-PnPOnline -Url https://folkis2018.sharepoint.com/sites/David -Credentials folkis2018

Get-PnPPropertyBag


Set-PnPPropertyBagValue -Key "foo" -Value "bar"
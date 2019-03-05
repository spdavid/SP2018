Connect-PnPOnline -Url https://folkis2018.sharepoint.com/sites/David -Credentials folkis2018

$userField = Get-PnPField -Identity Folk_Approver

$userField.SchemaXml | clip.exe # clip.exe copies the xml to your clipboard

$list = Get-PnPList -Identity "Fun List"

$view = Get-PnPView -List $list -Identity "All Items"

$ctx = Get-PnPContext

$ctx.Load($view)
$ctx.ExecuteQuery()

$view.ListViewXml


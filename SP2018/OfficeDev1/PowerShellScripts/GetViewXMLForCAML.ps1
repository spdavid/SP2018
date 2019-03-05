Connect-PnPOnline -Url https://folkis2018.sharepoint.com/sites/David -Credentials folkis2018



$view = Get-PnPView -List "Cars" -Identity "GreenCars"

$ctx = Get-PnPContext

$ctx.Load($view);
$ctx.ExecuteQuery()


$view.ListViewXml
$view.ListViewXml | clip.exe


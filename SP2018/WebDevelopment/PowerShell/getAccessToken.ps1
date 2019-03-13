$url = 'https://folkis2018.sharepoint.com/sites/David/'
$certPath = 'c:\folkis2018.local.pfx'
$password = 'p@ssw0rd'
$clientId = '4ec29f20-e269-405d-a2de-674fa10d095c'
$tenant = 'folkis2018.onmicrosoft.com'

$securestring = ConvertTo-SecureString $password -AsPlainText -Force
connect-pnpOnline -Url $url -CertificatePath $certPath -CertificatePassword $securestring  -ClientId $clientId -Tenant $tenant 

Get-PnPAppAuthAccessToken | clip.exe




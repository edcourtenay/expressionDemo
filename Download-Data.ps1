
$dataPath = Join-Path $PSScriptRoot "data"

if (Test-Path $dataPath) {
    Remove-Item $dataPath -Force -Recurse
}

New-Item $dataPath -ItemType Directory

function Get-Data([String] $name) {
    $url = "http://download.geonames.org/export/dump/{0}.zip" -f $name
    $tempFile = "{0}.tmp.zip" -f $name

    "Downloading to $tempFile"
    Invoke-WebRequest $url -OutFile $tempFile

    "Extracting file to data folder"
    Expand-Archive -Path $tempFile -DestinationPath $dataPath

    "Removing temporary file"
    Remove-Item $tempFile
}

Get-Data("cities1000")
Get-Data("cities5000")
Get-Data("cities15000")
Get-Data("allCountries")

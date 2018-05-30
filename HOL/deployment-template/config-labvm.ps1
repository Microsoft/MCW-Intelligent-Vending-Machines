#Fix RDP Issue
$HKLM = "HKLM:\SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp"
Set-ItemProperty -Path $HKLM -Name "SecurityLayer" -Value 0

#Enable Windows Subsystem for Linux (WSL)
Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Windows-Subsystem-Linux
Invoke-WebRequest -Uri https://aka.ms/wsl-ubuntu-1604 -Outfile ~/Ubuntu.zip -UseBasicParsing
Expand-Archive ~/Ubuntu.zip ~/Ubuntu
#Invoke-Expression -Command:'ubuntu.exe'

#Install Chocolatey
Set-ExecutionPolicy Bypass -Scope Process -Force; Invoke-Expression ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))

#Install Visual Studio Community 2017 with packages
choco install 'visualstudio2017community --add Microsoft.VisualStudio.Workload.DataScience --add Microsoft.VisualStudio.Workload.Azure --add Microsoft.VisualStudio.Workload.ManagedDesktop --add Microsoft.VisualStudio.Workload.NetWeb --includeRecommended --quiet --norestart'

#Assign Packages to Install
$Packages = 'googlechrome',`
            'powerbi'
#            'visualstudio2017-workload-azure',`
#            'visualstudio2017-workload-manageddesktop',`
#            'visualstudio2017-workload-netweb',`
#            'dotnetcore-sdk'

#Install Packages
ForEach ($PackageName in $Packages)
{choco install $PackageName -y}

#Update Visual Studio
$command1 = @'
cmd.exe /C C:\Users\demouser\AppData\Local\Temp\chocolatey\visualstudio2017community\15.2.26430.20170605\vs_community.exe --update --quiet --wait
'@
$command2 = @'
cmd.exe /C C:\Users\demouser\AppData\Local\Temp\chocolatey\visualstudio2017community\15.2.26430.20170605\vs_community.exe --update --quiet --wait --passive --norestart --installPath `
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community"
'@
Set-ExecutionPolicy Bypass -Scope Process -Force
Invoke-Expression -Command:$command1
Invoke-Expression -Command:$command2

#Install RTVS
Invoke-WebRequest -Uri https://aka.ms/rtvs-current -OutFile ~/rtools.exe
Invoke-Expression -Command:'rtools.exe'

#Reboot
Restart-Computer
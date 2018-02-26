@ECHO OFF
SET InstallPath=D:\PKSWebSites\PKS.MgmtServices.Host
"%InstallPath%\PKS.MgmtServices.Host.exe" install
net start PKS_MgmtServices_Host

PAUSE

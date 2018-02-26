@ECHO OFF

cd /d "%~dp0"
call ..\InitVariables.bat

SET SourcePath=%PKSInstallPath%\WebSites\PKS.MgmtServices.Host
SET TargetPath=%PKSWebSitesPath%\PKS.MgmtServices.Host

net stop PKS_MgmtServices_Host

robocopy "%SourcePath%" "%TargetPath%" /S /SL

net start PKS_MgmtServices_Host

PAUSE

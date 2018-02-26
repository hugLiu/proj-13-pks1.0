@ECHO OFF
SET PKSWebSitesPath=D:\PKSWebSites

IF NOT EXIST "%PKSWebSitesPath%" md "%PKSWebSitesPath%"

IF NOT EXIST "%PKSWebSitesPath%\PKS.MgmtServices.Host" md "%PKSWebSitesPath%\PKS.MgmtServices.Host"
IF NOT EXIST "%PKSWebSitesPath%\PKS.Portal" md "%PKSWebSitesPath%\PKS.Portal"
IF NOT EXIST "%PKSWebSitesPath%\PKS.PortalMgmt" md "%PKSWebSitesPath%\PKS.PortalMgmt"
IF NOT EXIST "%PKSWebSitesPath%\PKS.Sooil" md "%PKSWebSitesPath%\PKS.Sooil"
IF NOT EXIST "%PKSWebSitesPath%\PKS.SZXT" md "%PKSWebSitesPath%\PKS.SZXT"
IF NOT EXIST "%PKSWebSitesPath%\PKS.WebAPI" md "%PKSWebSitesPath%\PKS.WebAPI"

pause

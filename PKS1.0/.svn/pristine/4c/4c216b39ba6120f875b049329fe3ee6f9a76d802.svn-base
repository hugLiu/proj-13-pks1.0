@ECHO OFF

cd /d "%~dp0%"
call ..\InitVariables.bat

REM ECHO 编译
REM "%DevenvPath%\devenv.exe" "%SolutionPath%..\..\..\PKS1.0.sln"

ECHO 发布管理服务宿主
SET OutputPath=..\..\..\05PKS.MgmtTools\PKS.MgmtServices\PKS.MgmtServices.Host\Bin\Debug
SET TargetPath=%PKSWebSitesPath%\PKS.MgmtServices.Host
IF NOT EXIST %TargetPath% MD %TargetPath%
robocopy "%OutputPath%" "%TargetPath%" /S /SL
del /Q "%TargetPath%\*.xml"
del /Q "%TargetPath%\*.config"
SET OutputPath=..\..\..\09PKS.Install\PKS.Install\WebSites\PKS.MgmtServices.Host
robocopy "%OutputPath%" "%TargetPath%" /S /SL

REM ECHO 复制Web.Config
REM xcopy . "%PKSWebSitesPath%" /s /y

PAUSE

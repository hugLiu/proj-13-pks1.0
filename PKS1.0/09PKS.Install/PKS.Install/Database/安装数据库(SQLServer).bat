@ECHO OFF
cd /d "%~dp0%"
call "..\InitVariables.bat"

"%SQLServerToolsPath%\sqlcmd.exe" -i "创建框架库(SQLServer).sql"
"%SQLServerToolsPath%\sqlcmd.exe" -i "创建PKS库(SQLServer).sql"

pause

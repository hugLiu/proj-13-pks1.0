@ECHO OFF
cd /d "%~dp0%"
call "..\InitVariables.bat"

"%SQLServerToolsPath%\sqlcmd.exe" -i "������ܿ�(SQLServer).sql"
"%SQLServerToolsPath%\sqlcmd.exe" -i "����PKS��(SQLServer).sql"

pause

@ECHO OFF
cd /d "%~dp0%"
call "..\InitVariables.bat"

IF NOT EXIST "%PKSDataPath%" md "%PKSDataPath%"

IF NOT EXIST "%PKSDataPath%\Database" md "%PKSDataPath%\Database"

pause

@ECHO OFF

cd /d "%~dp0%"
call ..\InitVariables.bat

"%MongoServerPath%\bin\mongo.exe" "Data.js"

pause

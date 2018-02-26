@ECHO OFF
SET PKSData=D:\PKSData

IF NOT EXIST "%PKSData%" md "%PKSData%"

IF NOT EXIST "%PKSData%\MongoDB" md "%PKSData%\MongoDB"
IF NOT EXIST "%PKSData%\MongoDB\Config" md "%PKSData%\MongoDB\Config"
IF NOT EXIST "%PKSData%\MongoDB\Data" md "%PKSData%\MongoDB\Data"
IF NOT EXIST "%PKSData%\MongoDB\Logs" md "%PKSData%\MongoDB\Logs"

pause

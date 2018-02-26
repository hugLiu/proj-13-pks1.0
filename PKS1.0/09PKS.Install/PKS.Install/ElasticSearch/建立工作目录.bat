@ECHO OFF
SET PKSData=D:\PKSData

IF NOT EXIST "%PKSData%" md "%PKSData%"

IF NOT EXIST "%PKSData%\ElasticSearch" md "%PKSData%\ElasticSearch"
IF NOT EXIST "%PKSData%\ElasticSearch\Config" md "%PKSData%\ElasticSearch\Config"
IF NOT EXIST "%PKSData%\ElasticSearch\Data" md "%PKSData%\ElasticSearch\Data"
IF NOT EXIST "%PKSData%\ElasticSearch\Logs" md "%PKSData%\ElasticSearch\Logs"

pause

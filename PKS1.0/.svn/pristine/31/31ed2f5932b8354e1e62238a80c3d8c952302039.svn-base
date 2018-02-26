@ECHO OFF
SET MongoServerPath=C:\Program Files\MongoDB\Server\3.4
SET MongoWorkPath=D:\PKSData\MongoDB

"%MongoServerPath%\bin\mongod.exe" --config "%MongoWorkPath%\Config\mongodb.cfg" --install

net start MongoDB
pause

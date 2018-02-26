@ECHO OFF
REM Installation instructions for Win32 memcached
REM 1. In a command shell, create a directory of your choice name and location
REM 2. Unzip the archive memcached-win32-1.4.4-14.zip in this location
REM 3. Run on the command line: memcached.exe -d install
REM 4. Run, from the Start button "Administrative Tools -> "Services"
REM 5. Double-click on the listing for "memcached"
REM 6. In the panel, you will see:
REM   * "Startup type:" - this should be "automatic"
REM   * "Service status:". Click the start button to start memcached (As shown in the image below)Windows_service
REM 7. Enjoy using memcached!
SET RarPath=C:\Program Files\WinRAR
SET TargetPath=C:\Program Files (x86)\MemCached

cd /d "%~dp0%"

IF NOT EXIST "%TargetPath%" md "%TargetPath%"

"%RarPath%\rar.exe" x MemCached.rar "%TargetPath%\.."

"%TargetPath%\SetupFile\memcached.exe" -d install

sc config memcached start= delayed-auto

net start memcached

pause

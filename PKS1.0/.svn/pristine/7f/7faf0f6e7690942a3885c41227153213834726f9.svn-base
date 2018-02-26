@ECHO OFF

netsh advfirewall firewall add rule name="PKS-SQLServer-TCP" dir=in action=allow protocol=TCP localport=1433
netsh advfirewall firewall add rule name="PKS-SQLServer-UDP" dir=in action=allow protocol=UDP localport=1433

pause

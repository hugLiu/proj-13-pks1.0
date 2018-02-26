@ECHO OFF

netsh advfirewall firewall add rule name="PKS-MemCached" dir=in action=allow protocol=TCP localport=11211

pause

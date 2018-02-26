@ECHO OFF

netsh advfirewall firewall add rule name="PKS-ElasticSearch" dir=in action=allow protocol=TCP localport=9200-9300

pause

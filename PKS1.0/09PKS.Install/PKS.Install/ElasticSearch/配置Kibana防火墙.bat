@ECHO OFF

netsh advfirewall firewall add rule name="PKS-ElasticSearch-Kibana" dir=in action=allow protocol=TCP localport=5601

pause

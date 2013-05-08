Rem To modify this script to you work
Rem  you must supply the network path 
Rem  where the northstar client is stored 
Rem  SERVER_NAME would become the network path

Rem  Before running this batch, make sure you create
Rem  C:\NorthStar and C:\NorthStar\Logs folders on 
Rem  user's local harddrive, and grant user permission
Rem  to add/delete/modify files in this local folder.
 
 
xcopy /a /y /q /r \\dalwfprd01\Apps\NorthStar\Client C:\NorthStar
 
start C:\NorthStar\NorthStar.NorthStarClient.exe

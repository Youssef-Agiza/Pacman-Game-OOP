How to use AspNetForm sample projects:

- Open IIS Manager -> Default Web Site -> Right Click -> "Add Application"
- Input any name as Alias and select one of Application Pool. 
  Because current sample project\bin folder uses EASendMail.dll for .Net 4.0, 
  so please use Net 4.0 or higher version in Application Pool. If you used older 
  .Net framework, please copy Installation path\Lib\net[version]\EASendMail.dll 
  to sample project\bin folder.
- Select path of one of sample project in physical path, click OK.
- Then you can use http://localhost/[alias]/default.aspx to test the sample. 
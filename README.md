<h1>E-Commerce Fashion Application</h1>

<h3>Description</h3>

This is a .NET Core-based e-commerce application focused on fashion, allowing users to browse, filter, and purchase fashion products. The application is designed to connect to a SQL Server database.

<h3>Requirements</h3>
 <ul> 
 <li>Visual Studio 2022 and above</li>
 <li>.NET Core SDK (version 6 and above)</li>
 <li> SQL Server</li>
 <li> Swagger UI for API testing and documentation</li>
 </ul>

 <h3>Installation</h3>
 <ul>
   <li>Clone the repository:</li>

>   https://github.com/Deepsikha/ECommerceFashion-FE.git

<li> Update the database connection string in <b>appsettings.json</b> as follows:</li>

> "ConnectionStrings": {
  "DefaultConnection": "Your_SQL_Server_Connection_String_Here"
}

</ul>
Open up the .sln file from master branch in Visual Studio. If you are correctly talking to your local database and you Start Debugging from Visual Studio.
Once the application is running, you can access the Swagger documentation to explore the API:

> https://localhost:7207/swagger/index.html

 

# Contributing

You need Visual Studio 2019, ASP.NET 5, Entity Framework, Azure tools for Visual Studio 2019 
and SQL Server 2016 to work on this solution locally. 

If it is a small change then do it and create a pull request. If it is a medium to big change
then create a new issue first.

- Fork the repo.
- Clone it to your dev computer.
- Create the databases and tables with the
[script provided](https://github.com/Arnab-Developer/MedicalSystem/tree/main/DatabaseScripts) 
in the repo before executing this app.
- Create a new branch from `main`.
- Update the connection strings and other configs in `appsettings.Development.json` file in 
different projects as applicable to your local settings.
- Do your code changes and commit to the new branch.
- Create new unit tests if applicable and make sure all the tests are passing.
- Create a pull request to `main`.

Please wait while your pull request is being reviewed. If everything is fine then it will be
merged. 

Thanks for your contribution.

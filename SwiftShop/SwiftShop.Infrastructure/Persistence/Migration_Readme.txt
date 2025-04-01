For migration to work, the following packages need to be installed to SwiftShop.Infrastructure
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools


To create / update Migrations using Package Manager Console (PMC)

1. Go to Tools > NuGet Package Manager > Package Manager Console

2. Ensure Default project (dropdown in PMC) is set to Infrastructure\SwiftShop.Infrastructure, which is the project that contains the DbContext class

3. Migrations are created inside the Persistence folder using following command:
		Add-Migration MigrationName -OutputDir Persistence/Migrations
   Replace MigrationName with a meaningful name. ex: InitialCreate

4. To apply the migration to database
		Update-Database

5. To remove the last migration (if not applied)
		Remove-Migration

6. Check applied migrations:
		Get-Migrations

7. Revert to specific migration 
		Update-Database -Migration InitialCreate
   Replace InitialCreate with the name of the migration you want to revert to

8. Reset database (use with caution): 
		Drop-Database
		Update-Database
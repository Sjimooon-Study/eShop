# eShop
## About

### Architecture
The basic structure of the project is allustraited in figure 1 below.

![E Shop.Drawio](eShop.drawio.png)

*Figure 1.0 - Project structure*

#### DataLayer
Abstract classes are used to devide different categories of products into different entities, while keeping consistency between their common properties as *products* (see class diagram in figure 2.0).

![Class Diagram](ClassDiagram.png)

*Figure 2.0 - Class diagram*

This project implements TPH (Table Per Hierarchy), which is the standard in Entity Framework 5 (EF5).
However, the abstract classes used in this project, set the stage for a TPC (Table Per Concrete) implementation, which is *not* supported in EF5.
Therefore, as seen in the database diagram in figure 2.1, the `Product` base class is used to create one table with appropiate fields to represent any concrete class derrived herefrom, hence TPH.
A `Discriminator` shadow property of type is implicitly added to the `Product` table to distinguish between each derrived type.

Potential side effects of using TPH include;
- not being able to use required properties in derived entities, since the table is shared.
- non-nullable fields (ie. CHAR) will take up disk space regardless of what type is stored in that row. Variable length types (ie. VARCHAR) can have a data size of zero.

The looped one-to-many relation ship on the product table is caused by the `Locomotive` entity referencing one or zero `Digitaldecoder`'s, which again has a collection navigation property back to `Locomotive`.

![Database Diagram](DatabaseDiagram.png)

*Figure 2.1 - Database diagram*

A TPC (Table Per Concrete) implementation would mean a table for each non-abstract class is created.
In this instance this would be `DigitalDecoder`, `Locomotive`, and `RailCar`, which all derive from `Product`. 

#### ServiceLayer
Responsible for calling the underlying database (through the DataLayer).
Retrievd entities from the database get their properties mapped into DTO's and passed to the depending application - or wise versa.
This conversion is handled by the `DtoPropertyMapper` class and brought to the application via the '<entity_name>Service' class.

The ServiceLayer contains interfaces (ie. `ILocomotiveService`) for accessing a specific service. The concrete class is conveniently located in the 'concrete' folder, from where it accesses various utility classes. These are located in both 'Utilities' (if they are shared or generic) and alongside the conrete class itself.

The `GetList<entity_name>` methods provide options for ordering, filtering, searching, and pagination.

#### NuGet Packages & Dependecies
*ConsoleApp*
- Microsoft.EntityFrameworkCore.Design v5.0.10

*DataLayer*
- Microsoft.EntityFrameworkCore v5.0.10
- Microsoft.EntityFrameworkCore.Tools v5.0.10
- Microsoft.EntityFrameworkCore.SqlServer v5.0.10
- Microsoft.EntityFrameworkCore.InMemory v5.0.10 *(for unit testing)*
- Microsoft.Extensions.Logging.Console v5.0.0

*UnitTests*
- Microsoft.EntityFrameworkCore v5.0.10

*All of the above can be installed using NuGet Package Manager or Package Manager Console (PMC): `Install-Package <package_name>` (See script below)*

##### Script
```
Install-Package Microsoft.EntityFrameworkCore.Design -ProjectName ConsoleApp
Install-Package Microsoft.EntityFrameworkCore -ProjectName DataLayer
Install-Package Microsoft.EntityFrameworkCore.Tools -ProjectName DataLayer
Install-Package Microsoft.EntityFrameworkCore.SqlServer -ProjectName DataLayer
Install-Package Microsoft.EntityFrameworkCore.InMemory -Projectname DataLayer
Install-Package Microsoft.Extensions.Logging.Console -Projectname DataLayer
Install-Package Microsoft.EntityFrameworkCore -Projectname UnitTests
```

## Usage
### Migrations & Database
*Below commands is executed using PMC in DataLayer.*

**New migration:** `Add-Migration <migration_name>`

**Undo latest migration:** `Remove-Migration`

**Update database:** `Update-Database`

**Drop database:** `Drop-Database`

**Create migration script:** `Script-Migration <script_id_> <migration_name>`

A detailed description of the above and other additional commands can be found in Microsoft Docs: https://docs.microsoft.com/en-us/ef/core/cli/powershell

The `Initial.sql` script located in `DataLayer/Migrations/Scripts` can be used to initialize the `EShopDb` database. 

## Known Issues & Limitations
- Enums lack display names.
- ServiceLayer and UnitTest lack documentation.
- ServiceLayer doesn't provide all types of GRUD actions for all entities.

## Versioning
**v0.1.0**
- Initial version.

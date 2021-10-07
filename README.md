# eShop
## About

### Architecture
#### DataLayer
Abstract classes are used to devide different categories of products into different tables, while keeping consistency between their common properties as *products*.

This project implements TPH (Table Per Hierarchy), which is the standard in EF5.
However, the abstract classes used in this project, set the stage for a TPC (Table Per Concrete) implementation (Table Per Concrete), which is *not* supported in EF5.
Therefore the `Product` base class is used to create one table with appropiate fields to represent any concrete class derrived herefrom, hence TPH.
A `Discriminator` shadow property of type is implicitly added to the `Product` table to distinguish between each derrived type.

A TPC (Table Per Concrete) implementation would mean a table for each non-abstract class is created.
In this instance this would be `DigitalDecoder`, `Locomotive`, and `RailCar`, which all derive from `Product`.

### Nuget Packages
*ConsoleApp*
- Microsoft.EntityFrameworkCore.Design v5.0.10

*DataLayer*
- Microsoft.EntityFrameworkCore v5.0.10
- Microsoft.EntityFrameworkCore.Tools v5.0.10
- Microsoft.EntityFrameworkCore.SqlServer v5.0.10
- Microsoft.Extensions.Logging.Console v5.0.0

*All of the above can be installed using NuGet Package Manager or Package Manager Console (PMC): `Install-Package <package_name>_`*

## Usage
### Migrations & Database
*Below commands is executed using PMC in DataLayer.*

**New migration:** `Add-Migration <migration_name>`

**Undo latest migration:** `Remove-Migration`

**Update database:** `Update-Database`

**Drop database:** `Drop-Database`

**Create migration script:** `Script-Migration <script_id_> <migration_name>`

A detailed description of the above and other additional commands can be found in Microsoft Docs: https://docs.microsoft.com/en-us/ef/core/cli/powershell

## Known Issues & Limitations

## Versioning

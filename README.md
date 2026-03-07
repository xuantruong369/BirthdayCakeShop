dotnet add Presentation reference BusinessLogic

Them project vào solution .sln
dotnet sln add DataAccess/DataAccess.csproj

/DataAccess
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.2
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.2
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.2
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.2

cd DataAccess/
dotnet ef dbcontext scaffold "Server=.;Database=BirthdayCakeShop;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o Entities --context-dir Context -c AppDbContext --use-database-names --no-onconfiguring --force

Program.cs
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

# BirthdayCakeShop

## Clone project

git clone https://github.com/USERNAME/BirthdayCakeShop.git

## Run project

cd Presentation
dotnet run

## Requirements

.NET 8 SDK
SQL Server

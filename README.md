dotnet add Presentation reference BusinessLogic

Them project vào solution .sln
dotnet sln add DataAccess/DataAccess.csproj

/DataAccess
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.2
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.2
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.2
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.2

cd DataAccess/
dotnet ef dbcontext scaffold "Server=.;Database=BirthdayCakeShop;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o Entities --context-dir Context -c BirthdayCakeShopDbContext --use-database-names --no-onconfiguring --force

Program.cs
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
###################################################

# BirthdayCakeShop

## Clone project

git clone https://github.com/USERNAME/BirthdayCakeShop.git

## Run project

cd Presentation
dotnet run

## Requirements

.NET 8 SDK
SQL Server

## Cac buoc clone project tren vscode

B1: git clone ...
B2: Mo project: File --> OpenFolder
B3: Chay 2 file .sql trong thu muc Database tren pham mem ssms
B4: tai ../BirthdayCakeShop> chạy dotnet restore
B5: chay cd Presentation --> ../BirthdayCakeShop/Presentation>
B6: chay dotnet run

## Cac buoc clone project tren vs

B1: git clone ...
B2: Mo project: Open file BirthdayCakeShop.sln
B3: Chay 2 file .sql trong thu muc Database tren pham mem ssms
B4: nhan F5

## Dieu kien

Cai san git va .Net 8

## Quy trinh lam viec khi code

B1: Cap nhap code moi nhat
Run: git pull origin main
B2: Tao branch moi
Run: git checkout -b feature/product-crud
B3: Code va kiem tra
Run: git status
B4: add file
Run: git add .
B5: commit
Run: git commit -m "message"
B6: push lên github
Run: git push origin feature/product-crud

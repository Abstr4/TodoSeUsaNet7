# Inventory Management System (IMS) for TodoSeUsa

Project Description
-------------------
Hello I'm **[Matias Rojas](https://www.linkedin.com/in/matiasrojasmargaritini/)** ([LinkedIn](https://www.linkedin.com/in/matiasrojasmargaritini/)). This project is a simple inventory management system made with **C# .NET 7** for our family clothing store **TodoSeUsa**.

  This **IMS** project will allow you to create, edit, and save clients, bills and products to a **SQL Server** database and keep them secure with the accounts system. If you find my project useful star :star: the repository :smile:. You can also [support me financially here]() or by leaving a comment in my **[LinkedIn post]()**. 

## Table of contents
* [Introduction](#project-description)
* [Showcase](#showcase)
* [Features](#features)
* [Setup](#setup)
* [Features](#features)
* [Branches](#branches)
* [Contribution](#contribution)
* [License](#license)

## Showcase

### Login and Register pages
<div> 
  <p align="center">
    <img src="TodoSeUsaNet7/Images/LoginAndRegisterScreen.png" alt="Login and Register View" width="75%" />
  </p>
</div>

### Menu

  <p align="center">
    <img src="TodoSeUsaNet7/Images/Menu.png" alt="Menu View" width="100%" />
  </p>

### Clients page

  <p align="center">
    <img src="TodoSeUsaNet7/Images/ClientsScreen.png" alt="Clients View" width="100%" />
  </p>
  
### Bills page

  <p align="center">
    <img src="TodoSeUsaNet7/Images/BillsScreen.png" alt="Bills View" width="100%" />
  </p>

### Products page

  <p align="center">
    <img src="TodoSeUsaNet7/Images/ProductsScreen.png" alt="Products View" width="100%" />
  </p>
  
### Each client, bill and product has a dropdown menu to view detailed info.

  <p align="center">
    <img src="TodoSeUsaNet7/Images/ClientDropdown.png" alt="Clien Dropdown" width="45%" />
    <img src="TodoSeUsaNet7/Images/BillDropdown.png" alt="Bill Dropdown" width="45%" />
    <img src="TodoSeUsaNet7/Images/ProductDropdown.png" alt="Product Dropdown" width="45%" />
  </p>

## Features

- An accounts system (register, login, and logout) implemented with .NET Identity to ensure data security.
- CRUD operations over products, clients and bills.
- Listing, searching and filtering the entities above.
- **[Demo version](#demo-environment)**

## Setup

Follow these steps to set up the project on your local machine.

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Git](https://git-scm.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

Ensure you have SQL Server installed and configured on your machine before proceeding with the setup.

### Installation

1. **Clone the repository:**

    ```sh
    git clone https://github.com/yourusername/yourrepository.git
    cd yourrepository
    ```

2. **Set up environment variables:**

    You need to set up an environment variable for your connection string.

    - On **Windows**:

      - (change the database name to whatever you like)

      Open Command Prompt or PowerShell and run:

      ```sh
      setx ConnectionStrings__DefaultConnection "Server=localhost;Database=TodoSeUsaNet7;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true"
      ``` 

      Or add it to your system environment variables through the Control Panel.

    - On **Linux/macOS**:

      Open your terminal and add the following line to your `.bashrc`, `.zshrc`, or `.bash_profile` file:

      ```sh
      export ConnectionStrings__DefaultConnection="Server=localhost;Database=TodoSeUsaNet7;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true"
      ```

      Then, run:

      ```sh
      source ~/.bashrc  # or ~/.zshrc or ~/.bash_profile
      ```

3. **Modify `Program.cs`:**

    Open the `Program.cs` file and update the line for the connection string to match the name you used for the environment variable:

    ```csharp
    var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
    ```

4. **Restore dependencies:**

    ```sh
    dotnet restore
    ```

5. **Build the project:**

    ```sh
    dotnet build
    ```

6. **Run the project:**

    ```sh
    dotnet run
    ```

7. **Restore, build, and run from your IDE:**

    I use Visual Studio IDE, and it works perfectly.

### Additional Notes

- If you use a different environment variable name, make sure to update it in both the environment variable setup step and the `Program.cs` file.

## Branches

### Demo Environment 

The project includes a demo version where you can observe its functionality without creating any real entities. I've utilized **[Bogus](https://github.com/bchavez/Bogus)** to generate fake data. To access the demo, switch to the `feature/demo-environment` branch.

```sh
git checkout feature/demo-environment
```
## Database Triggers

This project includes database triggers defined within a migration file to automate certain operations and ensure data integrity.

### Trigger Details

- **Trigger: `UpdateBillOnProductChange`**
  - **Event:** AFTER INSERT, UPDATE on `Product` table
  - **Function:** Updates the `Bill` table when changes occur in the `Product` table. Specifically, it updates the following fields in the `Bill` table:
    - `TotalProducts`
    - `ProductsSold`
    - `TotalAmountPerProducts`
    - `TotalAmountSold`

- **Trigger: `UpdateClientOnBillChange`**
  - **Event:** AFTER INSERT, UPDATE on `Bill` table
  - **Function:** Updates the `Client` table when changes occur in the `Bill` table. Specifically, it updates the following fields in the `Client` table:
    - `TotalProducts`
    - `TotalAmountPerProducts`
    - `ProductsSold`
    - `TotalAmountSold`
    - `TotalBills`

### Trigger Code

You can find the triggers defined inside the following migration file:
- [`TodoSeUsaNet7.Models/Migrations/20240709192011_ProductsAndBillsTrigger`](TodoSeUsaNet7.Models/Migrations/20240709192011_ProductsAndBillsTrigger)

Here is a snippet of the trigger definitions:

```sql
-- UpdateBillOnProductChange Trigger
CREATE TRIGGER UpdateBillOnProductChange
ON Product
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @BillId INT = (SELECT BillId FROM inserted);
    UPDATE Bill 
    SET TotalProducts = (SELECT COUNT(ProductId) FROM Product WHERE BillId = @BillId)
    WHERE BillId = @BillId;

    UPDATE Bill 
    SET ProductsSold = (SELECT COUNT(ProductId) FROM Product WHERE BillId = @BillId AND Sold = 1)
    WHERE BillId = @BillId;

    UPDATE Bill 
    SET TotalAmountPerProducts = (SELECT ISNULL(SUM(Price), 0) FROM Product WHERE BillId = @BillId)
    WHERE BillId = @BillId;

    UPDATE Bill 
    SET TotalAmountSold = (SELECT ISNULL(SUM(Price), 0) FROM Product WHERE BillId = @BillId AND Sold = 1)
    WHERE BillId = @BillId;
END

-- UpdateClientOnBillChange Trigger
CREATE TRIGGER UpdateClientOnBillChange
ON Bill
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @ClientId int = (SELECT ClientId FROM inserted)

    UPDATE Client SET TotalProducts = (SELECT ISNULL(sum(TotalProducts), 0) FROM Bill WHERE ClientId = @ClientId)
    WHERE ClientId = @ClientId;

    UPDATE Client SET TotalAmountPerProducts = (SELECT ISNULL(sum(TotalAmountPerProducts), 0) FROM Bill WHERE ClientId = @ClientId)
    WHERE ClientId = @ClientId;

    UPDATE Client SET ProductsSold = (SELECT ISNULL(sum(ProductsSold), 0) FROM Bill WHERE ClientId = @ClientId)
    WHERE ClientId = @ClientId;

    UPDATE Client SET TotalAmountSold = (SELECT ISNULL(sum(TotalAmountSold), 0) FROM Bill WHERE ClientId = @ClientId)
    WHERE ClientId = @ClientId;

    UPDATE Client SET TotalBills = (SELECT count(BillId) FROM Bill WHERE ClientId = @ClientId)
    WHERE ClientId = @ClientId;
END

Please make sure to review the migration file for details on the triggers and how they operate within the database.
## Contribution

Your contributions are always welcome and appreciated:

- **Report a Bug or request a feature:**
  If you think you have encountered a bug or want to request a feature, feel free to do it directly to me via [Twitter](https://x.com/Abstr4_) or [LinkedIn](https://www.linkedin.com/in/matiasrojasmargaritini).

- **Contact:**
  You can reach out to me via [contact.abstr4@gmail.com](mailto:contact.abstr4@gmail.com), [Twitter](https://x.com/Abstr4_) or [LinkedIn](https://www.linkedin.com/in/matiasrojasmargaritini).

## License

This project is licensed under the [MIT License](LICENSE).






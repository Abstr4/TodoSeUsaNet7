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
  
### Each client, bill and product has a dropdown menu to see detailed info.

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

### Installation

1. **Clone the repository:**

    ```sh
    git clone https://github.com/Abstr4/TodoSeUsaNet7
    cd TodoSeUsaNet7
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

3. **Restore dependencies:**

    ```sh
    dotnet restore
    ```

4. **Build the project:**

    ```sh
    dotnet build
    ```

5. **Run the project:**

    ```sh
    dotnet run
    ```
    
6. **Restore, build and run from your IDE**

  I use Visual Studio IDE, works perfectly.

## Branches

### Demo Environment 

The project includes a demo version where you can observe its functionality without creating any real entities. I've utilized **[Bogus](https://github.com/bchavez/Bogus)** to generate fake data. To access the demo, switch to the `feature/demo-environment` branch.

```sh
git checkout feature/demo-environment
```
## Contribution

Your contributions are always welcome and appreciated:

- **Report a Bug or request a feature:**
  If you think you have encountered a bug or want to request a feature, feel free to do it directly to me via [Twitter](https://x.com/Abstr4_) or [LinkedIn](https://www.linkedin.com/in/matiasrojasmargaritini).

- **Contact:**
  You can reach out to me via [contact.abstr4@gmail.com](mailto:contact.abstr4@gmail.com), [Twitter](https://x.com/Abstr4_) or [LinkedIn](https://www.linkedin.com/in/matiasrojasmargaritini).

## License

This project is licensed under the [MIT License](LICENSE).






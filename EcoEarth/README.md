# EcoEarthPOC

## Cloning the Repository
Follow these steps when cloning the project:

### 1. Handling **MauiProgram.cs** Errors
- If an error appears related to `MauiProgram.cs`, simply **close Visual Studio** and **reopen it**.

### 2. Setting Up **EcoEarthAppAPI**
1. Set **EcoEarthAppAPI** as the **Startup Project**.
2. Open the **Package Manager Console**:
   - Navigate to: `Tools` ? `NuGet Package Manager` ? `Package Manager Console`
3. In the **Package Manager Console**, set the default project to `EcoEarthAppAPI`.
4. Run the migration command:
   ```powershell
   Add-Migration InitialCreate
   ```
5. Apply the migration:
   ```powershell
   Update-Database
   ```

### 3. Setting Up **LoginAPI**
1. Set **LoginAPI** as the **Startup Project**.
2. Open the **Package Manager Console**:
   - Navigate to: `Tools` ? `NuGet Package Manager` ? `Package Manager Console`
3. Set the default project to `LoginAPI` within the Package Manager Console.
4. Apply the database update:
   ```powershell
   Update-Database
   ```
5. Navigate to the EcoEarth folder in your file explorer and enable "see hidden items"
6. Open the applicationhost.config file in a text editor and change the bindings to this:
        <bindings>
                    <binding protocol="http" bindingInformation="*:15019:127.0.0.1" />
                    <binding protocol="https" bindingInformation="*:44371:127.0.0.1" />
		    <binding protocol="http" bindingInformation="*:15019:localhost" />
        </bindings>

### 4. Configuring Multiple Startup Projects
- Set **EcoEarthPOC**, **EcoEarthAppAPI**, and **LoginAPI** as **multiple startup projects**.

### 5. Running the Program & Security Setup
- Start the application and **install necessary security and web certificates**.

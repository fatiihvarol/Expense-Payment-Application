#   Expense - Payment System

A company specifically requires an application to track and manage expense items for its personnel working in the field. With this application, the personnel working in the field will be able to instantly enter their expenses into the system, and the employer will be able to track and track this, as well as approve the expenditure and pay the personnel without wasting time. Employee and documents, receipts, etc. He will be saved from collecting money and will be able to receive his payment without delay if he is in the field for a long time.

The application will serve in two different roles in the company: manager and field personnel. Working field personnel will only enter expenses into the system and request reimbursement. Staff will see their current requests and be able to follow the status of their requests. He can see his requests pending approval and follow them. Company users who are system administrators will see existing requests and be able to approve or reject them.

Instant payment for approved payments will be made through bank integration and the amount will be deposited to the employee's account via EFT. For rejected requests, an explanation field should be entered and the requester should be able to see why the expense request was rejected.

## Project Structure

- **Models:** Contains classes representing the application's data model.
- **Api Responses** Includes classes responsible for defining standardized API response formats.
- **Validators:** Contains classes used for data validation processes.
- **MediatR** Includes classes that utilize the MediatR library to organize communication within the application. This encompasses handler classes responsible for processing requests and generating results.
- **Cqrs** Integrates Command Query Responsibility Segregation (CQRS) principles, separating command and query operations, often in conjunction with MediatR for better organization and scalability.
- **Memory Caching** I used this for memory efficiency to keep the employee IDs of authenticate loan application users.
- **JWT Token** I used jwt token for security for authentication and authorization transactions.
- **Factory Desing Pattern** I used factory design pattern for payment similitaon service.It allows to make money transaction via EFT nad HAVALE.


## Getting Started
1. **Clone Project**
    ```bash
    git clone https://github.com/fatiihvarol/Patika-Akbank-Final-Case.git
    ```

2. **Install Dependencies:** Navigate to the project folder in the terminal or command prompt and use the `dotnet restore` command to install dependencies.

    ```bash
    dotnet restore
    ```
3. **Connection String:** You should change the ConnectionString property in application.json file.

    ```bash
    "ConnectionStrings": {
    "MsSqlConnection": "data source=DESKTOP-E79JTP3;initial catalog=akbank;trusted_connection=true;Encrypt=False"
     }
    ```
4. **Migration :** You should migrate your database. In the .sln path.In the Initial migration file there are 2 users one of is admin the other one is employee.Also employee table has 1 employee in the migration file.

    ```bash
    dotnet ef database update --project "./Vb.Data" --startup-project "./Vb.Api"
    ```
5. **Run the Application:** Start the application using the following command.

    ```bash
    dotnet watch run
    ```
##
*After following these steps, the application should start and perform  operations using database.*

##
##
## ****Swing Screenshots****
##
- **Application User Endpoints** 
##
 ![](https://i.hizliresim.com/2p7gf22.png)
 ##


 - **Employee Endpoints**
 ##
 ![](https://i.hizliresim.com/slwy22r.png)
 ##
  - **Expense Category Endpoints**
  ##
 ![](https://i.hizliresim.com/mpyhk0u.png)
##

  - **Reports and Token Endpoints**
  ##
 ![](https://i.hizliresim.com/1rs38m9.png)
 ##
  - **Payment Endpoints**
  ##
 ![](https://i.hizliresim.com/gai6kvv.png)
 ##
 ##
 ##
  **Expenses Endpoints**
##
 ![](https://i.hizliresim.com/mfykrc5.png)








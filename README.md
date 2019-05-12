# Store Management System


![What is this](icon.png)


​				Console Application
​                                        (ver 1.0)

Database Teamwork Assignment

C# .NET Jan'19

Telerik Academy




Created by:

[Stanislav Dinev](https://gitlab.com/stakAtak)

[Vasil Yoshovski](https://gitlab.com/VasilNYoshovski)

Preliminary Requirements

Team name:
- Full name: Store Management Center Team
- Short name: SMC Team

Team leader:
- Stanislav Dinev

Team members:
- Vasil Yoshovski


Project provides process automation of a warehouse.


Provided features:

|    Command                      |    Menu item                                          |    Parameters                                                                                                                                                      |
|---------------------------------|-------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------|
|    addproductstooffer           |    Add products to offer                              |    Enter offer ID product_name1 quantity1 product_name2 quantity2 ...                                                                                              |
|    addproductstosale            |    Add products to sale                               |    Enter sale ID product_name1 quantity1 product_name2 quantity2 ...                                                                                               |
|    addproductstowarehouse       |    Add products to warehouse                          |    Enter warehouseID product_name1 quantity1 product_name2 quantity2 ...                                                                                           |
|    addpurchasestosupplier       |    Add purchases to supplier                          |    Enter supplierID purchaseID1 purchaseID2 ........ purchaseIDx                                                                                                   |
|    closesaleorder               |    Close sale (mark as delivered)                     |    Enter sale ID and delivery date                                                                                                                                 |
|    createclient                 |    Create new client                                  |    Enter client name, UIN, email, phone, address, city, country:                                                                                                   |
|    createoffer                  |    Create new offer                                   |    Add offer client name, discount, days to delivery, true or false for   client default address, delivery addres [NR], city [NR], country [NR]                    |
|    createproduct                |    Create new product                                 |    no                                                                                                                                                              |
|    createpurchase               |    Create new purchase                                |    Add purchase supplier name, days to delivery, Warehouse name                                                                                                    |
|    createsale                   |    Create new sale                                    |    Add sale client name, discount, days to delivery, true or false for   client default address, delivery addres [NR], city [NR], country [NR], offer   ID [NR]    |
|    createsupplier               |    Create new supplier                                |    Enter supplier name, UIN, Email, Phone, Address, City, Country:                                                                                                 |
|    createwarehouse              |    Create new warehouse                               |    Enter warehouse name, address, city, country:                                                                                                                   |
|    createsalefromoffer          |    Create sale from offer                             |    Enter offer ID:                                                                                                                                                 |
|    deletesale                   |    Delete sale                                        |    Enter sale ID:                                                                                                                                                  |
|    showallproductsinstock       |    List all product in stock                          |    Enter take and skip records, part of product name [NR]                                                                                                          |
|    shownotclosedsales           |    List not closed sales                              |    no                                                                                                                                                              |
|    showallclients               |    List of clients                                    |    Enter take and skip records, part of client name [NR]                                                                                                           |
|    showallsuppliers             |    List of suppliers                                  |    Enter take and skip records, part of suppliers name [NR]                                                                                                        |
|    showthismonthsale            |    List of this month sales                           |    no                                                                                                                                                              |
|    showthisweeksale             |    List of this week sales                            |    no                                                                                                                                                              |
|    showthisyearsale             |    List of this year sales                            |    no                                                                                                                                                              |
|    showtodaysale                |    List of today sales                                |    no                                                                                                                                                              |
|    updateclientinfo             |    Modify client information                          |    Enter client ID, name, UIN, email, phone, address, city, country (to   not modify field use "-"):                                                               |
|    populatedatabasefromjson     |    Populate database data from JSON archive           |    no                                                                                                                                                              |
|    autopopulatedatabase         |    Populate database with random data automaticaly    |    no                                                                                                                                                              |
|    productstotalsalequantity    |    Products total sale quantity                       |    no                                                                                                                                                              |
|    showclientinfo               |    Show client info                                   |    Enter client name or ID                                                                                                                                         |
|    showsalesperclient           |    Show client sales                                  |    Enter client name or ID                                                                                                                                         |
|    showoffer                    |    Show offer                                         |    Enter offer ID                                                                                                                                                  |
|    showsale                     |    Show sale                                          |    Enter sale ID                                                                                                                                                   |
|    updateproductdetails         |    Update product details                             |    Enter product ID, name, measure, quantity, buy price, retail price   (to not modify field use "-")                                                              |
|    writedatabasetojson          |    Write database data to JSON archive                |    no                                                                                                                                                              |


Features will provide information about:
- Clients
- Suppliers
- Products
- Offers
- Sales
- Purchases


In Trello.com : https://trello.com/smc_team/home the following information could be found:
- Name of Feature
- Feature Owner
- Estimated time it would take
- Actual time it took
- Estimated time it would take to unit test
- Actual time it took to unit test


Must Requirements
- Use Code First approach
- Use Entity Framework Core 2.0+
- Use SQL Server 2017
- At least five tables in the SQL Server database
- Provide at least two type of relations in the database and use both attributes and the Fluent API (Model builder) for configuration
- The user should be able to manipulate the database through the client (basic CRUD)
- Provide some usable user interface for the client (preferably console)

- Unit tests for some of the features of the application will be created. Will be defined later.
- The SOLID principles and the OOP principles will be followed.

Should Requirements
- Project will load some of the data from external JSON or XML files. Will be decided later.
- Project will generate PDF by using PDFsharp library which a non-commercial free library.

Could Requirements
- Service Layer will be defined later

# e-Commerce Recommendation Engine Project

<h3 align="left">Technologies & Tools</h3>
<p align="left"> 

<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/Visual_Studio_Code_1.35_icon.svg/1024px-Visual_Studio_Code_1.35_icon.svg.png" alt="vscode" width="30" height="30"/><a href="https://docs.microsoft.com/en-us/dotnet/csharp/" target="_blank" rel="noopener"> <img src="https://seeklogo.com/images/C/c-sharp-c-logo-02F17714BA-seeklogo.com.png" alt="csharp" width="27" height="30"/> </a><a href="https://dotnet.microsoft.com/" target="_blank" rel="noopener"> <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/e/ee/.NET_Core_Logo.svg/1200px-.NET_Core_Logo.svg.png" alt="dotnetcore" width="30" height="30"/> </a><a href="https://www.microsoft.com/en-us/sql-server/sql-server-2019" target="_blank" rel="noopener"> <img src="https://img.icons8.com/color/452/microsoft-sql-server.png" alt="sqlserver" width="30" height="30"/> </a><a href="https://learn.microsoft.com/en-us/sql/ssms/sql-server-management-studio-ssms?view=sql-server-ver16" target="_blank" rel="noopener"> <img src="https://d1jnx9ba8s6j9r.cloudfront.net/blog/wp-content/uploads/2019/10/logo.png" alt="microsoft sql server" width="30" height="30"/> </a> <a href="https://www.postgresql.org" target="_blank" rel="noopener"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/postgresql/postgresql-original-wordmark.svg" alt="postgresql" width="30" height="30"/> </a><a href="https://www.rabbitmq.com/" target="_blank" rel="noopener"> <img src="https://seeklogo.com/images/R/rabbitmq-logo-25641A76DE-seeklogo.com.png" alt="rabbitmq" width="30" height="30"/> </a><a href="https://www.docker.com/" target="_blank" rel="noopener"> <img src="https://brandlogos.net/wp-content/uploads/2021/11/docker-moby-logo-512x512.png" alt="docker" width="30" height="30"/> </a><a href="https://postman.com" target="_blank" rel="noopener"> <img src="https://www.vectorlogo.zone/logos/getpostman/getpostman-icon.svg" alt="postman" width="30" height="30"/> </a><a href="https://swagger.io/" target="_blank" rel="noopener"> <img src="https://seeklogo.com/images/S/swagger-logo-A49F73BAF4-seeklogo.com.png" alt="swaggerio" width="30" height="30"/></a>



e-Commerce Recommendation Engine's aim is to create real-time recommendation engine. First of all, you should run docker-compose file and establish all envoriments. 

## Requirements

1. Creating a **View Producer App** to read the attached product-views.json file and adding each history record to queue with this app and a message broker.

2. Creating a **Stream Reader App** to pull history transactions that are waiting in a Message Broker queue and transfer them to the db created for this project using the newly written API.

3. Using **ETL Process App** to access the Hepsiburada PostgreSql database mentioned in the attached docker-compose file and transfer the information there to the db created for this project using the newly written API.

4. Creating a **Recommendation REST API** for the above operations and custom APIs. 3 custom APIs are requested here, except for the needs of the above apps:

   * _Browsing History_: It should return the last ten products viewed by a given user and sorted by view date. 

     * ```c#
       GET/ https://localhost:44365/Historys/BrowsingHistory?UserId=user-88
       ```

   * _Delete History_: Delete product from their history

     * ```c#
       DELETE/ https://localhost:44365/Historys
       {
         "productid": "product-1",
         "userid": "user-1"
       }
       ```

   * _Best Seller Products_:  It should understand the interest of a user using his/her browsing history items and recommend best seller products to him/her only from the categories of these items. Otherwise, it should return a general best seller product list without any filter. 

     * ```c#
       https://localhost:44365/Products/BestSellerProducts?userId=user-88
       ```

   5. Response is the same for all GET APIs. If there is no history for the person, type: non-personalized, if there is a history of the person, the data is returned as personalized.

      {
      	"user-id": "fdsfsdfs",
      	"products": ["a","b","c","d"."e"],
      	"type" : "non-personalized"
      }

## Tools

This project was written in ***.NET6***. This project uses a number of open source projects to work properly:

* **RabbitMq** : Used as a Message Broker to listen to history transactions and write to the database.
* **PostgreSql** : Since it contains the data to be made ETL, the db is accessed here.
* **Microsoft SQL Server**: SQL server was raised as the project db and a new db was created.
* **Docker** : used to set up the project and its dependencies.

![](C:\Users\fahriye.cankaya\source\repos\Assignment\img\Workflow.PNG)





## Developed Projects

1. Recommendation.API
2. ETLProcess.APP
3. ViewProducerApp.Publisher
4. StreamReader.Consumer
5. Recommendation.API.Tests



### 1. Recommendation.API

This rest api project allows the whole system to do its work. Here, we first created all our table designs and created the tables in our database with the _CodeFirst_ logic. These are our tables:

* Products, Orders, OrderItems, Histories

  ![image-20221011170014747](C:\Users\fahriye.cankaya\AppData\Roaming\Typora\typora-user-images\image-20221011170014747.png)

  

* Product, Order and OrderItem POST methods allow the data captured in the ETL Process APP project to be transferred to the project db.

* History POST method saves the history taken from the queue in RabbitMq to the project db.

* GET and DELETE methods are the desired custom APIs.

![](C:\Users\fahriye.cankaya\source\repos\Assignment\img\ALL API.PNG)



In this project;

* There are differences between the model coming to the API and the table models in the DB and to map the objects to each other: **AutoMapper** is used.
* Empty, invalid, etc. data coming to APIs. : **FluentValidation** is used to check before coming to the API.
* **Entity Framework Core** is used to connect to the database and write minimal sql queries with the models of the tables and perform db operations with the code.
* Code duplication is avoided by writing a **Custom exceptionhandler middleware** instead of constantly writing try-catch.



### 2. ETL Process App

This is the console application that does an ETL job. first, to create the models of the tables existing in the Hepsiburada db in our project, the following command was run with the logic of DBFirst to connect to the raised Hepsiburada db. Thus, we created the same tables in the database in postgresql as a model in our project.

```
Scaffold-DbContext “Host=localhost;Database=data-db;Username=postgres;Password=123456” Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Models
```



Afterwards, the project database was fed by sending all the data of Product, Order and OrderItem to the post services in Recommendation.API on a record basis. Here, the reason for sending record-based data is checked whether it is in the db from the same record, otherwise it is created.

![](C:\Users\fahriye.cankaya\source\repos\Assignment\img\etl.PNG)



### 3. ViewProducerApp.Publisher

This is a console application. In this project, it connects to RabbitMq and creates a queue, reads the attached product-views.json file line by line and sends a message to the queue.

![](C:\Users\fahriye.cankaya\source\repos\Assignment\img\publisher.PNG)



## 4. StreamReader.Consumer

This is a console application. In this project, it connects to RabbitMq, reads the pending messages, sends it to the post history method in Recommendation.API, and feeds the project db.

![](C:\Users\fahriye.cankaya\source\repos\Assignment\img\consumer.PNG)



## 5. Recommendation.API.Tests

This is an **XUnit** test project. In this project, APIs written for ETL, StreamReader and unit test cases were written for custom APIs. I used **SQLite** for testing EFCore.

![image-20221011165224963](C:\Users\fahriye.cankaya\AppData\Roaming\Typora\typora-user-images\image-20221011165224963.png)



## Docker

I wrote Docker Compose to install and deploy in a Docker container. However, after I installed the SQL Server I installed for the project, the script I wrote to create an automatic database did not work, so I could not restore the project with docker compose. The dockerfile of the project and the docker-compose file containing all the installations are also attached.

If the docker-compose.yml file worked, it would be necessary to install the Docker and Docker Compose into your computer. Following commands would setup a RabbitMq, PostgreSQL and Microsoft SQL Server.

To setup databases, you should run following command:
docker-compose -f ./docker-compose.yml up -d

To tear down, you should run following command:
docker-compose -f ./docker-compose.yml down

After setup is completed;
⦁	You can access RabbitMQ from port 15672 on your localhost. Credentials are:
⦁	Username: guest
⦁	Password: guest

⦁	You can access PostgreSQL from port 5432 on your localhost. Credentials are:
⦁	Username: postgres
⦁	Password: 123456

⦁	You can access Microsoft SQL Server from port 1433 on your localhost. Credentials are:
⦁	Username: sa
⦁	Password: Password1!


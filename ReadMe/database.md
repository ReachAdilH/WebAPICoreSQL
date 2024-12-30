create table Product (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Name VARCHAR(50) not null ,
   ListPrice decimal not null
);

INSERT INTO Product( Name, ListPrice) values
('Marlboro', 11.80),
('Lucky Stripe' , 8.80),
( 'American Spirit', 13.20);

Select * from Product

 --------------------------------------------------------------------
create table Employee (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Name VARCHAR(50) not null ,
  Department VARCHAR(50),
  City VARCHAR(50),
  State VARCHAR(50)
);


INSERT into Employee ( Name, Department, City, State)  values
 
     ( 'John Doe', 'HR', 'Phoenix', 'Arizona'),
     ('Jane Smith', 'IT', 'Dallas', 'Texas'),
   ( 'Robert Johnson', 'Finance', 'Seattle', 'Washington') ;

    --------------------------------------------------------------------

   Exec sp_rename 'Employee.Name' , 'FirstName' , 'COLUMN'

    Alter table Employee Add LastName VARCHAR(50)

    --------------------------------------------------------------------

      CREATE Table Log(
   Id int Identity(1,1) primary key,
   CorrelationId uniqueidentifier not null,
   ApplicationName Nvarchar(100) not null,
   MethodName Nvarchar(100),
   Message Nvarchar(Max),
   Exception Nvarchar(Max),
   LoggedAt Datetime default Getdate()
   );

    --------------------------------------------------------------------
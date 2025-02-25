create database TeesAndTrees;
use TeesAndTrees

create table registerApp(
	userID int primary key identity,
	userName varchar(255) not null,
	pass varchar(255) not null
)

create table Tshirt_Inventory(
	tshirtID int primary key,
	tshirtCaption varchar(255) not null,
	color varchar(255) not null,
	price decimal (18,2) not null,
	orderCount int not null,
	userID int

	constraint fk_userIDtshirt foreign key (userID) references registerApp(userID)
)


create table Printing_Equipment(
	equipmentID int primary key,
	equipmentType varchar(255) not null,
	equipmentBrand varchar(255) not null,
	equipmentStocks varchar(255) not null check (equipmentStocks >= 0),
	userID int

	constraint fk_userIDequipment foreign key (userID) references registerApp(userID)
)

create table Printing_Materials(
	materialID int primary key,
	materialName varchar(255) not null,
	materialType varchar(255) not null,
	materialAvailable varchar(255) not null check (materialAvailable >= 0),
	userID int

	constraint fk_userIDmaterials foreign key (userID) references registerApp(userID)
)

create table Plant_Inventory(
	plantID int primary key,
	plantName varchar(255) not null,
	species varchar(255) not null,
	plantStock int not null check (plantStock >= 0),
	price decimal (18, 2) not null check (price >= 0),
	userID int

	constraint fk_userIDplant foreign key (userID) references registerApp(userID)
)

create table Sale_Transactions(
	transactionID int primary key,
	productID int,
	quantitySold int not null check (quantitySold >= 0),
	totalPrice decimal (18, 2) not null check (totalPrice >= 0),
	saleDate datetime not null,
	customerID int,
	userID int

	constraint tees_fkID foreign key(productID) references Tshirt_Inventory(tshirtID),
	constraint cust_fkID foreign key(customerID) references Customer(customerID),
	constraint fk_userIDsaleTees foreign key (userID) references registerApp(userID)
)

create table Sale_TransactionsPlant(
	transactionID int primary key,
	productID int,
	quantitySold int not null check (quantitySold >= 0),
	totalPrice decimal (18, 2) not null check (totalPrice >= 0),
	saleDate datetime not null,
	customerIDone int,
	userID int

	constraint custOne_fkIDPlant foreign key (customerIDone) references CustomerOne(customerID),
	constraint trees_fkID foreign key(productID) references Plant_Inventory(plantID),
	constraint fk_userIDsale foreign key (userID) references registerApp(userID)
)

create table Customer(
	customerID int primary key,
	fname varchar(255) not null,
	lname varchar(255) not null,
	town varchar(255) not null,
	city varchar(255) not null,
	contactInfo varchar(255) not null,
	userID int

	constraint fk_userIDcustomer foreign key (userID) references registerApp(userID)
)

create table CustomerOne(
	customerID int primary key,
	fname varchar(255) not null,
	lname varchar(255) not null,
	town varchar(255) not null,
	city varchar(255) not null,
	contactInfo varchar(255) not null,
	userID int

	constraint fk_userIDcustomerOne foreign key (userID) references registerApp(userID)
)

/*Register Procedures*/
create procedure sp_insert
	@userName varchar(255),
	@pass varchar(255)
	as begin
	insert into registerApp values(@userName, @pass);
	end

create procedure sp_view
as begin 
select * from registerApp
end

create procedure sp_delete
@userName varchar (255)
as begin
delete from registerApp 
where userName = @userName
end

/*Customized Tshirt Procedures*/
create proc sp_searchShirt
@tshirtID int
as begin
select 
	tshirtID as 'T-SHIRT ID', 
	tshirtCaption as 'T-SHIRT CAPTION', 
	color as 'T-SHIRT COLOR', 
	price as 'PRICE', 
	orderCount as 'ORDER COUNT' 
from 
	Tshirt_Inventory 
where 
	tshirtID = @tshirtID
end

create proc sp_insertShirt
	@tshirtID int,
	@tshirtCaption nvarchar (255),
	@color nvarchar(255),
	@price decimal (18, 2),
	@orderCount int
as begin
	insert into Tshirt_Inventory (tshirtID, tshirtCaption, color, price, orderCount)
	values (@tshirtID, @tshirtCaption, @color, @price, @orderCount)
end

create proc sp_viewShirt
as begin 
select tshirtID as 'T-SHIRT ID', tshirtCaption as 'T-SHIRT CAPTION', color as 'T-SHIRT COLOR', price as 'PRICE', orderCount as 'ORDER COUNT'
from Tshirt_Inventory
end

create proc sp_DeleteShirt
@tshirtID int
as begin
delete from Tshirt_Inventory where tshirtID = @tshirtID
end

create proc sp_updateShirt
@tshirtID int,
	@tshirtCaption nvarchar (255),
	@color nvarchar(255),
	@price decimal (18, 2),
	@orderCount int
as begin
	update Tshirt_Inventory set 
		tshirtCaption = @tshirtCaption, 
		color = @color, 
		price = @price, 
		orderCount = @orderCount
	where 
		tshirtID = @tshirtID
end

/*Shirt Equipment Procedures*/

create proc sp_searchEquipment
@equipmentID int
as begin
select 
	equipmentID as 'EQUIPMENT ID', 
	equipmentType as 'EQUIPMENT TYPE', 
	equipmentBrand as 'EQUIPMENT BRAND', 
	equipmentStocks as 'STOCKS AVAILABLE'
from Printing_Equipment where equipmentID = @equipmentID
end

create proc sp_viewEquipment
as begin 
select 
	equipmentID as 'EQUIPMENT ID', 
	equipmentType as 'EQUIPMENT TYPE', 
	equipmentBrand as 'EQUIPMENT BRAND', 
	equipmentStocks as 'STOCKS AVAILABLE'
from Printing_Equipment
end

create proc sp_insertEquipment
	@equipmentID int,
	@equipmentType nvarchar (255),
	@equipmentBrand nvarchar(255),
	@equipmentStocks int
as begin
	insert into Printing_Equipment(equipmentID, equipmentType, equipmentBrand, equipmentStocks)
	values (@equipmentID, @equipmentType, @equipmentBrand, @equipmentStocks)
end

create proc sp_updateEquipment
	@equipmentID int,
	@equipmentType nvarchar (255),
	@equipmentBrand nvarchar(255),
	@equipmentStocks int
as begin
	update Printing_Equipment set 
		equipmentType = @equipmentType,
		equipmentBrand = @equipmentBrand,
		equipmentStocks = @equipmentStocks
	where 
		equipmentID = @equipmentID
end

create proc sp_DeleteEquipment
@equipmentID int
as begin
delete from Printing_Equipment where equipmentID = @equipmentID
end

/*Material Procedures*/
select * from Printing_Materials

create proc sp_searchmaterials
@materialID int
as begin
select 
	materialID as 'MATERIAL ID', 
	materialName as 'MATERIAL NAME', 
	materialType as 'MATERIAL TYPE', 
	materialAvailable as 'STOCKS AVAILABLE'
from Printing_Materials where materialID = @materialID
end

create proc sp_viewMaterials
as begin 
select 
	materialID as 'MATERIAL ID', 
	materialName as 'MATERIAL NAME', 
	materialType as 'MATERIAL TYPE', 
	materialAvailable as 'STOCKS AVAILABLE'
from Printing_Materials
end

create proc sp_insertMaterials
	@materialID int,
	@materialName nvarchar (255),
	@materialType nvarchar(255),
	@materialAvailable int
as begin
	insert into Printing_Materials(materialID,materialName, materialType, materialAvailable)
	values (@materialID, @materialName, @materialType, @materialAvailable)
end

create proc sp_updateMaterials
	@materialID int,
	@materialName nvarchar (255),
	@materialType nvarchar(255),
	@materialAvailable int
as begin
	update Printing_Materials set
		materialName = @materialName,
		materialType = @materialType,
		materialAvailable = @materialAvailable
	where 
		materialID = @materialID
end

create proc sp_DeleteMaterials
@materialID int
as begin
delete from Printing_Materials where materialID = @materialID
end

/*Delete Accunt Proc*/
delete from registerApp

/*Floriculture Plants Procedure*/
select * from Plant_Inventory

create proc sp_searchPlants
@plantID int
as begin
select 
	plantID as 'PLANT ID', 
	plantName as 'PLANT NAME', 
	species as 'SPECIES', 
	plantStock as 'STOCKS',
	price as 'PRICE'
from Plant_Inventory where plantID = @plantID
end

create proc sp_viewPlants
as begin 
select 
	plantID as 'PLANT ID', 
	plantName as 'PLANT NAME', 
	species as 'SPECIES', 
	plantStock as 'STOCKS',
	price as 'PRICE'
from Plant_Inventory
end

create proc sp_insertPlants
	@plantID int,
	@plantName nvarchar(255),
	@species nvarchar (255),
	@plantStock int,
	@price decimal (18,2)
as begin
	insert into Plant_Inventory(plantID,plantName, species, plantStock, price)
	values (@plantID, @plantName, @species, @plantStock, @price)
end

create proc sp_updatePlants
	@plantID int,
	@plantName nvarchar(255),
	@species nvarchar (255),
	@plantStock int,
	@price decimal (18,2)
as begin
	update Plant_Inventory set
		plantName = @plantName,
		species = @species,
		plantStock = @plantStock,
		price = @price
	where 
		plantID = @plantID
end

create proc sp_DeletePlants
@plantID int
as begin
delete from Plant_Inventory where plantID = @plantID
end

/*SHIRT Customer Procedures */
select * from Customer

create proc sp_searchCustomer
@customerID int
as begin
select 
	customerID as 'CUSTOMER ID', 
	fname + lname as 'CUSTOMER NAME',
	town + city as 'CUSTOMER ADDRESS',
	contactInfo as 'CONTACTS'
from Customer where customerID = @customerID
end

create proc sp_viewCustomer
as begin 
select 
	customerID as 'CUSTOMER ID', 
	fname + lname as 'CUSTOMER NAME',
	town + city as 'CUSTOMER ADDRESS',
	contactInfo as 'CONTACTS'
from Customer
end

drop proc sp_viewCustomer

create proc sp_insertCustomer
	@customerID int,
	@fname nvarchar(255),
	@lname nvarchar (255),
	@contactInfo nvarchar (255),
	@town nvarchar (255),
	@city nvarchar (255)
as begin
	insert into Customer(customerID,fname, lname, contactInfo, town, city)
	values (@customerID, @fname, @lname, @contactInfo, @town, @city)
end
drop proc sp_insertCustomer

create proc sp_updateCustomer
		@customerID int,
	@fname nvarchar(255),
	@lname nvarchar (255),
	@contactInfo nvarchar (255),
	@town nvarchar (255),
	@city nvarchar (255)
as begin
	update Customer set
		fname = @fname,
		lname = @lname,
		contactInfo = @contactInfo,
		town = @town,
		city = @city
	where 
		customerID = @customerID
end

drop proc  sp_updateCustomer

create proc sp_DeleteCustomer
@customerID int
as begin
delete from Customer where customerID = @customerID
end

/*Shirt Transac*/
create proc sp_selectProdIDshirt
@tshirtID int
as begin
	select tshirtCaption as 'tshirtCaption' from Tshirt_Inventory where tshirtID = @tshirtID
end

create proc sp_selectQuantity
@tshirtID int
as begin
	select orderCount as 'Shirt pcs.' from Tshirt_Inventory where tshirtID = @tshirtID
end

create proc sp_selectPriceTransac
@tshirtID int
as begin
	select price as 'Commision Price' from Tshirt_Inventory where tshirtID = @tshirtID
end

/*Customers in Plant*/
select * from CustomerOne

create proc sp_searchCustomer1
@customerID int
as begin
select 
	customerID as 'CUSTOMER ID', 
	fname + lname as 'CUSTOMER NAME',
	town + city as 'CUSTOMER ADDRESS',
	contactInfo as 'CONTACTS'
from CustomerOne where customerID = @customerID
end

create proc sp_viewCustomer1
as begin 
select 
	customerID as 'CUSTOMER ID', 
	fname + lname as 'CUSTOMER NAME',
	town + city as 'CUSTOMER ADDRESS',
	contactInfo as 'CONTACTS'
from CustomerOne
end

create proc sp_insertCustomer1
	@customerID int,
	@fname nvarchar(255),
	@lname nvarchar (255),
	@contactInfo nvarchar (255),
	@town nvarchar (255),
	@city nvarchar (255)
as begin
	insert into CustomerOne(customerID,fname, lname, contactInfo, town, city)
	values (@customerID, @fname, @lname, @contactInfo, @town, @city)
end


create proc sp_updateCustomer1
	@customerID int,
	@fname nvarchar(255),
	@lname nvarchar (255),
	@contactInfo nvarchar (255),
	@town nvarchar (255),
	@city nvarchar (255)
as begin
	update CustomerOne set
		fname = @fname,
		lname = @lname,
		contactInfo = @contactInfo,
		town = @town,
		city = @city
	where 
		customerID = @customerID
end

create proc sp_DeleteCustomer1
@customerID int
as begin
delete from CustomerOne where customerID = @customerID
end

/*Sales Procedures in Shirt*/
create proc sp_insertTransac
@transactionID int,
@productID int,
@quantitySold int,
@totalPrice decimal (18,2),
@saleDate datetime,
@customerID int
as begin
	insert into Sale_Transactions (transactionID, productID, quantitySold, totalPrice, saleDate, customerID) values (@transactionID, @productID, @quantitySold, @totalPrice, @saleDate, @customerID)
end

create proc sp_viewTransac
as begin
	select transactionID as 'Transaction ID', productID as 'Product ID', quantitySold as 'Quantity', totalPrice as 'TotalPrice', saleDate as 'Sale Date', customerID as 'Customer ID' from Sale_Transactions
end

create proc sp_updateTransac
@transactionID int,
@productID int,
@quantitySold int,
@totalPrice decimal (18,2),
@saleDate datetime,
@customerID int
as begin
	update Sale_Transactions set transactionID = @transactionID, productID = @productID, quantitySold = @quantitySold, totalPrice = @totalPrice, saleDate = @saleDate, customerID = @customerID where transactionID = @transactionID
end

create procedure sp_deleteTransac
@transactionID int
as begin
	delete from Sale_Transactions where transactionID = @transactionID
end

create procedure sp_searchTransac
@transactionID int
as begin
	select transactionID as 'Transaction ID', productID as 'Product ID', quantitySold as 'Quantity', totalPrice as 'TotalPrice', saleDate as 'Sale Date', customerID as 'Customer ID' from Sale_Transactions where transactionID = @transactionID
end

create proc sp_viewCustName
as begin 
	select 
	transactionID as 'Transaction ID', 
	productID as 'Product ID',
	quantitySold as 'Quantity', 
	totalPrice as 'TotalPrice', 
	saleDate as 'Sale Date', 
	Customer.fname + ' ' + Customer.lname as 'Customer Name'
from 
	Sale_Transactions
	inner join Customer on Sale_Transactions.customerID = Customer.customerID
order by
	transactionID
end 

create proc sp_viewCustNameShirt
@transactionID int
as begin 
	select 
	transactionID as 'Transaction ID', 
	productID as 'Product ID',
	quantitySold as 'Quantity', 
	totalPrice as 'TotalPrice', 
	saleDate as 'Sale Date', 
	Customer.fname + ' ' + Customer.lname as 'Customer Name'
from 
	Sale_Transactions
	inner join Customer on Sale_Transactions.customerID = Customer.customerID
where 
	transactionID = @transactionID
end 

/*Plant Sales Transac*/
create proc sp_selectProdIDplant
@plantID int
as begin
	select plantName as 'Plant Name' from Plant_Inventory where plantID = @plantID
end

create proc sp_selectQuantityPlant
@plantID int
as begin
	select plantStock as 'Plant Stocks' from Plant_Inventory where plantID = @plantID
end

create proc sp_selectPriceTransacPlant
@plantID int
as begin
	select price as 'Price' from Plant_Inventory where plantID = @plantID
end

create proc sp_viewPlantTransac
as begin
	select transactionID as 'Transaction ID', productID as 'Product ID', quantitySold as 'Quantity', totalPrice as 'TotalPrice', saleDate as 'Sale Date', customerIDone as 'Customer ID' from Sale_TransactionsPlant
end

create proc sp_viewCustName1
as begin 
	select 
	transactionID as 'Transaction ID', 
	productID as 'Product ID',
	quantitySold as 'Quantity', 
	totalPrice as 'TotalPrice', 
	saleDate as 'Sale Date', 
	CustomerOne.fname + ' ' + CustomerOne.lname as 'Customer Name'
from 
	Sale_TransactionsPlant
	inner join CustomerOne on Sale_TransactionsPlant.customerIDOne = CustomerOne.customerID
order by
	transactionID
end 

create proc sp_viewCustNamePlant
@transactionID int
as begin 
	select 
	transactionID as 'Transaction ID', 
	productID as 'Product ID',
	quantitySold as 'Quantity', 
	totalPrice as 'TotalPrice', 
	saleDate as 'Sale Date', 
	CustomerOne.fname + ' ' + CustomerOne.lname as 'Customer Name'
from 
	Sale_TransactionsPlant
	inner join CustomerOne on Sale_TransactionsPlant.customerIDOne = CustomerOne.customerID
where
	transactionID = @transactionID
end 


create proc sp_insertPlantTransac
@transactionID int,
@productID int,
@quantitySold int,
@totalPrice decimal(18,2),
@saleDate datetime,
@customerIDone int
as begin
	insert into Sale_TransactionsPlant (transactionID, productID, quantitySold, totalPrice, saleDate, customerIDone) values (@transactionID, @productID, @quantitySold, @totalPrice, @saleDate, @customerIDone)
end 

create proc sp_updatePlantTransac
@transactionID int,
@productID int,
@quantitySold int,
@totalPrice decimal(18,2),
@saleDate datetime,
@customerIDone int
as begin
	update Sale_TransactionsPlant set productID = @productID, quantitySold = @quantitySold, totalPrice = @totalPrice, saleDate = @saleDate, customerIDone = @customerIDone where transactionID = @transactionID
end

create proc sp_deletePlantTransac
@transactionID int
as begin
	delete from Sale_TransactionsPlant where transactionID = @transactionID
end

/*Register App*/
CREATE PROCEDURE sp_updateUser
    @userID INT,
    @newUsername VARCHAR(255) = NULL,
    @newPassword VARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Update username if provided
    IF @newUsername IS NOT NULL
    BEGIN
        UPDATE registerApp
        SET userName = @newUsername
        WHERE userID = @userID;
    END

    -- Update password if provided
    IF @newPassword IS NOT NULL
    BEGIN
        UPDATE registerApp
        SET pass = @newPassword
        WHERE userID = @userID;
    END
END

drop proc sp_updateUser
select * from registerApp
create database TheUnknownGoose_Database
go
use TheUnknownGoose_Database
go

create table CategoriesOfProducts(
Id bigint primary key identity,
Name nvarchar(32) unique
);

insert into CategoriesOfProducts(Name)
Values ('Fruits'), ('Vegetables'), ('Cereals and Pulses'), ('Chesse'),('Meat'),('Drinks')

create table Products(
Id bigint primary key identity,
Name nvarchar(32) unique NOT NULL,
NumberOfCalories int NOT NULL,
Measure nvarchar(32),
CategoryId bigint foreign key references CategoriesOfProducts(Id) on update cascade,
Comment nvarchar(256)
);

insert into Products(Name, NumberOfCalories, Measure, CategoryId)
Values ('Apple', 20, 'stk', 1), ('Banana', 111, 'stk', 1),('Bluebarries', 62,'stk', 1),('Kiwi', 112, 'stk', 1),
('Broccoli', 207,'stk', 2),('Carrot', 25,'stk', 2),('Cabbage', 227,'stk', 1),('Black Olives', 2, 'stk', 1),
('Amaranth', 356,'per 100 grams', 3),('Barley', 370,'per 100 grams', 3),
('Brown Rice', 379,'per 100 grams', 3),('Buckwheat', 291,'per 100 grams', 3),
('Mozzarella', 299,'per 100 grams', 4),('Parmesan', 440,'per 100 grams', 4),
('Chesse Fondue', 228,'per 100 grams', 4),('Chester', 357,'per 100 grams', 4),
('Beef', 248,'per 100 grams', 5),('Chicken', 222,'per 100 grams', 5),
('Duck', 338,'per 100 grams', 5),('Pork', 196,'per 100 grams', 5),
('Coke', 149,'per 355 ml', 6),('Coke Zero', 0,'per 355 ml', 6),
('7up', 156,'per 355 ml', 6), ('Dr.Pepper', 96,'per 355 ml', 6)

create table CategoriesOfDishes(
Id bigint primary key identity,
Name nvarchar(32) unique
)

create table Dishes(
Id bigint primary key identity,
Name nvarchar(32) unique NOT NULL,
NumberOfCalories int NOT NULL,
Comment nvarchar(256),
CategoryId bigint foreign key references CategoriesOfDishes(Id) on update cascade
);


create procedure GetIdforList_CategProduct
@itemName nvarchar(32),
@itemId bigint output
as
begin
Select @itemId = Id from CategoriesOfProducts where Name = @itemName
end

create procedure GetIdforList_Products
@itemName nvarchar(32),
@itemId bigint output
as
begin
Select @itemId = Id from Products where Name = @itemName
end

create procedure GetIdforList_CategDish
@itemName nvarchar(32),
@itemId bigint output
as
begin
Select @itemId = Id from CategoriesOfDishes where Name = @itemName
end

create procedure GetIdforList_Dishes
@itemName nvarchar(32),
@itemId bigint output
as
begin
Select @itemId = Id from Dishes where Name = @itemName
end

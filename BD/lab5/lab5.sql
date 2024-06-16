CREATE TABLE Cart (
    cart_id INT PRIMARY KEY IDENTITY(1,1)
);

CREATE TABLE Discount (
    discount_id INT PRIMARY KEY IDENTITY(1,1),
    start_date DATE,
    end_date DATE,
    product_id INT,
    discount_amount DECIMAL(18,2),
    description VARCHAR(255)
);

CREATE TABLE Storage (
    storage_id INT PRIMARY KEY IDENTITY(1,1),
    product_id INT,
    quantity INT,
    entry_date DATE
);

CREATE TABLE Supplies (
    supply_id INT PRIMARY KEY IDENTITY(1,1),
    supplier_id INT,
    product_id INT,
    quantity INT,
    price DECIMAL(18,2),
    discount_id INT
);

CREATE TABLE Shop_floor (
    shop_floor_id INT PRIMARY KEY IDENTITY(1,1),
    shop_id INT,
    product_id INT,
    quantity INT,
    expiration_date DATE
);

CREATE TABLE Product_list (
    product_id INT PRIMARY KEY IDENTITY(1,1),
    product_name VARCHAR(255),
    price DECIMAL(18,2),
    expiration_date DATE,
    category_id INT,
    supplier_id INT
);

CREATE TABLE Check_list (
    check_id INT PRIMARY KEY IDENTITY(1,1),
    client_id INT,
    product_id INT,
    quantity INT,
    check_date DATE
);

CREATE TABLE Client (
    client_id INT PRIMARY KEY IDENTITY(1,1),
    first_name VARCHAR(255),
    last_name VARCHAR(255),
    birth_date DATE
);

CREATE TABLE Suppliers (
    supplier_id INT PRIMARY KEY IDENTITY(1,1),
    supplier_name VARCHAR(255)
);

CREATE TABLE Payments (
    payment_id INT PRIMARY KEY IDENTITY(1,1),
    description VARCHAR(255),
    amount DECIMAL(18,2),
    payment_date DATE
);

CREATE TABLE Transfer_history (
    transfer_id INT PRIMARY KEY IDENTITY(1,1),
    transfer_date DATE,
    from_storage_id INT,
    to_storage_id INT
);

CREATE TABLE Categories (
    category_id INT PRIMARY KEY IDENTITY(1,1),
    category_name VARCHAR(255)
);






insert into Transfer_history values(1, '17-03-2022', 1, 1);
insert into Transfer_history values(2, '15-03-2017', 2, 1);
insert into Transfer_history values(3, '01-08-2020', 2, 2);

insert into Categories values(1, 'молочная продукция');
insert into Categories values(2, 'кондитерские изделия');
insert into Categories values(3, 'выпечка');
insert into Categories values(4, 'бакалея');
insert into Categories values(5, 'бытовая техника');
insert into Categories values(6, 'безалкогольные напитки');

insert into Payments values(1, 'оплата коммунальных услуг', 1000, '01-01-2023');
insert into Payments values(2, 'оплата коммунальных услуг', 1000, '01-02-2023');
insert into Payments values(3, 'оплата коммунальных услуг', 1000, '01-03-2023');
insert into Payments values(4, 'выплата за рекламные услуги', 5500, '15-10-2022');
insert into Payments values(5, 'замена поврежденного стеллажа', 100, '01-01-2023');

insert into Suppliers values (1, 'Савушкин продукт');
insert into Suppliers values (2, 'Завод Горизонт');
insert into Suppliers values (3, 'Минский хлебокомбинат');
insert into Suppliers values (4, 'Мяспромторг');

insert into Client values (1, 'alexey', 'adamovich', '05-01-2000');
insert into Client values (2, 'denis', 'girovir', '02-12-1990');
insert into Client values (3, 'karina', 'lemonad', '15-07-1983');
insert into Client values (4, 'alina', 'brkovski', '05-01-1976');
insert into Client values (5, 'ivan', 'zubilko', '05-01-1998');

insert into Check_list values (1, 1, 2, '15-03-2023');
insert into Check_list values (2, 1, 2, '16-03-2023');
insert into Check_list values (3, 2, 12, '10-03-2023');
insert into Check_list values (4, 3, 4, '06-03-2023');
insert into Check_list values (5, 4, 4, '09-03-2023');
insert into Check_list values (6, 4, 4, '10-03-2023');
insert into Check_list values (7, 1, 1, '09-03-2023');
insert into Check_list values (8, 2, 1, '19-11-2022');
insert into Check_list values (9, 1, 4, '09-02-2023');
insert into Check_list values (10, 1, 6, '11-02-2023');
insert into Check_list values (11, 1, 1, '17-02-2023');
insert into Check_list values (12, 1, 10, '31-12-2022');
insert into Check_list values (13, 1, 7, '10-01-2023');
insert into Check_list values (14, 1, 5, '27-09-2021');
insert into Check_list values (15, 2, 4, '15-09-2022');
insert into Check_list values (16, 2, 6, '17-03-2020');


insert into Product_list values (1, 'Сырок Мячик', 10, '03-05-2023', 1, 1);
insert into Product_list values (2, 'Молоко 5%', 6, '03-05-2023', 1, 1);
insert into Product_list values (3, 'Колбаса Бегров', 17, '03-05-2023', 4, 4);
insert into Product_list values (4, 'Микроволновка', 460, '03-05-2023', 5, 2);
insert into Product_list values (5, 'Чайник', 190, '03-05-2023', 5, 2);
insert into Product_list values (6, 'Хлеб черный', 3, '05-04-2023', 3, 3);
insert into Product_list values (7, 'Хлеб белый', 7, '05-04-2023', 3, 3);
insert into Product_list values (8, 'Говяда элитная', 40, '15-04-2023', 4, 4);
insert into Product_list values (9, 'Курица', 18, '01-08-2023', 4, 4);
insert into Product_list values (10, 'Торт Гламур', 60, '28-04-2023', 2, 3);
insert into Product_list values (11, 'Чесночная булочка', 9, '20-03-2023', 3, 3);
insert into Product_list values (12, 'Сыр чедр', 10, '03-05-2023', 1, 1);
insert into Product_list values (13, 'Кокакола', 8, '06-12-2023', 6, 1);

insert into Shop_floor values (1, 1, 2, 20, '17-03-2024');
insert into Shop_floor values (2, 1, 2, 10, '02-03-2024');
insert into Shop_floor values (3, 4, 4, 3, '07-02-2024');
insert into Shop_floor values (4, 5, 7, 50, '11-03-2024');
insert into Shop_floor values (5, 6, 8, 13, '21-05-2024');
insert into Shop_floor values (6, 3, 12, 10, '09-03-2024');
insert into Shop_floor values (7, 6, 2, 20, '17-02-2024');

insert into Supplies values(1, 1, 1, 7, 200, 20);
insert into Supplies values(2, 2, 3, 7, 1130, 20);
insert into Supplies values(3, 3, 4, 7, 80, 4);
insert into Supplies values(4, 2, 6, 7, 200, 320);
insert into Supplies values(5, 1, 2, 7, 3450, 540);
insert into Supplies values(6, 2, 3, 7, 8900, 650);
insert into Supplies values(7, 3, 5, 7, 2000, 120);


insert into Storage values(1, 1, 1, '17-03-2024');
insert into Storage values(2, 2, 2, '17-03-2024');
insert into Storage values(3, 3, 3, '17-03-2024');
insert into Storage values(4, 4, 4, '17-03-2024');
insert into Storage values(5, 1, 5, '17-03-2024');
insert into Storage values(6, 2, 6, '17-03-2024');

insert into Discount values(1, 2, '15-03-2024', '20-03-2024', 1, '2 и более');
insert into Discount values(2, 6, '15-03-2024', '20-03-2024', 3, 'всегда');
insert into Discount values(3, 11, '01-01-2024', '31-12-2024', 5, 'после 6');

insert into Cart values (1, 1, 2);
insert into Cart values (2, 2, 2);
insert into Cart values (3, 4, 3);
insert into Cart values (4, 6, 1);
insert into Cart values (5, 2, 1);
insert into Cart values (6, 7, 5);
insert into Cart values (7, 8, 1);
insert into Cart values (1, 3, 3);
insert into Cart values (2, 3, 4);
insert into Cart values (3, 6, 5);
insert into Cart values (3, 7, 4);
insert into Cart values (3, 8, 1);
insert into Cart values (7, 2, 5);
insert into Cart values (7, 3, 4);
insert into Cart values (8, 6, 1);
insert into Cart values (8, 7, 5);
insert into Cart values (9, 1, 4);
insert into Cart values (9, 2, 1);
insert into Cart values (10, 6, 5);
insert into Cart values (10, 8, 4);
insert into Cart values (11, 11, 1);
insert into Cart values (12, 2, 5);
insert into Cart values (12, 1, 4);
insert into Cart values (13, 3, 1);
insert into Cart values (13, 8, 4);
insert into Cart values (14, 11, 1);
insert into Cart values (14, 2, 5);
insert into Cart values (14, 1, 4);
insert into Cart values (14, 3, 1);
insert into Cart values (15, 2, 2);
insert into Cart values (15, 1, 3);
insert into Cart values (15, 6, 6);
insert into Cart values (15, 1, 7);
insert into Cart values (16, 2, 10);
insert into Cart values (16, 3, 1);


select * from Transfer_history -- +
select * from Position; --+
select * from Department; -- +
select * from Employee; -- +
select * from Storage; -- +
select * from Shop_floor; -- +
select * from Product_list; -- +
select * from Discount; -- +
select * from Check_list; -- +
select * from Suppliers; -- +
select * from Supplies; -- +
select * from Categories; -- +
select * from Payments; -- +
select * from Client; -- +
select * from Cart; -- +


-- вычисление итогов работы продавцов промесячно, за квартал, за полгода, за год.
select Product_list.product_name, Employee.employee_id, Check_list.check_date, 
sum(Product_list.product_cost * Cart.amount) over(partition by Year(check_date), MONTH(check_date)) AS mont_res,
sum(Product_list.product_cost * Cart.amount) over ( partition by datepart(quarter, check_date), year(check_date) ) as quarter_res,
sum(Product_list.product_cost * Cart.amount) over ( partition by case when datepart(month, check_date) <= 6 then 1 else 2 end, year(check_date) ) as half_year_res,
sum(Product_list.product_cost * Cart.amount) over ( partition by year(check_date) ) as year_res
from Employee
join Check_list
on Employee.employee_id = Check_list.employee_id
join Cart
on Check_list.check_id = Cart.check_id
join Product_list 
on Product_list.product_id = Cart.product_id
group by Check_list.check_date, Employee.employee_id,Product_list.product_cost, Cart.amount, Product_list.product_name;


-- вычисление объема продаж
select Product_list.product_name, Employee.employee_id, 
sum(Cart.amount) as Count_of,
sum(Cart.amount) over() / (sum(Cart.amount) over(partition by Product_list.product_name)) as General,
sum(Cart.amount) over() / (sum(Cart.amount) over(partition by Product_list.product_name order by sum(Cart.amount))) as Maximum
from Employee
join Check_list
on Employee.employee_id = Check_list.employee_id
join Cart
on Check_list.check_id = Cart.check_id
join Product_list 
on Product_list.product_id = Cart.product_id
group by Check_list.check_date, Employee.employee_id,Product_list.product_cost, Cart.amount, Product_list.product_name;

-- суммы последних 6 заказов
declare @page int = 1;
declare @pageSize int = 2;

with rows_twe as( select Client.client_name,
sum(Cart.amount * Product_list.product_cost) as Items,
row_number() over ( order by Client.client_name ) as [RowNumber]
from Client
join Check_list
on Check_list.client_id = Client.client_id
join Cart
on Cart.check_id = Check_list.check_id
join Product_list
on Cart.product_id = Product_list.product_id
group by Client.client_name)
select client_name, Items from rows_twe
where [RowNumber] > (@page - 1) * @pageSize
  and [RowNumber] <= @page * @pageSize;


-- удаление дублей
with DeduplicatedTasks as (
    select
        product_name,
        row_number() over (partition by product_name order by product_name) as RowNum
    from
        Product_list
)
select
    product_name
from
    DeduplicatedTasks
where
    RowNum = 1;


-- вывод лучшего сотрнудника для клиента !!!
declare @name varchar(100) = 'alexey';

with Best_employee as(
select client_name ,Employee.employee_name,
sum(Cart.amount* Product_list.product_cost) over(partition by employee_name order by Cart.amount asc) as Name_emp
from Employee
join Check_list
on Check_list.employee_id = Employee.employee_id
join Client
on Check_list.client_id = Client.client_id
join Cart
on Cart.check_id = Check_list.check_id
join Product_list
on Product_list.product_id = Cart.product_id
group by client_name, employee_name, amount, product_cost)
select top(1) client_name,employee_name, Name_emp from Best_employee where client_name  = @name order by Name_emp desc


-- вычисление итогов работы продавцов промес€чно, за квартал, за полгода, за год.
select Product_list.product_name, Employee.employee_id, Check_list.check_date, 
sum(Product_list.product_cost * Cart.amount) over(partition by EXTRACT(year FROM check_date), EXTRACT(month FROM check_date)) AS mont_res,
sum(Product_list.product_cost * Cart.amount) over ( partition by CEIL(EXTRACT(MONTH FROM check_date) / 3), EXTRACT(year FROM check_date) ) as quarter_res,
sum(Product_list.product_cost * Cart.amount) over ( partition by case when EXTRACT(month FROM check_date) <= 6 then 1 else 2 end, EXTRACT(year FROM check_date) ) as half_year_res,
sum(Product_list.product_cost * Cart.amount) over ( partition by EXTRACT(year FROM check_date) ) as year_res
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

BEGIN
  FOR rows_twe IN (
    SELECT 
      Client_list.client_name,
      SUM(Cart.amount * Product_list.product_cost) AS Items
    FROM 
      Client_list
      JOIN Check_list 
      ON Check_list.client_id = Client_list.client_id
      JOIN Cart 
      ON Cart.check_id = Check_list.check_id
      JOIN Product_list 
      ON Cart.product_id = Product_list.product_id
    GROUP BY 
      Client_list.client_name
  )
  LOOP
      DBMS_OUTPUT.PUT_LINE(rows_twe.client_name || ', ' || rows_twe.Items);
  END LOOP;
END;
/


-- вывод лучшего сотрудника дл€ клиента
DECLARE
  name VARCHAR(100) := 'alexey';
BEGIN
  FOR Best_employee IN (
    SELECT 
      client_name,
      employee_name,
      SUM(Cart.amount * Product_list.product_cost) OVER (PARTITION BY employee_name ORDER BY Cart.amount ASC) AS Name_emp
    FROM 
      Employee
      JOIN Check_list ON Check_list.employee_id = Employee.employee_id
      JOIN Client_list ON Check_list.client_id = Client_list.client_id
      JOIN Cart ON Cart.check_id = Check_list.check_id
      JOIN Product_list ON Product_list.product_id = Cart.product_id
    GROUP BY 
      client_name, employee_name, amount, product_cost
    ORDER BY 
      Name_emp DESC
  )
  LOOP
    IF Best_employee.client_name = name THEN
      DBMS_OUTPUT.PUT_LINE(Best_employee.client_name || ', ' || Best_employee.employee_name || ', ' || Best_employee.Name_emp);
      EXIT; -- чтобы выйти после вывода лучшего сотрудника дл€ заданного клиента
    END IF;
  END LOOP;
END;
/
-- Get All Orders
CREATE PROCEDURE [dbo].[usp_GetAllOrders]
AS
BEGIN
    SELECT Id, CustomerName, Address, Status, CreatedAt
    FROM Orders
    ORDER BY CreatedAt DESC;
END
GO
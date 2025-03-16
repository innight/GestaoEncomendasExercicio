-- Get Order By ID
CREATE PROCEDURE [dbo].[usp_GetOrderById]
    @Id INT
AS
BEGIN
    SELECT Id, CustomerName, Address, Status, CreatedAt
    FROM Orders
    WHERE Id = @Id;
END
GO
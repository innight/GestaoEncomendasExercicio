-- Update Order Status
CREATE PROCEDURE [dbo].[usp_UpdateOrderStatus]
    @Id INT,
    @Status NVARCHAR(50)
AS
BEGIN
    UPDATE Orders
    SET Status = @Status
    WHERE Id = @Id;
END
GO
-- Cancel Order
CREATE PROCEDURE [dbo].[usp_CancelOrder]
    @Id INT
AS
BEGIN
    DELETE FROM Orders WHERE Id = @Id;
END
GO


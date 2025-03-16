-- Create Order
CREATE PROCEDURE [dbo].[usp_CreateOrder]
    @CustomerName NVARCHAR(100),
    @Address NVARCHAR(200),
    @Status NVARCHAR(50),
    @CreatedAt DATETIME
AS
BEGIN
    INSERT INTO Orders (CustomerName, Address, Status, CreatedAt)
    VALUES (@CustomerName, @Address, @Status, @CreatedAt);

    SELECT CAST(SCOPE_IDENTITY() AS INT);
END

GO
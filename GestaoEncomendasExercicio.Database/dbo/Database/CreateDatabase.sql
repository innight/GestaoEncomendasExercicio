-- Check if the database "EncomendasDapper" exists; if not, create it.
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'EncomendasDapper')
BEGIN
    CREATE DATABASE [EncomendasDapper];
END;
GO
-- Insert dummy data
INSERT INTO dbo.Orders (CustomerName, Address, Status)
VALUES
    ('João Silva', 'Rua da Liberdade, 123, Lisboa', 'Processamento'),
    ('Maria Fernandes', 'Avenida da República, 456, Porto', 'Concluído'),
    ('Carlos Pereira', 'Largo do Carmo, 789, Coimbra', 'Pendente'),
    ('Ana Sousa', 'Travessa das Flores, 101, Faro', 'Cancelado'),
    ('Pedro Costa', 'Rua dos Lusiadas, 202, Braga', 'Processamento');
GO
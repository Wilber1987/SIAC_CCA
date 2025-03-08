CREATE TABLE EmailAccounts (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Host NVARCHAR(100) NOT NULL,
    SentCount INT DEFAULT 0,
    LastUsedDate DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE)
);


INSERT INTO EmailAccounts (Email, Password, Host) VALUES
('envio5@cca.edu.ni', 'hcesrkwnythjlvcu', 'smtp.gmail.com'),
('envio4@cca.edu.ni', 'xwzcrndsvyiltogp', 'smtp.gmail.com'),
('envio3@cca.edu.ni', 'cpksqycdsuoytypa', 'smtp.gmail.com'),
('envio2@cca.edu.ni', 'jiudjfilraebipoh', 'smtp.gmail.com'),
('envio1@cca.edu.ni', 'rpvkltzqrtgmqvor', 'smtp.gmail.com'),
('envios@cca.edu.ni', 'arygcknlahvibkkn', 'smtp.gmail.com'),
('notificacionesportal@cca.edu.ni', 'czavspiafvhcttdg', 'smtp.gmail.com');
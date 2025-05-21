CREATE TABLE DataProtectionKeys
(
    Id           NVARCHAR(200) PRIMARY KEY,
    XmlData      NVARCHAR(MAX),
    LastModified DATETIME DEFAULT GETDATE()
);
CREATE TABLE [DataProtectionKeys]
(
    [Id]           NVARCHAR(200) NOT NULL PRIMARY KEY,
    [XmlData]      NVARCHAR(MAX) NULL,
    [LastModified] DATETIME      NOT NULL DEFAULT (GETUTCDATE())
);
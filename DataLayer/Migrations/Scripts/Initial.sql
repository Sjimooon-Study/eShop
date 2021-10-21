IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Country] (
    [CountryId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY ([CountryId])
);
GO

CREATE TABLE [Image] (
    [ImageId] int NOT NULL IDENTITY,
    [Path] nvarchar(max) NULL,
    CONSTRAINT [PK_Image] PRIMARY KEY ([ImageId])
);
GO

CREATE TABLE [Tag] (
    [TagId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Tag] PRIMARY KEY ([TagId])
);
GO

CREATE TABLE [Address] (
    [AddressId] int NOT NULL IDENTITY,
    [StreetName] nvarchar(max) NOT NULL,
    [StreetNumber] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NOT NULL,
    [Zip] nvarchar(max) NOT NULL,
    [CountryId] int NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY ([AddressId]),
    CONSTRAINT [FK_Address_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Country] ([CountryId]) ON DELETE CASCADE
);
GO

CREATE TABLE [RailwayCompany] (
    [RailwayCompanyId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [CountryId] int NOT NULL,
    CONSTRAINT [PK_RailwayCompany] PRIMARY KEY ([RailwayCompanyId]),
    CONSTRAINT [FK_RailwayCompany_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Country] ([CountryId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Users] (
    [UserId] int NOT NULL IDENTITY,
    [UserName] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [AddressId] int NULL,
    [IsAdmin] bit NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_Users_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([AddressId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Products] (
    [ProductId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Price] decimal(8,2) NOT NULL,
    [AmountInStock] bigint NOT NULL,
    [TagId] nvarchar(450) NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [Interface] int NULL,
    [Sound] bit NULL,
    [Control] int NULL,
    [LocoType] int NULL,
    [AutoCoupling] bit NULL,
    [NumOfDrivenAxels] int NULL,
    [DigitalDecoderId] int NULL,
    [Scale] int NULL,
    [Epoch] int NULL,
    [Length] real NULL,
    [NumOfAxels] int NULL,
    [RailwayCompanyId] int NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId]),
    CONSTRAINT [FK_Products_Products_DigitalDecoderId] FOREIGN KEY ([DigitalDecoderId]) REFERENCES [Products] ([ProductId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Products_RailwayCompany_RailwayCompanyId] FOREIGN KEY ([RailwayCompanyId]) REFERENCES [RailwayCompany] ([RailwayCompanyId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Products_Tag_TagId] FOREIGN KEY ([TagId]) REFERENCES [Tag] ([TagId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ImageProduct] (
    [ImagesImageId] int NOT NULL,
    [ProductsProductId] int NOT NULL,
    CONSTRAINT [PK_ImageProduct] PRIMARY KEY ([ImagesImageId], [ProductsProductId]),
    CONSTRAINT [FK_ImageProduct_Image_ImagesImageId] FOREIGN KEY ([ImagesImageId]) REFERENCES [Image] ([ImageId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ImageProduct_Products_ProductsProductId] FOREIGN KEY ([ProductsProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CountryId', N'Name') AND [object_id] = OBJECT_ID(N'[Country]'))
    SET IDENTITY_INSERT [Country] ON;
INSERT INTO [Country] ([CountryId], [Name])
VALUES (1, N'Denmark'),
(2, N'Germany'),
(3, N'Switzerland');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CountryId', N'Name') AND [object_id] = OBJECT_ID(N'[Country]'))
    SET IDENTITY_INSERT [Country] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ImageId', N'Path') AND [object_id] = OBJECT_ID(N'[Image]'))
    SET IDENTITY_INSERT [Image] ON;
INSERT INTO [Image] ([ImageId], [Path])
VALUES (1, N'2021_10_20_b51acd9b-05bc-415d-86ac-c3c2e17b0012.jpeg'),
(2, N'2021_10_20_c74b3599-6852-459a-9ee7-1b9129dcf3e0.jpeg'),
(3, N'2021_10_20_06e1d3fa-8751-4c0e-9fac-b830384b0051.jpeg'),
(4, N'2021_10_20_8aa35a49-50cd-4950-8689-38d547c35c91.jpeg'),
(5, N'2021_10_20_a6c0169e-1d21-47ce-acef-b0ac88de5990.jpeg'),
(6, N'2021_10_20_de2a7fc8-702f-4965-893e-816a22a4f403.jpeg'),
(7, N'2021_10_20_eb718ad5-202f-4fe7-9f36-772da6d3b9a4.jpeg'),
(8, N'2021_10_20_fa6c395b-d19f-44bc-9afc-d61d0905fe81.jpeg');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ImageId', N'Path') AND [object_id] = OBJECT_ID(N'[Image]'))
    SET IDENTITY_INSERT [Image] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'AmountInStock', N'Description', N'Discriminator', N'Interface', N'Name', N'Price', N'Sound', N'TagId') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] ON;
INSERT INTO [Products] ([ProductId], [AmountInStock], [Description], [Discriminator], [Interface], [Name], [Price], [Sound], [TagId])
VALUES (1, CAST(23 AS bigint), N'Suitable for Gauge H0. The decoder is equipped with the RailCom® function. Maximum motor current: 1.2 A.', N'DigitalDecoder', 0, N'PluX22 sound decoder (NEM 658)', 92.4, CAST(1 AS bit), NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'AmountInStock', N'Description', N'Discriminator', N'Interface', N'Name', N'Price', N'Sound', N'TagId') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'TagId') AND [object_id] = OBJECT_ID(N'[Tag]'))
    SET IDENTITY_INSERT [Tag] ON;
INSERT INTO [Tag] ([TagId])
VALUES (N'Sale'),
(N'New');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'TagId') AND [object_id] = OBJECT_ID(N'[Tag]'))
    SET IDENTITY_INSERT [Tag] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'AddressId', N'Email', N'FirstName', N'IsAdmin', N'LastName', N'Password', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([UserId], [AddressId], [Email], [FirstName], [IsAdmin], [LastName], [Password], [UserName])
VALUES (1, NULL, NULL, NULL, CAST(1 AS bit), NULL, N'admin', N'admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'AddressId', N'Email', N'FirstName', N'IsAdmin', N'LastName', N'Password', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RailwayCompanyId', N'CountryId', N'Name') AND [object_id] = OBJECT_ID(N'[RailwayCompany]'))
    SET IDENTITY_INSERT [RailwayCompany] ON;
INSERT INTO [RailwayCompany] ([RailwayCompanyId], [CountryId], [Name])
VALUES (1, 1, N'DSB'),
(2, 2, N'DB'),
(3, 2, N'DR'),
(4, 3, N'SBB');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RailwayCompanyId', N'CountryId', N'Name') AND [object_id] = OBJECT_ID(N'[RailwayCompany]'))
    SET IDENTITY_INSERT [RailwayCompany] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'AmountInStock', N'AutoCoupling', N'Control', N'Description', N'DigitalDecoderId', N'Discriminator', N'Epoch', N'Length', N'LocoType', N'Name', N'NumOfAxels', N'NumOfDrivenAxels', N'Price', N'RailwayCompanyId', N'Scale', N'TagId') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] ON;
INSERT INTO [Products] ([ProductId], [AmountInStock], [AutoCoupling], [Control], [Description], [DigitalDecoderId], [Discriminator], [Epoch], [Length], [LocoType], [Name], [NumOfAxels], [NumOfDrivenAxels], [Price], [RailwayCompanyId], [Scale], [TagId])
VALUES (2, CAST(5 AS bigint), CAST(0 AS bit), 2, N'The 023 series was a true all-round genius. The locomotive hauled commuter trains, fast and express trains. Sometimes they hauled even freight trains. The newly designed locomotive of the class 023 (which until 1968 was designated class 23) was being used even in the epoch IV. On Dec. 31 1971, 76 locomotives were a permanent part of the rolling stock of the DB and without exception they were stationed at the three railway depots Saarbrücken, Kaiserslautern and Crailsheim.', NULL, N'Locomotive', 3, CAST(24.5 AS real), 0, N'BR 023 040-9', 9, 4, 229.9, 2, 0, N'New');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'AmountInStock', N'AutoCoupling', N'Control', N'Description', N'DigitalDecoderId', N'Discriminator', N'Epoch', N'Length', N'LocoType', N'Name', N'NumOfAxels', N'NumOfDrivenAxels', N'Price', N'RailwayCompanyId', N'Scale', N'TagId') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'AmountInStock', N'AutoCoupling', N'Control', N'Description', N'DigitalDecoderId', N'Discriminator', N'Epoch', N'Length', N'LocoType', N'Name', N'NumOfAxels', N'NumOfDrivenAxels', N'Price', N'RailwayCompanyId', N'Scale', N'TagId') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] ON;
INSERT INTO [Products] ([ProductId], [AmountInStock], [AutoCoupling], [Control], [Description], [DigitalDecoderId], [Discriminator], [Epoch], [Length], [LocoType], [Name], [NumOfAxels], [NumOfDrivenAxels], [Price], [RailwayCompanyId], [Scale], [TagId])
VALUES (4, CAST(1 AS bigint), CAST(0 AS bit), 2, N'In the period between 1942 to 1950, over 7000 units of the class 52 war locomotive were built. These were constructed with as little effort as possible and savings were also made on the material wherever possible. With a weight of 84 tons, the loco achieved an output of 1,192 kW and a top speed of 80 km / h. The Deutsche Bundesbahn mainly got rid of the locomotives as early as 1953 - since it had sufficient machines of the series 50 and series 44 to haul the heavy goods trains. Only a few locomotives built in 1945 remained with the DB until 1962.', NULL, N'Locomotive', 2, CAST(26.5 AS real), 0, N'BR 52', 10, 4, 319.9, 2, 0, N'New');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'AmountInStock', N'AutoCoupling', N'Control', N'Description', N'DigitalDecoderId', N'Discriminator', N'Epoch', N'Length', N'LocoType', N'Name', N'NumOfAxels', N'NumOfDrivenAxels', N'Price', N'RailwayCompanyId', N'Scale', N'TagId') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'AmountInStock', N'AutoCoupling', N'Control', N'Description', N'DigitalDecoderId', N'Discriminator', N'Epoch', N'Length', N'LocoType', N'Name', N'NumOfAxels', N'NumOfDrivenAxels', N'Price', N'RailwayCompanyId', N'Scale', N'TagId') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] ON;
INSERT INTO [Products] ([ProductId], [AmountInStock], [AutoCoupling], [Control], [Description], [DigitalDecoderId], [Discriminator], [Epoch], [Length], [LocoType], [Name], [NumOfAxels], [NumOfDrivenAxels], [Price], [RailwayCompanyId], [Scale], [TagId])
VALUES (3, CAST(2 AS bigint), CAST(0 AS bit), 3, N'In 1992, the first locomotive Re 460 of the Swiss Federal Railways rolled out of the factory halls of the companies SLM and BBC in Oerlikon, Switzerland. The locomotive became known to the public as "Lok 2000". It stands for fast and modern passenger transport in Switzerland. An eye-catching and particularly aerodynamic design with a large front window, roof cladding and beads on the side wall make the class 460 visually an unbeatable rail vehicle.', 1, N'Locomotive', 5, CAST(21.2 AS real), 2, N'Re 460 068-0', 4, 4, 321.9, 4, 0, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'AmountInStock', N'AutoCoupling', N'Control', N'Description', N'DigitalDecoderId', N'Discriminator', N'Epoch', N'Length', N'LocoType', N'Name', N'NumOfAxels', N'NumOfDrivenAxels', N'Price', N'RailwayCompanyId', N'Scale', N'TagId') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ImagesImageId', N'ProductsProductId') AND [object_id] = OBJECT_ID(N'[ImageProduct]'))
    SET IDENTITY_INSERT [ImageProduct] ON;
INSERT INTO [ImageProduct] ([ImagesImageId], [ProductsProductId])
VALUES (2, 2),
(3, 4),
(4, 4),
(5, 4),
(6, 4),
(7, 4),
(1, 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ImagesImageId', N'ProductsProductId') AND [object_id] = OBJECT_ID(N'[ImageProduct]'))
    SET IDENTITY_INSERT [ImageProduct] OFF;
GO

CREATE INDEX [IX_Address_CountryId] ON [Address] ([CountryId]);
GO

CREATE INDEX [IX_ImageProduct_ProductsProductId] ON [ImageProduct] ([ProductsProductId]);
GO

CREATE INDEX [IX_Products_DigitalDecoderId] ON [Products] ([DigitalDecoderId]);
GO

CREATE INDEX [IX_Products_RailwayCompanyId] ON [Products] ([RailwayCompanyId]);
GO

CREATE INDEX [IX_Products_TagId] ON [Products] ([TagId]);
GO

CREATE INDEX [IX_RailwayCompany_CountryId] ON [RailwayCompany] ([CountryId]);
GO

CREATE INDEX [IX_Users_AddressId] ON [Users] ([AddressId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211021202254_Initial', N'5.0.10');
GO

COMMIT;
GO

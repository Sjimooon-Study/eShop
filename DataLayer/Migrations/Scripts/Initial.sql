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
    [Url] nvarchar(max) NULL,
    CONSTRAINT [PK_Image] PRIMARY KEY ([ImageId])
);
GO

CREATE TABLE [Tag] (
    [TagId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Tag] PRIMARY KEY ([TagId])
);
GO

CREATE TABLE [RailwayCompany] (
    [RailwayCompanyId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [CountryId] int NULL,
    CONSTRAINT [PK_RailwayCompany] PRIMARY KEY ([RailwayCompanyId]),
    CONSTRAINT [FK_RailwayCompany_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Country] ([CountryId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Product] (
    [ProductId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Price] decimal(8,2) NOT NULL,
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
    CONSTRAINT [PK_Product] PRIMARY KEY ([ProductId]),
    CONSTRAINT [FK_Product_Product_DigitalDecoderId] FOREIGN KEY ([DigitalDecoderId]) REFERENCES [Product] ([ProductId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Product_RailwayCompany_RailwayCompanyId] FOREIGN KEY ([RailwayCompanyId]) REFERENCES [RailwayCompany] ([RailwayCompanyId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Product_Tag_TagId] FOREIGN KEY ([TagId]) REFERENCES [Tag] ([TagId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ImageProduct] (
    [ImagesImageId] int NOT NULL,
    [ProductsProductId] int NOT NULL,
    CONSTRAINT [PK_ImageProduct] PRIMARY KEY ([ImagesImageId], [ProductsProductId]),
    CONSTRAINT [FK_ImageProduct_Image_ImagesImageId] FOREIGN KEY ([ImagesImageId]) REFERENCES [Image] ([ImageId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ImageProduct_Product_ProductsProductId] FOREIGN KEY ([ProductsProductId]) REFERENCES [Product] ([ProductId]) ON DELETE CASCADE
);
GO

CREATE TABLE [StockStatus] (
    [StockStatusId] int NOT NULL IDENTITY,
    [Amount] bigint NOT NULL,
    [NextStock] datetime2 NULL,
    [ProductId] int NOT NULL,
    CONSTRAINT [PK_StockStatus] PRIMARY KEY ([StockStatusId]),
    CONSTRAINT [FK_StockStatus_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([ProductId]) ON DELETE CASCADE
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

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ImageId', N'Url') AND [object_id] = OBJECT_ID(N'[Image]'))
    SET IDENTITY_INSERT [Image] ON;
INSERT INTO [Image] ([ImageId], [Url])
VALUES (1, N'https://www.roco.cc/doc/idimages/def2/1633611600/123106022013017006010001016009105021031014117.jpg'),
(2, N'https://www.roco.cc/doc/idimages/def2/1633611600/123109024010020014010001016009105021031014117.jpg'),
(3, N'https://www.roco.cc/doc/idimages/def2/1633611600/123109026011022009010001016009105021031014117.jpg'),
(4, N'https://www.roco.cc/doc/idimages/def2/1633611600/123109023008017012010001016009105021031014117.jpg'),
(5, N'https://www.roco.cc/doc/idimages/def2/1633615200/126105029012017019089002030013103027028015121010010.jpg');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ImageId', N'Url') AND [object_id] = OBJECT_ID(N'[Image]'))
    SET IDENTITY_INSERT [Image] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'Description', N'Discriminator', N'Interface', N'Name', N'Price', N'Sound', N'TagId') AND [object_id] = OBJECT_ID(N'[Product]'))
    SET IDENTITY_INSERT [Product] ON;
INSERT INTO [Product] ([ProductId], [Description], [Discriminator], [Interface], [Name], [Price], [Sound], [TagId])
VALUES (1, N'Suitable for Gauge H0. The decoder is equipped with the RailCom® function. Maximum motor current: 1.2 A.', N'DigitalDecoder', 0, N'PluX22 sound decoder (NEM 658)', 92.4, CAST(1 AS bit), NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'Description', N'Discriminator', N'Interface', N'Name', N'Price', N'Sound', N'TagId') AND [object_id] = OBJECT_ID(N'[Product]'))
    SET IDENTITY_INSERT [Product] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'TagId') AND [object_id] = OBJECT_ID(N'[Tag]'))
    SET IDENTITY_INSERT [Tag] ON;
INSERT INTO [Tag] ([TagId])
VALUES (N'Sale'),
(N'New');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'TagId') AND [object_id] = OBJECT_ID(N'[Tag]'))
    SET IDENTITY_INSERT [Tag] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ImagesImageId', N'ProductsProductId') AND [object_id] = OBJECT_ID(N'[ImageProduct]'))
    SET IDENTITY_INSERT [ImageProduct] ON;
INSERT INTO [ImageProduct] ([ImagesImageId], [ProductsProductId])
VALUES (1, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ImagesImageId', N'ProductsProductId') AND [object_id] = OBJECT_ID(N'[ImageProduct]'))
    SET IDENTITY_INSERT [ImageProduct] OFF;
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

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StockStatusId', N'Amount', N'NextStock', N'ProductId') AND [object_id] = OBJECT_ID(N'[StockStatus]'))
    SET IDENTITY_INSERT [StockStatus] ON;
INSERT INTO [StockStatus] ([StockStatusId], [Amount], [NextStock], [ProductId])
VALUES (1, CAST(23 AS bigint), '2021-11-08T12:29:26.1105528+01:00', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StockStatusId', N'Amount', N'NextStock', N'ProductId') AND [object_id] = OBJECT_ID(N'[StockStatus]'))
    SET IDENTITY_INSERT [StockStatus] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'AutoCoupling', N'Control', N'Description', N'DigitalDecoderId', N'Discriminator', N'Epoch', N'Length', N'LocoType', N'Name', N'NumOfAxels', N'NumOfDrivenAxels', N'Price', N'RailwayCompanyId', N'Scale', N'TagId') AND [object_id] = OBJECT_ID(N'[Product]'))
    SET IDENTITY_INSERT [Product] ON;
INSERT INTO [Product] ([ProductId], [AutoCoupling], [Control], [Description], [DigitalDecoderId], [Discriminator], [Epoch], [Length], [LocoType], [Name], [NumOfAxels], [NumOfDrivenAxels], [Price], [RailwayCompanyId], [Scale], [TagId])
VALUES (2, CAST(0 AS bit), 3, N'The 023 series was a true all-round genius. The locomotive hauled commuter trains, fast and express trains. Sometimes they hauled even freight trains. The newly designed locomotive of the class 023 (which until 1968 was designated class 23) was being used even in the epoch IV. On Dec. 31 1971, 76 locomotives were a permanent part of the rolling stock of the DB and without exception they were stationed at the three railway depots Saarbrücken, Kaiserslautern and Crailsheim.', NULL, N'Locomotive', 4, CAST(24.5 AS real), 1, N'BR 023 040-9', 9, 4, 229.9, 2, 1, N'New');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'AutoCoupling', N'Control', N'Description', N'DigitalDecoderId', N'Discriminator', N'Epoch', N'Length', N'LocoType', N'Name', N'NumOfAxels', N'NumOfDrivenAxels', N'Price', N'RailwayCompanyId', N'Scale', N'TagId') AND [object_id] = OBJECT_ID(N'[Product]'))
    SET IDENTITY_INSERT [Product] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'AutoCoupling', N'Control', N'Description', N'DigitalDecoderId', N'Discriminator', N'Epoch', N'Length', N'LocoType', N'Name', N'NumOfAxels', N'NumOfDrivenAxels', N'Price', N'RailwayCompanyId', N'Scale', N'TagId') AND [object_id] = OBJECT_ID(N'[Product]'))
    SET IDENTITY_INSERT [Product] ON;
INSERT INTO [Product] ([ProductId], [AutoCoupling], [Control], [Description], [DigitalDecoderId], [Discriminator], [Epoch], [Length], [LocoType], [Name], [NumOfAxels], [NumOfDrivenAxels], [Price], [RailwayCompanyId], [Scale], [TagId])
VALUES (4, CAST(0 AS bit), 3, N'In the period between 1942 to 1950, over 7000 units of the class 52 war locomotive were built. These were constructed with as little effort as possible and savings were also made on the material wherever possible. With a weight of 84 tons, the loco achieved an output of 1,192 kW and a top speed of 80 km / h. The Deutsche Bundesbahn mainly got rid of the locomotives as early as 1953 - since it had sufficient machines of the series 50 and series 44 to haul the heavy goods trains. Only a few locomotives built in 1945 remained with the DB until 1962.', NULL, N'Locomotive', 3, CAST(26.5 AS real), 1, N'BR 52', 10, 4, 319.9, 2, 1, N'New');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'AutoCoupling', N'Control', N'Description', N'DigitalDecoderId', N'Discriminator', N'Epoch', N'Length', N'LocoType', N'Name', N'NumOfAxels', N'NumOfDrivenAxels', N'Price', N'RailwayCompanyId', N'Scale', N'TagId') AND [object_id] = OBJECT_ID(N'[Product]'))
    SET IDENTITY_INSERT [Product] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'AutoCoupling', N'Control', N'Description', N'DigitalDecoderId', N'Discriminator', N'Epoch', N'Length', N'LocoType', N'Name', N'NumOfAxels', N'NumOfDrivenAxels', N'Price', N'RailwayCompanyId', N'Scale', N'TagId') AND [object_id] = OBJECT_ID(N'[Product]'))
    SET IDENTITY_INSERT [Product] ON;
INSERT INTO [Product] ([ProductId], [AutoCoupling], [Control], [Description], [DigitalDecoderId], [Discriminator], [Epoch], [Length], [LocoType], [Name], [NumOfAxels], [NumOfDrivenAxels], [Price], [RailwayCompanyId], [Scale], [TagId])
VALUES (3, CAST(0 AS bit), 4, N'In 1992, the first locomotive Re 460 of the Swiss Federal Railways rolled out of the factory halls of the companies SLM and BBC in Oerlikon, Switzerland. The locomotive became known to the public as "Lok 2000". It stands for fast and modern passenger transport in Switzerland. An eye-catching and particularly aerodynamic design with a large front window, roof cladding and beads on the side wall make the class 460 visually an unbeatable rail vehicle.', 1, N'Locomotive', 6, CAST(21.2 AS real), 3, N'Re 460 068-0', 4, 4, 321.9, 4, 1, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'AutoCoupling', N'Control', N'Description', N'DigitalDecoderId', N'Discriminator', N'Epoch', N'Length', N'LocoType', N'Name', N'NumOfAxels', N'NumOfDrivenAxels', N'Price', N'RailwayCompanyId', N'Scale', N'TagId') AND [object_id] = OBJECT_ID(N'[Product]'))
    SET IDENTITY_INSERT [Product] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ImagesImageId', N'ProductsProductId') AND [object_id] = OBJECT_ID(N'[ImageProduct]'))
    SET IDENTITY_INSERT [ImageProduct] ON;
INSERT INTO [ImageProduct] ([ImagesImageId], [ProductsProductId])
VALUES (3, 2),
(4, 4),
(5, 4),
(2, 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ImagesImageId', N'ProductsProductId') AND [object_id] = OBJECT_ID(N'[ImageProduct]'))
    SET IDENTITY_INSERT [ImageProduct] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StockStatusId', N'Amount', N'NextStock', N'ProductId') AND [object_id] = OBJECT_ID(N'[StockStatus]'))
    SET IDENTITY_INSERT [StockStatus] ON;
INSERT INTO [StockStatus] ([StockStatusId], [Amount], [NextStock], [ProductId])
VALUES (2, CAST(5 AS bigint), '2021-12-08T12:29:26.1143276+01:00', 2),
(4, CAST(1 AS bigint), '2022-05-08T12:29:26.1143395+02:00', 4),
(3, CAST(2 AS bigint), '2021-10-24T12:29:26.1143365+02:00', 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StockStatusId', N'Amount', N'NextStock', N'ProductId') AND [object_id] = OBJECT_ID(N'[StockStatus]'))
    SET IDENTITY_INSERT [StockStatus] OFF;
GO

CREATE INDEX [IX_ImageProduct_ProductsProductId] ON [ImageProduct] ([ProductsProductId]);
GO

CREATE INDEX [IX_Product_DigitalDecoderId] ON [Product] ([DigitalDecoderId]);
GO

CREATE INDEX [IX_Product_RailwayCompanyId] ON [Product] ([RailwayCompanyId]);
GO

CREATE INDEX [IX_Product_TagId] ON [Product] ([TagId]);
GO

CREATE INDEX [IX_RailwayCompany_CountryId] ON [RailwayCompany] ([CountryId]);
GO

CREATE UNIQUE INDEX [IX_StockStatus_ProductId] ON [StockStatus] ([ProductId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211008102926_Initial', N'5.0.10');
GO

COMMIT;
GO


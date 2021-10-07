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

CREATE TABLE [Image] (
    [ImageId] int NOT NULL IDENTITY,
    [Url] nvarchar(max) NULL,
    CONSTRAINT [PK_Image] PRIMARY KEY ([ImageId])
);
GO

CREATE TABLE [Tag] (
    [TagId] int NOT NULL IDENTITY,
    [Text] nvarchar(max) NULL,
    CONSTRAINT [PK_Tag] PRIMARY KEY ([TagId])
);
GO

CREATE TABLE [Product] (
    [ProductId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Price] decimal(8,2) NOT NULL,
    [TagId] int NULL,
    [Discriminator] nvarchar(max) NOT NULL,
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
    CONSTRAINT [PK_Product] PRIMARY KEY ([ProductId]),
    CONSTRAINT [FK_Product_Product_DigitalDecoderId] FOREIGN KEY ([DigitalDecoderId]) REFERENCES [Product] ([ProductId]) ON DELETE NO ACTION,
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

CREATE INDEX [IX_ImageProduct_ProductsProductId] ON [ImageProduct] ([ProductsProductId]);
GO

CREATE INDEX [IX_Product_DigitalDecoderId] ON [Product] ([DigitalDecoderId]);
GO

CREATE INDEX [IX_Product_TagId] ON [Product] ([TagId]);
GO

CREATE UNIQUE INDEX [IX_StockStatus_ProductId] ON [StockStatus] ([ProductId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211007082632_Initial', N'5.0.10');
GO

COMMIT;
GO


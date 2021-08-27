
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/29/2020 15:01:48
-- Generated from EDMX file: C:\Users\USER\Dropbox\bookHotel_Version_2\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Hotel_Booking];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CustomerCreditCard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CreditCardSet] DROP CONSTRAINT [FK_CustomerCreditCard];
GO
IF OBJECT_ID(N'[dbo].[FK_CurrencyCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerSet] DROP CONSTRAINT [FK_CurrencyCustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerEvaluation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EvaluationSet] DROP CONSTRAINT [FK_CustomerEvaluation];
GO
IF OBJECT_ID(N'[dbo].[FK_HotelOwnerHotel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HotelSet] DROP CONSTRAINT [FK_HotelOwnerHotel];
GO
IF OBJECT_ID(N'[dbo].[FK_HotelEvaluation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EvaluationSet] DROP CONSTRAINT [FK_HotelEvaluation];
GO
IF OBJECT_ID(N'[dbo].[FK_HotelRoom]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoomSet] DROP CONSTRAINT [FK_HotelRoom];
GO
IF OBJECT_ID(N'[dbo].[FK_CurrencyHotelOwner]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HotelOwnerSet] DROP CONSTRAINT [FK_CurrencyHotelOwner];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerBooking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BookingSet] DROP CONSTRAINT [FK_CustomerBooking];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomBooking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BookingSet] DROP CONSTRAINT [FK_RoomBooking];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomService]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceSet] DROP CONSTRAINT [FK_RoomService];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CustomerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerSet];
GO
IF OBJECT_ID(N'[dbo].[CreditCardSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CreditCardSet];
GO
IF OBJECT_ID(N'[dbo].[HotelSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HotelSet];
GO
IF OBJECT_ID(N'[dbo].[BookingSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BookingSet];
GO
IF OBJECT_ID(N'[dbo].[HotelOwnerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HotelOwnerSet];
GO
IF OBJECT_ID(N'[dbo].[CurrencySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CurrencySet];
GO
IF OBJECT_ID(N'[dbo].[EvaluationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EvaluationSet];
GO
IF OBJECT_ID(N'[dbo].[ServiceSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServiceSet];
GO
IF OBJECT_ID(N'[dbo].[RoomSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoomSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CustomerSet'
CREATE TABLE [dbo].[CustomerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Email] nvarchar(30) UNIQUE NOT NULL,
    [Phone] nvarchar(20) UNIQUE NOT NULL,
    [Password] nvarchar(30)  NOT NULL,
    [StatusName] nvarchar(8)  NOT NULL,
    [BookingsMadeTotal] smallint  NOT NULL,
    [Currency_ISOcode] nvarchar(3)  NULL
);
GO

-- Creating table 'CreditCardSet'
CREATE TABLE [dbo].[CreditCardSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CreditCardNumber] nvarchar(16)  NOT NULL,
    [CreditCardType] nvarchar(20)  NOT NULL,
    [CardholderName] nvarchar(50)  NOT NULL,
    [Customer_Id] int  NULL
);
GO

-- Creating table 'HotelSet'
CREATE TABLE [dbo].[HotelSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HotelName] nvarchar(30)  NOT NULL,
    [StreetNameNumber] nvarchar(40)  NOT NULL,
    [PostalCode] nvarchar(8)  NOT NULL,
    [City] nvarchar(20)  NOT NULL,
    [Country] nvarchar(20)  NOT NULL,
    [HotelPhotoUrl] nvarchar(50)  NULL,	
    [AverageRating] float  NULL,
    [HotelOwner_Id] int  NULL
);
GO

-- Creating table 'BookingSet'
CREATE TABLE [dbo].[BookingSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CheckIn] datetime  NOT NULL,
    [CheckOut] datetime  NOT NULL,
    [PersonsNumber] smallint  NOT NULL,
    [AmountPaid] float  NOT NULL,
    [DateMade] datetime  NOT NULL,
    [Evaluated] bit  NOT NULL,
    [CustomerId] int  NULL,
    [RoomId] int  NULL
);
GO

-- Creating table 'HotelOwnerSet'
CREATE TABLE [dbo].[HotelOwnerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(30) UNIQUE NOT NULL,
    [Password] nvarchar(30)  NOT NULL,
    [Currency_ISOcode] nvarchar(3)  NULL
);
GO

-- Creating table 'CurrencySet'
CREATE TABLE [dbo].[CurrencySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ISOcode] nvarchar(3)  NOT NULL,
    [CurrencyName] nvarchar(25) UNIQUE NOT NULL
);
GO

-- Creating table 'EvaluationSet'
CREATE TABLE [dbo].[EvaluationSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EvaluationDate] datetime  NOT NULL,
    [Rating] float  NOT NULL,
    [Comment] nvarchar(500)  NOT NULL,
    [Customer_Id] int  NULL,
    [Hotel_Id] int  NULL
);
GO

-- Creating table 'ServiceSet'
CREATE TABLE [dbo].[ServiceSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ServiceName] nvarchar(20)  NOT NULL,
    [Room_Id] int  NULL
);
GO

-- Creating table 'RoomSet'
CREATE TABLE [dbo].[RoomSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoomName] nvarchar(40)  NOT NULL,
    [Rate] float  NOT NULL,
    [MaxPersons] smallint  NOT NULL,
    [RoomNumber] nvarchar(5)  NOT NULL,
    [RoomPhotoUrl] nvarchar(50)  NULL,
    [Hotel_Id] int  NULL
);
GO


-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CustomerSet'
ALTER TABLE [dbo].[CustomerSet]
ADD CONSTRAINT [PK_CustomerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [CreditCardNumber] in table 'CreditCardSet'
ALTER TABLE [dbo].[CreditCardSet]
ADD CONSTRAINT [PK_CreditCardSet]
    PRIMARY KEY CLUSTERED ([CreditCardNumber] ASC);
GO

-- Creating primary key on [Id] in table 'HotelSet'
ALTER TABLE [dbo].[HotelSet]
ADD CONSTRAINT [PK_HotelSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BookingSet'
ALTER TABLE [dbo].[BookingSet]
ADD CONSTRAINT [PK_BookingSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HotelOwnerSet'
ALTER TABLE [dbo].[HotelOwnerSet]
ADD CONSTRAINT [PK_HotelOwnerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ISOcode] in table 'CurrencySet'
ALTER TABLE [dbo].[CurrencySet]
ADD CONSTRAINT [PK_CurrencySet]
    PRIMARY KEY CLUSTERED ([ISOcode] ASC);
GO

-- Creating primary key on [Id] in table 'EvaluationSet'
ALTER TABLE [dbo].[EvaluationSet]
ADD CONSTRAINT [PK_EvaluationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ServiceSet'
ALTER TABLE [dbo].[ServiceSet]
ADD CONSTRAINT [PK_ServiceSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoomSet'
ALTER TABLE [dbo].[RoomSet]
ADD CONSTRAINT [PK_RoomSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Customer_Id] in table 'CreditCardSet'
ALTER TABLE [dbo].[CreditCardSet]
ADD CONSTRAINT [FK_CustomerCreditCard]
    FOREIGN KEY ([Customer_Id])
    REFERENCES [dbo].[CustomerSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerCreditCard'
CREATE INDEX [IX_FK_CustomerCreditCard]
ON [dbo].[CreditCardSet]
    ([Customer_Id]);
GO

-- Creating foreign key on [Currency_ISOcode] in table 'CustomerSet'
ALTER TABLE [dbo].[CustomerSet]
ADD CONSTRAINT [FK_CurrencyCustomer]
    FOREIGN KEY ([Currency_ISOcode])
    REFERENCES [dbo].[CurrencySet]
        ([ISOcode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CurrencyCustomer'
CREATE INDEX [IX_FK_CurrencyCustomer]
ON [dbo].[CustomerSet]
    ([Currency_ISOcode]);
GO

-- Creating foreign key on [Customer_Id] in table 'EvaluationSet'
ALTER TABLE [dbo].[EvaluationSet]
ADD CONSTRAINT [FK_CustomerEvaluation]
    FOREIGN KEY ([Customer_Id])
    REFERENCES [dbo].[CustomerSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerEvaluation'
CREATE INDEX [IX_FK_CustomerEvaluation]
ON [dbo].[EvaluationSet]
    ([Customer_Id]);
GO

-- Creating foreign key on [HotelOwner_Id] in table 'HotelSet'
ALTER TABLE [dbo].[HotelSet]
ADD CONSTRAINT [FK_HotelOwnerHotel]
    FOREIGN KEY ([HotelOwner_Id])
    REFERENCES [dbo].[HotelOwnerSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HotelOwnerHotel'
CREATE INDEX [IX_FK_HotelOwnerHotel]
ON [dbo].[HotelSet]
    ([HotelOwner_Id]);
GO

-- Creating foreign key on [Hotel_Id] in table 'EvaluationSet'
ALTER TABLE [dbo].[EvaluationSet]
ADD CONSTRAINT [FK_HotelEvaluation]
    FOREIGN KEY ([Hotel_Id])
    REFERENCES [dbo].[HotelSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HotelEvaluation'
CREATE INDEX [IX_FK_HotelEvaluation]
ON [dbo].[EvaluationSet]
    ([Hotel_Id]);
GO

-- Creating foreign key on [Hotel_Id] in table 'RoomSet'
ALTER TABLE [dbo].[RoomSet]
ADD CONSTRAINT [FK_HotelRoom]
    FOREIGN KEY ([Hotel_Id])
    REFERENCES [dbo].[HotelSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HotelRoom'
CREATE INDEX [IX_FK_HotelRoom]
ON [dbo].[RoomSet]
    ([Hotel_Id]);
GO

-- Creating foreign key on [Currency_ISOcode] in table 'HotelOwnerSet'
ALTER TABLE [dbo].[HotelOwnerSet]
ADD CONSTRAINT [FK_CurrencyHotelOwner]
    FOREIGN KEY ([Currency_ISOcode])
    REFERENCES [dbo].[CurrencySet]
        ([ISOcode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CurrencyHotelOwner'
CREATE INDEX [IX_FK_CurrencyHotelOwner]
ON [dbo].[HotelOwnerSet]
    ([Currency_ISOcode]);
GO

-- Creating foreign key on [CustomerId] in table 'BookingSet'
ALTER TABLE [dbo].[BookingSet]
ADD CONSTRAINT [FK_CustomerBooking]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[CustomerSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerBooking'
CREATE INDEX [IX_FK_CustomerBooking]
ON [dbo].[BookingSet]
    ([CustomerId]);
GO

-- Creating foreign key on [RoomId] in table 'BookingSet'
ALTER TABLE [dbo].[BookingSet]
ADD CONSTRAINT [FK_RoomBooking]
    FOREIGN KEY ([RoomId])
    REFERENCES [dbo].[RoomSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomBooking'
CREATE INDEX [IX_FK_RoomBooking]
ON [dbo].[BookingSet]
    ([RoomId]);
GO

-- Creating foreign key on [Room_Id] in table 'ServiceSet'
ALTER TABLE [dbo].[ServiceSet]
ADD CONSTRAINT [FK_RoomService]
    FOREIGN KEY ([Room_Id])
    REFERENCES [dbo].[RoomSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomService'
CREATE INDEX [IX_FK_RoomService]
ON [dbo].[ServiceSet]
    ([Room_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
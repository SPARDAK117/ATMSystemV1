USE [master]
GO
/****** Object:  Database [ATMSystem]    Script Date: 18/07/2025 03:48:22 p. m. ******/
CREATE DATABASE [ATMSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ATMSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ATMSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ATMSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ATMSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ATMSystem] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ATMSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ATMSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ATMSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ATMSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ATMSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ATMSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [ATMSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ATMSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ATMSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ATMSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ATMSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ATMSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ATMSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ATMSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ATMSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ATMSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ATMSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ATMSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ATMSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ATMSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ATMSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ATMSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ATMSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ATMSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [ATMSystem] SET  MULTI_USER 
GO
ALTER DATABASE [ATMSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ATMSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ATMSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ATMSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ATMSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ATMSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ATMSystem', N'ON'
GO
ALTER DATABASE [ATMSystem] SET QUERY_STORE = ON
GO
ALTER DATABASE [ATMSystem] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ATMSystem]
GO
/****** Object:  Schema [Audit]    Script Date: 18/07/2025 03:48:22 p. m. ******/
CREATE SCHEMA [Audit]
GO
/****** Object:  Schema [Bank]    Script Date: 18/07/2025 03:48:22 p. m. ******/
CREATE SCHEMA [Bank]
GO
/****** Object:  Schema [Config]    Script Date: 18/07/2025 03:48:22 p. m. ******/
CREATE SCHEMA [Config]
GO
/****** Object:  Schema [Security]    Script Date: 18/07/2025 03:48:22 p. m. ******/
CREATE SCHEMA [Security]
GO
/****** Object:  UserDefinedFunction [Security].[ComputePinHash]    Script Date: 18/07/2025 03:48:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

    CREATE FUNCTION [Security].[ComputePinHash]
    (
        @Pin  NVARCHAR(20),
        @Salt UNIQUEIDENTIFIER
    )
    RETURNS VARBINARY(64)
    AS
    BEGIN
        -- Simple salt+pin hash; replace w/ stronger KDF externally if needed.
        RETURN HASHBYTES('SHA2_512', CONCAT(CONVERT(NVARCHAR(36), @Salt), @Pin));
    END;
GO
/****** Object:  Table [Audit].[TraceEvents]    Script Date: 18/07/2025 03:48:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Audit].[TraceEvents](
	[TraceId] [bigint] IDENTITY(1,1) NOT NULL,
	[EventTime] [datetime2](3) NOT NULL,
	[ProcName] [sysname] NOT NULL,
	[Step] [nvarchar](50) NULL,
	[CardLast4] [char](4) NULL,
	[AccountNumber] [varchar](20) NULL,
	[Amount] [decimal](18, 2) NULL,
	[ResultCode] [int] NULL,
	[ResultMessage] [nvarchar](200) NULL,
	[ErrorNumber] [int] NULL,
	[ErrorSeverity] [int] NULL,
	[ErrorState] [int] NULL,
	[ErrorMessage] [nvarchar](4000) NULL,
PRIMARY KEY CLUSTERED 
(
	[TraceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Bank].[Accounts]    Script Date: 18/07/2025 03:48:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Bank].[Accounts](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[AccountNumber] [varchar](20) NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[CreatedAt] [datetime2](3) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Accounts_AccountNumber] UNIQUE NONCLUSTERED 
(
	[AccountNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Bank].[Cards]    Script Date: 18/07/2025 03:48:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Bank].[Cards](
	[CardId] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[CardNumber] [varchar](16) NOT NULL,
	[PinSalt] [uniqueidentifier] NOT NULL,
	[PinHash] [varbinary](64) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](3) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Cards_CardNumber] UNIQUE NONCLUSTERED 
(
	[CardNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Bank].[Customers]    Script Date: 18/07/2025 03:48:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Bank].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[CreatedAt] [datetime2](3) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Bank].[Transactions]    Script Date: 18/07/2025 03:48:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Bank].[Transactions](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[TransactionType] [varchar](10) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[CreatedAt] [datetime2](3) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Config].[AtmParameters]    Script Date: 18/07/2025 03:48:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Config].[AtmParameters](
	[ParameterId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ValueDecimal] [decimal](18, 2) NULL,
	[ValueString] [nvarchar](200) NULL,
	[UpdatedAt] [datetime2](3) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ParameterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Security].[AppUsers]    Script Date: 18/07/2025 03:48:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Security].[AppUsers](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[PasswordSalt] [uniqueidentifier] NOT NULL,
	[PasswordHash] [varbinary](64) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](3) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_AppUsers_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transactions_AccountId]    Script Date: 18/07/2025 03:48:22 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Transactions_AccountId] ON [Bank].[Transactions]
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [Audit].[TraceEvents] ADD  CONSTRAINT [DF_TraceEvents_EventTime]  DEFAULT (sysutcdatetime()) FOR [EventTime]
GO
ALTER TABLE [Bank].[Accounts] ADD  CONSTRAINT [DF_Accounts_Balance]  DEFAULT ((0)) FOR [Balance]
GO
ALTER TABLE [Bank].[Accounts] ADD  CONSTRAINT [DF_Accounts_CreatedAt]  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [Bank].[Cards] ADD  CONSTRAINT [DF_Cards_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [Bank].[Cards] ADD  CONSTRAINT [DF_Cards_CreatedAt]  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [Bank].[Customers] ADD  CONSTRAINT [DF_Customers_CreatedAt]  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [Bank].[Transactions] ADD  CONSTRAINT [DF_Transactions_CreatedAt]  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [Config].[AtmParameters] ADD  CONSTRAINT [DF_AtmParameters_UpdatedAt]  DEFAULT (sysutcdatetime()) FOR [UpdatedAt]
GO
ALTER TABLE [Security].[AppUsers] ADD  CONSTRAINT [DF_AppUsers_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [Security].[AppUsers] ADD  CONSTRAINT [DF_AppUsers_CreatedAt]  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [Bank].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Customers] FOREIGN KEY([CustomerId])
REFERENCES [Bank].[Customers] ([CustomerId])
GO
ALTER TABLE [Bank].[Accounts] CHECK CONSTRAINT [FK_Accounts_Customers]
GO
ALTER TABLE [Bank].[Cards]  WITH CHECK ADD  CONSTRAINT [FK_Cards_Accounts] FOREIGN KEY([AccountId])
REFERENCES [Bank].[Accounts] ([AccountId])
GO
ALTER TABLE [Bank].[Cards] CHECK CONSTRAINT [FK_Cards_Accounts]
GO
ALTER TABLE [Bank].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Accounts] FOREIGN KEY([AccountId])
REFERENCES [Bank].[Accounts] ([AccountId])
GO
ALTER TABLE [Bank].[Transactions] CHECK CONSTRAINT [FK_Transactions_Accounts]
GO
ALTER TABLE [Bank].[Transactions]  WITH CHECK ADD  CONSTRAINT [CK_Transactions_TransactionType] CHECK  (([TransactionType]='Withdraw' OR [TransactionType]='Deposit'))
GO
ALTER TABLE [Bank].[Transactions] CHECK CONSTRAINT [CK_Transactions_TransactionType]
GO
/****** Object:  StoredProcedure [Bank].[UspChangePin]    Script Date: 18/07/2025 03:48:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    CREATE PROCEDURE [Bank].[UspChangePin]
        @CardNumber    VARCHAR(16),
		@NewSalt UNIQUEIDENTIFIER,
		@NewHash VARBINARY(64),
        @ResultCode    INT OUTPUT,
        @ResultMessage NVARCHAR(200) OUTPUT
    AS
    BEGIN
        SET NOCOUNT ON;
		BEGIN TRY
            UPDATE Bank.Cards
		SET PinSalt = @NewSalt,
        PinHash = @NewHash
		WHERE CardNumber = @CardNumber;

            SET @ResultCode = 0; SET @ResultMessage = N'OK';

Done:
            INSERT Audit.TraceEvents(ProcName, Step, CardLast4, ResultCode, ResultMessage)
            VALUES('Bank.UspChangePin','END',RIGHT(@CardNumber,4),@ResultCode,@ResultMessage);
        END TRY
        BEGIN CATCH
            SET @ResultCode = 900;
            SET @ResultMessage = N'SQL error.';
            INSERT Audit.TraceEvents(ProcName, Step, CardLast4, ResultCode, ResultMessage,
                                     ErrorNumber, ErrorSeverity, ErrorState, ErrorMessage)
            VALUES('Bank.UspChangePin','CATCH',RIGHT(@CardNumber,4),@ResultCode,@ResultMessage,
                   ERROR_NUMBER(),ERROR_SEVERITY(),ERROR_STATE(),ERROR_MESSAGE());
        END CATCH
    END;
GO
/****** Object:  StoredProcedure [Bank].[UspDeposit]    Script Date: 18/07/2025 03:48:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    CREATE PROCEDURE [Bank].[UspDeposit]
        @AccountOrCard VARCHAR(20),
        @Amount        DECIMAL(18,2),
        @ResultCode    INT OUTPUT,
        @ResultMessage NVARCHAR(200) OUTPUT
    AS
    BEGIN
        SET NOCOUNT ON;

        DECLARE
            @AccountId INT,
            @AccountNumber VARCHAR(20),
            @MaxDeposit DECIMAL(18,2),
            @Dummy DECIMAL(18,2);

        BEGIN TRY
            /* Resolve to AccountId */
            SELECT @AccountId = a.AccountId, @AccountNumber = a.AccountNumber
            FROM Bank.Accounts a
            WHERE a.AccountNumber = @AccountOrCard;

            IF @AccountId IS NULL
            BEGIN
                SELECT @AccountId = c.AccountId, @AccountNumber = a.AccountNumber
                FROM Bank.Cards c
                JOIN Bank.Accounts a ON a.AccountId = c.AccountId
                WHERE c.CardNumber = @AccountOrCard AND c.IsActive = 1;
            END

            IF @AccountId IS NULL
            BEGIN SET @ResultCode = 1; SET @ResultMessage = N'Account not found.'; GOTO Done; END

            /* Limits */
            EXEC Config.UspGetAtmLimits @Dummy OUTPUT, @MaxDeposit OUTPUT;

            IF @Amount <= 0
            BEGIN SET @ResultCode = 3; SET @ResultMessage = N'Amount must be > 0.'; GOTO Done; END

            IF @Amount >= @MaxDeposit
            BEGIN SET @ResultCode = 4; SET @ResultMessage = CONCAT(N'Exceeds deposit limit ($', @MaxDeposit, ').'); GOTO Done; END

            BEGIN TRAN;
                UPDATE Bank.Accounts
                    SET Balance = Balance + @Amount
                WHERE AccountId = @AccountId;

                INSERT Bank.Transactions(AccountId, TransactionType, Amount)
                VALUES(@AccountId, 'Deposit', @Amount);
            COMMIT;

            SET @ResultCode = 0; SET @ResultMessage = N'OK';

Done:
            INSERT Audit.TraceEvents(ProcName, Step, AccountNumber, Amount, ResultCode, ResultMessage)
            VALUES('Bank.UspDeposit','END',@AccountNumber,@Amount,@ResultCode,@ResultMessage);
        END TRY
        BEGIN CATCH
            IF XACT_STATE() <> 0 ROLLBACK;
            SET @ResultCode = 900;
            SET @ResultMessage = N'SQL error.';
            INSERT Audit.TraceEvents(ProcName, Step, AccountNumber, Amount, ResultCode, ResultMessage,
                                     ErrorNumber, ErrorSeverity, ErrorState, ErrorMessage)
            VALUES('Bank.UspDeposit','CATCH',@AccountNumber,@Amount,@ResultCode,@ResultMessage,
                   ERROR_NUMBER(),ERROR_SEVERITY(),ERROR_STATE(),ERROR_MESSAGE());
        END CATCH
    END;
GO
/****** Object:  StoredProcedure [Bank].[UspGetAccountNumberByCard]    Script Date: 18/07/2025 03:48:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [Bank].[UspGetAccountNumberByCard]
    @CardNumber     VARCHAR(16),
    @AccountNumber  VARCHAR(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT @AccountNumber = a.AccountNumber
    FROM Bank.Cards c
    JOIN Bank.Accounts a ON a.AccountId = c.AccountId
    WHERE c.CardNumber = @CardNumber
      AND c.IsActive = 1;
END
GO
/****** Object:  StoredProcedure [Bank].[UspGetBalance]    Script Date: 18/07/2025 03:48:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    CREATE PROCEDURE [Bank].[UspGetBalance]
        @CardNumber    VARCHAR(16),
        @ResultCode    INT OUTPUT,
        @ResultMessage NVARCHAR(200) OUTPUT
    AS
    BEGIN
        SET NOCOUNT ON;

        DECLARE
            @AccountId INT,
            @Salt UNIQUEIDENTIFIER,
            @StoredHash VARBINARY(64),
            @ComputedHash VARBINARY(64);

        BEGIN TRY
            SELECT
                @AccountId = c.AccountId,
                @Salt      = c.PinSalt,
                @StoredHash= c.PinHash
            FROM Bank.Cards c
            WHERE c.CardNumber = @CardNumber AND c.IsActive = 1;

            IF @AccountId IS NULL
            BEGIN SET @ResultCode = 1; SET @ResultMessage = N'Card not found.'; GOTO Done; END

            SELECT Balance FROM Bank.Accounts WHERE AccountId = @AccountId;

            SET @ResultCode = 0; SET @ResultMessage = N'OK';

Done:
            INSERT Audit.TraceEvents(ProcName, Step, CardLast4, ResultCode, ResultMessage)
            VALUES('Bank.UspGetBalance','END',RIGHT(@CardNumber,4),@ResultCode,@ResultMessage);
        END TRY
        BEGIN CATCH
            SET @ResultCode = 900;
            SET @ResultMessage = N'SQL error.';
            INSERT Audit.TraceEvents(ProcName, Step, CardLast4, ResultCode, ResultMessage,
                                     ErrorNumber, ErrorSeverity, ErrorState, ErrorMessage)
            VALUES('Bank.UspGetBalance','CATCH',RIGHT(@CardNumber,4),@ResultCode,@ResultMessage,
                   ERROR_NUMBER(),ERROR_SEVERITY(),ERROR_STATE(),ERROR_MESSAGE());
        END CATCH
    END;
GO
/****** Object:  StoredProcedure [Bank].[UspGetCardHashInfo]    Script Date: 18/07/2025 03:48:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Bank].[UspGetCardHashInfo]
    @CardNumber VARCHAR(16)
AS
BEGIN
    SELECT PinSalt, PinHash, IsActive
    FROM Bank.Cards
    WHERE CardNumber = @CardNumber;
END
GO
/****** Object:  StoredProcedure [Bank].[UspWithdraw]    Script Date: 18/07/2025 03:48:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    CREATE PROCEDURE [Bank].[UspWithdraw]
        @CardNumber    VARCHAR(16),
        @Amount        DECIMAL(18,2),
        @ResultCode    INT OUTPUT,
        @ResultMessage NVARCHAR(200) OUTPUT
    AS
    BEGIN
        SET NOCOUNT ON;

        DECLARE
            @AccountId INT,
            @Salt UNIQUEIDENTIFIER,
            @StoredHash VARBINARY(64),
            @ComputedHash VARBINARY(64),
            @Balance DECIMAL(18,2),
            @MaxWithdraw DECIMAL(18,2),
            @Dummy DECIMAL(18,2);

        BEGIN TRY

            SELECT
                @AccountId = c.AccountId,
                @Salt      = c.PinSalt,
                @StoredHash= c.PinHash
            FROM Bank.Cards c
            WHERE c.CardNumber = @CardNumber AND c.IsActive = 1;

            IF @AccountId IS NULL
            BEGIN SET @ResultCode = 1; SET @ResultMessage = N'Card not found or inactive.'; GOTO Done; END

            EXEC Config.UspGetAtmLimits @MaxWithdraw OUTPUT, @Dummy OUTPUT;

            IF @Amount <= 0
            BEGIN SET @ResultCode = 3; SET @ResultMessage = N'Amount must be > 0.'; GOTO Done; END

            IF @Amount >= @MaxWithdraw
            BEGIN SET @ResultCode = 4; SET @ResultMessage = CONCAT(N'Exceeds withdraw limit ($', @MaxWithdraw, ').'); GOTO Done; END

            SELECT @Balance = a.Balance FROM Bank.Accounts a WHERE a.AccountId = @AccountId;
            IF @Balance < @Amount
            BEGIN SET @ResultCode = 5; SET @ResultMessage = N'Insufficient funds.'; GOTO Done; END

            BEGIN TRAN;
                UPDATE Bank.Accounts
                    SET Balance = Balance - @Amount
                WHERE AccountId = @AccountId AND Balance >= @Amount;

                IF @@ROWCOUNT = 0
                BEGIN ROLLBACK; SET @ResultCode = 5; SET @ResultMessage = N'Insufficient funds.'; GOTO Done; END

                INSERT Bank.Transactions(AccountId, TransactionType, Amount)
                VALUES(@AccountId, 'Withdraw', @Amount);
            COMMIT;

            SET @ResultCode = 0; SET @ResultMessage = N'OK';

Done:
            INSERT Audit.TraceEvents(ProcName, Step, CardLast4, AccountNumber, Amount, ResultCode, ResultMessage)
            SELECT 'Bank.UspWithdraw', 'END', RIGHT(@CardNumber,4),
                   a.AccountNumber, @Amount, @ResultCode, @ResultMessage
            FROM Bank.Accounts a WHERE a.AccountId = @AccountId;
        END TRY
        BEGIN CATCH
            IF XACT_STATE() <> 0 ROLLBACK;
            SET @ResultCode = 900;
            SET @ResultMessage = N'SQL error.';
            INSERT Audit.TraceEvents(ProcName, Step, CardLast4, Amount, ResultCode, ResultMessage,
                                     ErrorNumber, ErrorSeverity, ErrorState, ErrorMessage)
            VALUES('Bank.UspWithdraw','CATCH',RIGHT(@CardNumber,4),@Amount,
                   @ResultCode,@ResultMessage,
                   ERROR_NUMBER(),ERROR_SEVERITY(),ERROR_STATE(),ERROR_MESSAGE());
        END CATCH
    END;
GO
/****** Object:  StoredProcedure [Config].[UspGetAtmLimits]    Script Date: 18/07/2025 03:48:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    CREATE PROCEDURE [Config].[UspGetAtmLimits]
        @MaxWithdraw DECIMAL(18,2) OUTPUT,
        @MaxDeposit  DECIMAL(18,2) OUTPUT
    AS
    BEGIN
        SET NOCOUNT ON;
        SELECT
            @MaxWithdraw = MAX(CASE WHEN Name = 'MaxWithdraw' THEN ValueDecimal END),
            @MaxDeposit  = MAX(CASE WHEN Name = 'MaxDeposit'  THEN ValueDecimal END)
        FROM Config.AtmParameters;

        SET @MaxWithdraw = ISNULL(@MaxWithdraw, 8000.00);
        SET @MaxDeposit  = ISNULL(@MaxDeposit, 15000.00);
    END;
GO
USE [master]
GO
ALTER DATABASE [ATMSystem] SET  READ_WRITE 
GO
--Insertar datos iniciales de configuración
    INSERT INTO Config.AtmParameters (Name, ValueDecimal, UpdatedAt) VALUES (N'MaxWithdraw', 8000.00, SYSDATETIME());
    INSERT INTO Config.AtmParameters (Name, ValueDecimal, UpdatedAt) VALUES (N'MaxDeposit', 15000.00, SYSDATETIME());

    -- INSERTS DE DATOS DE PRUEBA GENERADOS (reemplaza con la salida de tu programa C#)
    -- A continuación, el output de mi programa C# para 5 nuevas personas:

INSERT INTO Bank.Customers (FullName, CreatedAt)
VALUES (N'John Smith', SYSDATETIME());

INSERT INTO Bank.Accounts (CustomerId, AccountNumber, Balance, CreatedAt)
VALUES (2, 'ACC002', 6000, SYSDATETIME());

INSERT INTO Bank.Cards (AccountId, CardNumber, PinSalt, PinHash, IsActive, CreatedAt)
VALUES (
    2,
    '1397017288273011',
    '6c8d6e0c-801a-4f99-bb6e-4777e084ba62',
    0x680883f47bb528e4aca6beeea9d283b61891bd7606e65e953d313fac2ab92432d0727869f809e9bb01bd03b151aef000cbbe7f714cef3ec072df11d4ae97153a,
    1,
    SYSDATETIME()
);
GO

COMMIT TRANSACTION; 
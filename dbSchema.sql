/****** Object:  Database [powerplatformbootcamp]    Script Date: 9.3.2023 17:00:23 ******/
CREATE DATABASE [powerplatformbootcamp]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 2 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS, LEDGER = OFF;
GO
ALTER DATABASE [powerplatformbootcamp] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [powerplatformbootcamp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [powerplatformbootcamp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [powerplatformbootcamp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [powerplatformbootcamp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [powerplatformbootcamp] SET ARITHABORT OFF 
GO
ALTER DATABASE [powerplatformbootcamp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [powerplatformbootcamp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [powerplatformbootcamp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [powerplatformbootcamp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [powerplatformbootcamp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [powerplatformbootcamp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [powerplatformbootcamp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [powerplatformbootcamp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [powerplatformbootcamp] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [powerplatformbootcamp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [powerplatformbootcamp] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [powerplatformbootcamp] SET  MULTI_USER 
GO
ALTER DATABASE [powerplatformbootcamp] SET ENCRYPTION ON
GO
ALTER DATABASE [powerplatformbootcamp] SET QUERY_STORE = ON
GO
ALTER DATABASE [powerplatformbootcamp] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  Table [dbo].[Order]    Script Date: 9.3.2023 17:00:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](100) NULL,
	[IsApproved] [bit] NULL,
	[IsInitial] [bit] NULL,
	[CreatedOnUtc] [datetime] NULL,
	[ModifiedOnUtc] [datetime] NULL,
	[IsReviewed] [bit] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER DATABASE [powerplatformbootcamp] SET  READ_WRITE 
GO

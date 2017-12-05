
CREATE FUNCTION [dbo].[OnDebugMode]()
RETURNS int
AS
BEGIN
	
	RETURN 1;

END
GO

/****** Object:  Table [dbo].[__SqlLog]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__SqlLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[EntryTime] [datetime2](7) NOT NULL,
	[SqlQuery] [nvarchar](max) NOT NULL,
	[CallerName] [varchar](200) NOT NULL,
	[LineLocation] [varchar](50) NOT NULL,
 CONSTRAINT [PK___SqlLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK__AspNetRo__3214EC07366123BB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[ClaimType] [varchar](max) NULL,
	[ClaimValue] [varchar](max) NULL,
 CONSTRAINT [PK__AspNetUs__3214EC072B2DD054] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [varchar](128) NOT NULL,
	[ProviderKey] [varchar](128) NOT NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK__AspNetUs__663BD39EBC358BE5] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
 CONSTRAINT [PK__AspNetUs__AF2760AD72B57DCE] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [bigint] NOT NULL,
	[Email] [varchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [varchar](max) NULL,
	[SecurityStamp] [varchar](max) NULL,
	[PhoneNumber] [varchar](128) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [varchar](256) NOT NULL,
	[FullName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK__AspNetUs__3214EC07FFFBE7EA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF__AspNetUse__Email__239E4DCF]  DEFAULT (NULL) FOR [Email]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF__AspNetUse__Locko__24927208]  DEFAULT (NULL) FOR [LockoutEndDateUtc]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
GO
/****** Object:  StoredProcedure [dbo].[__SA_AspNet_IdentityRoles_GetAll]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetRoles_GetAll
-------------------------
CREATE PROCEDURE [dbo].[__SA_AspNet_IdentityRoles_GetAll]
AS
BEGIN
    SELECT [Id]
         , [Name]
           
		FROM dbo.[AspNetRoles]  
END
-------------------------
--END of AspNetRoles_GetAll
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[__SA_AspNet_IdentityRoles_GetById]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetRoles_GetById
-------------------------
CREATE PROCEDURE [dbo].[__SA_AspNet_IdentityRoles_GetById]
	@id bigint
AS
BEGIN
    SELECT  [Id]
         , [Name]
         FROM dbo.AspNetRoles  
		 WHERE [Id] = @Id
END
-------------------------
--END of AspNetRoles_GetById
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[__SA_AspNet_IdentityRoles_LookupItems]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-------------------------
--- Start of AspNetRoles_LookupItems
-------------------------

CREATE PROCEDURE [dbo].[__SA_AspNet_IdentityRoles_LookupItems]
AS
BEGIN
	SELECT [Id], [Name] 
		FROM dbo.[AspNetRoles]
		ORDER BY [Name];
END
-------------------------
--END of AspNetRoles_LookupItems
-------------------------
GO
/****** Object:  StoredProcedure [dbo].[__SA_AspNet_IdentityUserRoles_GetAll]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetUserRoles_GetAll
-------------------------
CREATE PROCEDURE [dbo].[__SA_AspNet_IdentityUserRoles_GetAll]
AS
BEGIN
    SELECT [UserId]
         , [RoleId]
           
		FROM dbo.[AspNetUserRoles]  
END
-------------------------
--END of AspNetUserRoles_GetAll
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[__SA_AspNet_IdentityUserRoles_GetById]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetUserRoles_GetById
-------------------------
CREATE PROCEDURE [dbo].[__SA_AspNet_IdentityUserRoles_GetById]
	@userId bigint
	, @roleId bigint
AS
BEGIN
    SELECT  [UserId]
         , [RoleId]
         FROM dbo.AspNetUserRoles  
		 WHERE [UserId] = @UserId
			AND [RoleId] = @RoleId
END
-------------------------
--END of AspNetUserRoles_GetById
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[__SA_AspNet_IdentityUsers_GetAll]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of SA_AspNet_IdentityUser_GetAll
-------------------------
CREATE PROCEDURE [dbo].[__SA_AspNet_IdentityUsers_GetAll]
AS
BEGIN
    SELECT [Id]
         , [Email]
         , [EmailConfirmed]
         , [PasswordHash]
         , [SecurityStamp]
         , [PhoneNumber]
         , [PhoneNumberConfirmed]
         , [TwoFactorEnabled]
         , [LockoutEndDateUtc]
         , [LockoutEnabled]
         , [AccessFailedCount]
         , [UserName]
		 , [FullName] 

		FROM dbo.AspNetUsers  
END
-------------------------
--END of SA_AspNet_IdentityUser_GetAll
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[__SA_AspNet_IdentityUsers_GetById]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of SA_AspNet_IdentityUser_GetById
-------------------------
CREATE PROCEDURE [dbo].[__SA_AspNet_IdentityUsers_GetById]
	@id bigint
AS
BEGIN
    SELECT  [Id]
         , [Email]
         , [EmailConfirmed]
         , [PasswordHash]
         , [SecurityStamp]
         , [PhoneNumber]
         , [PhoneNumberConfirmed]
         , [TwoFactorEnabled]
         , [LockoutEndDateUtc]
         , [LockoutEnabled]
         , [AccessFailedCount]
         , [UserName]
		 , [FullName] 
         FROM dbo.AspNetUsers  
		 WHERE [Id] = @Id
END
-------------------------
--END of SA_AspNet_IdentityUser_GetById
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityRoles_Delete]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetRoles_Delete
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityRoles_Delete]
	@id bigint
AS
BEGIN

    DELETE FROM dbo.[AspNetRoles]
    WHERE
		[Id] = @Id
END
-------------------------
--END of AspNetRoles_Delete
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityRoles_Find]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
-------------------------
--- Start of AspNetRoles_Find
-------------------------
 CREATE PROCEDURE  [dbo].[SA_AspNet_IdentityRoles_Find]
	@whereClause NVARCHAR(4000)
    WITH EXEC AS CALLER
AS
BEGIN
    DECLARE @sql NVARCHAR(4000);
    SET @sql = 
		'SELECT ' + ' [Id] ' +
         ' , [Name] ' +
         'FROM dbo.AspNetRoles ' +
	ISNULL(@whereClause, '');

	IF ( dbo.OnDebugMode() = 1 ) 
	BEGIN
		INSERT  INTO [dbo].[__SqlLog]
				( [EntryTime] ,
				  [SqlQuery] ,
				  [CallerName] ,
				  [LineLocation]
				)
		VALUES  ( GETDATE() ,
				  @sql ,
				  'AspNetRoles_Find' ,
				  'before executing query'
				);
	END		
		
     EXEC sp_executesql @sql;
END
-------------------------
--END of AspNetRoles_Find
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityRoles_GetAllNamesByUserId]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of SA_AspNet_IdentityRoles_GetAllNamesByUserId
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityRoles_GetAllNamesByUserId]
	@userId bigint
AS
BEGIN
    SELECT [Name]           
		FROM dbo.[AspNetRoles] as r INNER JOIN 
			dbo.AspNetUserRoles as ur ON r.Id = ur.RoleId
		WHERE ur.UserId = @userId;
END
-------------------------
--END of AspNetRoles_GetAll
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityRoles_GetPagedList]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetRoles_GetPagedList
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityRoles_GetPagedList]
    @name           NVARCHAR(MAX)
    , @startIndex   INT    
    , @pageSize INT
    , @sortExpression VARCHAR(255)
    , @totalRows BIGINT OUTPUT
    WITH EXEC AS CALLER
AS 
BEGIN
    DECLARE @sql NVARCHAR(4000);
	DECLARE @whereClause NVARCHAR(300) = ' WHERE 1 = 1';   
    BEGIN 

    IF @name IS NOT NULL AND @name != ''
    BEGIN
        SET @whereClause += ' AND Name LIKE ''%' + @name + '%''';
    END;

    SET @sql = 'SELECT @totalRows = COUNT(*) From [dbo].[AspNetRoles] '
        + @whereClause;

    EXEC sp_executesql @sql, N'@totalRows INT OUTPUT', @totalRows OUTPUT;
  
SET @sql = 
'SELECT ' + ' [Id] ' +
         ' , [Name] ' +
             ' FROM ( ' +
    ' SELECT * ' +
       ' , ROW_NUMBER() OVER ( ORDER BY ' + @sortExpression + ' ) AS [RowNumber] ' +
        ' FROM [dbo].[AspNetRoles] ' +
         @whereClause +
    ' ) AS pagedList ' +
     ' WHERE RowNumber BETWEEN ('+ CONVERT(varchar(10), @StartIndex + 1) + ') AND ( ' + 
  CONVERT(varchar(10), @StartIndex + @PageSize) + ');';
  
        IF ( dbo.OnDebugMode() = 1 ) 
            BEGIN
                INSERT  INTO [dbo].[__SqlLog]
                        ( [EntryTime] ,
                          [SqlQuery] ,
                          [CallerName] ,
                          [LineLocation]
                        )
                VALUES  ( GETDATE() ,
                          @sql ,
                          'AspNetRoles_GetPagedList' ,
                          'before executing query'
                        );
            END

  --SET @sqlToReturn = @sql;
        EXEC sp_executesql @sql;
    END;

END
-------------------------
--END of AspNetRoles_GetPagedList
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityRoles_Insert]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetRoles_Insert
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityRoles_Insert]
	       @id bigint
	 , @name NVARCHAR(256)
    
        AS
		BEGIN
		
        SET NOCOUNT ON;
		SET XACT_ABORT ON
		
		
        INSERT INTO dbo.[AspNetRoles]
        (
            [Id]
		 , [Name]

        ) VALUES (
            @id
		 , @name
	 
	);

END
-------------------------
--END of AspNetRoles_Insert
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityRoles_Update]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetRoles_Update
-------------------------

CREATE PROCEDURE [dbo].[SA_AspNet_IdentityRoles_Update]
	    @id bigint
	 , @name NVARCHAR(256)
	
        AS 
		BEGIN
		
		SET XACT_ABORT ON
		
        UPDATE dbo.[AspNetRoles] SET
            [Name] = @name
      WHERE [Id] = @id 


END
-------------------------
--END of AspNetRoles_Update
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUserClaims_Delete]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-------------------------
--- Start of AspNetUserClaims_Delete
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityUserClaims_Delete]
	@id INT
AS
BEGIN

    DELETE FROM dbo.[AspNetUserClaims]
    WHERE
		[Id] = @Id
END
-------------------------
--END of AspNetUserClaims_Delete
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUserClaims_Find]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
-------------------------
--- Start of AspNetUserClaims_Find
-------------------------
 CREATE PROCEDURE  [dbo].[SA_AspNet_IdentityUserClaims_Find]
	@whereClause NVARCHAR(4000)
    WITH EXEC AS CALLER
AS
BEGIN
    DECLARE @sql NVARCHAR(4000);
    SET @sql = 
		'SELECT ' + ' [Id] ' +
         ' , [UserId] ' +
         ' , [ClaimType] ' +
         ' , [ClaimValue] ' +
         'FROM dbo.AspNetUserClaims ' +
	ISNULL(@whereClause, '');

	IF ( dbo.OnDebugMode() = 1 ) 
	BEGIN
		INSERT  INTO [dbo].[__SqlLog]
				( [EntryTime] ,
				  [SqlQuery] ,
				  [CallerName] ,
				  [LineLocation]
				)
		VALUES  ( GETDATE() ,
				  @sql ,
				  'AspNetUserClaims_Find' ,
				  'before executing query'
				);
	END		
		
     EXEC sp_executesql @sql;
END
-------------------------
--END of AspNetUserClaims_Find
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUserClaims_GetAll]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetUserClaims_GetAll
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityUserClaims_GetAll]
AS
BEGIN
    SELECT [Id]
         , [UserId]
         , [ClaimType]
         , [ClaimValue]
           
		FROM dbo.[AspNetUserClaims]  
END
-------------------------
--END of AspNetUserClaims_GetAll
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUserClaims_GetById]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetUserClaims_GetById
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityUserClaims_GetById]
	@id INT
AS
BEGIN
    SELECT  [Id]
         , [UserId]
         , [ClaimType]
         , [ClaimValue]
         FROM dbo.AspNetUserClaims  
		 WHERE [Id] = @Id
END
-------------------------
--END of AspNetUserClaims_GetById
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUserClaims_Insert]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetUserClaims_Insert
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityUserClaims_Insert]
	    
	   @userId bigint
	 , @claimType VARCHAR(MAX)
	 , @claimValue VARCHAR(MAX)
	,@Id int OUTPUT    
    
        AS
		BEGIN
		
        SET NOCOUNT ON;
		SET XACT_ABORT ON
		
		
        INSERT INTO dbo.[AspNetUserClaims]
        (
            [UserId]
		 , [ClaimType]
		 , [ClaimValue]

        ) VALUES (
            @userId
		 , @claimType
		 , @claimValue
	 
	)
	SELECT @Id = SCOPE_IDENTITY()


END
-------------------------
--END of AspNetUserClaims_Insert
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUserClaims_Update]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetUserClaims_Update
-------------------------

CREATE PROCEDURE [dbo].[SA_AspNet_IdentityUserClaims_Update]
	    @id INT
	 , @userId bigint
	 , @claimType VARCHAR(MAX)
	 , @claimValue VARCHAR(MAX)
	
        AS 
		BEGIN
		
		SET XACT_ABORT ON
		
        UPDATE dbo.[AspNetUserClaims] SET
            [UserId] = @userId
          , [ClaimType] = @claimType
          , [ClaimValue] = @claimValue
      WHERE [Id] = @id 


END
-------------------------
--END of AspNetUserClaims_Update
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUserLogins_Delete]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetUserLogins_Delete
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityUserLogins_Delete]
	@loginProvider VARCHAR
	, @providerKey VARCHAR
	, @userId bigint
AS
BEGIN

    DELETE FROM dbo.[AspNetUserLogins]
    WHERE
		[LoginProvider] = @LoginProvider
		AND [ProviderKey] = @ProviderKey
		AND [UserId] = @UserId
END
-------------------------
--END of AspNetUserLogins_Delete
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUserLogins_Find]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
-------------------------
--- Start of AspNetUserLogins_Find
-------------------------
 CREATE PROCEDURE  [dbo].[SA_AspNet_IdentityUserLogins_Find]
	@whereClause NVARCHAR(4000)
    WITH EXEC AS CALLER
AS
BEGIN
    DECLARE @sql NVARCHAR(4000);
    SET @sql = 
		'SELECT ' + ' [LoginProvider] ' +
         ' , [ProviderKey] ' +
         ' , [UserId] ' +
         'FROM dbo.AspNetUserLogins ' +
	ISNULL(@whereClause, '');

	IF ( dbo.OnDebugMode() = 1 ) 
	BEGIN
		INSERT  INTO [dbo].[__SqlLog]
				( [EntryTime] ,
				  [SqlQuery] ,
				  [CallerName] ,
				  [LineLocation]
				)
		VALUES  ( GETDATE() ,
				  @sql ,
				  'AspNetUserLogins_Find' ,
				  'before executing query'
				);
	END		
		
     EXEC sp_executesql @sql;
END
-------------------------
--END of AspNetUserLogins_Find
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUserLogins_GetAll]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetUserLogins_GetAll
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityUserLogins_GetAll]
AS
BEGIN
    SELECT [LoginProvider]
         , [ProviderKey]
         , [UserId]
		FROM dbo.[AspNetUserLogins]  
END
-------------------------
--END of AspNetUserLogins_GetAll
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUserLogins_GetById]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetUserLogins_GetById
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityUserLogins_GetById]
	@loginProvider VARCHAR
	, @providerKey VARCHAR
	, @userId bigint
AS
BEGIN
    SELECT  [LoginProvider]
         , [ProviderKey]
         , [UserId]
         FROM dbo.AspNetUserLogins  
		 WHERE [LoginProvider] = @LoginProvider
			AND [ProviderKey] = @ProviderKey
			AND [UserId] = @UserId
END
-------------------------
--END of AspNetUserLogins_GetById
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUserLogins_Insert]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetUserLogins_Insert
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityUserLogins_Insert]
	    @loginProvider VARCHAR(128)
	 , @providerKey VARCHAR(128)
	 , @userId bigint
    
        AS
		BEGIN
		
        SET NOCOUNT ON;
		SET XACT_ABORT ON
		
		
        INSERT INTO dbo.[AspNetUserLogins]
        (
            [LoginProvider]
		 , [ProviderKey]
		 , [UserId]

        ) VALUES (
            @loginProvider
		 , @providerKey
		 , @userId
	 
	)


END
-------------------------
--END of AspNetUserLogins_Insert
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUserRoles_Delete]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetUserRoles_Delete
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityUserRoles_Delete]
	@userId bigint
	, @roleId bigint
AS
BEGIN

    DELETE FROM dbo.[AspNetUserRoles]
    WHERE
		[UserId] = @UserId
		AND [RoleId] = @RoleId
END
-------------------------
--END of AspNetUserRoles_Delete
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUserRoles_Find]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
-------------------------
--- Start of AspNetUserRoles_Find
-------------------------
 CREATE PROCEDURE  [dbo].[SA_AspNet_IdentityUserRoles_Find]
	@whereClause NVARCHAR(4000)
    WITH EXEC AS CALLER
AS
BEGIN
    DECLARE @sql NVARCHAR(4000);
    SET @sql = 
		'SELECT ' + ' [UserId] ' +
         ' , [RoleId] ' +
         'FROM dbo.AspNetUserRoles ' +
	ISNULL(@whereClause, '');

	IF ( dbo.OnDebugMode() = 1 ) 
	BEGIN
		INSERT  INTO [dbo].[__SqlLog]
				( [EntryTime] ,
				  [SqlQuery] ,
				  [CallerName] ,
				  [LineLocation]
				)
		VALUES  ( GETDATE() ,
				  @sql ,
				  'AspNetUserRoles_Find' ,
				  'before executing query'
				);
	END		
		
     EXEC sp_executesql @sql;
END
-------------------------
--END of AspNetUserRoles_Find
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUserRoles_GetPagedList]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetUserRoles_GetPagedList
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityUserRoles_GetPagedList]
	@startIndex INT
    , @pageSize INT
    , @sortExpression VARCHAR(255)
    , @totalRows BIGINT OUTPUT
    WITH EXEC AS CALLER
AS 
BEGIN
    DECLARE @sql NVARCHAR(4000);
	DECLARE @whereClause NVARCHAR(300) = ' WHERE 1 = 1';   
    BEGIN 

    SET @sql = 'SELECT @totalRows = COUNT(*) From [dbo].[AspNetUserRoles] '
        + @whereClause;

    EXEC sp_executesql @sql, N'@totalRows INT OUTPUT', @totalRows OUTPUT;
  
SET @sql = 
'SELECT ' + ' [UserId] ' +
         ' , [RoleId] ' +
             ' FROM ( ' +
    ' SELECT * ' +
       ' , ROW_NUMBER() OVER ( ORDER BY ' + @sortExpression + ' ) AS [RowNumber] ' +
        ' FROM [dbo].[AspNetUserRoles] ' +
         @whereClause +
    ' ) AS pagedList ' +
     ' WHERE RowNumber BETWEEN ('+ CONVERT(varchar(10), @StartIndex + 1) + ') AND ( ' + 
  CONVERT(varchar(10), @StartIndex + @PageSize) + ');';
  
        IF ( dbo.OnDebugMode() = 1 ) 
            BEGIN
                INSERT  INTO [dbo].[__SqlLog]
                        ( [EntryTime] ,
                          [SqlQuery] ,
                          [CallerName] ,
                          [LineLocation]
                        )
                VALUES  ( GETDATE() ,
                          @sql ,
                          'AspNetUserRoles_GetPagedList' ,
                          'before executing query'
                        );
            END

  --SET @sqlToReturn = @sql;
        EXEC sp_executesql @sql;
    END;

END
-------------------------
--END of AspNetUserRoles_GetPagedList
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUserRoles_Insert]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of AspNetUserRoles_Insert
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityUserRoles_Insert]
	@userId bigint
	 , @roleId bigint
    
        AS
		BEGIN
		
        SET NOCOUNT ON;
		SET XACT_ABORT ON
		
		
        INSERT INTO dbo.[AspNetUserRoles]
        (
            [UserId]
		 , [RoleId]

        ) VALUES (
            @userId
		 , @roleId
	 
	);

END
-------------------------
--END of AspNetUserRoles_Insert
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUsers_Delete]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of SA_AspNet_IdentityUser_Delete
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityUsers_Delete]
	@id bigint
AS
BEGIN

    DELETE FROM dbo.[SA_AspNet_IdentityUser]
    WHERE
		[Id] = @Id
END
-------------------------
--END of SA_AspNet_IdentityUser_Delete
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUsers_Find]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
-------------------------
--- Start of SA_AspNet_IdentityUser_Find
-------------------------
 CREATE PROCEDURE  [dbo].[SA_AspNet_IdentityUsers_Find]
	@whereClause NVARCHAR(4000)
    WITH EXEC AS CALLER
AS
BEGIN
    DECLARE @sql NVARCHAR(4000);
    SET @sql = 
		'SELECT ' + ' [Id] ' +
         ' , [Email] ' +
         ' , [EmailConfirmed] ' +
         ' , [PasswordHash] ' +
         ' , [SecurityStamp] ' +
         ' , [PhoneNumber] ' +
         ' , [PhoneNumberConfirmed] ' +
         ' , [TwoFactorEnabled] ' +
         ' , [LockoutEndDateUtc] ' +
         ' , [LockoutEnabled] ' +
         ' , [AccessFailedCount] ' +
         ' , [UserName] ' +
		 ' , [FullName] ' +
         'FROM dbo.AspNetUsers ' +
	ISNULL(@whereClause, '');

	IF ( dbo.OnDebugMode() = 1 ) 
	BEGIN
		INSERT  INTO [dbo].[__SqlLog]
				( [EntryTime] ,
				  [SqlQuery] ,
				  [CallerName] ,
				  [LineLocation]
				)
		VALUES  ( GETDATE() ,
				  @sql ,
				  'SA_AspNet_IdentityUser_Find' ,
				  'before executing query'
				);
	END		
		
     EXEC sp_executesql @sql;
END
-------------------------
--END of SA_AspNet_IdentityUser_Find
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUsers_GetPagedList]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of SA_AspNet_IdentityUser_GetPagedList
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityUsers_GetPagedList]
	@startIndex INT
    , @pageSize INT
    , @sortExpression VARCHAR(255)
    , @totalRows BIGINT OUTPUT
    WITH EXEC AS CALLER
AS 
BEGIN
    DECLARE @sql NVARCHAR(4000);
	DECLARE @whereClause NVARCHAR(300) = ' WHERE 1 = 1';   
    BEGIN 

    SET @sql = 'SELECT @totalRows = COUNT(*) From [dbo].[SA_AspNet_IdentityUser] '
        + @whereClause;

    EXEC sp_executesql @sql, N'@totalRows INT OUTPUT', @totalRows OUTPUT;
  
SET @sql = 
'SELECT ' + ' [Id] ' +
         ' , [Email] ' +
         ' , [EmailConfirmed] ' +
         ' , [PhoneNumber] ' +
         ' , [PhoneNumberConfirmed] ' +
         ' , [TwoFactorEnabled] ' +
         ' , [LockoutEndDateUtc] ' +
         ' , [LockoutEnabled] ' +
         ' , [AccessFailedCount] ' +
         ' , [UserName] ' +
		 ' , [FullName] ' +
             ' FROM ( ' +
    ' SELECT * ' +
       ' , ROW_NUMBER() OVER ( ORDER BY ' + @sortExpression + ' ) AS [RowNumber] ' +
        ' FROM [dbo].[AspNetUsers] ' +
         @whereClause +
    ' ) AS pagedList ' +
     ' WHERE RowNumber BETWEEN ('+ CONVERT(varchar(10), @StartIndex + 1) + ') AND ( ' + 
  CONVERT(varchar(10), @StartIndex + @PageSize) + ');';
  
        IF ( dbo.OnDebugMode() = 1 ) 
            BEGIN
                INSERT  INTO [dbo].[__SqlLog]
                        ( [EntryTime] ,
                          [SqlQuery] ,
                          [CallerName] ,
                          [LineLocation]
                        )
                VALUES  ( GETDATE() ,
                          @sql ,
                          'SA_AspNet_IdentityUser_GetPagedList' ,
                          'before executing query'
                        );
            END

  --SET @sqlToReturn = @sql;
        EXEC sp_executesql @sql;
    END;

END
-------------------------
--END of SA_AspNet_IdentityUser_GetPagedList
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUsers_Insert]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of SA_AspNet_IdentityUser_Insert
-------------------------
CREATE PROCEDURE [dbo].[SA_AspNet_IdentityUsers_Insert]
	       @id bigint
	 , @email VARCHAR(256)
	 , @emailConfirmed BIT
	 , @passwordHash VARCHAR(MAX)
	 , @securityStamp VARCHAR(MAX)
	 , @phoneNumber VARCHAR(128)
	 , @phoneNumberConfirmed BIT
	 , @twoFactorEnabled BIT
	 , @lockoutEndDateUtc DATETIME
	 , @lockoutEnabled BIT
	 , @accessFailedCount INT
	 , @userName VARCHAR(256)
	 , @fullName VARCHAR(256)
    
        AS
		BEGIN
		
        SET NOCOUNT ON;
		SET XACT_ABORT ON
		
		
        INSERT INTO dbo.AspNetUsers
        (
            [Id]
		 , [Email]
		 , [EmailConfirmed]
		 , [PasswordHash]
		 , [SecurityStamp]
		 , [PhoneNumber]
		 , [PhoneNumberConfirmed]
		 , [TwoFactorEnabled]
		 , [LockoutEndDateUtc]
		 , [LockoutEnabled]
		 , [AccessFailedCount]
		 , [UserName]
		 , [FullName]

        ) VALUES (
            @id
		 , @email
		 , @emailConfirmed
		 , @passwordHash
		 , @securityStamp
		 , @phoneNumber
		 , @phoneNumberConfirmed
		 , @twoFactorEnabled
		 , @lockoutEndDateUtc
		 , @lockoutEnabled
		 , @accessFailedCount
		 , @userName
		 , @fullName
	 
	);

END
-------------------------
--END of SA_AspNet_IdentityUser_Insert
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[SA_AspNet_IdentityUsers_Update]    Script Date: 12/5/2017 7:20:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of SA_AspNet_IdentityUser_Update
-------------------------

CREATE PROCEDURE [dbo].[SA_AspNet_IdentityUsers_Update]
	    @id bigint
	 , @email VARCHAR(256)
	 , @emailConfirmed BIT
	 , @passwordHash VARCHAR(MAX)
	 , @securityStamp VARCHAR(MAX)
	 , @phoneNumber VARCHAR(128)
	 , @phoneNumberConfirmed BIT
	 , @twoFactorEnabled BIT
	 , @lockoutEndDateUtc DATETIME
	 , @lockoutEnabled BIT
	 , @accessFailedCount INT
	 , @userName VARCHAR(256)
	 , @fullName VARCHAR(256)

        AS 
		BEGIN
		
		SET XACT_ABORT ON
		
        UPDATE dbo.AspNetUsers SET
            [Email] = @email
          , [EmailConfirmed] = @emailConfirmed
          , [PasswordHash] = @passwordHash
          , [SecurityStamp] = @securityStamp
          , [PhoneNumber] = @phoneNumber
          , [PhoneNumberConfirmed] = @phoneNumberConfirmed
          , [TwoFactorEnabled] = @twoFactorEnabled
          , [LockoutEndDateUtc] = @lockoutEndDateUtc
          , [LockoutEnabled] = @lockoutEnabled
          , [AccessFailedCount] = @accessFailedCount
          , [UserName] = @userName
		  , [FullName] = @fullName 

      WHERE [Id] = @id 


END
-------------------------
--END of SA_AspNet_IdentityUser_Update
-------------------------


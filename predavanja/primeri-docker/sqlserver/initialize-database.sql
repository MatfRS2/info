CREATE DATABASE OlimpScraperPodaci
GO

SELECT Name from sys.Databases
GO

USE OlimpScraperPodaci
GO





CREATE TABLE [dbo].[Mec](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Sifra] [varchar](200) NULL,
	[NazivKrajnjeTacke] [nvarchar](10) NULL,
	[Sport] [nvarchar](50) NULL,
	[Ime1] [nvarchar](100) NULL,
	[Ime2] [nvarchar](100) NULL,
 CONSTRAINT [PK_match] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO







CREATE TABLE [dbo].[Igra](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TipKladjenja] [nvarchar](50) NOT NULL,
	[IgraVrsta] [nvarchar](50) NOT NULL,
	[IgraVrstaStruktura] [nvarchar](50) NOT NULL,
	[MecId] [bigint] NOT NULL,
	[UpisVreme] [datetime] NULL,
	[AzuriranjeVreme] [datetime] NULL,
	[ProveravanjeVreme] [datetime] NULL,
	[PoslednjaPromenaVreme] [datetime] NULL,
	[KvotaPrvogUpisa] [decimal](10, 4) NULL,
	[Kvota] [decimal](10, 4) NULL,
	[PoslednjaPromena] [decimal](10, 4) NULL,
	[UkupnaPromena] [decimal](10, 4) NULL,
	[KoordinatorObavesten] [bit] NOT NULL,
	[KoordinatorObavestenVreme] [datetime] NULL,
 CONSTRAINT [PK_igra_1] PRIMARY KEY CLUSTERED 
(
	[TipKladjenja] ASC,
	[IgraVrsta] ASC,
	[IgraVrstaStruktura] ASC,
	[MecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Igra]  WITH CHECK ADD  CONSTRAINT [FK_igra_mec] FOREIGN KEY([MecId])
REFERENCES [dbo].[Mec] ([Id])
GO

ALTER TABLE [dbo].[Igra] CHECK CONSTRAINT [FK_igra_mec]
GO














CREATE TABLE [dbo].[Rezultat](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TipKladjenja] [nvarchar](50) NULL,
	[RezultatVrsta] [nvarchar](50) NULL,
	[Naziv] [nvarchar](50) NULL,
	[Vrednost] [nvarchar](max) NULL,
	[Status] [nvarchar](10) NULL,
	[MecVremeMin] [int] NULL,
	[MecVremeSec] [int] NULL,
	[KoordinatorObavesten] [bit] NOT NULL,
	[MecId] [bigint] NULL,
	[UpisVreme] [datetime] NULL,
	[MecPeriod] [varchar](50) NULL,
	[ProveravanjeVreme] [datetime] NULL,
	[PoslednjaPromenaVreme] [datetime] NULL,
	[AzuriranjeVreme] [datetime] NULL,
	[KoordinatorObavestenVreme] [datetime] NULL,
 CONSTRAINT [PK_rezultat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Rezultat] ADD  CONSTRAINT [DF_rezultat_koordinatorobavestenopromeni]  DEFAULT ((0)) FOR [KoordinatorObavesten]
GO

ALTER TABLE [dbo].[Rezultat]  WITH CHECK ADD  CONSTRAINT [FK_Rezultat_Mec] FOREIGN KEY([MecId])
REFERENCES [dbo].[Mec] ([Id])
GO

ALTER TABLE [dbo].[Rezultat] CHECK CONSTRAINT [FK_Rezultat_Mec]
GO
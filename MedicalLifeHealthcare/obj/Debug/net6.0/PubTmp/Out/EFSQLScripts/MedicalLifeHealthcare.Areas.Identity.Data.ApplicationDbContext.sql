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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [FirstName] nvarchar(255) NOT NULL,
        [LastName] nvarchar(255) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [ChronicMedicationTB] (
        [Id] int NOT NULL IDENTITY,
        [MedicationName] nvarchar(max) NOT NULL,
        [Dosage] nvarchar(max) NOT NULL,
        [PatientId] int NOT NULL,
        CONSTRAINT [PK_ChronicMedicationTB] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [CounsellingTB] (
        [Id] int NOT NULL IDENTITY,
        [SessionDate] datetime2 NOT NULL,
        [TherapistName] nvarchar(max) NOT NULL,
        [PatientName] nvarchar(max) NOT NULL,
        [PatientContact] nvarchar(max) NOT NULL,
        [TherapistContact] nvarchar(max) NOT NULL,
        [SessionNotes] nvarchar(max) NOT NULL,
        [IsCompleted] bit NOT NULL,
        [IsFollowUpRequired] bit NOT NULL,
        [FollowUpDate] datetime2 NOT NULL,
        [FollowUpNotes] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_CounsellingTB] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [CounsellorPatientTB] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [DateOfBirth] datetime2 NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [Gender] nvarchar(max) NOT NULL,
        [MedicalHistory] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_CounsellorPatientTB] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [CounselorTB] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [Specialization] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [Bio] nvarchar(max) NOT NULL,
        [ProfileImage] nvarchar(max) NOT NULL,
        [IsActive] bit NOT NULL,
        [Education] nvarchar(max) NOT NULL,
        [YearsOfExperience] int NOT NULL,
        [LicenseNumber] nvarchar(max) NOT NULL,
        [LanguagesSpoken] nvarchar(max) NOT NULL,
        [OfficeAddress] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_CounselorTB] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [GBVReportTB] (
        [Id] int NOT NULL IDENTITY,
        [ReportDate] datetime2 NOT NULL,
        [VictimName] nvarchar(max) NOT NULL,
        [PerpetratorName] nvarchar(max) NOT NULL,
        [IncidentDetails] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_GBVReportTB] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [LabTB] (
        [Id] int NOT NULL IDENTITY,
        [TestId] int NOT NULL,
        [ResultDetails] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_LabTB] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [PatientTB] (
        [patientID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [DOB] nvarchar(max) NOT NULL,
        [Gender] nvarchar(max) NOT NULL,
        [MedicalHistory] nvarchar(max) NOT NULL,
        [PrescriptionHistory] nvarchar(max) NOT NULL,
        [AdmissionTime] datetime2 NOT NULL,
        [MyProperty] int NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [MyProperty1] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_PatientTB] PRIMARY KEY ([patientID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [SessionType] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_SessionType] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [TestTB] (
        [Id] int NOT NULL IDENTITY,
        [TestName] nvarchar(max) NOT NULL,
        [TestDate] datetime2 NOT NULL,
        [PatientId] int NOT NULL,
        CONSTRAINT [PK_TestTB] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [WalkinTB] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [RegistrationDate] datetime2 NOT NULL,
        [Symptoms] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_WalkinTB] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [AppointmentTB] (
        [Id] int NOT NULL IDENTITY,
        [PatientId] int NOT NULL,
        [CounselorId] int NOT NULL,
        [StartTime] datetime2 NOT NULL,
        [EndTime] datetime2 NOT NULL,
        [IsConfirmed] bit NOT NULL,
        [Notes] nvarchar(max) NOT NULL,
        [CounsellorPatientId] int NULL,
        CONSTRAINT [PK_AppointmentTB] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AppointmentTB_CounsellorPatientTB_CounsellorPatientId] FOREIGN KEY ([CounsellorPatientId]) REFERENCES [CounsellorPatientTB] ([Id]),
        CONSTRAINT [FK_AppointmentTB_CounselorTB_CounselorId] FOREIGN KEY ([CounselorId]) REFERENCES [CounselorTB] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AppointmentTB_PatientTB_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [PatientTB] ([patientID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE TABLE [SessionBooking] (
        [Id] int NOT NULL IDENTITY,
        [AppointmentId] int NOT NULL,
        [SessionTypeId] int NOT NULL,
        CONSTRAINT [PK_SessionBooking] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SessionBooking_AppointmentTB_AppointmentId] FOREIGN KEY ([AppointmentId]) REFERENCES [AppointmentTB] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_SessionBooking_SessionType_SessionTypeId] FOREIGN KEY ([SessionTypeId]) REFERENCES [SessionType] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE INDEX [IX_AppointmentTB_CounsellorPatientId] ON [AppointmentTB] ([CounsellorPatientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE INDEX [IX_AppointmentTB_CounselorId] ON [AppointmentTB] ([CounselorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE INDEX [IX_AppointmentTB_PatientId] ON [AppointmentTB] ([PatientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE INDEX [IX_SessionBooking_AppointmentId] ON [SessionBooking] ([AppointmentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    CREATE INDEX [IX_SessionBooking_SessionTypeId] ON [SessionBooking] ([SessionTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908205408_initDB')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230908205408_initDB', N'6.0.21');
END;
GO

COMMIT;
GO


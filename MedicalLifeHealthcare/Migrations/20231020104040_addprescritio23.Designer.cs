﻿// <auto-generated />
using System;
using MedicalLifeHealthcare.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedicalLifeHealthcare.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231020104040_addprescritio23")]
    partial class addprescritio23
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LastName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("MyPicture")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Alert", b =>
                {
                    b.Property<int>("AlertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlertId"), 1L, 1);

                    b.Property<string>("IntendedUser")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Purpose")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.HasKey("AlertId");

                    b.HasIndex("IntendedUser");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Appointments", b =>
                {
                    b.Property<int>("AppointmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentID"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date_Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("PatientID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AppointmentID");

                    b.HasIndex("PatientID");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Counselling_Sessions", b =>
                {
                    b.Property<int>("SessionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SessionID"), 1L, 1);

                    b.Property<int?>("AppointemtID")
                        .HasColumnType("int");

                    b.Property<string>("CounsellorID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SessionCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SessionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SessionTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SessionID");

                    b.HasIndex("AppointemtID");

                    b.HasIndex("CounsellorID");

                    b.ToTable("Counselling_Sessions");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.IncidentReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ActionsTaken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("InciodentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ReportDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PatientID");

                    b.ToTable("IncidentReport");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Medical_Feedback", b =>
                {
                    b.Property<int>("FeedbackID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackID"), 1L, 1);

                    b.Property<DateTime?>("AnsweredDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DoctorsFeedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoctorsID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Feedback")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FeedbackDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PatientID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("PrescresptionID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("FeedbackID");

                    b.HasIndex("DoctorsID");

                    b.HasIndex("PatientID");

                    b.HasIndex("PrescresptionID");

                    b.ToTable("Medical_Feedback");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Medical_File", b =>
                {
                    b.Property<int>("FileID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FileID"), 1L, 1);

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Allergies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnySurgeries")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("BloodType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmergencyContactNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmergencyPerson")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ExtraNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("IDNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("PatientID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Relationship")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FileID");

                    b.HasIndex("PatientID");

                    b.ToTable("Medical_File");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Medical_Records", b =>
                {
                    b.Property<int>("RecordsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecordsID"), 1L, 1);

                    b.Property<decimal?>("BloodPressure")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CurrentMedications")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FileID")
                        .HasColumnType("int");

                    b.Property<int?>("HeartRate")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<decimal?>("Height")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NurseID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("RecordDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Temperature")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Weight")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("RecordsID");

                    b.HasIndex("FileID");

                    b.HasIndex("NurseID");

                    b.ToTable("Medical_Records");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.MedicalRefill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("ApprovalDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DoctorsID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PrescriptionId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityRequested")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorsID");

                    b.HasIndex("PatientID");

                    b.HasIndex("PrescriptionId");

                    b.ToTable("MedicalRefill");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Prescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DatePrescribed")
                        .HasColumnType("datetime2");

                    b.Property<string>("DoctorsID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Dosage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MedicationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DoctorsID");

                    b.HasIndex("PatientId");

                    b.ToTable("Prescription");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Que", b =>
                {
                    b.Property<int>("QueID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QueID"), 1L, 1);

                    b.Property<int>("AppointmentID")
                        .HasColumnType("int");

                    b.Property<string>("ClinicianID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dateOFQue")
                        .HasColumnType("datetime2");

                    b.HasKey("QueID");

                    b.HasIndex("AppointmentID");

                    b.HasIndex("ClinicianID");

                    b.ToTable("Que");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.SampleResults", b =>
                {
                    b.Property<int>("SampleResultsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SampleResultsId"), 1L, 1);

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Interpretation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PathologyID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReferenceRange")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ResultDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResultValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SamplesID")
                        .HasColumnType("int");

                    b.Property<string>("TestName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitOfMeasure")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SampleResultsId");

                    b.HasIndex("PathologyID");

                    b.HasIndex("SamplesID");

                    b.ToTable("SampleResults");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Samples", b =>
                {
                    b.Property<int>("SampleCollectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SampleCollectionId"), 1L, 1);

                    b.Property<DateTime?>("CollectionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CollectionLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CollectionMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CollectorName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SampleContainerNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TestRequestId")
                        .HasColumnType("int");

                    b.HasKey("SampleCollectionId");

                    b.HasIndex("CollectorName");

                    b.HasIndex("TestRequestId");

                    b.ToTable("Samples");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Session_Feedback", b =>
                {
                    b.Property<int>("FeedbackID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackID"), 1L, 1);

                    b.Property<string>("Counsellor_Feedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Feedback")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FeedbackDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Satus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SessionID")
                        .HasColumnType("int");

                    b.HasKey("FeedbackID");

                    b.HasIndex("SessionID");

                    b.ToTable("Session_Feedback");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.TestRequest", b =>
                {
                    b.Property<int>("TestRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TestRequestId"), 1L, 1);

                    b.Property<string>("Instructions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RequestedBy")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TestName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TestRequestId");

                    b.HasIndex("PatientId");

                    b.HasIndex("RequestedBy");

                    b.ToTable("TestRequest");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Alert", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "CurrentUser")
                        .WithMany()
                        .HasForeignKey("IntendedUser");

                    b.Navigation("CurrentUser");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Appointments", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "MainUser")
                        .WithMany()
                        .HasForeignKey("PatientID");

                    b.Navigation("MainUser");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Counselling_Sessions", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Models.Appointments", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointemtID");

                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "Counsellor")
                        .WithMany()
                        .HasForeignKey("CounsellorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Counsellor");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.IncidentReport", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Medical_Feedback", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorsID");

                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID");

                    b.HasOne("MedicalLifeHealthcare.Models.Prescription", "Prescription")
                        .WithMany()
                        .HasForeignKey("PrescresptionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Medical_File", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "mainUser")
                        .WithMany()
                        .HasForeignKey("PatientID");

                    b.Navigation("mainUser");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Medical_Records", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Models.Medical_File", "File")
                        .WithMany()
                        .HasForeignKey("FileID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "Nurse")
                        .WithMany()
                        .HasForeignKey("NurseID");

                    b.Navigation("File");

                    b.Navigation("Nurse");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.MedicalRefill", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorsID");

                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID");

                    b.HasOne("MedicalLifeHealthcare.Models.Prescription", "Prescription")
                        .WithMany()
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Prescription", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorsID");

                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Que", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Models.Appointments", "Appointments")
                        .WithMany()
                        .HasForeignKey("AppointmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "Clinician")
                        .WithMany()
                        .HasForeignKey("ClinicianID");

                    b.Navigation("Appointments");

                    b.Navigation("Clinician");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.SampleResults", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "Pathology")
                        .WithMany()
                        .HasForeignKey("PathologyID");

                    b.HasOne("MedicalLifeHealthcare.Models.Samples", "Samples")
                        .WithMany()
                        .HasForeignKey("SamplesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pathology");

                    b.Navigation("Samples");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Samples", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "Nurse")
                        .WithMany()
                        .HasForeignKey("CollectorName");

                    b.HasOne("MedicalLifeHealthcare.Models.TestRequest", "TestRequest")
                        .WithMany()
                        .HasForeignKey("TestRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nurse");

                    b.Navigation("TestRequest");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.Session_Feedback", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Models.Counselling_Sessions", "Session")
                        .WithMany()
                        .HasForeignKey("SessionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Session");
                });

            modelBuilder.Entity("MedicalLifeHealthcare.Models.TestRequest", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");

                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", "Clinician")
                        .WithMany()
                        .HasForeignKey("RequestedBy");

                    b.Navigation("Clinician");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MedicalLifeHealthcare.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

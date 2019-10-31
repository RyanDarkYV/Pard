﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pard.Persistence.Contexts;

namespace Pard.Persistence.Migrations
{
    [DbContext(typeof(RecordsContext))]
    [Migration("20191030124824_CascadeDelete")]
    partial class CascadeDelete
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pard.Domain.Entities.Locations.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressCity");

                    b.Property<string>("AddressCountry");

                    b.Property<string>("AddressState");

                    b.Property<string>("AddressStreet");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<Guid>("RecordId");

                    b.HasKey("Id");

                    b.HasIndex("RecordId")
                        .IsUnique();

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Pard.Domain.Entities.Records.Record", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<DateTime?>("FinishedAt");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsDone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("Pard.Domain.Entities.Locations.Location", b =>
                {
                    b.HasOne("Pard.Domain.Entities.Records.Record", "Record")
                        .WithOne("Location")
                        .HasForeignKey("Pard.Domain.Entities.Locations.Location", "RecordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

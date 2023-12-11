﻿// <auto-generated />
using System;
using HorseAuction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HorseAuction.Migrations
{
    [DbContext(typeof(AuctionDbContext))]
    [Migration("20231211180158_AddedFullName")]
    partial class AddedFullName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("Bid", b =>
                {
                    b.Property<int>("BidId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<int>("BidderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HorseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("BidId");

                    b.HasIndex("BidderId");

                    b.HasIndex("HorseId");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("Bidder", b =>
                {
                    b.Property<int>("BidderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BidderName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BidderId");

                    b.ToTable("Bidders");
                });

            modelBuilder.Entity("Horse", b =>
                {
                    b.Property<int>("HorseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("HorseName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PerformanceType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("StartingBid")
                        .HasColumnType("TEXT");

                    b.HasKey("HorseId");

                    b.ToTable("Horses");

                    b.HasData(
                        new
                        {
                            HorseId = 1,
                            Age = 3,
                            Color = "Red Dun",
                            Description = "Easy Keeper. Fun to be Around",
                            HorseName = "Genetic",
                            PerformanceType = "Western Pleasure",
                            StartingBid = 3500m
                        },
                        new
                        {
                            HorseId = 2,
                            Age = 4,
                            Color = "Bay with a snip",
                            Description = "Barn sour and will kick you",
                            HorseName = "Rowdy",
                            PerformanceType = "Reining",
                            StartingBid = 5000m
                        },
                        new
                        {
                            HorseId = 3,
                            Age = 2,
                            Color = "Brown",
                            Description = "Very green, just started under saddle",
                            HorseName = "Brownie",
                            PerformanceType = "Western Pleasure",
                            StartingBid = 10000m
                        },
                        new
                        {
                            HorseId = 4,
                            Age = 10,
                            Color = "Gray",
                            Description = "Trained Professionally Ready to Go",
                            HorseName = "Cinnamon",
                            PerformanceType = "Western Pleasure",
                            StartingBid = 15000m
                        });
                });

            modelBuilder.Entity("HorseAuction.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CellPhone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Bid", b =>
                {
                    b.HasOne("Bidder", "Bidder")
                        .WithMany("Bids")
                        .HasForeignKey("BidderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Horse", "Horse")
                        .WithMany("Bids")
                        .HasForeignKey("HorseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bidder");

                    b.Navigation("Horse");
                });

            modelBuilder.Entity("Bidder", b =>
                {
                    b.Navigation("Bids");
                });

            modelBuilder.Entity("Horse", b =>
                {
                    b.Navigation("Bids");
                });
#pragma warning restore 612, 618
        }
    }
}

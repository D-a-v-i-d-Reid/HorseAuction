﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HorseAuction.Migrations
{
    [DbContext(typeof(AuctionDbContext))]
    [Migration("20231129192227_InitialCreate")]
    partial class InitialCreate
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

                    b.Property<string>("BidderName")
                        .HasColumnType("TEXT");

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
                });

            modelBuilder.Entity("Bid", b =>
                {
                    b.HasOne("Bidder", "Bidder")
                        .WithMany()
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

            modelBuilder.Entity("Horse", b =>
                {
                    b.Navigation("Bids");
                });
#pragma warning restore 612, 618
        }
    }
}

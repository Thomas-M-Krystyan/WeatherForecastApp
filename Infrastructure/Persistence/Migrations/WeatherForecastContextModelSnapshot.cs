﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherForecastApp.Persistence.Context;

#nullable disable

namespace WeatherForecastApp.Persistence.Migrations
{
    [DbContext(typeof(WeatherForecastContext))]
    partial class WeatherForecastContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WeatherForecastApp.Domain.Models.WeatherForecastEntity", b =>
                {
                    b.Property<DateOnly>("Date")
                        .HasColumnType("date")
                        .HasColumnName("Date")
                        .HasColumnOrder(0);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnOrder(2);

                    b.Property<float>("TempCelsius")
                        .HasColumnType("real")
                        .HasColumnOrder(1);

                    b.HasKey("Date");

                    b.ToTable("WeatherForecast");
                });
#pragma warning restore 612, 618
        }
    }
}

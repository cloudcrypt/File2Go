using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using F2GWeb.Services;

namespace F2GWeb.Migrations
{
    [DbContext(typeof(F2GContext))]
    [Migration("20170222055423_Migration1")]
    partial class Migration1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("F2GWeb.Models.File", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("name");

                    b.Property<DateTime>("uploaded");

                    b.Property<string>("useremail");

                    b.HasKey("ID");

                    b.HasIndex("useremail");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("F2GWeb.Models.User", b =>
                {
                    b.Property<string>("email")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created");

                    b.Property<string>("fname");

                    b.Property<string>("hash");

                    b.Property<string>("lname");

                    b.HasKey("email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("F2GWeb.Models.File", b =>
                {
                    b.HasOne("F2GWeb.Models.User", "user")
                        .WithMany("Files")
                        .HasForeignKey("useremail");
                });
        }
    }
}

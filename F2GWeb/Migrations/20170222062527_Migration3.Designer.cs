using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using F2GWeb.Services;

namespace F2GWeb.Migrations
{
    [DbContext(typeof(F2GContext))]
    [Migration("20170222062527_Migration3")]
    partial class Migration3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("F2GWeb.Models.Client", b =>
                {
                    b.Property<string>("ip")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Useremail");

                    b.Property<string>("name");

                    b.HasKey("ip");

                    b.HasIndex("Useremail");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("F2GWeb.Models.File", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("contents");

                    b.Property<string>("name");

                    b.Property<DateTime>("uploaded");

                    b.Property<string>("useremail");

                    b.HasKey("ID");

                    b.HasIndex("useremail");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("F2GWeb.Models.Request", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Useremail");

                    b.Property<string>("clientip");

                    b.Property<string>("fileName");

                    b.HasKey("ID");

                    b.HasIndex("Useremail");

                    b.HasIndex("clientip");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("F2GWeb.Models.Response", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("requestID");

                    b.Property<bool>("success");

                    b.HasKey("ID");

                    b.HasIndex("requestID");

                    b.ToTable("Responses");
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

            modelBuilder.Entity("F2GWeb.Models.Client", b =>
                {
                    b.HasOne("F2GWeb.Models.User", "User")
                        .WithMany("clients")
                        .HasForeignKey("Useremail");
                });

            modelBuilder.Entity("F2GWeb.Models.File", b =>
                {
                    b.HasOne("F2GWeb.Models.User", "user")
                        .WithMany("files")
                        .HasForeignKey("useremail");
                });

            modelBuilder.Entity("F2GWeb.Models.Request", b =>
                {
                    b.HasOne("F2GWeb.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Useremail");

                    b.HasOne("F2GWeb.Models.Client", "client")
                        .WithMany()
                        .HasForeignKey("clientip");
                });

            modelBuilder.Entity("F2GWeb.Models.Response", b =>
                {
                    b.HasOne("F2GWeb.Models.Request", "request")
                        .WithMany()
                        .HasForeignKey("requestID");
                });
        }
    }
}

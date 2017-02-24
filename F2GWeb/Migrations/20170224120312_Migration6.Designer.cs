using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using F2GWeb.Services;

namespace F2GWeb.Migrations
{
    [DbContext(typeof(F2GContext))]
    [Migration("20170224120312_Migration6")]
    partial class Migration6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("F2G.Models.Client", b =>
                {
                    b.Property<string>("ip")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Useremail");

                    b.Property<bool>("active");

                    b.Property<string>("name");

                    b.HasKey("ip");

                    b.HasIndex("Useremail");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("F2G.Models.File", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Useremail");

                    b.Property<byte[]>("contents");

                    b.Property<string>("name");

                    b.Property<int?>("responseID");

                    b.Property<DateTime>("uploaded");

                    b.HasKey("ID");

                    b.HasIndex("Useremail");

                    b.HasIndex("responseID");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("F2G.Models.Request", b =>
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

            modelBuilder.Entity("F2G.Models.Response", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Useremail");

                    b.Property<string>("clientip");

                    b.Property<bool>("success");

                    b.HasKey("ID");

                    b.HasIndex("Useremail");

                    b.HasIndex("clientip");

                    b.ToTable("Responses");
                });

            modelBuilder.Entity("F2G.Models.User", b =>
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

            modelBuilder.Entity("F2G.Models.Client", b =>
                {
                    b.HasOne("F2G.Models.User", "User")
                        .WithMany("clients")
                        .HasForeignKey("Useremail");
                });

            modelBuilder.Entity("F2G.Models.File", b =>
                {
                    b.HasOne("F2G.Models.User")
                        .WithMany("files")
                        .HasForeignKey("Useremail");

                    b.HasOne("F2G.Models.Response", "response")
                        .WithMany()
                        .HasForeignKey("responseID");
                });

            modelBuilder.Entity("F2G.Models.Request", b =>
                {
                    b.HasOne("F2G.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Useremail");

                    b.HasOne("F2G.Models.Client", "client")
                        .WithMany()
                        .HasForeignKey("clientip");
                });

            modelBuilder.Entity("F2G.Models.Response", b =>
                {
                    b.HasOne("F2G.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Useremail");

                    b.HasOne("F2G.Models.Client", "client")
                        .WithMany()
                        .HasForeignKey("clientip");
                });
        }
    }
}

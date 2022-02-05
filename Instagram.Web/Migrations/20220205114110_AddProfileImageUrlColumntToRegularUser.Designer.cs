﻿// <auto-generated />
using Instagram.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Instagram.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220205114110_AddProfileImageUrlColumntToRegularUser")]
    partial class AddProfileImageUrlColumntToRegularUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Instagram.Web.Entities.Friendship", b =>
                {
                    b.Property<int>("FromId")
                        .HasColumnType("int");

                    b.Property<int>("ToId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("FromId", "ToId");

                    b.HasIndex("ToId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("Instagram.Web.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PublisherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Instagram.Web.Entities.PostLikes", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("UserId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("PostLikes");
                });

            modelBuilder.Entity("Instagram.Web.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Instagram.Web.Entities.Admin", b =>
                {
                    b.HasBaseType("Instagram.Web.Entities.User");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Instagram.Web.Entities.RegularUser", b =>
                {
                    b.HasBaseType("Instagram.Web.Entities.User");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("RegularUsers");
                });

            modelBuilder.Entity("Instagram.Web.Entities.Friendship", b =>
                {
                    b.HasOne("Instagram.Web.Entities.RegularUser", "From")
                        .WithMany("Follows")
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Instagram.Web.Entities.RegularUser", "To")
                        .WithMany("Followers")
                        .HasForeignKey("ToId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("From");

                    b.Navigation("To");
                });

            modelBuilder.Entity("Instagram.Web.Entities.Post", b =>
                {
                    b.HasOne("Instagram.Web.Entities.RegularUser", "Publisher")
                        .WithMany("Posts")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("Instagram.Web.Entities.PostLikes", b =>
                {
                    b.HasOne("Instagram.Web.Entities.Post", "Post")
                        .WithMany("PostLikes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Instagram.Web.Entities.RegularUser", "User")
                        .WithMany("PostLikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Instagram.Web.Entities.Admin", b =>
                {
                    b.HasOne("Instagram.Web.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("Instagram.Web.Entities.Admin", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Instagram.Web.Entities.RegularUser", b =>
                {
                    b.HasOne("Instagram.Web.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("Instagram.Web.Entities.RegularUser", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Instagram.Web.Entities.Post", b =>
                {
                    b.Navigation("PostLikes");
                });

            modelBuilder.Entity("Instagram.Web.Entities.RegularUser", b =>
                {
                    b.Navigation("Followers");

                    b.Navigation("Follows");

                    b.Navigation("PostLikes");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service.Catalog.Persistence;

#nullable disable

namespace Service.Catalog.Persistence.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    [Migration("20240802074911_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("catalog")
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Primitives.Entity<Service.Catalog.Domain.ImageSources.ImageSourceId>", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("nvarchar(34)")
                        .HasColumnName("discriminator");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.HasKey("Id")
                        .HasName("pk_image_sources");

                    b.ToTable("image_sources", "catalog");

                    b.HasDiscriminator().HasValue("Entity<ImageSourceId>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Persistence.Inbox.InboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_inbox_messages");

                    b.ToTable("inbox_messages", "catalog");
                });

            modelBuilder.Entity("Persistence.Inbox.InboxMessageConsumer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("name");

                    b.HasKey("Id", "Name")
                        .HasName("pk_inbox_message_consumers");

                    b.ToTable("inbox_message_consumers", "catalog");
                });

            modelBuilder.Entity("Persistence.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("outbox_messages", "catalog");
                });

            modelBuilder.Entity("Persistence.Outbox.OutboxMessageConsumer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("name");

                    b.HasKey("Id", "Name")
                        .HasName("pk_outbox_message_consumers");

                    b.ToTable("outbox_message_consumers", "catalog");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Authors.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_on_utc");

                    b.Property<string>("Description")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("")
                        .HasColumnName("description");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("last_name");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_on_utc");

                    b.HasKey("Id")
                        .HasName("pk_authors");

                    b.ToTable("authors", "catalog");
                });

            modelBuilder.Entity("Service.Catalog.Domain.BookSources.BookSource", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("book_id");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_on_utc");

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("format");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_on_utc");

                    b.Property<string>("PreviewUrl")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("preview_url");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

                    b.Property<long?>("StockQuantity")
                        .HasColumnType("bigint")
                        .HasColumnName("stock_quantity");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("url");

                    b.HasKey("Id")
                        .HasName("pk_book_sources");

                    b.HasIndex("BookId")
                        .HasDatabaseName("ix_book_sources_book_id");

                    b.ToTable("book_sources", "catalog");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Books.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<long>("AgeRating")
                        .HasColumnType("bigint")
                        .HasColumnName("age_rating");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_on_utc");

                    b.Property<string>("Description")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("")
                        .HasColumnName("description");

                    b.Property<string>("ISBN")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("isbn");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)")
                        .HasColumnName("language");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_on_utc");

                    b.Property<DateOnly?>("PublishedDate")
                        .HasColumnType("date")
                        .HasColumnName("published_date");

                    b.Property<Guid?>("PublisherId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("publisher_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_books");

                    b.HasIndex("ISBN")
                        .IsUnique()
                        .HasDatabaseName("ix_books_isbn")
                        .HasFilter("[isbn] IS NOT NULL");

                    b.HasIndex("PublisherId")
                        .HasDatabaseName("ix_books_publisher_id");

                    b.ToTable("books", "catalog");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Books.BookAuthor", b =>
                {
                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("book_id");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("author_id");

                    b.HasKey("BookId", "AuthorId")
                        .HasName("pk_book_author");

                    b.HasIndex("AuthorId")
                        .HasDatabaseName("ix_book_author_author_id");

                    b.ToTable("book_author", "catalog");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Books.BookCategory", b =>
                {
                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("book_id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("category_id");

                    b.HasKey("BookId", "CategoryId")
                        .HasName("pk_book_category");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_book_category_category_id");

                    b.ToTable("book_category", "catalog");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Categories.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_on_utc");

                    b.Property<string>("Description")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_on_utc");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.HasIndex("Title")
                        .IsUnique()
                        .HasDatabaseName("ix_categories_title");

                    b.ToTable("categories", "catalog");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Publishers.Publisher", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("address");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("country");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_on_utc");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_on_utc");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_publishers");

                    b.ToTable("publishers", "catalog");
                });

            modelBuilder.Entity("Service.Catalog.Domain.ImageSources.ImageSource<Service.Catalog.Domain.Authors.AuthorImageType>", b =>
                {
                    b.HasBaseType("Domain.Primitives.Entity<Service.Catalog.Domain.ImageSources.ImageSourceId>");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("author_id");

                    b.Property<string>("Source")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Source");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Type");

                    b.HasIndex("AuthorId")
                        .HasDatabaseName("ix_image_sources_author_id");

                    b.ToTable("image_sources", "catalog");

                    b.HasDiscriminator().HasValue("ImageSource<AuthorImageType>");
                });

            modelBuilder.Entity("Service.Catalog.Domain.ImageSources.ImageSource<Service.Catalog.Domain.Books.BookImageType>", b =>
                {
                    b.HasBaseType("Domain.Primitives.Entity<Service.Catalog.Domain.ImageSources.ImageSourceId>");

                    b.Property<Guid?>("BookId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("book_id");

                    b.Property<string>("Source")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Source");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Type");

                    b.HasIndex("BookId")
                        .HasDatabaseName("ix_image_sources_book_id");

                    b.ToTable("image_sources", "catalog");

                    b.HasDiscriminator().HasValue("ImageSource<BookImageType>");
                });

            modelBuilder.Entity("Service.Catalog.Domain.ImageSources.ImageSource<Service.Catalog.Domain.Categories.CategoryImageType>", b =>
                {
                    b.HasBaseType("Domain.Primitives.Entity<Service.Catalog.Domain.ImageSources.ImageSourceId>");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("category_id");

                    b.Property<string>("Source")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Source");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Type");

                    b.HasIndex("CategoryId")
                        .IsUnique()
                        .HasDatabaseName("ix_image_sources_category_id")
                        .HasFilter("[category_id] IS NOT NULL");

                    b.ToTable("image_sources", "catalog");

                    b.HasDiscriminator().HasValue("ImageSource<CategoryImageType>");
                });

            modelBuilder.Entity("Service.Catalog.Domain.ImageSources.ImageSource<Service.Catalog.Domain.Publishers.PublisherImageType>", b =>
                {
                    b.HasBaseType("Domain.Primitives.Entity<Service.Catalog.Domain.ImageSources.ImageSourceId>");

                    b.Property<Guid?>("PublisherId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("publisher_id");

                    b.Property<string>("Source")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Source");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Type");

                    b.HasIndex("PublisherId")
                        .HasDatabaseName("ix_image_sources_publisher_id");

                    b.ToTable("image_sources", "catalog");

                    b.HasDiscriminator().HasValue("ImageSource<PublisherImageType>");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Authors.Author", b =>
                {
                    b.OwnsOne("Service.Catalog.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("AuthorId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<string>("EmailAddress")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("email_email_address");

                            b1.HasKey("AuthorId");

                            b1.HasIndex("EmailAddress")
                                .HasDatabaseName("ix_authors_email_email_address");

                            b1.ToTable("authors", "catalog");

                            b1.WithOwner()
                                .HasForeignKey("AuthorId")
                                .HasConstraintName("fk_authors_authors_id");
                        });

                    b.OwnsOne("Service.Catalog.Domain.ValueObjects.Website", "Website", b1 =>
                        {
                            b1.Property<Guid>("AuthorId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("website_url");

                            b1.HasKey("AuthorId");

                            b1.ToTable("authors", "catalog");

                            b1.WithOwner()
                                .HasForeignKey("AuthorId")
                                .HasConstraintName("fk_authors_authors_id");
                        });

                    b.Navigation("Email");

                    b.Navigation("Website");
                });

            modelBuilder.Entity("Service.Catalog.Domain.BookSources.BookSource", b =>
                {
                    b.HasOne("Service.Catalog.Domain.Books.Book", "Book")
                        .WithMany("Sources")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_book_sources_books_book_id");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Books.Book", b =>
                {
                    b.HasOne("Service.Catalog.Domain.Publishers.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_books_publishers_publisher_id");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Books.BookAuthor", b =>
                {
                    b.HasOne("Service.Catalog.Domain.Authors.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_book_author_authors_author_id");

                    b.HasOne("Service.Catalog.Domain.Books.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_book_author_books_book_id");

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Books.BookCategory", b =>
                {
                    b.HasOne("Service.Catalog.Domain.Books.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_book_category_books_book_id");

                    b.HasOne("Service.Catalog.Domain.Categories.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_book_category_categories_category_id");

                    b.Navigation("Book");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Publishers.Publisher", b =>
                {
                    b.OwnsOne("Service.Catalog.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("PublisherId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<string>("EmailAddress")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("email_email_address");

                            b1.HasKey("PublisherId");

                            b1.ToTable("publishers", "catalog");

                            b1.WithOwner()
                                .HasForeignKey("PublisherId")
                                .HasConstraintName("fk_publishers_publishers_id");
                        });

                    b.OwnsOne("Service.Catalog.Domain.ValueObjects.Website", "Website", b1 =>
                        {
                            b1.Property<Guid>("PublisherId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("website_url");

                            b1.HasKey("PublisherId");

                            b1.ToTable("publishers", "catalog");

                            b1.WithOwner()
                                .HasForeignKey("PublisherId")
                                .HasConstraintName("fk_publishers_publishers_id");
                        });

                    b.OwnsOne("Service.Catalog.Domain.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("PublisherId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("nvarchar(15)")
                                .HasColumnName("phone_number_number");

                            b1.HasKey("PublisherId");

                            b1.ToTable("publishers", "catalog");

                            b1.WithOwner()
                                .HasForeignKey("PublisherId")
                                .HasConstraintName("fk_publishers_publishers_id");
                        });

                    b.Navigation("Email");

                    b.Navigation("PhoneNumber");

                    b.Navigation("Website");
                });

            modelBuilder.Entity("Service.Catalog.Domain.ImageSources.ImageSource<Service.Catalog.Domain.Authors.AuthorImageType>", b =>
                {
                    b.HasOne("Service.Catalog.Domain.Authors.Author", null)
                        .WithMany("Images")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_image_sources_authors_author_id");
                });

            modelBuilder.Entity("Service.Catalog.Domain.ImageSources.ImageSource<Service.Catalog.Domain.Books.BookImageType>", b =>
                {
                    b.HasOne("Service.Catalog.Domain.Books.Book", null)
                        .WithMany("Images")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_image_sources_books_book_id");
                });

            modelBuilder.Entity("Service.Catalog.Domain.ImageSources.ImageSource<Service.Catalog.Domain.Categories.CategoryImageType>", b =>
                {
                    b.HasOne("Service.Catalog.Domain.Categories.Category", null)
                        .WithOne("Icon")
                        .HasForeignKey("Service.Catalog.Domain.ImageSources.ImageSource<Service.Catalog.Domain.Categories.CategoryImageType>", "CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_image_sources_categories_category_id");
                });

            modelBuilder.Entity("Service.Catalog.Domain.ImageSources.ImageSource<Service.Catalog.Domain.Publishers.PublisherImageType>", b =>
                {
                    b.HasOne("Service.Catalog.Domain.Publishers.Publisher", null)
                        .WithMany("Images")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_image_sources_publishers_publisher_id");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Authors.Author", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Books.Book", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Sources");
                });

            modelBuilder.Entity("Service.Catalog.Domain.Categories.Category", b =>
                {
                    b.Navigation("Icon")
                        .IsRequired();
                });

            modelBuilder.Entity("Service.Catalog.Domain.Publishers.Publisher", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}

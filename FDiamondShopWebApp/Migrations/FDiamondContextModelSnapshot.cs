﻿// <auto-generated />
using System;
using FDiamondShop.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FDiamondShop.API.Migrations
{
    [DbContext(typeof(FDiamondContext))]
    partial class FDiamondContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FDiamondShop.API.Models.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("account_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<DateTime?>("DateCreate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("date_create")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("Googleid")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("googleid");

                    b.Property<bool?>("IsGoogleAccount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("is_google_account")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("password_hash");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("role")
                        .HasDefaultValueSql("('Customer')");

                    b.HasKey("AccountId")
                        .HasName("PK__accounts__46A222CD8ABCA3F2");

                    b.HasIndex(new[] { "Email" }, "UQ__accounts__AB6E6164A2413C2B")
                        .IsUnique();

                    b.ToTable("accounts", (string)null);
                });

            modelBuilder.Entity("FDiamondShop.API.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("category_name");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("description");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("image_url");

                    b.HasKey("CategoryId")
                        .HasName("PK__categori__D54EE9B40910DE00");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("FDiamondShop.API.Models.CategoryVariant", b =>
                {
                    b.Property<int>("VariantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("variant_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VariantId"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("description");

                    b.Property<string>("VariantName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("variant_name");

                    b.HasKey("VariantId")
                        .HasName("PK__category__EACC68B780BFAFCD");

                    b.HasIndex("CategoryId");

                    b.ToTable("category_variants", (string)null);
                });

            modelBuilder.Entity("FDiamondShop.API.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("base_price");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted")
                        .HasDefaultValueSql("((0))");

                    b.Property<bool>("IsVisible")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("is_visible")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("product_name");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<int?>("SubCategoryId")
                        .HasColumnType("int")
                        .HasColumnName("sub_category_id");

                    b.HasKey("ProductId")
                        .HasName("PK__products__47027DF50082A97F");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("FDiamondShop.API.Models.ProductImage", b =>
                {
                    b.Property<int>("ProductImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("product_image_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductImageId"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("image_url");

                    b.Property<bool?>("IsGia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("is_GIA")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    b.HasKey("ProductImageId")
                        .HasName("PK__product___0302EB4A81F0C8B7");

                    b.HasIndex("ProductId");

                    b.ToTable("product_images", (string)null);
                });

            modelBuilder.Entity("FDiamondShop.API.Models.ProductVariantValue", b =>
                {
                    b.Property<int>("VariantId")
                        .HasColumnType("int")
                        .HasColumnName("variant_id");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("value");

                    b.HasKey("VariantId", "ProductId")
                        .HasName("PK__product___AEBC4F683554C815");

                    b.HasIndex("ProductId");

                    b.ToTable("product_variant_values", (string)null);
                });

            modelBuilder.Entity("FDiamondShop.API.Models.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("sub_category_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubCategoryId"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("description");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("image_url");

                    b.Property<string>("SubcategoryName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("subcategory_name");

                    b.HasKey("SubCategoryId")
                        .HasName("PK__sub_cate__0A556D5F0EB73593");

                    b.HasIndex("CategoryId");

                    b.ToTable("sub_categories", (string)null);
                });

            modelBuilder.Entity("FDiamondShop.API.Models.CategoryVariant", b =>
                {
                    b.HasOne("FDiamondShop.API.Models.Category", "Category")
                        .WithMany("CategoryVariants")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK__category___categ__33D4B598");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("FDiamondShop.API.Models.Product", b =>
                {
                    b.HasOne("FDiamondShop.API.Models.SubCategory", "SubCategory")
                        .WithMany("Products")
                        .HasForeignKey("SubCategoryId")
                        .HasConstraintName("FK__products__sub_ca__36B12243");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("FDiamondShop.API.Models.ProductImage", b =>
                {
                    b.HasOne("FDiamondShop.API.Models.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__product_i__produ__3F466844");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FDiamondShop.API.Models.ProductVariantValue", b =>
                {
                    b.HasOne("FDiamondShop.API.Models.Product", "Product")
                        .WithMany("ProductVariantValues")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__product_v__produ__3C69FB99");

                    b.HasOne("FDiamondShop.API.Models.CategoryVariant", "Variant")
                        .WithMany("ProductVariantValues")
                        .HasForeignKey("VariantId")
                        .IsRequired()
                        .HasConstraintName("FK__product_v__varia__3B75D760");

                    b.Navigation("Product");

                    b.Navigation("Variant");
                });

            modelBuilder.Entity("FDiamondShop.API.Models.SubCategory", b =>
                {
                    b.HasOne("FDiamondShop.API.Models.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK__sub_categ__categ__30F848ED");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("FDiamondShop.API.Models.Category", b =>
                {
                    b.Navigation("CategoryVariants");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("FDiamondShop.API.Models.CategoryVariant", b =>
                {
                    b.Navigation("ProductVariantValues");
                });

            modelBuilder.Entity("FDiamondShop.API.Models.Product", b =>
                {
                    b.Navigation("ProductImages");

                    b.Navigation("ProductVariantValues");
                });

            modelBuilder.Entity("FDiamondShop.API.Models.SubCategory", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}

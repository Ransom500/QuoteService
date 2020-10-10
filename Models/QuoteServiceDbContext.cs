using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuoteService.Models
{
    public partial class QuoteServiceDbContext : DbContext
    {
        public QuoteServiceDbContext()
        {
        }

        public QuoteServiceDbContext(DbContextOptions<QuoteServiceDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inquirers> Inquirers { get; set; }
        public virtual DbSet<Quotes> Quotes { get; set; }
        public virtual DbSet<ServiceTypes> ServiceTypes { get; set; }
        public virtual DbSet<Services> Services { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=QuoteService;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inquirers>(entity =>
            {
                entity.HasKey(e => e.InquirerId)
                    .HasName("PK__Inquirer__6CD13BB473889C65");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailAddress)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FirstName)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastName)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PhoneNumber)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Quotes>(entity =>
            {
                entity.HasKey(e => e.QuoteId)
                    .HasName("PK__Quotes__0D37DF0CFB57BE23");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExpirationDate).HasDefaultValueSql("('1/1/1900')");

                entity.Property(e => e.QuoteEstimate)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Inquirer)
                    .WithMany(p => p.Quotes)
                    .HasForeignKey(d => d.InquirerId)
                    .HasConstraintName("FK__Quotes__inquirer__46E78A0C");
            });

            modelBuilder.Entity<ServiceTypes>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__Service___2C0005989B46B1CF");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Services>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("PK__Services__3E0DB8AFD4DA8869");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ServiceName)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__Services__type_i__412EB0B6");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

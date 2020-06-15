using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Elections.Models
{
    public partial class ElectionsContext : DbContext
    {
        public ElectionsContext()
        {
        }

        public ElectionsContext(DbContextOptions<ElectionsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CandidateCategory> Candidate { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Vote> Vote { get; set; }
        public virtual DbSet<Voters> Voters { get; set; }
        public virtual DbSet<Candidates> Candidates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = USHYDYECEEP1; Database = Elections; Trusted_Connection = True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateCategory>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("UQ__Candidat__1788CC4DC723FC3F")
                    .IsUnique();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CandidateCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Candidate__Categ__7F2BE32F");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.CandidateCategory)
                    .HasForeignKey<CandidateCategory>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Candidate__UserI__00200768");
            });

            modelBuilder.Entity<Candidates>(entity =>
            {
                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ClientId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ClientSecret).IsUnicode(false);
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.Vote)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vote__CandidateI__04E4BC85");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Vote)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vote__CategoryId__02FC7413");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Vote)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vote__UserId__03F0984C");
            });

            modelBuilder.Entity<Voters>(entity =>
            {
                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

//using System;
//using HomitagChallenge.Common;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;

//namespace HomitagChallenge.DataAccessLayer.Models
//{
//    public partial class HomitagChallengeContext : DbContext
//    {
//        public HomitagChallengeContext()
//        {
//        }

//        public HomitagChallengeContext(DbContextOptions<HomitagChallengeContext> options)
//            : base(options)
//        {
//        }

//        public virtual DbSet<Genres> Genres { get; set; }
//        public virtual DbSet<MovieGenres> MovieGenres { get; set; }
//        public virtual DbSet<Movies> Movies { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer(ConfigurationHelper.GetConnectionString(AppContants.HOMITAG_CHALLENGE_DB));
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Genres>(entity =>
//            {
//                entity.Property(e => e.Description)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .IsUnicode(false);
//            });

//            modelBuilder.Entity<MovieGenres>(entity =>
//            {
//                entity.HasOne(d => d.Genre)
//                    .WithMany(p => p.MovieGenres)
//                    .HasForeignKey(d => d.GenreId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__MovieGenr__Genre__4F7CD00D");

//                entity.HasOne(d => d.Movie)
//                    .WithMany(p => p.MovieGenres)
//                    .HasForeignKey(d => d.MovieId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__MovieGenr__Movie__4E88ABD4");
//            });

//            modelBuilder.Entity<Movies>(entity =>
//            {
//                entity.Property(e => e.Description)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

//                //entity.HasOne(d => d.Genre)
//                //    .WithMany(p => p.Movies)
//                //    .HasForeignKey(d => d.GenreId)
//                //    .OnDelete(DeleteBehavior.ClientSetNull)
//                //    .HasConstraintName("FK__Movies__GenreId__4BAC3F29");
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}
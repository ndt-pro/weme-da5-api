using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WeMe.Models
{
    public partial class WeMeContext : DbContext
    {
        //public WeMeContext()
        //{
        //}

        public WeMeContext(DbContextOptions<WeMeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<NewfeedComments> NewfeedComments { get; set; }
        public virtual DbSet<NewfeedImages> NewfeedImages { get; set; }
        public virtual DbSet<NewfeedLikes> NewfeedLikes { get; set; }
        public virtual DbSet<Newfeeds> Newfeeds { get; set; }
        public virtual DbSet<ThreadComments> ThreadComments { get; set; }
        public virtual DbSet<ThreadLikes> ThreadLikes { get; set; }
        public virtual DbSet<Threads> Threads { get; set; }
        public virtual DbSet<Users> Users { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=NDTPRO\\SQLEXPRESS;Database=WeMe;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.ToTable("messages");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("ntext");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.FromUserId).HasColumnName("from_user_id");

                entity.Property(e => e.ToUserId).HasColumnName("to_user_id");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.MessagesFromUser)
                    .HasForeignKey(d => d.FromUserId)
                    .HasConstraintName("FK__messages__from_u__52593CB8");

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.MessagesToUser)
                    .HasForeignKey(d => d.ToUserId)
                    .HasConstraintName("FK__messages__to_use__534D60F1");
            });

            modelBuilder.Entity<NewfeedComments>(entity =>
            {
                entity.ToTable("newfeed_comments");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("ntext");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdNewfeed).HasColumnName("id_newfeed");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.IdNewfeedNavigation)
                    .WithMany(p => p.NewfeedComments)
                    .HasForeignKey(d => d.IdNewfeed)
                    .HasConstraintName("FK__newfeed_c__id_ne__4222D4EF");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.NewfeedComments)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__newfeed_c__id_us__4316F928");
            });

            modelBuilder.Entity<NewfeedImages>(entity =>
            {
                entity.ToTable("newfeed_images");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdNewfeed).HasColumnName("id_newfeed");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(200);

                entity.HasOne(d => d.IdNewfeedNavigation)
                    .WithMany(p => p.NewfeedImages)
                    .HasForeignKey(d => d.IdNewfeed)
                    .HasConstraintName("FK__newfeed_i__id_ne__3B75D760");
            });

            modelBuilder.Entity<NewfeedLikes>(entity =>
            {
                entity.ToTable("newfeed_likes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdNewfeed).HasColumnName("id_newfeed");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.IdNewfeedNavigation)
                    .WithMany(p => p.NewfeedLikes)
                    .HasForeignKey(d => d.IdNewfeed)
                    .HasConstraintName("FK__newfeed_l__id_ne__3E52440B");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.NewfeedLikes)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__newfeed_l__id_us__3F466844");
            });

            modelBuilder.Entity<Newfeeds>(entity =>
            {
                entity.ToTable("newfeeds");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("ntext");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Newfeeds)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__newfeeds__id_use__38996AB5");
            });

            modelBuilder.Entity<ThreadComments>(entity =>
            {
                entity.ToTable("thread_comments");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("ntext");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdThread).HasColumnName("id_thread");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.IdThreadNavigation)
                    .WithMany(p => p.ThreadComments)
                    .HasForeignKey(d => d.IdThread)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__thread_co__id_th__4E88ABD4");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.ThreadComments)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__thread_co__id_us__4F7CD00D");
            });

            modelBuilder.Entity<ThreadLikes>(entity =>
            {
                entity.ToTable("thread_likes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdThread).HasColumnName("id_thread");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.IdThreadNavigation)
                    .WithMany(p => p.ThreadLikes)
                    .HasForeignKey(d => d.IdThread)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__thread_li__id_th__4AB81AF0");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.ThreadLikes)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__thread_li__id_us__4BAC3F29");
            });

            modelBuilder.Entity<Threads>(entity =>
            {
                entity.ToTable("threads");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("ntext");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.Pin).HasColumnName("pin");

                entity.Property(e => e.Tags)
                    .HasColumnName("tags")
                    .HasMaxLength(250);

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Threads)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__threads__id_cate__47DBAE45");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(200);

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasColumnName("avatar")
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("full_name")
                    .HasMaxLength(50);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("password_hash")
                    .HasMaxLength(64)
                    .IsFixedLength();

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasColumnName("password_salt")
                    .HasMaxLength(128)
                    .IsFixedLength();

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Role).HasColumnName("role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

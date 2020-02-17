using HappyThoughts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyThoughts.Data
{
    public class HappyThoughtsDbContext : DbContext
    {
        public HappyThoughtsDbContext()
        {
        }

        public HappyThoughtsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<TopicCategory> TopicsCategories { get; set; }

        public DbSet<SubComment> SubComments { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasOne(s => s.Sender)
                .WithMany(u => u.MessagesSent)
                .HasForeignKey(s => s.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Receiver)
                .WithMany(u => u.MessagesReceived)
                .HasForeignKey(s => s.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(p => p.Post)
                .WithMany(c => c.Comments)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SubComment>(entity =>
            {
                entity.HasOne(p => p.Post)
                .WithMany(c => c.SubComments)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.RootComment)
                .WithMany(c => c.SubComments)
                .HasForeignKey(c => c.RootCommentId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TopicCategory>(entity =>
            {
                entity.HasKey(k => new { k.PostId, k.CategoryId });
            });
        }
    }
}

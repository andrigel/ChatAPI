﻿using DataLayer.Entityes;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

//: IdentityDbContext<User>
namespace DataLayer
{
    public class EFDBContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<ApplicationUser> ApplicationsUsers { get; set; }
        public DbSet<Chat> Conversations { get; set; }
        public DbSet<UserChat> UserConversations { get; set; }
        public EFDBContext(DbContextOptions<EFDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Chat>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<UserChat>()
                .HasKey(uc => new { uc.ConversationId, uc.UserId });


            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.Id);

            modelBuilder.Entity<UserChat>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserChats)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserChat>()
                .HasOne(uc => uc.Conversation)
                .WithMany(c => c.UserChats)
                .HasForeignKey(uc => uc.ConversationId);
        }
    }
            
}
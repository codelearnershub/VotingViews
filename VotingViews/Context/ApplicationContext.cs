using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Model.Entity;

namespace VotingViews.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Vote>().HasIndex(v => new { v.ContestantId, v.PositionId, v.VoterId, v.ElectionId })
            //    .IsUnique();
            //modelBuilder.Entity<Contestant>()
            //    .HasMany(c => c.Votes)
            //    .WithOne(c => c.Contestant)
            //    .HasForeignKey(c=>c.ContestantId).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Position>()
            //   .HasMany(c => c.Votes)
            //   .WithOne(c => c.Position)
            //   .HasForeignKey(c => c.PositionId).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Voter>()
            //   .HasMany(c => c.Votes)
            //   .WithOne(c => c.Voter)
            //   .HasForeignKey(c => c.VoterId).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Election>()
            //   .HasMany(c => c.Votes)
            //   .WithOne(c => c.Election)
            //   .HasForeignKey(c => c.ElectionId).OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Contestant>().HasIndex(c => c.PositionId)
            //    .IsUnique();
            //modelBuilder.Entity<Position>().HasMany(p => p.Contestants)
            //    .WithOne(p => p.Position)
            //    .HasForeignKey(p => p.PositionId).OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Position>().HasIndex(c => c.ElectionId)
            //    .IsUnique();
            //modelBuilder.Entity<Election>().HasMany(p => p.Positions)
            //    .WithOne(p => p.Election)
            //    .HasForeignKey(p => p.ElectionId).OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<User>()
            //   .HasOne(u => u.Voter)
            //   .WithOne(i => i.User);

            //modelBuilder.Entity<User>()
            //    .HasOne(u => u.Admin)
            //    .WithOne(m => m.User);
            //modelBuilder.Entity<Admin>().HasKey(a => a.UserId);

            //modelBuilder.Entity<Voter>().HasKey(v => v.UserId);
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Contestant> Contestants { get; set; }
        public DbSet<Voter> Voters { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}

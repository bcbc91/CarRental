using System;

using System.Collections.Generic;
using System.Text;
using Core.DataAccess.Configs;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Contexts
{
    public class CarRentalContext:DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Brand> Brands { get; set; }
        
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionConfig.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasOne(car => car.Brand).WithMany(brand => brand.Cars)
                .HasForeignKey(car => car.BrandId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Car>().HasOne(car => car.Color).WithMany(color => color.Cars)
                .HasForeignKey(car => car.ColorId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rental>().HasOne(rental => rental.Car).WithMany(car => car.Rentals)
                .HasForeignKey(rental => rental.CarId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rental>().HasOne(rental => rental.UserDetail).WithMany(ud => ud.Rentals)
                .HasForeignKey(rental => rental.UserDetailId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>().HasOne(user => user.UserDetail).WithOne(userDetail => userDetail.User)
                .HasForeignKey<User>(user => user.UserDetailId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>().HasOne(user => user.Role).WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId).OnDelete(DeleteBehavior.NoAction);

           
        }
    }
}

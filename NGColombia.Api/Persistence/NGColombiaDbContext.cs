using Microsoft.EntityFrameworkCore;
using NGColombia.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Persistence
{
    public class NGColombiaDbContext: DbContext
    {
        public NGColombiaDbContext(DbContextOptions<NGColombiaDbContext> options)
            : base(options)
        {  }

        public DbSet<Event> Events { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionTicketDetail> TransactionTicketDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PaymentResponseLog> PaymentResponseLogs { get; set; }
        public DbSet<PaymentConfirmationLog> PaymentConfirmationLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TicketType>().HasKey(x => x.Code);
            modelBuilder.Entity<TicketType>().Property(x => x.Code)
                .HasMaxLength(100);

            modelBuilder.Entity<Transaction>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TransactionTicketDetail>()
                .Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            modelBuilder.Entity<Event>()
               .Property(x => x.Id)
                  .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Customer>()
               .Property(x => x.Id)
                  .ValueGeneratedOnAdd();

            modelBuilder.Entity<PaymentConfirmationLog>()
               .Property(x => x.Id)
                  .ValueGeneratedOnAdd();
            modelBuilder.Entity<PaymentResponseLog>()
               .Property(x => x.Id)
                  .ValueGeneratedOnAdd();
            modelBuilder.Entity<PaymentConfirmationLog>()
              .Property(x => x.RawData)
                 .HasColumnType("text");

            modelBuilder.Entity<PaymentResponseLog>()
              .Property(x => x.RawData)
                 .HasColumnType("text");



            modelBuilder.Entity<Event>()
                .HasMany(@event => @event.TicketTypes)
                .WithOne(customer => customer.@event);

            modelBuilder.Entity<Transaction>()
                .HasOne(transaction => transaction.Customer)
                .WithMany(customer => customer.Transactions);
            
            modelBuilder.Entity<Transaction>()
                .HasMany(details => details.Details)
                .WithOne(detail => detail.Transaction);

            modelBuilder.Entity<Transaction>()
                .HasMany(details => details.Confirmations)
                .WithOne(detail => detail.Transaction);

            modelBuilder.Entity<Transaction>()
                .HasMany(details => details.Responses)
                .WithOne(detail => detail.Transaction);

            modelBuilder.Entity<TicketType>()
                .HasMany(details => details.Details)
                .WithOne(ticket => ticket.TicketType);

        }
    }
}

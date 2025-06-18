using APBD_DODATKOWE.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_DODATKOWE.Data;


public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<EventSpeaker> EventSpeakers { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.IdEvent);
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300);
                entity.Property(e => e.CurrentParticipants)
                    .HasDefaultValue(0);
            });
            
            modelBuilder.Entity<Speaker>(entity =>
            {
                entity.HasKey(s => s.IdSpeaker);
                entity.Property(s => s.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(s => s.LastName)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.Property(s => s.Email)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(s => s.Bio)
                    .HasMaxLength(255);
            });
            
            modelBuilder.Entity<Participant>(entity =>
            {
                entity.HasKey(p => p.IdParticipant);
                entity.Property(p => p.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(p => p.LastName)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.Property(p => p.Email)
                    .IsRequired()
                    .HasMaxLength(100);
            });
            
            modelBuilder.Entity<EventSpeaker>(entity =>
            {
                entity.HasKey(es => new { es.IdSpeaker, es.IdEvent });
                entity.Property(es => es.PresentationTitle)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.HasOne(es => es.Speaker)
                    .WithMany(s => s.EventSpeakers)
                    .HasForeignKey(es => es.IdSpeaker);
                
                entity.HasOne(es => es.Event)
                    .WithMany(e => e.EventSpeakers)
                    .HasForeignKey(es => es.IdEvent);
            });
            
            modelBuilder.Entity<Registration>(entity =>
            {
                entity.HasKey(r => new { r.IdEvent, r.IdParticipant });
                entity.Property(r => r.Status)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.HasOne(r => r.Event)
                    .WithMany(e => e.Registrations)
                    .HasForeignKey(r => r.IdEvent);
                
                entity.HasOne(r => r.Participant)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(r => r.IdParticipant);
            });
        }
    }
using FDP.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FDP.Infrastructure;

public partial class FdpContext : DbContext
{
    public FdpContext()
    {
    }

    public FdpContext(DbContextOptions<FdpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public DbSet<AddressDto> AddressDtos { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FDP;User Id=sa;Password=Admin@135;TrustServerCertificate=True;");
    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("address");

            entity.Property(e => e.AddressId).HasColumnName("addressId");
            entity.Property(e => e.Area)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("area");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.CountryId).HasColumnName("countryId");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Pincode).HasColumnName("pincode");
            entity.Property(e => e.StateId).HasColumnName("stateId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_address_user");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("country");

            entity.Property(e => e.CountryId).HasColumnName("countryId");
            entity.Property(e => e.Code)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phonecode).HasColumnName("phonecode");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.ToTable("state");

            entity.Property(e => e.StateId).HasColumnName("stateId");
            entity.Property(e => e.CountryId).HasColumnName("countryId");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_state_country");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "UK_email").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UK_phoneNumber").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreationBy).HasColumnName("creationBy");
            entity.Property(e => e.CreationDatetime)
                .HasColumnType("datetime")
                .HasColumnName("creationDatetime");
            entity.Property(e => e.Email)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurantId");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdationBy).HasColumnName("updationBy");
            entity.Property(e => e.UpdationDatetime)
                .HasColumnType("datetime")
                .HasColumnName("updationDatetime");
        });

        modelBuilder.Entity<AddressDto>().HasNoKey();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

public class AddressDto
{
    public int AddressId { get; set; }
    public int UserId { get; set; }
    public string Location { get; set; } = default!;
    public int? Pincode { get; set; }
    public int Status { get; set; }
}
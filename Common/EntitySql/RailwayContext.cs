using EntitySql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace EntitySql
{
    public class RailwayContext : DbContext
    {
        private static DbContextOptions<RailwayContext> optionsSql;

        public RailwayContext():base(optionsSql)
        {
           
            Database.EnsureCreated();
          
        }


        public RailwayContext(DbContextOptions<RailwayContext> options)
                : base(options)
        {
            optionsSql = options;
            Database.EnsureCreated();
           
        }


        



        public virtual DbSet<Railway> Railways { get; set; }
        public virtual DbSet<Station> Stations { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Railway>(entity =>
            {
                    entity.Property(e => e.Id).ValueGeneratedNever();

            });

            modelBuilder.Entity<Station>(entity =>
            {
                entity.Property(e => e.Code).ValueGeneratedNever();

                entity.HasOne(d => d.Railway)
                    .WithMany(p => p.Stations)
                    .HasForeignKey(d => d.RailwayId)
                    .HasConstraintName("FK_Station_Railway");

            });

            base.OnModelCreating(modelBuilder);
            
        }
    }
}

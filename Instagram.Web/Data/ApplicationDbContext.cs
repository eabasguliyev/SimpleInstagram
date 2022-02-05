using Instagram.Web.Entities;
using Instagram.Web.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Web.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RegularUser> RegularUsers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostLikes> PostLikes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(nameof(User).ToPluralize());
            modelBuilder.Entity<RegularUser>().ToTable(nameof(RegularUser).ToPluralize());
            modelBuilder.Entity<Admin>().ToTable(nameof(Admin).ToPluralize());


            modelBuilder.Entity<Friendship>().HasKey(i => new { i.FromId, i.ToId });
            modelBuilder.Entity<Friendship>().HasOne(i => i.From)
                                .WithMany(i => i.Follows)
                                .HasForeignKey(i => i.FromId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Friendship>().HasOne(i => i.To)
                                .WithMany(i => i.Followers)
                                .HasForeignKey(i => i.ToId).OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Post>().HasOne(i => i.Publisher)
                                        .WithMany(i => i.Posts)
                                        .HasForeignKey(i => i.PublisherId);

            modelBuilder.Entity<PostLikes>().HasKey(i => new { i.UserId, i.PostId });
            modelBuilder.Entity<PostLikes>().HasOne(i => i.User)
                                            .WithMany(i => i.PostLikes)
                                            .HasForeignKey(i => i.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PostLikes>().HasOne(i => i.Post)
                                            .WithMany(i => i.PostLikes)
                                            .HasForeignKey(i => i.PostId).OnDelete(DeleteBehavior.NoAction);
            

            base.OnModelCreating(modelBuilder);
        }
    }
}

using Core.Database.Models;
using Microsoft.EntityFrameworkCore;
namespace Web.Models.EF
{
    public class FoodContext : DbContext
    {
        public FoodContext(DbContextOptions<FoodContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasData(
                new Group()
                {
                    Id = Guid.Parse("164EADAC-199A-4DB7-BBC3-81B6254767B9"),
                    Name = "Quản trị viên"
                }
            );
            modelBuilder.Entity<Member>().HasData(
                new Member()
                {
                    Id = Guid.Parse("11336570-9607-4634-8244-207E19971E98"),
                    Name = "Ngô Tuấn Cảnh",
                    Picture = "/img/users/ngocanh.jpg",
                    LoginName = "ngo.canh",
                    Password = "c4ca4238a0b923820dcc509a6f75849b",
                    Email = "canhnt.24th@sv.dla.edu.vn",
                    CreatedOn = DateTime.Now,
                    GroupId = Guid.Parse("164EADAC-199A-4DB7-BBC3-81B6254767B9")
                }
            );
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = Guid.Parse("F053BA68-398A-4019-87A1-5BC9466E9FE4"), //lấy id ở csdl
                    Name = "Root",
                    CreatedBy = Guid.Parse("11336570-9607-4634-8244-207E19971E98"),
                    CreatedOn = DateTime.Now
                },
                new Category()
                {
                    Id = Guid.Parse("F053BA68-398A-4019-87A1-5BC9466E9FE4"), //dán id mới ở sql
                    Name = "Authorized", //bảng authorized
                    CreatedBy = Guid.Parse("11336570-9607-4634-8244-207E19971E98"),
                    CreatedOn = DateTime.Now //bảng tg
                }
                ,
                new Category()
                {
                    Id = Guid.Parse("F053BA68-398A-4019-87A1-5BC9466E9FE4"), //dán id mới ở sql
                    Name = "Article", //bảng authorized
                    CreatedBy = Guid.Parse("11336570-9607-4634-8244-207E19971E98"),
                    CreatedOn = DateTime.Now //bảng tg
                },
                new Category()
                {
                    Id = Guid.Parse("F053BA68-398A-4019-87A1-5BC9466E9FE4"), //dán id mới ở sql
                    Name = "Product", //bảng product 
                    CreatedBy = Guid.Parse("11336570-9607-4634-8244-207E19971E98"),
                    CreatedOn = DateTime.Now //bảng tg
                }
            );
            modelBuilder.Entity<Role>().HasData(
    new Role()
    {
        Id = Guid.Parse("76D93C1D-3457-4624-8D6A-8A8D3B780458"),
        Name = "Xem danh sách",
        Code = "view-groups",
        CategoryId = Guid.Parse("3BCD9C68-1B73-44C9-ABA7-D167B98CFE54")
    },
    new Role()
    {
        Id = Guid.Parse("76D93C1D-3457-4624-8D6A-8A8D3B780458"),
        Name = "Cập nhật",
        Code = "edit-group",
        CategoryId = Guid.Parse("3BCD9C68-1B73-44C9-ABA7-D167B98CFE54")
    }
);
            modelBuilder.Entity<Authorized>().HasData(
    new Authorized()
    {
        Id = Guid.NewGuid(),
        GroupId = Guid.Parse("164EADAC-199A-4DB7-BBC3-81B6254767B9"),
        RoleId = Guid.Parse("76D93C1D-3457-4624-8D6A-8A8D3B780458") //phân quyền cho quản trị viên
    }
);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Authorized> Authorizeds { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Role> Roles { get; set; }
    }
}
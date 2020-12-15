using Microsoft.EntityFrameworkCore;

namespace Shop.Models
{
    //класс контекста данных для взаимодействия с базой данных
    public class MobileContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public MobileContext(DbContextOptions<MobileContext> options)
            : base(options)
        {
            //создает базу данных если она отсутствует
            //если база данных есть, то ничего не создает
            Database.EnsureCreated();
        }
    }
}

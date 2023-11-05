using DownloadVideo.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DownloadVideo.Data.DbContexts;

public class DownloadVideoDbContext:DbContext
{
    string cs = @"server=localhost;userid=root;password=Bekmurod21;database=DownloadVideodb";
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(cs);
    }
    DbSet<User> Users { get; set; }

}

using AlusnosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlusnosApi.Context
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options) { } 

        public DbSet<Aluno>? Alunos { get; set; }
    }
}

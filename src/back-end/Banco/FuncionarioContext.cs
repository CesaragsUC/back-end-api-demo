using back_end.Entidade;
using Microsoft.EntityFrameworkCore;

namespace back_end.Banco
{
    public class FuncionarioContext : DbContext
    {
        public FuncionarioContext(DbContextOptions<FuncionarioContext> options) : base(options)
        {
        }

        public DbSet<Funcionario> Funcionarios { get; set; }    
    }
}

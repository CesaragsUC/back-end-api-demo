using System.ComponentModel.DataAnnotations;

namespace back_end.Entidade
{
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }

        public string Login { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}

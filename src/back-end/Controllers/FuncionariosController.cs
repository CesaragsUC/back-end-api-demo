using back_end.Banco;
using back_end.DTO;
using back_end.Entidade;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosController : Controller
    {
        private readonly FuncionarioContext _context;
        private readonly ILogger<FuncionarioContext> _logger;
        public FuncionariosController(FuncionarioContext context, ILogger<FuncionarioContext> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("listar-funconarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListarFuncionarios()
        {
            _logger.LogInformation("Mensagem do Middleware: Antes", DateTime.Now);

            var funcionario = await _context.Funcionarios.ToListAsync();

            var funcionarioDTO = funcionario.Select(x => new FuncionarioDTO
            {
                Id = x.Id,
                Nome = x.Nome,
                CPF = x.CPF,
                DataCadastro = x.DataCadastro
            });

            return Ok(funcionarioDTO);
        }

        [HttpGet]
        [Route("obter-por-id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(x=> x.Id == id);

            var funcionarioDTO =  new FuncionarioDTO
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                CPF = funcionario.CPF,
                DataCadastro = funcionario.DataCadastro
            };

            return Ok(funcionarioDTO);
        }

        [HttpPost]
        [Route("cadastrar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Cadastrar(FuncionarioAddDTO model)
        {
            var funcionario = new Funcionario { Nome = model.Nome, CPF = model.CPF, Senha = model.Senha, Login = model.Login };

            await _context.Funcionarios.AddAsync(funcionario);
            _context.SaveChanges();
            return Created("", funcionario);
        }

        [HttpPut]
        [Route("atualizar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Atualizar(FuncionarioUpdateDTO model)
        {
            var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (funcionario == null)
            {
                return BadRequest("funcionado nao encontrado.");
            }
            else
                funcionario.Nome = model.Nome;
            {
                funcionario.CPF = model.CPF;
                _context.Entry(funcionario).State = EntityState.Modified;
                _context.SaveChanges();
                return Created("", funcionario);
            }
        }


        [HttpPut]
        [Route("atualizar-login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarLogin(FuncionarioAtualizarLogin model)
        {

            var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (funcionario == null)
            {
                return BadRequest("funcionado nao encontrado.");
            }
            else
            {

                funcionario.Login = model.Login;
                funcionario.Senha = model.Senha;

                _context.Entry(funcionario).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(funcionario);
            }


        }

        [HttpDelete]
        [Route("excluir/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Excluir(int id)
        {
            var fnc = await _context.Funcionarios.FirstOrDefaultAsync(x => x.Id == id);
            if (fnc == null)
            {
                return BadRequest("funcionado nao encontrado.");
            }
            else
            {
                _context.Funcionarios.Remove(fnc);
                _context.SaveChanges();
                return Ok("Funcionario excluido.");
            }
        }
    }

}

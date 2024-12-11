using AlusnosApi.Models;
using AlusnosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlusnosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Produces("application/json")]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoService _service;

        public AlunosController(IAlunoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _service.GetAlunos();
                return Ok(alunos);
            }
            catch
            {
                //return BadRequest("request invalid");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao obter alunos");
            }
        }

        [HttpGet("AlunoPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByName([FromQuery] string nome)
        {
            try
            {
                var alunos = await _service.GetAlunosByNome(nome);

                if (alunos == null)
                    return NotFound($"Não existe alunos com o critério {nome}");

                return Ok(alunos);
            }
            catch
            {
                return BadRequest("request invalid");
            }
        }

        [HttpGet("{id:int}", Name = "GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            try
            {
                var aluno = await _service.GetAluno(id);

                if (aluno == null)
                    return NotFound($"Não existe aluno com o id={id}");

                return Ok(aluno);

            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            try
            {
                await _service.CreateAluno(aluno);
                return CreatedAtRoute(nameof(GetAluno), new { id = aluno.AlunoId }, aluno);
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Aluno aluno)
        {
            try
            {
                if (aluno.AlunoId == id)
                {
                    await _service.UpdateAluno(aluno);
                    return Ok($"Aluno com o id = {id} foi atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Dados inconsistentes");
                }
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var aluno = await _service.GetAluno(id);

                if (aluno != null)
                {
                    await _service.DeleteAluno(aluno);
                    return Ok($"Aluno de id={id} foi excludo com sucesso");
                }
                else
                {
                    return NotFound($"Aluno com o id = {id} não encontrado");
                }
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

    }
}

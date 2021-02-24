using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.models;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Helpers;

namespace SmartSchool.WebAPI.V1.Controllers
{
    /// <sumary>
    /// Versão 1 do sistema
    /// </sumary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;

        public readonly IMapper _mapper;
        
        /// <sumary>
        ///
        /// </sumary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        /// <sumary>
        /// Método responsavel para retornar todos os meus alunos.
        /// </sumary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var alunos = await _repo.GetAllAlunosAsync(pageParams, true);  

            var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

            return Ok(alunosResult);
        }

        /// <sumary>
        /// Método responsavel por retornar apenas um aluno por id.
        /// </sumary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoByID(id, false);
            if (aluno == null) return BadRequest("O aluno não foi encontrado");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
        }

        /*api/aluno/byName?nome=nome&sobrenome=sobrenome
        [HttpGet("ByName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));
            if (aluno == null) return BadRequest("O aluno não foi encontrado");
            return Ok(aluno);
        }
        */
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoByID(id);
            if (aluno == null) return BadRequest("Aluno não encontrado!");
            
            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoByID(id);
            if (aluno == null) return BadRequest("Aluno não encontrado!");
            
            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoByID(id);
            if (aluno == null) return BadRequest("Aluno não encontrado!");

            _repo.Delete(aluno);
            if (_repo.SaveChanges())
            {
                return Ok("Aluno deletado com sucesso");
            }
            return BadRequest("Aluno não deletado");
        }
    }
}

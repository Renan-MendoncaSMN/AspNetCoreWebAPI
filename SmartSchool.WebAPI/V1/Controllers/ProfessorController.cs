using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.models;

namespace SmartSchool.WebAPI.V1.Controllers
{
    /// <sumary>
    /// Versão 1 do sistema
    /// </sumary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository _repo;
        public readonly IMapper _mapper;
        public ProfessorController(IRepository repo,  IMapper mapper) 
        { 
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professor = _repo.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professor));
        }
        
        //api/professor/is
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            var professor = _repo.GetProfessorById(id, true);
            if (professor == null) return BadRequest("Professor não foi encontrado");

            var professorDto = _mapper.Map<ProfessorDto>(professor);
            return Ok(professorDto);
        }

         [HttpGet("byaluno/{alunoId}")]
        public IActionResult GetByAlunoId(int alunoId)
        {
            var Professores = _repo.GetProfessoresByAlunoId(alunoId, true);
            if (Professores == null) return BadRequest("Professores não encontrados");
            
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(Professores));
        }


        /*[HttpGet("ByName/{nome}")]
        public IActionResult GetByName(string nome)
        {
            var professor = _context.Professores.FirstOrDefault(a => a.Nome.Contains(nome));
            if (professor == null) return BadRequest("O professor não foi encontrado");
            return Ok(professor);
        }*/

        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repo.Add(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorById(id, false);
            if(professor == null) return BadRequest("Professor não encontrado!");

            _mapper.Map(model, professor);

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorById(id, false);
            if(professor == null) return BadRequest("Professor não encontrado!");

            _mapper.Map(model, professor);

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
            if(professor == null) return BadRequest("Professor não encontrado!");

            _repo.Delete(professor);
            if (_repo.SaveChanges())
            {
                return Ok("Professor deletado com sucesso");
            }
            return BadRequest("Professor não deletado");
        }

    }
}

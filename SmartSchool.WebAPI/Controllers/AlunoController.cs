using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {

        public List<Aluno> Alunos = new List<Aluno>(){
            new Aluno() {
                Id = 1,
                Nome = "Renan",
                Sobrenome = "Klinsmann",
                Telefone = "3000-0000"
            },
            new Aluno() {
                Id = 2,
                Nome = "Khys",
                Sobrenome = "Sousa",
                Telefone = "4000-0000"
            },
            new Aluno() {
                Id = 3,
                Nome = "Miguel",
                Sobrenome = "Renan",
                Telefone = "5000-0000"
            },
        };
        public AlunoController() { }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        //api/aluno/byId/id
        [HttpGet ("byId/{id}")]
        public IActionResult GetById(int id)
        {

            var aluno = Alunos.FirstOrDefault(a => a.Id == id);
            if(aluno == null) return BadRequest("O aluno não foi encontrado");
            return Ok(aluno);
        }
        
        //api/aluno/byName?nome=nome&sobrenome=sobrenome
        [HttpGet ("ByName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));
            if(aluno == null) return BadRequest("O aluno não foi encontrado");
            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPost ("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}

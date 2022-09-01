using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroControllers :ControllerBase
    {
        private CadastroService _cadastroService;

        public CadastroControllers(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario(CreateUsuarioDto createDto)
        {
            var resultado = await _cadastroService.CadastrarUsuario(createDto);

            if (resultado.IsFailed)
                return StatusCode(500);

            return Ok();
        }
    }
}

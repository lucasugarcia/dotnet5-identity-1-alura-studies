using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private EnderecoService _enderecoService;

        public EnderecoController(EnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpPost]
        public IActionResult AdicionarEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            var endereco = _enderecoService.AdicionarEndereco(enderecoDto);

            return CreatedAtAction(nameof(RecuperarEnderecoPorId), new { id = endereco.Id }, endereco);
        }

        [HttpGet]
        public IActionResult RecuperarEnderecos()
        {
            var enderecos = _enderecoService.RecuperarEnderecos();

            if (enderecos == null)
                return NotFound();

            return Ok(enderecos);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarEnderecoPorId(int id)
        {
            var endereco = _enderecoService.RecuperarEnderecoPorId(id);

            if (endereco == null)
                return NotFound();

            return Ok(endereco);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            var resultado = _enderecoService.AtualizarEndereco(id, enderecoDto);

            if (resultado.IsFailed)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarEndereco(int id)
        {
            var resultado = _enderecoService.DeletarEndereco(id);

            if (resultado.IsFailed)
                return NotFound();

            return NoContent();
        }
    }
}

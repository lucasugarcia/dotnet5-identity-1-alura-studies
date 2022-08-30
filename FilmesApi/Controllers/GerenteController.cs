using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        private GerenteService _gerenteService;
        private IMapper _mapper;

        public GerenteController(GerenteService gerenteService)
        {
            _gerenteService = gerenteService;
        }

        [HttpPost]
        public IActionResult AdicionaGerente(CreateGerenteDto gerenteDto)
        {
            var gerente = _gerenteService.AdicionaGerente(gerenteDto);

            return CreatedAtAction(nameof(RecuperarGerentePorId), new { Id = gerente.Id }, gerente);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarGerentePorId(int id)
        {
            var gerente = _gerenteService.RecuperarGerentePorId(id);

            if (gerente == null)
                return NotFound();

            return Ok(gerente);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarGerente(int id)
        {
            var resultado = _gerenteService.DeletarGerente(id);

            if (resultado.IsFailed)
                return NotFound();

            return NoContent();
        }
    }
}

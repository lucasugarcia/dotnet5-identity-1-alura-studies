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
    public class FilmeController : ControllerBase
    {
        private FilmeService _filmeService;

        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpPost]
        public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
        {
            var readFilmeDto = _filmeService.AdicionarFilme(filmeDto);

            return CreatedAtAction(nameof(RecuperarFilmePorId), new { id = readFilmeDto.Id }, readFilmeDto);
        }

        [HttpGet]
        public IActionResult RecuperarFilmes([FromQuery] int? classificacaoEtaria = null)
        {
            var filmesDto = _filmeService.RecuperarFilmes(classificacaoEtaria);

            if (filmesDto == null)
                return NotFound();

            return Ok(filmesDto);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarFilmePorId(int id)
        {
            var filmeDto = _filmeService.RecuperarFilmePorId(id);

            if (filmeDto == null)
                return NotFound();

            return Ok(filmeDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            var resultado = _filmeService.AtualizarFilme(id, filmeDto);

            if (resultado.IsFailed)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarFilme(int id)
        {
            var resultado = _filmeService.DeletarFilme(id);

            if (resultado.IsFailed)
                return NotFound();

            return NoContent();
        }
    }
}

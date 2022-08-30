using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class GerenteService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public GerenteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadGerenteDto AdicionaGerente(CreateGerenteDto gerenteDto)
        {
            var gerente = _mapper.Map<Gerente>(gerenteDto);

            _context.Gerentes.Add(gerente);
            _context.SaveChanges();

            return _mapper.Map<ReadGerenteDto>(gerente);
        }

        public ReadGerenteDto RecuperarGerentePorId(int id)
        {
            var gerente = _context.Gerentes.FirstOrDefault(g => g.Id == id);

            if (gerente == null)
                return null;

            return _mapper.Map<ReadGerenteDto>(gerente);
        }

        public Result DeletarGerente(int id)
        {
            var gerente = _context.Gerentes.FirstOrDefault(g => g.Id == id);

            if (gerente == null)
                return Result.Fail("Gerente não encontrado");

            _context.Remove(gerente);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}

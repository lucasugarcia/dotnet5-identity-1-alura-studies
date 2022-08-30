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
    public class CinemaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CinemaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadCinemaDto AdicionaCinema(CreateCinemaDto cinemaDto)
        {
            var cinema = _mapper.Map<Cinema>(cinemaDto);

            _context.Cinemas.Add(cinema);
            _context.SaveChanges();

            return _mapper.Map<ReadCinemaDto>(cinema);
        }

        public List<ReadCinemaDto> RecuperaCinemas(string nomeDoFilme)
        {
            var cinemas = _context.Cinemas.ToList();

            if (cinemas == null)
                return null;

            if (string.IsNullOrEmpty(nomeDoFilme))
                return _mapper.Map<List<ReadCinemaDto>>(cinemas);

            var query = from cinema in cinemas
                        where cinema.Sessoes.Any(s => s.Filme.Titulo == nomeDoFilme)
                        select cinema;

            return _mapper.Map<List<ReadCinemaDto>>(query);
        }

        public ReadCinemaDto RecuperaCinemasPorId(int id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema == null)
                return null;

            return _mapper.Map<ReadCinemaDto>(cinema);
        }

        public Result AtualizaCinema(int id, UpdateCinemaDto cinemaDto)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema == null)
                return Result.Fail("Cinema não encontrado");

            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeletaCinema(int id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema == null)
                return Result.Fail("Cinema não encontrado");

            _context.Remove(cinema);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}

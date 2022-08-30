﻿using AutoMapper;
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
    public class FilmeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FilmeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadFilmeDto AdicionarFilme(CreateFilmeDto filmeDto)
        {
            var filme = _mapper.Map<Filme>(filmeDto);

            _context.Filmes.Add(filme);
            _context.SaveChanges();

            return _mapper.Map<ReadFilmeDto>(filme);
        }

        internal List<ReadFilmeDto> RecuperarFilmes(int? classificacaoEtaria)
        {
            List<Filme> filmes;

            if (classificacaoEtaria != null)
                filmes = _context.Filmes.Where(f => f.ClassificacaoEtaria <= classificacaoEtaria).ToList();
            else
                filmes = _context.Filmes.ToList();

            return _mapper.Map<List<ReadFilmeDto>>(filmes);
        }

        internal ReadFilmeDto RecuperarFilmePorId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);

            if (filme == null)
                return null;

            return _mapper.Map<ReadFilmeDto>(filme);
        }

        internal Result AtualizarFilme(int id, UpdateFilmeDto filmeDto)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);

            if (filme == null)
                return Result.Fail("Filme não encontrado");

            _mapper.Map(filmeDto, filme);

            _context.SaveChanges();

            return Result.Ok();
        }

        internal Result DeletarFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);

            if (filme == null)
                return Result.Fail("Filme não encontrado");

            _context.Remove(filme);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}

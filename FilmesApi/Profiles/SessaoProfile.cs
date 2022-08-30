using AutoMapper;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class SessaoProfile : Profile
    {
        public SessaoProfile()
        {
            CreateMap<CreateSessaoDto, Sessao>();
            CreateMap<Sessao, ReadSessaoDto>()
                .ForMember(sessao => sessao.HorarioDeEncerramento, opts => opts
                .MapFrom(sessao => sessao.HorarioDeEncerramento.AddMinutes(-sessao.Filme.Duracao)));
        }
    }
}
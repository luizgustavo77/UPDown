using AulaAPI.Data.Entities;
using AutoMapper;
using System;
using UPDown.Common.AulaAPI;

namespace AulaAPI.Service
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> Lazy = new(() =>
        {
            MapperConfiguration config = new(cfg =>
            {
                // This line ensures that internal Propertys are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MappingProfile>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            _ = CreateMap<Competencia, CompetenciaDTO>().ReverseMap();
            _ = CreateMap<Materia, MateriaDTO>().ReverseMap();
            _ = CreateMap<Conteudo, ConteudoDTO>().ReverseMap();
        }
    }
}

using AutoMapper;
using PessoaAPI.Data.Entities;
using System;
using UPDown.Common.PessoaAPI;

namespace PessoaAPI.Service
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
            _ = CreateMap<Aluno, AlunoDTO>().ReverseMap();
            _ = CreateMap<Professor, ProfessorDTO>().ReverseMap();

        }
    }
}

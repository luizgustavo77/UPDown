using AulaAPI.Data;
using AulaAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using UPDown.Common.AulaAPI;

namespace AulaAPI.Service
{
    public class ConteudoService
    {
        #region Private members
        private readonly DatabaseContext _dbContext;
        #endregion

        #region Constructor
        public ConteudoService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Public methods
        public ConteudoDTO Get(long Id)
        {
            ConteudoDTO result = new();

            try
            {
                result = _dbContext.Conteudos.Where(x => x.Id == Id)
                                                .Select(x => Mapping.Mapper.Map<ConteudoDTO>(x))
                                                .FirstOrDefault(); ;
            }
            catch (Exception)
            {

            }

            return result;
        }

        public List<ConteudoDTO> GetAll(int pagesize, int currentpage)
        {
            List<ConteudoDTO> result = new();

            try
            {
                IQueryable<Conteudo> query = _dbContext.Conteudos;

                if (currentpage > 0)
                {
                    query = query.Skip(currentpage * pagesize);
                }

                if (pagesize != 0)
                {
                    query = query.Take(pagesize);
                }

                List<Conteudo> list = query.ToList();

                result = list.Select(x => Mapping.Mapper.Map<ConteudoDTO>(x)).ToList();
            }
            catch (Exception)
            {

            }

            return result;
        }

        public void Add(ConteudoDTO dto)
        {
            try
            {
                long newId = _dbContext.Conteudos.OrderBy(x => x.Id).Select(x => x.Id).LastOrDefault();

                dto.Id = (newId + 1).ToString();

                _ = _dbContext.Conteudos.Add(Mapping.Mapper.Map<Conteudo>(dto));
                _ = _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Edit(ConteudoDTO dto)
        {
            try
            {
                _ = _dbContext.Update(Mapping.Mapper.Map<Conteudo>(dto));
                _ = _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(long Id)
        {
            try
            {
                Conteudo Conteudo = _dbContext.Conteudos.Where(x => x.Id == Id).FirstOrDefault(); ;
                _ = _dbContext.Conteudos.Remove(Mapping.Mapper.Map<Conteudo>(Conteudo));
                _ = _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}

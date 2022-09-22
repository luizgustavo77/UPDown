using AulaAPI.Data;
using AulaAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using UPDown.Common.AulaAPI;

namespace AulaAPI.Service
{
    public class CompetenciaService
    {
        #region Private members
        private readonly DatabaseContext _dbContext;
        #endregion

        #region Constructor
        public CompetenciaService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Public methods
        public CompetenciaDTO Get(long Id)
        {
            CompetenciaDTO result = new();

            try
            {
                result = _dbContext.Competencias.Where(x => x.Id == Id)
                                                .Select(x => Mapping.Mapper.Map<CompetenciaDTO>(x))
                                                .FirstOrDefault(); ;
            }
            catch (Exception)
            {

            }

            return result;
        }

        public List<CompetenciaDTO> GetAll(int pagesize, int currentpage)
        {
            List<CompetenciaDTO> result = new();

            try
            {
                IQueryable<Competencia> query = _dbContext.Competencias;

                if (currentpage > 0)
                {
                    query = query.Skip(currentpage * pagesize);
                }

                if (pagesize != 0)
                {
                    query = query.Take(pagesize);
                }

                List<Competencia> list = query.ToList();

                result = list.Select(x => Mapping.Mapper.Map<CompetenciaDTO>(x)).ToList();
            }
            catch (Exception)
            {

            }

            return result;
        }

        public void Add(CompetenciaDTO dto)
        {
            try
            {
                long newId = _dbContext.Competencias.OrderBy(x => x.Id).Select(x => x.Id).LastOrDefault();

                dto.Id = (newId + 1).ToString();

                _ = _dbContext.Competencias.Add(Mapping.Mapper.Map<Competencia>(dto));
                _ = _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Edit(CompetenciaDTO dto)
        {
            try
            {
                _ = _dbContext.Update(Mapping.Mapper.Map<Competencia>(dto));
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
                Competencia Competencia = _dbContext.Competencias.Where(x => x.Id == Id).FirstOrDefault(); ;
                _ = _dbContext.Competencias.Remove(Mapping.Mapper.Map<Competencia>(Competencia));
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

using AulaAPI.Data;
using AulaAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using UPDown.Common.AulaAPI;

namespace AulaAPI.Service
{
    public class MateriaService
    {
        #region Private members
        private readonly DatabaseContext _dbContext;
        #endregion

        #region Constructor
        public MateriaService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Public methods
        public MateriaDTO Get(long Id)
        {
            MateriaDTO result = new();

            try
            {
                result = _dbContext.Materias.Where(x => x.Id == Id)
                                                .Select(x => Mapping.Mapper.Map<MateriaDTO>(x))
                                                .FirstOrDefault(); ;
            }
            catch (Exception)
            {

            }

            return result;
        }

        public List<MateriaDTO> GetAll(int pagesize, int currentpage)
        {
            List<MateriaDTO> result = new();

            try
            {
                IQueryable<Materia> query = _dbContext.Materias;

                if (currentpage > 0)
                {
                    query = query.Skip(currentpage * pagesize);
                }

                if (pagesize != 0)
                {
                    query = query.Take(pagesize);
                }

                List<Materia> list = query.ToList();

                result = list.Select(x => Mapping.Mapper.Map<MateriaDTO>(x)).ToList();
            }
            catch (Exception)
            {

            }

            return result;
        }

        public void Add(MateriaDTO dto)
        {
            try
            {
                long newId = _dbContext.Materias.OrderBy(x => x.Id).Select(x => x.Id).LastOrDefault();

                dto.Id = (newId + 1).ToString();

                _ = _dbContext.Materias.Add(Mapping.Mapper.Map<Materia>(dto));
                _ = _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Edit(MateriaDTO dto)
        {
            try
            {
                _ = _dbContext.Update(Mapping.Mapper.Map<Materia>(dto));
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
                Materia Materia = _dbContext.Materias.Where(x => x.Id == Id).FirstOrDefault(); ;
                _ = _dbContext.Materias.Remove(Mapping.Mapper.Map<Materia>(Materia));
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

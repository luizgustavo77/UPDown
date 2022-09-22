using PessoaAPI.Data;
using PessoaAPI.Data.Entities;
using PessoaAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using UPDown.Common.PessoaAPI;

namespace ProfessorAPI.Service
{
    public class ProfessorService
    {
        #region Private members
        private readonly DatabaseContext _dbContext;
        #endregion

        #region Constructor
        public ProfessorService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Public methods
        public ProfessorDTO Get(long Id)
        {
            ProfessorDTO result = new ProfessorDTO();

            try
            {
                result = _dbContext.Professores.Where(x => x.Id == Id)
                                                .Select(x => Mapping.Mapper.Map<ProfessorDTO>(x))
                                                .FirstOrDefault(); ;
            }
            catch (Exception)
            {

            }

            return result;
        }

        public List<ProfessorDTO> GetAll(int pagesize, int currentpage)
        {
            List<ProfessorDTO> result = new List<ProfessorDTO>();

            try
            {
                IQueryable<Professor> query = _dbContext.Professores;

                if (currentpage > 0)
                {
                    query = query.Skip(currentpage * pagesize);
                }

                if (pagesize != 0)
                {
                    query = query.Take(pagesize);
                }

                List<Professor> list = query.ToList();

                result = list.Select(x => Mapping.Mapper.Map<ProfessorDTO>(x)).ToList();
            }
            catch (Exception)
            {

            }

            return result;
        }

        public void Add(ProfessorDTO dto)
        {
            try
            {
                long newId = _dbContext.Professores.OrderBy(x => x.Id).Select(x => x.Id).LastOrDefault();

                dto.Id = (newId + 1).ToString();

                _dbContext.Professores.Add(Mapping.Mapper.Map<Professor>(dto));
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Edit(ProfessorDTO dto)
        {
            try
            {
                _dbContext.Update(Mapping.Mapper.Map<Professor>(dto));
                _dbContext.SaveChanges();
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
                Professor Professor = _dbContext.Professores.Where(x => x.Id == Id).FirstOrDefault(); ;
                _dbContext.Professores.Remove(Mapping.Mapper.Map<Professor>(Professor));
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}

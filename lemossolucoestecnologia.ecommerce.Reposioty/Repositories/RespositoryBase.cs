using lemossolucoestecnologia.ecommerce.Data.Contexto;
using lemossolucoestecnologia.ecommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lemossolucoestecnologia.ecommerce.Reposioty.Repositories
{
    public class RespositoryBase<TEntiy> : IDisposable, IRepositoryBase<TEntiy> where TEntiy : class
    {
        private readonly DataContext _context;

        public RespositoryBase(DataContext context)
        {
           _context = context;
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntiy>> GetAll()
        {
            //var result = await _context.Set<TEntiy>().ToList();
            //return result;
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntiy>> GetAllByName(string name)
        {
            throw new NotImplementedException();
        }

        public TEntiy GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntiy> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task Register(TEntiy entity)
        {
            _context.Set<TEntiy>().Add(entity);
           await  _context.SaveChangesAsync();
        }

        public void Update(string name)
        {
            throw new NotImplementedException();
        }
    }
}

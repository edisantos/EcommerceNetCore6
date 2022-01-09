namespace lemossolucoestecnologia.ecommerce.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task Register(TEntity entity);  
        TEntity GetById(int id);
        Task<TEntity> GetByName(string name);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAllByName(string name);
        void Delete(int id);
        void Update(string name);
       

    }
}

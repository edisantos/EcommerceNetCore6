using lemossolucoestecnologia.ecommerce.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lemossolucoestecnologia.ecommerce.Reposioty.Interface
{
    public interface IUserServices
    {
        Task<IEnumerable<Users>> GetAll();
    }
}

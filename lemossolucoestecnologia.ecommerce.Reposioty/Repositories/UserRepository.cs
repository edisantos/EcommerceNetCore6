using lemossolucoestecnologia.ecommerce.Domain.Entities.Account;
using lemossolucoestecnologia.ecommerce.Reposioty.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lemossolucoestecnologia.ecommerce.Reposioty.Repositories
{
    public class UserRepository : IUserServices
    {
        readonly string _connection;
        private SqlCommand? cmd;
        private SqlDataReader? Dr;
        public UserRepository(IConfiguration config)
        {
            _connection = config.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<Users>> GetAll()
        {
            try
            {
                string strSelect = string.Format(@"SELECT FirstName, LastName, UserName, Email,Address FROM AspNetUsers");
                List<Users> list = new List<Users>();
                using var con = new SqlConnection(_connection);
                con.Open();
                using (cmd = new SqlCommand(strSelect, con))
                {
                    using (Dr = await cmd.ExecuteReaderAsync())
                    {
                        Users mod = null;
                        while (await Dr.ReadAsync())
                        {
                            mod = new Users();
                            mod.FirstName = Convert.ToString(Dr[0]); 
                            mod.LastName = Convert.ToString(Dr[1]);
                            mod.UserName = Convert.ToString(Dr[2]);
                            mod.Email = Convert.ToString(Dr[3]);
                            mod.Address = Convert.ToString(Dr[4]);
                            list.Add(mod);
                        }
                        return list;
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

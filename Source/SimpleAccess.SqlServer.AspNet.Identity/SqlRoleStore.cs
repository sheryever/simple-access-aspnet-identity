using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAccess.SqlServer.AspNet.Identity
{
    public class SqlRoleStore<TRole> : IQueryableRoleStore<TRole>
        where TRole : IdentityRole, new()
    {
        private readonly string _connectionString;
        //private readonly RoleRepository<TRole> _roleRepository;
        private readonly ISqlRepository _repository;

        public SqlRoleStore()
            : this("defaultConnection")
        {

        }

        public SqlRoleStore(string connectionStringName)
        {
            _connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            //_roleRepository = new RoleRepository<TRole>(_connectionString);
            _repository = new SqlRepository(_connectionString);
        }

        public SqlRoleStore(ISqlRepository repository)
        {
            _repository = repository;
        }


        public IQueryable<TRole> Roles
        {
            get
            {
                //return _roleRepository.GetRoles();
                return _repository.FindAll<TRole>().AsQueryable();
            }
        }

        public Task CreateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            //_roleRepository.Insert(role);
            _repository.Insert(role);

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("user");
            }

            //_roleRepository.Delete(role.Id);
            _repository.Delete<TRole>(new { id = role.Id });
            return Task.FromResult<Object>(null);
        }

        public Task<TRole> FindByIdAsync(string roleId)
        {
            //var result = _roleRepository.GetRoleById(roleId) as TRole;
            //var result = _repository.Get<TRole>(new { id = roleId });
            var result = _repository.Find<TRole>(r => (IComparable)r.Id == (IComparable)roleId);

            return Task.FromResult(result);
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            //var result = _roleRepository.GetRoleByName(roleName) as TRole;
            var result = _repository.Find<TRole>(r => r.Name == roleName);
            return Task.FromResult(result);
        }

        public TRole FindByName(SqlTransaction transaction, string roleName)
        {
            //var result = _roleRepository.GetRoleByName(roleName) as TRole;
            var result = _repository.Find<TRole>(transaction, r => r.Name == roleName);
            return result;
        }

        public Task UpdateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("user");
            }

            //_roleRepository.Update(role);
            _repository.Update(role);
            return Task.FromResult<Object>(null);
        }

        public void Dispose()
        {
            // connection is automatically disposed
        }
    }
}

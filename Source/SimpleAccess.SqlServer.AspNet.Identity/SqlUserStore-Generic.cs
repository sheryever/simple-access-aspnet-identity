using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;

namespace SimpleAccess.SqlServer.AspNet.Identity
{
    public class SqlUserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> 
        : IUserLoginStore<TUser, TKey>
        , IUserClaimStore<TUser, TKey>
        , IUserRoleStore<TUser, TKey>
        , IUserPasswordStore<TUser, TKey>
        , IUserSecurityStampStore<TUser, TKey>
        , IQueryableUserStore<TUser, TKey>
        , IUserEmailStore<TUser, TKey>
        , IUserPhoneNumberStore<TUser, TKey>
        , IUserTwoFactorStore<TUser, TKey>
        , IUserLockoutStore<TUser, TKey>
        , IUserStore<TUser, TKey>, IDisposable
        where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>, new()
        where TRole : IdentityRole<TKey, TUserRole>, new()
        where TKey : struct, IEquatable<TKey>
        where TUserLogin : IdentityUserLogin<TKey>, new()
        where TUserRole : IdentityUserRole<TKey>, new()
        where TUserClaim : IdentityUserClaim<TKey>, new()
    {
        private readonly string _connectionString;
        private readonly ISqlRepository _repository;
        private readonly SqlRoleStore<IdentityRole<TKey, IdentityUserRole<TKey>>, TKey> _roleStote;

        public SqlUserStore()
            : this("defaultConnection")
        {

        }

        public SqlUserStore(string connectionStringName)
        {
            _connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            //_userRepository = new UserRepository<TUser>(_connectionString);
            //_userLoginRepository = new UserLoginRepository(_connectionString);
            //_userClaimRepository = new UserClaimRepository<TUser>(_connectionString);
            //_userRoleRepository = new UserRoleRepository<TUser>(_connectionString);

            _repository = new SqlRepository(connectionStringName);
            _roleStote = new SqlRoleStore<IdentityRole<TKey, IdentityUserRole<TKey>>, TKey>(_repository);
        }

        public SqlUserStore(ISqlRepository repository)
        {
            _repository = repository;
        }

        public virtual void Dispose()
        {
            // connection is automatically disposed
        }


        public Task CreateAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            //if (user.Id)
            //    throw new InvalidOperationException("user.Id property must be specified before calling CreateAsync");

            //_userRepository.Insert(user);
            _repository.Insert(user);
            return Task.FromResult(true);
        }

        public Task CreateAsync(TUser user, params string[] roles)
        {
            SqlTransaction transaction = null;
            using (transaction = _repository.SimpleAccess.BeginTrasaction())
            {
                try
                {

                    Create(transaction, user, roles);
                    _repository.SimpleAccess.EndTransaction(transaction);
                    return Task.FromResult(true);

                }
                catch (Exception)
                {
                    _repository.SimpleAccess.EndTransaction(transaction, false);
                    return Task.FromResult(false);
                }
            }
        }

        public void Create(SqlTransaction transaction, TUser user, params string[] roles)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if ((IComparable)user.Id == null)
                throw new InvalidOperationException("user.Id property must be specified before calling CreateAsync");
            if (roles == null)
                throw new ArgumentNullException("roles");
            if (roles.Length < 1)
                throw new IndexOutOfRangeException("No role found in the roles");

            _repository.Insert(transaction, user);

            foreach (var role in roles)
            {
                AddToRole(transaction, user, role);
            }
        }

        public Task DeleteAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            //_userRepository.Delete(user);
            _repository.Delete<TUser>(new { id = user.Id });

            return Task.FromResult(true);
        }

        public Task<TUser> FindByIdAsync(TKey userId)
        {
            //var user =_userRepository.GetById(userId);
            var user = _repository.Find<TUser>(u => (IComparable)u.Id == (IComparable)userId);
            if (user != null)
            {
                //user.Roles = _userRoleRepository.PopulateRoles(user.Id);
                //user.Claims = _userClaimRepository.PopulateClaims(user.Id);
                //user.Logins = _userLoginRepository.PopulateLogins(user.Id);
                LoadUserDetails(user);
                return Task.FromResult(user);
            }

            return Task.FromResult(default(TUser));
        }

        private void LoadUserDetails(TUser user)
        {
            user.Roles = _repository.SimpleAccess.ExecuteValues<string>("SA_AspNet_IdentityRoles_GetAllNamesByUserId", new { userId = user.Id }).ToList();
            //user.Claims = _repository.FindAll<IdentityUserClaim<TKey>>(uc => uc.UserId == user.Id).ToList();
            //var userLogins = _repository.FindAll<IdentityUserLogin<TKey>>(ur => ur.UserId == user.Id);
            //if (userLogins.Count() > 0)
            //{
                //user.Logins = userLogins.Select(ul => new UserLoginInfo(ul.LoginProvider, ul.ProviderKey)).ToList();
            //}
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            if (userName == null)
            {
                return Task.FromResult(default(TUser));
            }

            //var user = _userRepository.GetByName(userName);
            var user = _repository.Find<TUser>(u => u.UserName == userName);
            if (user != null && !string.IsNullOrEmpty(user.Email))
            {                
                //user.Roles = _userRoleRepository.PopulateRoles(user.Id);
                //user.Claims = _userClaimRepository.PopulateClaims(user.Id);
                //user.Logins = _userLoginRepository.PopulateLogins(user.Id);
                LoadUserDetails(user);

                return Task.FromResult(user);
            }

            return Task.FromResult(default(TUser));
        }

        public Task UpdateAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            //if (user.Id > 0)
            //    throw new InvalidOperationException("user.Id property must be specified before calling CreateAsync");


            //_userRepository.Update(user);
            if (_repository.Update(user) > 0)
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }

        public Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var userLoginInfo = new UserLoginInfo(login.LoginProvider, login.ProviderKey);
            //_userLoginRepository.Insert(user, login);
            if (_repository.Insert(userLoginInfo) > 0)
            {
                user.Logins.Add(userLoginInfo);
                return Task.FromResult<int>(0);
            }
            else
            {
                return Task.FromResult<int>(-1);
            }
        }

        public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            var tUserLogin = user.Logins.SingleOrDefault(l =>
            {
                if (l.LoginProvider != login.LoginProvider)
                {
                    return false;
                }
                return l.ProviderKey == login.ProviderKey;
            });

            if (tUserLogin != null)
            {
                user.Logins.Remove(tUserLogin);

                //_userLoginRepository.Delete(user, login);
                _repository.Delete<UserLoginInfo>(tUserLogin);

            }
            return Task.FromResult<int>(0);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult<IList<UserLoginInfo>>(user.Logins.ToList());
        }


        public Task AddToRoleAsync(TUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var role = _roleStote.FindByNameAsync(roleName).Result;

            if (role == null)
            {
                throw new ArgumentException("Invalid roleName", "roleName");
            }

            var identityUserRole = new IdentityUserRole<TKey>
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            //_userRoleRepository.Insert(user,roleName);

            _repository.Insert(identityUserRole);

            if (!user.Roles.Contains(roleName, StringComparer.InvariantCultureIgnoreCase))
            {
                user.Roles.Add(roleName);
            }
            
            return Task.FromResult(0);
        }

        public void AddToRole(SqlTransaction transaction, TUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var role = _roleStote.FindByNameAsync(roleName).Result;

            if (role == null)
            {
                throw new ArgumentException("Invalid roleName", "roleName");
            }

            var identityUserRole = new IdentityUserRole<TKey>
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            _repository.Insert(transaction, identityUserRole);

            if (!user.Roles.Contains(roleName, StringComparer.InvariantCultureIgnoreCase))
            {
                user.Roles.Add(roleName);
            }
        }

        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var role = _roleStote.FindByNameAsync(roleName).Result;

            if (role == null)
            {
                throw new ArgumentException("Invalid roleName", "roleName");
            }


            if (_repository.Delete<IdentityUserRole<TKey>>(new { userId = user.Id, roleId = role.Id }) > 0)
            {
                user.Roles.Remove(roleName);
            }

            //_userRoleRepository.Delete(user, roleName);

            return Task.FromResult(0);
        }

        public void RemoveFromRole(SqlTransaction transaction, TUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var role = _roleStote.FindByName(transaction, roleName);

            if (role == null)
            {
                throw new ArgumentException("Invalid roleName", "roleName");
            }

            if (_repository.Delete<IdentityUserRole>(transaction,
                new SqlParameter("userId", user.Id),
                new SqlParameter("roleId", role.Id)) > 0)
            {
                user.Roles.Remove(roleName);
            }
        }
        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult<IList<string>>(user.Roles.ToArray());
        }

        public Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.Roles.Contains(roleName, StringComparer.InvariantCultureIgnoreCase));
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult<bool>(user.PasswordHash != null);
        }

        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetEmailAsync(TUser user, string email)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.Email = email;

            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.EmailConfirmed = confirmed;

            return Task.FromResult(0);
        }

        public Task<TUser> FindByEmailAsync(string email)
        {
            if (email == null)
                throw new ArgumentNullException("email");

            //var user = _userRepository.GetByEmail(email);
            var user = _repository.Find<TUser>(u => u.Email == email);
            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
                //user.Roles = _userRoleRepository.PopulateRoles(user.Id);
                //user.Claims = _userClaimRepository.PopulateClaims(user.Id);
                //user.Logins = _userLoginRepository.PopulateLogins(user.Id);
                LoadUserDetails(user);
                return Task.FromResult(user);
            }
            return Task.FromResult(default(TUser));
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            DateTimeOffset dateTimeOffset;
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (user.lockoutEndDateUtc.HasValue)
            {
                DateTime? lockoutEndDateUtc = user.lockoutEndDateUtc;
                dateTimeOffset = new DateTimeOffset(DateTime.SpecifyKind(lockoutEndDateUtc.Value, DateTimeKind.Utc));
            }
            else
            {
                dateTimeOffset = new DateTimeOffset();
            }
            return Task.FromResult(dateTimeOffset);
        }

        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            DateTime? nullable;
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (lockoutEnd == DateTimeOffset.MinValue)
            {
                nullable = null;
            }
            else
            {
                nullable = new DateTime?(lockoutEnd.UtcDateTime);
            }
            user.lockoutEndDateUtc = nullable;
            return Task.FromResult<int>(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.LockoutEnabled = enabled;

            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.TwoFactorEnabled = enabled;

            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.PhoneNumber = phoneNumber;

            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.PhoneNumberConfirmed = confirmed;

            return Task.FromResult(0);
        }

        public IQueryable<TUser> Users
        {
            get{
                //return _userRepository.GetAll();
                //return _repository.GetAll<TUser>().AsQueryable();
                return _repository.FindAll<TUser>().AsQueryable();
            }
        }

        #region External Providers and User Claim

        public Task<TUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var userLoginInfo = _repository.SimpleAccess.ExecuteDynamic("SA_AspNet_IdentityUserLoginInfo_GetByProviderKeyAndLoginProvider"
                , new { ProviderKey = login.ProviderKey, LoginProvider = login.LoginProvider });

            if (userLoginInfo != null)
            {
                return FindByIdAsync(userLoginInfo.UserId);
            }
            return Task.FromResult(default(TUser));
        }

        public Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            IList<Claim> result = user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
            return Task.FromResult(result);
        }

        public Task AddClaimAsync(TUser user, Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (!user.Claims.Any(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value))
            {
                var userClaim = new IdentityUserClaim<TKey>
                {
                    UserId = user.Id,
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                };
                _repository.Insert(userClaim);

                user.Claims.Add(userClaim);
                //throw new NotImplementedException();

            }
            return Task.FromResult(0);
        }

        public Task RemoveClaimAsync(TUser user, Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var userClaim = user.Claims.FirstOrDefault(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            user.Claims.Remove(userClaim);

            _repository.Delete<IdentityUserClaim>(userClaim.Id);

            return Task.FromResult(0);
        }
        #endregion
    }
}

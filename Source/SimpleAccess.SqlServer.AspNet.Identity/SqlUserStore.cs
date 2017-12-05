using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SimpleAccess.Core.Entity.RepoWrapper;
using System.Data.SqlClient;

namespace SimpleAccess.SqlServer.AspNet.Identity
{
    public class SqlUserStore<TUser> :
        IUserStore<TUser>,
        IUserLoginStore<TUser>,
        IUserClaimStore<TUser>,
        IUserRoleStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserSecurityStampStore<TUser>,
        IUserEmailStore<TUser>,
        IUserLockoutStore<TUser, string>,
        IUserTwoFactorStore<TUser, string>,
        IUserPhoneNumberStore<TUser>,
        IQueryableUserStore<TUser>
    where TUser : IdentityUser, new()
    {
        private readonly string _connectionString;
        private readonly ISqlRepository _repository;
        private readonly SqlRoleStore<IdentityRole> _roleStote;
         public SqlUserStore()
            : this("defaultConnection")
        {

        }

        public SqlUserStore(string connectionStringName)
        {
            _connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

            _repository = new SqlRepository(connectionStringName);
            _roleStote = new SqlRoleStore<IdentityRole>(_repository);
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
            if (string.IsNullOrEmpty(user.Id))
                throw new InvalidOperationException("user.Id property must be specified before calling CreateAsync");

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
            if (string.IsNullOrEmpty(user.Id))
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

        public Task<TUser> FindByIdAsync(string userId)
        {
            //var user =_userRepository.GetById(userId);
            //var user = _repository.Get<TUser>(new { id = userId } );
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
            user.Claims = _repository.FindAll<IdentityUserClaim>(uc => uc.UserId == user.Id).ToList();
            var userLogins = _repository.FindAll<IdentityUserLogin>(ur => ur.UserId == user.Id );
            if (userLogins.Count() > 0)
            {
                user.Logins = userLogins.Select(ul => new UserLoginInfo(ul.LoginProvider, ul.ProviderKey)).ToList();
            }
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            if (userName == null)
            {
                return Task.FromResult(default(TUser));
            }

            var user = _repository.Find<TUser>(u => u.UserName == userName);
            if (user != null && !string.IsNullOrEmpty(user.Email))
            {                
                LoadUserDetails(user);

                return Task.FromResult(user);
            }

            return Task.FromResult(default(TUser));
        }

        public Task UpdateAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(user.Id))
                throw new InvalidOperationException("user.Id property must be specified before calling CreateAsync");


            if (_repository.Update(user) > 0)
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }

        public Task UpdateAsync(TUser user, params string[] roles)
        {
            using (SqlTransaction transaction = _repository.SimpleAccess.BeginTrasaction())
            {
                try
                {
                    Update(transaction, user, roles);
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

        public void Update(SqlTransaction transaction,  TUser user, params string[] roles)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(user.Id))
                throw new InvalidOperationException("user.Id property must be specified before calling CreateAsync");


            if (_repository.Update(transaction, user) > 0)
            {

            }
            else
            {
                throw new Exception("No user updated.");
            }

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

            var identityUserRole = new IdentityUserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };


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

            var identityUserRole = new IdentityUserRole
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


            if (_repository.Delete<IdentityUserRole>(new { userId = user.Id, roleId = role.Id }) > 0)
            {
                user.Roles.Remove(roleName);
            }

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

            var user = _repository.Find<TUser>(u => u.Email == email);
            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
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
                user.Claims.Add(new IdentityUserClaim
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                });
                throw new NotImplementedException();
                //_userClaimRepository.Insert(user, claim);

            }
            return Task.FromResult(0);
        }

        public Task RemoveClaimAsync(TUser user, Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var userClaim = user.Claims.FirstOrDefault(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            user.Claims.Remove(userClaim);

            //_userClaimRepository.Delete(user, claim);
            throw new NotImplementedException();
            return Task.FromResult(0);
        }
        #endregion
    }
}

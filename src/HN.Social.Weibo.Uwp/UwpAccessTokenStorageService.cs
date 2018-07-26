using System;
using System.Linq;
using System.Threading.Tasks;
using HN.Social.Weibo.Models;
using Windows.Security.Credentials;

namespace HN.Social.Weibo
{
    public class UwpAccessTokenStorageService : IAccessTokenStorageService
    {
        private const string WeiboPasswordVaultResourceName = "WeiboAccessToken";

        public Task ClearAsync()
        {
            var passwordVault = new PasswordVault();
            var passwordCredentials = passwordVault.FindAllByResource(WeiboPasswordVaultResourceName);
            foreach (var passwordCredential in passwordCredentials)
            {
                passwordVault.Remove(passwordCredential);
            }
            return Task.CompletedTask;
        }

        public Task<AccessToken> LoadAsync()
        {
            var passwordVault = new PasswordVault();
            var passwordCredentials = passwordVault.RetrieveAll().Where(temp => temp.Resource == WeiboPasswordVaultResourceName).ToList();
            var accessTokenPasswordCredential = passwordCredentials.FirstOrDefault(temp => temp.UserName == "AccessToken");
            var expiressAtPasswordCredential = passwordCredentials.FirstOrDefault(temp => temp.UserName == "ExpiressAt");
            var userIdPasswordCredential = passwordCredentials.FirstOrDefault(temp => temp.UserName == "UserId");
            if (accessTokenPasswordCredential == null ||
                expiressAtPasswordCredential == null ||
                userIdPasswordCredential == null)
            {
                return Task.FromResult<AccessToken>(null);
            }

            accessTokenPasswordCredential.RetrievePassword();
            expiressAtPasswordCredential.RetrievePassword();
            userIdPasswordCredential.RetrievePassword();

            return Task.FromResult(new AccessToken()
            {
                Value = accessTokenPasswordCredential.Password,
                ExpiressAt = DateTime.Parse(expiressAtPasswordCredential.Password),
                UserId = long.Parse(userIdPasswordCredential.Password)
            });
        }

        public Task SaveAsync(AccessToken accessToken)
        {
            if (accessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            var passwordVault = new PasswordVault();
            passwordVault.Add(new PasswordCredential(WeiboPasswordVaultResourceName, "AccessToken", accessToken.Value));
            passwordVault.Add(new PasswordCredential(WeiboPasswordVaultResourceName, "ExpiressAt", accessToken.ExpiressAt.ToString("O")));
            passwordVault.Add(new PasswordCredential(WeiboPasswordVaultResourceName, "UserId", accessToken.UserId.ToString()));
            return Task.CompletedTask;
        }
    }
}
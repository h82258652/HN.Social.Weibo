using System;
using System.Threading.Tasks;
using HN.Social.Weibo.Models;
using Windows.Security.Credentials;

namespace HN.Social.Weibo
{
    public class UwpAccessTokenStorageService : IAccessTokenStorageService
    {
        private const string PasswordVaultResourceName = "WeiboAccessToken";

        public Task ClearAsync()
        {
            var passwordVault = new PasswordVault();
            var passwordCredentials = passwordVault.FindAllByResource(PasswordVaultResourceName);
            foreach (var passwordCredential in passwordCredentials)
            {
                passwordVault.Remove(passwordCredential);
            }
            return Task.CompletedTask;
        }

        public Task<AccessToken> LoadAsync()
        {
            var passwordVault = new PasswordVault();
            var accessTokenPasswordCredential = passwordVault.Retrieve(PasswordVaultResourceName, "AccessToken");
            var expiressAtPasswordCredential = passwordVault.Retrieve(PasswordVaultResourceName, "ExpiressAt");
            var userIdPasswordCredential = passwordVault.Retrieve(PasswordVaultResourceName, "UserId");
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
            passwordVault.Add(new PasswordCredential(PasswordVaultResourceName, "AccessToken", accessToken.Value));
            passwordVault.Add(new PasswordCredential(PasswordVaultResourceName, "ExpiressAt", accessToken.ExpiressAt.ToString("O")));
            passwordVault.Add(new PasswordCredential(PasswordVaultResourceName, "UserId", accessToken.UserId.ToString()));
            return Task.CompletedTask;
        }
    }
}
using System;
using System.Linq;
using HN.Social.Weibo.Models;
using Windows.Security.Credentials;

namespace HN.Social.Weibo
{
    public class UwpAccessTokenStorage : IAccessTokenStorage
    {
        private const string WeiboPasswordVaultResourceName = "WeiboAccessToken";
        private readonly PasswordVault _vault = new PasswordVault();

        public void Clear()
        {
            var passwordCredentials = _vault.RetrieveAll().Where(temp => temp.Resource == WeiboPasswordVaultResourceName);
            foreach (var passwordCredential in passwordCredentials)
            {
                _vault.Remove(passwordCredential);
            }
        }

        public AccessToken Load()
        {
            try
            {
                var accessTokenPasswordCredential = _vault.Retrieve(WeiboPasswordVaultResourceName, "AccessToken");
                accessTokenPasswordCredential.RetrievePassword();

                var expiresAtPasswordCredential = _vault.Retrieve(WeiboPasswordVaultResourceName, "ExpiresAt");
                expiresAtPasswordCredential.RetrievePassword();

                var userIdPasswordCredential = _vault.Retrieve(WeiboPasswordVaultResourceName, "UserId");
                userIdPasswordCredential.RetrievePassword();

                return new AccessToken
                {
                    Value = accessTokenPasswordCredential.Password,
                    ExpiresAt = DateTime.Parse(expiresAtPasswordCredential.Password),
                    UserId = long.Parse(userIdPasswordCredential.Password)
                };
            }
            catch
            {
                return null;
            }
        }

        public void Save(AccessToken accessToken)
        {
            if (accessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            _vault.Add(new PasswordCredential(WeiboPasswordVaultResourceName, "AccessToken", accessToken.Value));
            _vault.Add(new PasswordCredential(WeiboPasswordVaultResourceName, "ExpiresAt", accessToken.ExpiresAt.ToString("O")));
            _vault.Add(new PasswordCredential(WeiboPasswordVaultResourceName, "UserId", accessToken.UserId.ToString()));
        }
    }
}

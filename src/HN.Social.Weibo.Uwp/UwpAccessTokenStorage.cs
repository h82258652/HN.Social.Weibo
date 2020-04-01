using System;
using System.Linq;
using HN.Social.Weibo.Models;
using Windows.Security.Credentials;

namespace HN.Social.Weibo
{
    /// <inheritdoc />
    public class UwpAccessTokenStorage : IAccessTokenStorage
    {
        private const string AccessTokenKey = "AccessToken";
        private const string ExpiresAtKey = "ExpiresAt";
        private const string UserIdKey = "UserId";
        private const string WeiboPasswordVaultResourceName = "WeiboAccessToken";
        private readonly PasswordVault _vault = new PasswordVault();

        /// <inheritdoc />
        public void Clear()
        {
            var passwordCredentials = _vault.RetrieveAll().Where(temp => temp.Resource == WeiboPasswordVaultResourceName);
            foreach (var passwordCredential in passwordCredentials)
            {
                _vault.Remove(passwordCredential);
            }
        }
        
        /// <inheritdoc />
        public AccessToken? Load()
        {
            try
            {
                var accessTokenPasswordCredential = _vault.Retrieve(WeiboPasswordVaultResourceName, AccessTokenKey);
                accessTokenPasswordCredential.RetrievePassword();

                var expiresAtPasswordCredential = _vault.Retrieve(WeiboPasswordVaultResourceName, ExpiresAtKey);
                expiresAtPasswordCredential.RetrievePassword();

                var userIdPasswordCredential = _vault.Retrieve(WeiboPasswordVaultResourceName, UserIdKey);
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

        /// <inheritdoc />
        public void Save(AccessToken accessToken)
        {
            if (accessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            _vault.Add(new PasswordCredential(WeiboPasswordVaultResourceName, AccessTokenKey, accessToken.Value));
            _vault.Add(new PasswordCredential(WeiboPasswordVaultResourceName, ExpiresAtKey, accessToken.ExpiresAt.ToString("O")));
            _vault.Add(new PasswordCredential(WeiboPasswordVaultResourceName, UserIdKey, accessToken.UserId.ToString()));
        }
    }
}

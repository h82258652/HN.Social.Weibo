using System;
using System.Linq;
using HN.Social.Weibo.Models;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
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
        private readonly WeiboOptions _weiboOptions;

        /// <summary>
        /// 初始化 <see cref="UwpAccessTokenStorage" /> 类的新实例。
        /// </summary>
        /// <param name="weiboOptionsAccessor"><see cref="WeiboOptions" /> 实例的访问。</param>
        public UwpAccessTokenStorage(
            [NotNull] IOptions<WeiboOptions> weiboOptionsAccessor)
        {
            if (weiboOptionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(weiboOptionsAccessor));
            }

            _weiboOptions = weiboOptionsAccessor.Value;
        }

        /// <inheritdoc />
        public void Clear()
        {
            var passwordVaultResourceName = GetPasswordVaultResourceName();
            var passwordCredentials = _vault.RetrieveAll().Where(temp => temp.Resource == passwordVaultResourceName);
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
                var passwordVaultResourceName = GetPasswordVaultResourceName();

                var accessTokenPasswordCredential = _vault.Retrieve(passwordVaultResourceName, AccessTokenKey);
                accessTokenPasswordCredential.RetrievePassword();

                var expiresAtPasswordCredential = _vault.Retrieve(passwordVaultResourceName, ExpiresAtKey);
                expiresAtPasswordCredential.RetrievePassword();

                var userIdPasswordCredential = _vault.Retrieve(passwordVaultResourceName, UserIdKey);
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

            var passwordVaultResourceName = GetPasswordVaultResourceName();

            _vault.Add(new PasswordCredential(passwordVaultResourceName, AccessTokenKey, accessToken.Value));
            _vault.Add(new PasswordCredential(passwordVaultResourceName, ExpiresAtKey, accessToken.ExpiresAt.ToString("O")));
            _vault.Add(new PasswordCredential(passwordVaultResourceName, UserIdKey, accessToken.UserId.ToString()));
        }

        private string GetPasswordVaultResourceName()
        {
            return $"{WeiboPasswordVaultResourceName}:{_weiboOptions.AppKey}";
        }
    }
}

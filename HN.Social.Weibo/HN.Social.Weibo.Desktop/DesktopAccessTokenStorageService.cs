using System.IO;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Serialization;
using HN.Social.Weibo.Models;

namespace HN.Social.Weibo
{
    public class DesktopAccessTokenStorageService : IAccessTokenStorageService
    {
        private const string AccessTokenStorageFileName = "weibo_accesstoken";

        public Task ClearAsync()
        {
            using (var isolatedStorageFile = GetStore())
            {
                if (isolatedStorageFile.FileExists(AccessTokenStorageFileName))
                {
                    isolatedStorageFile.DeleteFile(AccessTokenStorageFileName);
                }
            }

            return Task.CompletedTask;
        }

        public async Task<AccessToken> LoadAsync()
        {
            using (var isolatedStorageFile = GetStore())
            {
                if (isolatedStorageFile.FileExists(AccessTokenStorageFileName))
                {
                    using (var fileStream = isolatedStorageFile.OpenFile(AccessTokenStorageFileName, FileMode.Open))
                    {
                        var memoryStream = new MemoryStream();
                        await fileStream.CopyToAsync(memoryStream);

                        var data = ProtectedData.Unprotect(memoryStream.ToArray(), null, DataProtectionScope.CurrentUser);

                        var xmlSerializer = new XmlSerializer(typeof(AccessToken));
                        return (AccessToken)xmlSerializer.Deserialize(new MemoryStream(data));
                    }
                }

                return null;
            }
        }

        public async Task SaveAsync(AccessToken accessToken)
        {
            var xmlSerializer = new XmlSerializer(typeof(AccessToken));
            var memoryStream = new MemoryStream();
            xmlSerializer.Serialize(memoryStream, accessToken);
            var protectedData = ProtectedData.Protect(memoryStream.ToArray(), null, DataProtectionScope.CurrentUser);

            using (var isolatedStorageFile = GetStore())
            {
                using (var fileStream = isolatedStorageFile.CreateFile(AccessTokenStorageFileName))
                {
                    await new MemoryStream(protectedData).CopyToAsync(fileStream);
                }
            }
        }

        private static IsolatedStorageFile GetStore()
        {
            return IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
        }
    }
}
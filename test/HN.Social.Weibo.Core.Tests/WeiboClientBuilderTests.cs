using System;
using Xunit;

namespace HN.Social.Weibo.Core.Tests
{
    public class WeiboClientBuilderTests
    {
        [Fact]
        public void Build_NotSetAccessTokenStorage_ThrowException()
        {
            // Arrange
            var builder = new WeiboClientBuilder().UseAuthorizationProvider<MockAuthorizationProvider>();

            // Act
            var exception = Assert.Throws<WeiboException>(() => builder.Build());

            // Assert
            Assert.Equal("未配置 AccessToken 存储", exception.Message);
        }

        [Fact]
        public void Build_NotSetAuthorizationProvider_ThrowException()
        {
            // Arrange
            var builder = new WeiboClientBuilder();

            // Act
            var exception = Assert.Throws<WeiboException>(() => builder.Build());

            // Assert
            Assert.Equal("未配置 AuthorizationProvider", exception.Message);
        }

        [Fact]
        public void Service_NotNull()
        {
            // Arrange
            var builder = new WeiboClientBuilder();

            // Act
            var services = builder.Services;

            // Assert
            Assert.NotNull(services);
        }
    }
}

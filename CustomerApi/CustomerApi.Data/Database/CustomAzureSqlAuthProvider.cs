using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;

namespace CustomerApi.Data.Database
{
    public class CustomAzureSqlAuthProvider : SqlAuthenticationProvider
    {
        private static readonly string[] AzureSqlScopes =
        {
            "https://database.windows.net//.default"
        };

        private static readonly TokenCredential Credential = new DefaultAzureCredential();
        private readonly string _tenandId;

        public CustomAzureSqlAuthProvider(string tenantId)
        {
            _tenandId = tenantId;
        }

        public override async Task<SqlAuthenticationToken> AcquireTokenAsync(SqlAuthenticationParameters parameters)
        {
            var tokenRequestContext = new TokenRequestContext(AzureSqlScopes, tenantId: _tenandId);
            var tokenResult = await Credential.GetTokenAsync(tokenRequestContext, default);

            return new SqlAuthenticationToken(tokenResult.Token, tokenResult.ExpiresOn);
        }

        public override bool IsSupported(SqlAuthenticationMethod authenticationMethod)
        {
            return authenticationMethod.Equals(SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow);
        }
    }
}
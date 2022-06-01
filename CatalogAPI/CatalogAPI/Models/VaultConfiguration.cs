using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;

namespace CatalogAPI.Models
{
    public class VaultConfiguration
    {
        private IConfiguration _configuration;
        public VaultConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public VaultConfiguration()
        {
          
        }
        public async Task<Dictionary<string, object>> GetDBCredentials()
        {
          string token = _configuration.GetConnectionString("Root_Token");
            string url = _configuration.GetConnectionString("Vault_Url");

            // Initialize one of the several auth methods.
            IAuthMethodInfo authMethod = new TokenAuthMethodInfo("s.7a1lmH5XuRV3LsLbmcVE23fh");

            // Initialize settings. You can also set proxies, custom delegates etc. here.
            var vaultClientSettings = new VaultClientSettings("http://localhost:8200", authMethod);

            IVaultClient vaultClient = new VaultClient(vaultClientSettings);
            Console.WriteLine(vaultClient.V1.Secrets);
            
            var result = await vaultClient.V1.Secrets.KeyValue.V1.ReadSecretAsync("mssqlserver", 
                "secret", null);

            return result.Data;

        }
    }
}

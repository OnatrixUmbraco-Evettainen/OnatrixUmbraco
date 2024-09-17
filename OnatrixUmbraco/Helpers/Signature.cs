using System.Security.Cryptography;
using System.Text;

namespace OnatrixUmbraco.Helpers
{
    public class Signature
    {
        public string GenerateSignature(string fieldAlias, bool isRequired, string regex)
        {
            var data = $"{fieldAlias} : {isRequired} : {regex}";
            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(hashBytes);
        }
    }
}

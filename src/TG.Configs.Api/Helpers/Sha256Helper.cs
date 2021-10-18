using System.Security.Cryptography;
using System.Text;

namespace TG.Configs.Api.Helpers
{
  public static class Sha256Helper
  {
    public static string GetSha256Hash(string src)
    {
      var encryptor = new SHA256Managed();
      var crypto = encryptor.ComputeHash(Encoding.ASCII.GetBytes(src), 0, Encoding.ASCII.GetByteCount(src));
      var hash = new StringBuilder();
      foreach (var b in crypto)
      {
        hash.Append(b.ToString("x2"));
      }
      return hash.ToString();
    }
  }
}
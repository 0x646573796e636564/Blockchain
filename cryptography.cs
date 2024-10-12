using System;
using System.Security.Cryptography;
using System.Text;

public class Cryptography
{
    private readonly RSA rsa;

    public Cryptography(string pemPrivateKey)
    {
        rsa = RSA.Create();
        ImportPrivateKey(pemPrivateKey);
    }

    private void ImportPrivateKey(string pemPrivateKey)
    {
        try
        {
            // Ensure the private key is formatted correctly
            if (string.IsNullOrWhiteSpace(pemPrivateKey))
            {
                throw new ArgumentException("Private key cannot be null or empty.");
            }

            // Remove the header and footer
            var privateKey = pemPrivateKey.Replace("-----BEGIN PRIVATE KEY-----", "")
                                           .Replace("-----END PRIVATE KEY-----", "")
                                           .Replace("\n", "")
                                           .Replace("\r", "");

            // Convert from Base64
            byte[] privateKeyBytes = Convert.FromBase64String(privateKey);
            rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error importing private key: {ex.Message}");
            throw;
        }
    }

    public string Sign(string data)
    {
        try
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] signatureBytes = rsa.SignData(dataBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            return Convert.ToBase64String(signatureBytes);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error signing data: {ex.Message}");
            throw;
        }
    }

    public bool Verify(string data, string signature)
    {
        try
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] signatureBytes = Convert.FromBase64String(signature);
            return rsa.VerifyData(dataBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error verifying signature: {ex.Message}");
            throw;
        }
    }
}
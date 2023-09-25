using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TrelloDotNet
{
    internal static class WebhookSignatureValidator
    {
        private static byte[] Digest(byte[] data, int dataIndex, int dataLength, string secret)
        {
            var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(secret));
            return hmac.ComputeHash(data, dataIndex, dataLength);
        }

        internal static bool ValidateSignature(string json, string signature, string webhookUrl, string secret)
        {
            if (string.IsNullOrEmpty(json) || string.IsNullOrEmpty(signature) || string.IsNullOrEmpty(webhookUrl) || string.IsNullOrEmpty(secret))
            {
                return false;
            }
            
            var payloadLength = Encoding.UTF8.GetByteCount(json) + Encoding.UTF8.GetByteCount(webhookUrl);
            var payloadBytes = new byte[payloadLength];
            var payloadBytesWritten = Encoding.UTF8.GetBytes(json, 0, json.Length, payloadBytes, 0);
            payloadBytesWritten += Encoding.UTF8.GetBytes(webhookUrl, 0, webhookUrl.Length, payloadBytes, payloadBytesWritten);
            var hashBytes = Digest(payloadBytes, 0, payloadBytesWritten, secret);
            var signatureBytes = Convert.FromBase64String(signature);
            return hashBytes.SequenceEqual(signatureBytes); 
        }
    }
}
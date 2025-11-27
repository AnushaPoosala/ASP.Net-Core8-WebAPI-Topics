using System.Security.Cryptography;

namespace AESServerApp.Models.Services
{
    public class AESEncryptionService
    {
        private readonly KeyManagementService _keyManagementService;

        public AESEncryptionService(KeyManagementService key)
        {
                 _keyManagementService = key;

        }

        //Encrypts the given plaintext using AES encryption with the provided key and IV.
        public async Task<string> EncryptStringAsync(string clientId, string plaintext)
        {
            // Retrieve the key and IV for the given clientId
            var clientKeyIV = await _keyManagementService.GetClientKeyIVAsync(clientId);
            if (clientKeyIV == null)
            {
                throw new Exception("Client key and IV not found.");
            }

            //Create an AES instance and set the key and IV
            using (var aes = Aes.Create())
            {

                //Assign the key and IV from the retrieved ClientkeyIV record
                aes.Key = Convert.FromBase64String(clientKeyIV.Key);
                aes.IV = Convert.FromBase64String(clientKeyIV.IV);

                // Create an encryptor to perform the encryption transformation
                ICryptoTransform cryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV);

                //Convert the plaintext to bytes using UTF8 encoding

                byte[] plaintextBytes = System.Text.Encoding.UTF8.GetBytes(plaintext);

                //Perform the encryption on the plaintext bytes and get the ciphertext
                byte[] encryptedBytes = cryptoTransform.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);

                //Convert the encrypted bytes to a Base64 string and return it
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        //Decrypts the given ciphertext using AES decryption with the provided key and IV.
        public async Task<string> DecryptStringAsync(string clientId, string ciphertext)
        {
            // Retrieve the key and IV for the given clientId
            var clientKeyIV = await _keyManagementService.GetClientKeyIVAsync(clientId);
            if (clientKeyIV == null)
            {
                throw new Exception("Client key and IV not found.");
            }

            //Create an AES instance and set the key and IV
            using (var aes = Aes.Create())
            {
                //Assign the key and IV from the retrieved ClientkeyIV record
                aes.Key = Convert.FromBase64String(clientKeyIV.Key);
                aes.IV = Convert.FromBase64String(clientKeyIV.IV);

                // Create a decryptor to perform the decryption transformation
                ICryptoTransform cryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV);

                //Convert the Base64 encoded ciphertext to bytes
                byte[] encryptedBytes = Convert.FromBase64String(ciphertext);

                //Perform the decryption on the encrypted bytes and get the plaintext
                byte[] decryptedBytes = cryptoTransform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                //Convert the decrypted bytes to a UTF8 string and return it
                return System.Text.Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}

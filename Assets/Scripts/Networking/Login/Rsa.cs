using System;
using System.Security.Cryptography;
using UnityEngine;

namespace Networking.Login
{
    /// <summary>
    ///     RSA class for encrypting a string
    /// </summary>
    public class Rsa : MonoBehaviour
    {
        private static string _key;

        /// <summary>
        ///     Loads the public key on application load
        /// </summary>
        private void Awake()
        {
            try
            {
                var keyFile = Resources.Load("PublicKey") as TextAsset;
                _key = keyFile.text;
            }
            catch (Exception e)
            {
                Debug.Log("Failed to load key: " + e.Message + " - " + e.StackTrace);
            }
        }

        /// <summary>
        ///     Encrypts data stream with the selected private key
        /// </summary>
        /// <param name="input"></param>
        /// <returns>The encrypted string</returns>
        public static byte[] Encrypt(byte[] input)
        {
            using var rsa = new RSACryptoServiceProvider(4096);
            rsa.PersistKeyInCsp = false;
            rsa.FromXmlString(_key);
            var encrypted = rsa.Encrypt(input, true);

            return encrypted;
        }
    }
}
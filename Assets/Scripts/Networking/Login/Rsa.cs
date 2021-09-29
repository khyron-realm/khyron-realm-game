using System;
using System.Security.Cryptography;
using UnityEngine;

namespace Networking.Login
{
    public class Rsa : MonoBehaviour
    {
        private static string _key;
        
        private void Awake()
        {
            try
            {
                if (Resources.Load("PublicKey") is TextAsset { } keyFile) _key = keyFile.text;
            }
            catch (Exception e)
            {
                Debug.Log("Failed to load key: " + e.Message + " - " + e.StackTrace);
            }
        }

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
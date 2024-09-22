using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public static class ElGamal
    {
        public static (ElGamalKeyPairGenerator keyPairGenerator, ElGamalPublicKeyParameters publicKey, ElGamalPrivateKeyParameters privateKey) GenerateElGamalKeys()
        {
            var generator = new ElGamalKeyPairGenerator();
            var paramGen = new ElGamalParametersGenerator();
            paramGen.Init(1024, 20, new SecureRandom());
            var parameters = paramGen.GenerateParameters();
            var keyGenParams = new ElGamalKeyGenerationParameters(new SecureRandom(), parameters);
            generator.Init(keyGenParams);
            var keyPair = generator.GenerateKeyPair();
            return (generator, (ElGamalPublicKeyParameters)keyPair.Public, (ElGamalPrivateKeyParameters)keyPair.Private);
        }

        public static byte[] ElGamalEncrypt(string plainText, ElGamalPublicKeyParameters publicKey)
        {
            var engine = new ElGamalEngine();
            engine.Init(true, publicKey);
            var bytes = Encoding.UTF8.GetBytes(plainText);
            return engine.ProcessBlock(bytes, 0, bytes.Length);
            }

        public static string ElGamalDecrypt(byte[] cipherText, ElGamalPrivateKeyParameters privateKey)
        {
            var engine = new ElGamalEngine();
            engine.Init(false, privateKey);
            var decryptedBytes = engine.ProcessBlock(cipherText, 0, cipherText.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}

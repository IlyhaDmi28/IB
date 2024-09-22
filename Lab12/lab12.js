const forge = require('node-forge');
const EC = require('elliptic').ec;
const crypto = require('crypto');

// RSA
async function generateRSAKeys() {
  const { privateKey, publicKey } = forge.pki.rsa.generateKeyPair(2048);
  return { privateKey, publicKey };
}

function rsaSignData(data, privateKey) {
  const md = forge.md.sha256.create();
  md.update(data, 'utf8');
  return privateKey.sign(md);
}

function rsaVerifyData(data, signature, publicKey) {
  const md = forge.md.sha256.create();
  md.update(data, 'utf8');
  return publicKey.verify(md.digest().bytes(), signature);
}

// ElGamal (example code, may require adaptation)
// ElGamal
async function generateElGamalKeys() {
    const ec = new EC('secp256k1'); // Выбираем эллиптическую кривую, подходящую для применения алгоритма Эль-Гамаля
    const key = ec.genKeyPair(); // Генерируем случайную пару ключей
    return { privateKey: key.getPrivate(), publicKey: key.getPublic() };
  }
  
  function elGamalSignData(data, privateKey) {
    const ec = new EC('secp256k1'); // Выбираем эллиптическую кривую, подходящую для применения алгоритма Эль-Гамаля
    const key = ec.keyFromPrivate(privateKey); // Получаем приватный ключ
    const hash = crypto.createHash('sha256').update(data).digest(); // Хешируем данные
    const signature = key.sign(hash); // Подписываем данные
    return { r: signature.r.toString(16), s: signature.s.toString(16) }; // Возвращаем подпись в виде объекта с компонентами r и s
  }
  
  function elGamalVerifyData(data, signature, publicKey) {
    const ec = new EC('secp256k1');
    const key = ec.keyFromPublic(publicKey); 
    const hash = crypto.createHash('sha256').update(data).digest(); 
    return key.verify(hash, signature); 
  }
// Schnorr using secp256k1
const ec = new EC('secp256k1');

function generateSchnorrKeys() {
  const key = ec.genKeyPair();
  return { privateKey: key.getPrivate(), publicKey: key.getPublic() };
}

function schnorrSignData(data, privateKey) {
  const hash = crypto.createHash('sha256').update(data).digest();
  const key = ec.keyFromPrivate(privateKey);
  const signature = key.sign(hash);
  return { r: signature.r.toString(16), s: signature.s.toString(16) };
}

function schnorrVerifyData(data, signature, publicKey) {
  const hash = crypto.createHash('sha256').update(data).digest();
  const key = ec.keyFromPublic(publicKey, 'hex');
  return key.verify(hash, signature);
}

async function main() {
  const text = "Hello, World!";
  const data = text;

  // RSA
  const rsaKeys = await generateRSAKeys();
  const rsaSignature = rsaSignData(data, rsaKeys.privateKey);
  const rsaVerified = rsaVerifyData(data, rsaSignature, rsaKeys.publicKey);
  console.log(`RSA Verified: ${rsaVerified}`);
  // console.log(`Private key: ${forge.pki.privateKeyToPem(rsaKeys.privateKey)}`);
  // console.log(`Public key: ${forge.pki.publicKeyToPem(rsaKeys.publicKey)}`);

  console.time('RSA Signing Time');
  rsaSignData(data, rsaKeys.privateKey);
  rsaVerifyData(data, rsaSignature, rsaKeys.publicKey);
  console.timeEnd('RSA Signing Time');

  // ElGamal
  const elGamalKeys = await generateElGamalKeys();
  const elGamalSignature = elGamalSignData(data, elGamalKeys.privateKey);
  const elGamalVerified = elGamalVerifyData(data, elGamalSignature, elGamalKeys.publicKey);
  console.log(`ElGamal Verified: ${elGamalVerified}`);
  // console.log(`Private key: ${elGamalKeys.privateKey}`);
  // console.log(`Public key: ${elGamalKeys.publicKey.encode('hex')}`);

  console.time('ElGamal Signing Time');
  elGamalSignData(data, elGamalKeys.privateKey);
  elGamalVerifyData(data, elGamalSignature, elGamalKeys.publicKey);
  console.timeEnd('ElGamal Signing Time');

  // Schnorr
  const schnorrKeys = generateSchnorrKeys();
  const schnorrSignature = schnorrSignData(data, schnorrKeys.privateKey);
  const schnorrVerified = schnorrVerifyData(data, schnorrSignature, schnorrKeys.publicKey);
  console.log(`Schnorr Verified: ${schnorrVerified}`);
  // console.log(`Private key: ${schnorrKeys.privateKey}`);
  // console.log(`Public key: ${schnorrKeys.publicKey.encode('hex')}`);

  console.time('Schnorr Signing Time');
  schnorrSignData(data, schnorrKeys.privateKey);
  schnorrVerifyData(data, schnorrSignature, schnorrKeys.publicKey);
  console.timeEnd('Schnorr Signing Time');
}

main().catch(err => console.error(err));

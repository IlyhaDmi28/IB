const crypto = require('crypto');

function rc4KeyScheduling(key) {
    let S = [];
    for (let i = 0; i < 256; i++) {
        S[i] = i;
    }

    let j = 0;
    for (let i = 0; i < 256; i++) {
        j = (j + S[i] + key[i % key.length]) % 256;
        [S[i], S[j]] = [S[j], S[i]];
    }
    return S;
}

function rc4PseudoRandomGeneration(S, messageLength) {
    let i = 0, j = 0;
    let keyStream = [];
    
    for (let k = 0; k < messageLength; k++) {
        i = (i + 1) % 256;
        j = (j + S[i]) % 256;
        [S[i], S[j]] = [S[j], S[i]];
        keyStream.push(S[(S[i] + S[j]) % 256]);
    }
    return keyStream;
}

function rc4EncryptDecrypt(message, key) {
    const keyScheduling = rc4KeyScheduling(key);
    const startTime = performance.now(); 
    const keyStream = rc4PseudoRandomGeneration(keyScheduling, message.length);
    const endTime = performance.now(); 
    if(key) console.log(`Время генерации ПСП: ${Number(endTime - startTime).toFixed(4)} мс`) 
    let result = Buffer.alloc(message.length);

    for (let i = 0; i < message.length; i++) {
        result[i] = message[i] ^ keyStream[i];
    }
    return result;
}

const key = Buffer.from([123, 125, 41, 84, 203]);

const text = 'Hello, my name Ilya!';
const message = Buffer.from(text, 'utf8');

const encryptedMessage = rc4EncryptDecrypt(message, key);

console.log(`Зашифрованный текст: ${encryptedMessage.toString('hex')}`);

const decryptedMessage = rc4EncryptDecrypt(encryptedMessage, key);
console.log(`Расшифрованный текст: ${decryptedMessage.toString('utf8')}`);
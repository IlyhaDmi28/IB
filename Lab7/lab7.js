const crypto = require('crypto');

function desEde2Encrypt(text, key1, key2) {
    const cipher1 = crypto.createCipheriv('des-ede', Buffer.concat([key1, key2]), null);
    const cipher2 = crypto.createCipheriv('des-ede', Buffer.concat([key2, key1]), null);

    let encrypted = cipher1.update(text, 'utf8', 'hex');
    encrypted += cipher1.final('hex');

    let doubleEncrypted = cipher2.update(Buffer.from(encrypted, 'hex'), 'hex', 'hex');
    doubleEncrypted += cipher2.final('hex');

    return doubleEncrypted;
}

function desEde2Decrypt(encryptedText, key1, key2) {
    const decipher1 = crypto.createDecipheriv('des-ede', Buffer.concat([key2, key1]), null);
    const decipher2 = crypto.createDecipheriv('des-ede', Buffer.concat([key1, key2]), null);

    let decrypted = decipher1.update(encryptedText, 'hex', 'hex');
    decrypted += decipher1.final('hex');

    let doubleDecrypted = decipher2.update(Buffer.from(decrypted, 'hex'), 'hex', 'utf8');
    doubleDecrypted += decipher2.final('utf8');

    return doubleDecrypted;
}

function countChangedBits(original, encrypted) {
    let originalBits = Buffer.from(original, 'utf8');
    let encryptedBits = Buffer.from(encrypted, 'hex');
 //   console.log(originalBits);
 //   console.log(encryptedBits);

    let changedBits = 0;
    for (let i = 0; i < originalBits.length; i++) {
        let diff = originalBits[i] ^ encryptedBits[i];
        for (let j = 0; j < 8; j++) {
            if (diff & (1 << j)) {
                changedBits++;
            }
        }
    }
    return changedBits;
}

function getBitSize(text) {
    return Buffer.from(text, 'utf8').length * 8;
}

//Норм
//12345678
//89abcdef
//Полуслабые
//01FE01FE01FE01FE
//FE01FE01FE01FE01
//Полуслабые
//0101010101010101
//0101010101010101
const key1 = Buffer.from('12345678', 'utf8'); // 8 байт
const key2 = Buffer.from('89abcdef', 'utf8'); // 8 байт
const text = 'Hello, World!';

// Измерение времени шифрования
let startTime; let endTime;
startTime = process.hrtime.bigint();
const encryptedText = desEde2Encrypt(text, key1, key2);
endTime = process.hrtime.bigint();

// Расчет измененных битов
const changedBits = countChangedBits(text, encryptedText);

// Вычисление размера сообщения в битах
const messageBitSize = getBitSize(text);

console.log(`Зашифрованный текст: ${encryptedText}`);
console.log(`Время шифрования: ${Number(endTime - startTime) / 1e6} мс`);
console.log(`Количество изменённых битов: ${changedBits}`);
console.log(`Размер сообщения: ${messageBitSize} бит`);

// Дешифрование (для проверки)
startTime = process.hrtime.bigint();
const decryptedText = desEde2Decrypt(encryptedText, key1, key2);
endTime = process.hrtime.bigint();

console.log(`Расшифрованный текст: ${decryptedText}`);
console.log(`Время расшифрования: ${Number(endTime - startTime) / 1e6} мс`);

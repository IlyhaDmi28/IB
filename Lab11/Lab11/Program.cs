using Lab11;

string message = "Hello, Wordl!";
Console.WriteLine($"Исходное сообщение {message}");

// Генерация случайной соли
string salt = SHA.GenerateSalt(16); // Длина соли 16 байт

// Хеширование сообщения с солью
string hash = SHA.HashSHA256(message + salt);

Console.WriteLine($"Соль: {salt}");
Console.WriteLine($"SHA-256 хеш: {hash}");


// Оценка быстродействия
int iterations = 1000000;
var watch = System.Diagnostics.Stopwatch.StartNew();

for (int i = 0; i < iterations; i++)
{
    SHA.HashSHA256(message + salt);
}

watch.Stop();
Console.WriteLine($"Время, затраченное на {iterations} итераций: {watch.ElapsedMilliseconds} мс");
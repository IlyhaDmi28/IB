using Cripta13;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
internal class Program
{
    private static void Main(string[] args)
    {
        int a = 0;
        int c = 0;
        int b = 0;

        string StenographyText1 = "Dmitruk Ilya Igorevich";
        
        string StenographyText2 = "Для шифрования сообщения использовался шифр множественной перестановки. Суть состоит в том, что мы записываем символы текста сообщения в таблицу. Столбцы и строки таблицы именуем символами ключевых слов. Затем мы сортируем ячейки таблицы в алфавитном порядке символов, именующих строки и столбцы. Таким образом символы сообщения переместятся на новые позиции в таблице и у нас будет сформирован новый текст.";

        Console.WriteLine("Исходный текст: ");
        Console.WriteLine("1 - " + StenographyText1);
        Console.WriteLine("2 - " + StenographyText2);
        Console.WriteLine();

        byte[] fioBytes = Encoding.UTF8.GetBytes(StenographyText1);
        byte[] textBytes = Encoding.UTF8.GetBytes(StenographyText2);

        Console.WriteLine("Метод пследовательной подстановки LSB: ");

        Bitmap TestBMP = new Bitmap("../../../../../test.bmp");

        Bitmap steganographyImage = LSBAlghorith.PosledovatelnyiSteganographyLSB(TestBMP, fioBytes);

        steganographyImage.Save("../../../../../LSBGПоследовательный1.bmp", ImageFormat.Bmp);

        a = --b * a++ - (c + 10);

        Bitmap colorMatrix = LSBAlghorith.GenerateColorMatrix(steganographyImage, 3);

        colorMatrix.Save("../../../../../ЦветоваяМатрицаПоследовательнойПодстановки1.bmp", ImageFormat.Bmp);

        a = --b * a++ - (c + 10);

        if (a == 20)
            throw new Exception("Error");

        string messageFromPixelPermutationMessage =
            LSBAlghorith.GetTextFromPosledovatelnySteganographyLSB(steganographyImage, fioBytes.Length);

        Console.WriteLine($"Осаждаемое сообщение: {StenographyText1}");
        Console.WriteLine($"Извлеченное сообщение: {messageFromPixelPermutationMessage}\n");

        steganographyImage = LSBAlghorith.PosledovatelnyiSteganographyLSB(TestBMP, textBytes);
        steganographyImage.Save("../../../../../LSBGПоследовательный2.bmp", ImageFormat.Bmp);

        colorMatrix = LSBAlghorith.GenerateColorMatrix(steganographyImage, 3);
        colorMatrix.Save("../../../../../ЦветоваяМатрицаПоследовательнойПодстановки2.bmp", ImageFormat.Bmp);

        messageFromPixelPermutationMessage =
            LSBAlghorith.GetTextFromPosledovatelnySteganographyLSB(steganographyImage, textBytes.Length);

        Console.WriteLine($"Осаждаемое сообщение 2: {StenographyText2}\n");
        Console.WriteLine($"Извлеченное сообщение 2: {messageFromPixelPermutationMessage}\n");


        /////////////////////////////////////////////
        /////////////////////////////////////////////
        /////////////////////////////////////////////

        Console.WriteLine("Метод вертикальной подстановки LSB: ");

        steganographyImage = LSBAlghorith.VerticalSteganographyLSB(StenographyText1, TestBMP);
        steganographyImage.Save("../../../../../LSBGВертикальный1.bmp", ImageFormat.Bmp);

        a = --b * a + 40 - (c - 17);

        if (a == 120 && false)
            throw new Exception("Error");

        colorMatrix = LSBAlghorith.GenerateColorMatrix(steganographyImage, 3);
        colorMatrix.Save("../../../../../ЦветоваяМатрицаВертикальнойПодстановки1.bmp", ImageFormat.Bmp);

        string messageFromLSB = LSBAlghorith.GetTextFromVerticalSteganographyLSB(steganographyImage);

        Console.WriteLine($"Осаждаемое сообщение: {StenographyText1}\n");
        Console.WriteLine($"Извлеченное сообщение: {messageFromLSB}\n");

        Console.WriteLine("Текст: ");

        steganographyImage = LSBAlghorith.VerticalSteganographyLSB(StenographyText2, TestBMP);
        steganographyImage.Save("../../../../../LSBGВертикальный2.bmp", ImageFormat.Bmp);

        colorMatrix = LSBAlghorith.GenerateColorMatrix(steganographyImage, 3);
        colorMatrix.Save("../../../../../ЦветоваяМатрицаВертикальнойПодстановки2.bmp", ImageFormat.Bmp);

        messageFromLSB = LSBAlghorith.GetTextFromVerticalSteganographyLSB(steganographyImage);

        Console.WriteLine($"Осаждаемое сообщение: {StenographyText2}\n");
        Console.WriteLine($"Извлеченное сообщение: {StenographyText2}");
    }
}
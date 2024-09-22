using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cripta13
{
    internal class LSBAlghorith
    {
        
        public static Bitmap PosledovatelnyiSteganographyLSB(Bitmap originalImage, byte[] message)
        {
            int height = originalImage.Height, width = originalImage.Width;
            int messageIndex = 0, bitIndex = 0;
            Bitmap NewImg = new Bitmap(originalImage);
            
            for (int y = 0; y < height; y++)
            {
               
                for (int x = 0; x < width; x++)
                {
                    
                    if (messageIndex < message.Length)
                    {
                        Color pixel = originalImage.GetPixel(x, y);
                        int red = pixel.R;
                        int green = pixel.G;
                        int blue = pixel.B;
                        byte currentByte = message[messageIndex];
                        int currentBit = (currentByte >> (7 - bitIndex)) & 0x01;
                        red = (red & 0xFE) | currentBit;
                        Color stegoPixel = Color.FromArgb(red, green, blue);
                        NewImg.SetPixel(x, y, stegoPixel);
                        bitIndex++;
                        if (bitIndex >= 8)
                        {
                            bitIndex = 0;
                            messageIndex++;
                        }
                    }
                }
            }

            return NewImg;
        }

        public static string GetTextFromPosledovatelnySteganographyLSB(Bitmap stegoImage, int messageLength)
        {
            int width = stegoImage.Width, height = stegoImage.Height;
            List<byte> messageBytes = new List<byte>();
            int bitIndex = 0;
            byte currentByte = 0;
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = stegoImage.GetPixel(x, y);
                    int redBit = pixel.R & 0x01;
                    int greenBit = pixel.G & 0x01;
                    int blueBit = pixel.B & 0x01;
                    currentByte = (byte)((currentByte << 1) | redBit);
                    bitIndex++;
                    if (bitIndex >= 8)
                    {
                        messageBytes.Add(currentByte);
                        currentByte = 0;
                        bitIndex = 0;
                    }
                    if (messageBytes.Count >= messageLength)
                        break;
                }
                if (messageBytes.Count >= messageLength)
                    break;
            }
            string message = Encoding.UTF8.GetString(messageBytes.ToArray());
            
            return message;
        }

        public static Bitmap VerticalSteganographyLSB(string text, Bitmap image)
        {
            int flag = 1, IndexOfSymbol = 0, SymbolNumb = 0, zeros = 0;
            long pixelIndex = 0;
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Color pixel = image.GetPixel(j, i);
                    int modifiedR = pixel.R - pixel.R % 2;
                    int modifiedG = pixel.G - pixel.G % 2;
                    int modifiedB = pixel.B - pixel.B % 2;
                    for (int k = 0; k < 3; k++)
                    {
                        if (pixelIndex % 8 == 0)
                        {
                            if (flag == 0 && zeros == 8)
                            {
                                if ((pixelIndex - 1) % 3 < 2)
                                    image.SetPixel(j, i, Color.FromArgb(modifiedR, modifiedG, modifiedB));
                                return image;
                            }
                            if (IndexOfSymbol >= text.Length)
                                flag = 0;
                            else
                                SymbolNumb = text[IndexOfSymbol++];
                        }
                        switch (pixelIndex % 3)
                        {
                            case 0:
                                {
                                    if (flag == 1)
                                    {
                                        modifiedR += SymbolNumb % 2;
                                        SymbolNumb /= 2;
                                    }
                                }
                                break;
                            case 1:
                                {
                                    if (flag == 1)
                                    {
                                        modifiedG += SymbolNumb % 2;
                                        SymbolNumb /= 2;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    if (flag == 1)
                                    {
                                        modifiedB += SymbolNumb % 2;
                                        SymbolNumb /= 2;
                                    }
                                    image.SetPixel(j, i, Color.FromArgb(modifiedR, modifiedG, modifiedB));
                                }
                                break;
                        }
                        pixelIndex++;
                        if (flag == 0)
                            zeros++;
                    }
                }
            }
            return image;
        }

        public static string GetTextFromVerticalSteganographyLSB(Bitmap image)
        {
            int pixelIndex = 0;
            int charValue = 0;
            StringBuilder extractedText = new StringBuilder();

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Color pixel = image.GetPixel(j, i);
                    for (int k = 0; k < 3; k++)
                    {
                        switch (pixelIndex % 3)
                        {
                            case 0:
                                charValue = charValue * 2 + pixel.R % 2; break;
                            case 1:
                                charValue = charValue * 2 + pixel.G % 2; break;
                            case 2:
                                charValue = charValue * 2 + pixel.B % 2; break;
                        }
                        pixelIndex++;
                        if (pixelIndex % 8 == 0)
                        {
                            charValue = SwapBits(charValue);
                            if (charValue == 0)
                                return extractedText.ToString();
                            char c = (char)charValue;
                            extractedText.Append(c);
                        }
                    }
                }
            }
            return extractedText.ToString();
        }

        public static Bitmap GenerateColorMatrix(Bitmap originalImage, int bitLevel)
        {
            int width = originalImage.Width;
            int height = originalImage.Height;
            Bitmap colorMatrix = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = originalImage.GetPixel(x, y);
                    int red = pixel.R;
                    int green = pixel.G;
                    int blue = pixel.B;
                    int bitValue = GetBit(red, green, blue, bitLevel);
                    Color matrixPixel = GetColorForBit(bitValue);
                    colorMatrix.SetPixel(x, y, matrixPixel);
                }
            }
            return colorMatrix;
        }

        public static int GetBit(int red, int green, int blue, int bitLevel)
        {
            int bitMask = 1 << bitLevel;
            int redBit = (red & bitMask) >> bitLevel;
            int greenBit = (green & bitMask) >> bitLevel;
            int blueBit = (blue & bitMask) >> bitLevel;
            return (redBit << 2) | (greenBit << 1) | blueBit;
        }

        public static Color GetColorForBit(int bitValue)
        {
            int red = (bitValue & 0x04) != 0 ? 255 : 0;
            int green = (bitValue & 0x02) != 0 ? 255 : 0;
            int blue = (bitValue & 0x01) != 0 ? 255 : 0;

            Color color = Color.FromArgb(red, green, blue);
            return color;
        }

        private static int SwapBits(int n)
        {
            n = (n & 0xF0) >> 4 | (n & 0x0F) << 4;
            n = (n & 0xCC) >> 2 | (n & 0x33) << 2;
            n = (n & 0xAA) >> 1 | (n & 0x55) << 1;
            return n;
        }

    }
}

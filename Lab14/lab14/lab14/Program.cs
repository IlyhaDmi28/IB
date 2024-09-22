using System;
using System.IO;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

namespace ApproachEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            string docxFilePath = @".\Document.docx";
            string docxFilePathEnc = @".\Ecrypted.docx";

            string messageToEncrypt = "Dmitruk Ilya Igorevich";

            EncryptMessageInDocx(docxFilePath, messageToEncrypt);
            Console.WriteLine("Сообщение успешно зашифровано в файл!");
            
            string decryptedMessage = DecryptMessageFromDocx(docxFilePathEnc);
            Console.WriteLine($"Извлеченное сообщение: {decryptedMessage}");
        }


        static void EncryptMessageInDocx(string docxFilePath, string message)
        {
            File.Copy(docxFilePath, "./Ecrypted.docx", true);

            using (WordprocessingDocument document = WordprocessingDocument.Open("./Ecrypted.docx", true))
            {
                Body body = document.MainDocumentPart.Document.Body;

                IEnumerable<Paragraph> paragraphs = body.Descendants<Paragraph>();

                EncryptMessage(message, paragraphs);

                document.MainDocumentPart.Document.Save();
            }
        }
        static void EncryptMessage(string message, IEnumerable<Paragraph> paragraphs)
        {
            int bitIndex = 0;
            
            List<char> bits = new List<char>();

            foreach (var item in message)
            {
                string rawBits = Convert.ToString(item, 2);

                if (rawBits.Length % 8 != 0)
                    for (int i = 0; i < rawBits.Length % 8; i++)
                        rawBits = rawBits.Insert(0, "0");

                bits.AddRange(rawBits);
            }

            foreach (Paragraph paragraph in paragraphs)
            {
                foreach (Run run in paragraph.Descendants<Run>())
                {
                    foreach (Text txt in run.Elements<Text>())   
                    {
                        foreach (char letter in txt.Text)
                        {

                            if (bitIndex < bits.Count)
                            {
                                RunProperties runProperties = new RunProperties();

                                Color colorZero = new Color() { Val = "010000" };
                                Color colorOne = new Color() { Val = "000100" };

                                if (bits[bitIndex] == '0')
                                    runProperties.Append(colorZero);
                                else
                                    runProperties.Append(colorOne);

                                runProperties.Append(new FontSize() { Val = run.RunProperties.FontSize.Val, });
                                runProperties.Append(new RunFonts { Ascii = "Times New Roman" });

                                Run newRun;

                                if (char.IsWhiteSpace(letter))
                                {
                                    Text newText = new Text(letter.ToString())
                                    {
                                        Space = SpaceProcessingModeValues.Preserve
                                    };

                                    newRun = new Run(runProperties, newText);
                                    run.InsertBeforeSelf(newRun);
                                    continue;
                                }
                                else newRun = new Run(runProperties, new Text(letter.ToString()));

                                run.InsertBeforeSelf(newRun);

                                bitIndex++;
                            }
                        }
                    }

                    if (bitIndex < bits.Count)
                        run.Remove();
                }
            }
        }

        static string DecryptMessageFromDocx(string docxFilePath)
        {
            using (WordprocessingDocument document = WordprocessingDocument.Open(docxFilePath, false))
            {
                Body body = document.MainDocumentPart.Document.Body;

                IEnumerable<Paragraph> paragraphs = body.Descendants<Paragraph>();

                string decryptedMessage = DecryptMessage(paragraphs);

                return decryptedMessage;
            }
        }
        static string DecryptMessage(IEnumerable<Paragraph> paragraphs)
        {
            string bits = string.Empty;
            string text = string.Empty;
            
            foreach (Paragraph paragraph in paragraphs)
            {
                foreach (Run run in paragraph.Descendants<Run>())
                {
                    if (run.RunProperties.Color == null)
                        continue;

                    if (run.InnerText == " ") continue;
                    else if (run.RunProperties.Color.Val == "010000")
                        bits += "0";
                    else if (run.RunProperties.Color.Val == "000100")
                        bits += "1";

                    if (bits.Length % 8 == 0)
                    {
                        text += (char)Convert.ToInt32(bits, 2);

                        bits = string.Empty;
                    }
                }
            }

            return text;
        }
    }
}
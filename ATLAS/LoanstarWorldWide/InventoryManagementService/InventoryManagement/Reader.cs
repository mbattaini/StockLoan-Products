using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace StockLoan.InventoryService
{
    public class Reader
    {
        public struct ReaderOutput
        {
            public ArrayList file;
            public bool endOfFile;
        }
        private static TextReader textReader;

        public Reader(string filePath)
        {
            textReader = new StreamReader(filePath);            
        }

        public ReaderOutput ReadLines(int numOfLines)
        {
            string line;

            ReaderOutput readerOutput;

            ArrayList file = new ArrayList();
            int lineCounter = 0;

            while (((line = textReader.ReadLine()) != "") && ((lineCounter < numOfLines)))
            {
                file.Add(line);
                lineCounter++;
            }

            if (line.Equals(""))
            {
                readerOutput.endOfFile = true;
            }
            else
            {
                readerOutput.endOfFile = false;
            }

            readerOutput.file = file;

            return readerOutput;
        }

        public static void Terminate()
        {
            textReader.Close();
        }
    }
}

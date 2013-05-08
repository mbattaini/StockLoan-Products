using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using StockLoan.Common;

namespace StockLoan.BDData
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

            try
            {
                while (((line = textReader.ReadLine()) != null) && ((lineCounter < numOfLines)))
                {
                    file.Add(line);
                    lineCounter++;
                }

                if (line == null)
                {
                    readerOutput.endOfFile = true;
                }
                else
                {
                    readerOutput.endOfFile = false;
                }
            } 
            catch (Exception error)
            {
                Log.Write(error.Message, 1);                
                throw;
            }         

            readerOutput.file = file;

            return readerOutput;
        }

        public void Terminate()
        {
            textReader.Close();
        }
    }
}

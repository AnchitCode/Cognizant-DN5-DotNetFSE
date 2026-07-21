using System;

namespace FactoryMethodPatternExample
{
    class Program
    {
        static void Main(string[] args)
        {
            DocumentFactory wordFactory = new WordDocumentFactory();
            IDocument wordDocument = wordFactory.CreateDocument();
            wordDocument.Open();

            DocumentFactory pdfFactory = new PdfDocumentFactory();
            IDocument pdfDocument = pdfFactory.CreateDocument();
            pdfDocument.Open();

            DocumentFactory excelFactory = new ExcelDocumentFactory();
            IDocument excelDocument = excelFactory.CreateDocument();
            excelDocument.Open();
        }
    }
}

using BillingAPI.Interfaces;
using BillingAPI.Models;
using BillingAPI.Models.DTO;
using BillingAPI.Models.POCO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ServiceStack.OrmLite;

namespace BillingAPI.Repositaries
{
    /// <summary>
    /// Implements IBIL01Service interface
    /// </summary>
    public class DbBIL01Context : IBIL01Service
    {
        #region Private Members

        /// <summary>
        /// Instance of OrmLiteConnectionFactory
        /// </summary>
        private readonly OrmLiteConnectionFactory dbFactory;

        /// <summary>
        /// Path of folder to store bills
        /// </summary>
        private string _path { get; set; } = Directory.GetCurrentDirectory() + "\\wwwroot\\Bills\\";

        #endregion

        #region Public Members 

        /// <summary>
        /// Incerement for count of products in bill
        /// </summary>
        public int idCount;

        #endregion

        #region Constructors

        /// <summary>
        /// Establishes database connections
        /// </summary>
        public DbBIL01Context()
        {
            dbFactory = new OrmLiteConnectionFactory(CRUDImplementation<BIL01>.connectionString, MySqlDialect.Provider);
            idCount = 0;
        }

        #endregion

        #region Public Methods 

        /// <summary>
        /// Calculates total amount of bill including taxes
        /// </summary>
        /// <param name="currentCMP01">Instance of type CMP01 which indicates current company</param>
        /// <param name="objBIL01">Instance of BIL01 class</param>
        /// <returns>Total amount of bill</returns>
        public double CalculateTotal(CMP01 currentCMP01, BIL01 objBIL01)
        {
            double total = 0;

            using (var db = dbFactory.OpenDbConnection())
            {
                CMP01 clientCMP01 = db.SingleById<CMP01>(objBIL01.L01F03);

                if (clientCMP01 != null)
                {
                    foreach (DTOPRO02 product in objBIL01.L01F05)
                    {
                        var price = db.SingleById<PRO01>(product.O02F01).O01F03;
                        total += (price * product.O02F03);
                    }
                    if (clientCMP01.P01F05 == currentCMP01.P01F05)
                    {
                        total += (total * 9) / 100;
                    }
                    else
                    {
                        total += (total * 18) / 100;
                    }
                }
            }

            return total;
        }

        /// <summary>
        /// Gets product list from BIL01 class
        /// </summary>
        /// <param name="objBIL01">Instance of BIL01 class</param>
        /// <returns>List of PRO02 objects present in bill</returns>
        public List<PRO02> GetProductsList(BIL01 objBIL01)
        {
            List<PRO02> lstPRO02 = new List<PRO02>();

            using (var db = dbFactory.OpenDbConnection())
            {
                foreach (DTOPRO02 objDTOPRO02 in objBIL01.L01F05)
                {
                    var product = db.SingleById<PRO01>(objDTOPRO02.O02F01);

                    double total = (product.O01F03 * objDTOPRO02.O02F03);

                    PRO02 objPRO02 = new PRO02 { O02F01 = ++idCount, O02F02 = product.O01F02, O02F03 = objDTOPRO02.O02F03, O02F04 = total };

                    lstPRO02.Add(objPRO02);
                }
            }
            return lstPRO02;
        }

        /// <summary>
        /// Generates bill's pdf
        /// </summary>
        /// <param name="currentCMP01">Instance of type CMP01 which indicates current company</param>
        /// <param name="id">Id of bill to be generate</param>
        /// <returns>Response contating result of generating bill pdf operation</returns>
        public Response FinalBill(CMP01 currentCMP01, int id)
        {
            Response response = new Response();

            int cgst = 9, sgst = 9;

            double total = 0;

            using (var db = dbFactory.OpenDbConnection())
            {
                BIL01 objBIL01 = db.SingleById<BIL01>(id);

                if (objBIL01 != null)
                {
                    CMP01 client = db.SingleById<CMP01>(objBIL01.L01F03);

                    USR01 user = db.SingleById<USR01>(objBIL01.L01F02);

                    List<PRO02> lstPRO02 = GetProductsList(objBIL01);

                    _path = _path + "Bill_" + objBIL01.L01F01 + "_" + DateTime.Now.ToShortDateString() + ".pdf";
                    
                    response.message = _path;

                    if (!File.Exists(_path))
                    {
                        // Calculate total
                        foreach (PRO02 product in lstPRO02)
                        {
                            total += product.O02F04;
                        }

                        // Determine CGST and SGST based on client's state
                        if (client.P01F05 == currentCMP01.P01F05)
                        {
                            cgst = 0;
                            sgst = 18;
                        }

                        // Create PDF document
                        using (FileStream fs = new FileStream(_path, FileMode.Create))
                        {
                            Document doc = new Document();

                            PdfWriter.GetInstance(doc, fs);

                            doc.Open();

                            // Add current company details in bold and centered
                            Paragraph companyDetails = new Paragraph(currentCMP01.P01F02 + "\n" + currentCMP01.P01F04 + "\n" + currentCMP01.P01F03 + "\n\n", new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD));
                            companyDetails.Alignment = Element.ALIGN_CENTER;
                            doc.Add(companyDetails);

                            // Add client and bill details
                            PdfPTable clientBillTable = new PdfPTable(2); // 2 columns
                            clientBillTable.WidthPercentage = 100;
                            float[] columnWidths = { 70f, 30f }; // Column widths
                            clientBillTable.SetWidths(columnWidths);

                            // Add client details
                            Paragraph clientDetails = new Paragraph();
                            clientDetails.Add(new Phrase("Client Name: ", new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));
                            clientDetails.Add(new Phrase(client.P01F02 + "\n", new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                            clientDetails.Add(new Phrase("Client Address: ", new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));
                            clientDetails.Add(new Phrase(client.P01F04 + "\n", new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                            clientDetails.Add(new Phrase("Client GST: ", new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));
                            clientDetails.Add(new Phrase(client.P01F03 + "\n", new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                            PdfPCell clientCell = new PdfPCell(clientDetails);
                            clientBillTable.AddCell(clientCell);

                            // Add bill details
                            Paragraph billDetails = new Paragraph();
                            billDetails.Add(new Phrase("Bill No.: ", new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));
                            billDetails.Add(new Phrase(objBIL01.L01F01 + "\n", new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                            billDetails.Add(new Phrase("Date: ", new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));
                            billDetails.Add(new Phrase(objBIL01.L01F06 + "\n", new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                            billDetails.Add(new Phrase("Issuer: ", new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));
                            billDetails.Add(new Phrase(user.R01F02 + "\n", new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                            billDetails.Add(new Phrase("Transport: ", new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));
                            billDetails.Add(new Phrase(objBIL01.L01F04 + "\n\n", new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                            clientBillTable.AddCell(billDetails);

                            doc.Add(clientBillTable);

                            // Add table headers
                            PdfPTable table = new PdfPTable(5); // 5 columns
                            table.WidthPercentage = 100;
                            float[] columnWidth = { 5f, 20f, 10f, 10f, 15f }; // Column widths
                            table.SetWidths(columnWidth);

                            string[] headers = { "Sr.", "Product", "Quantity", "Price", "Amount" };
                            foreach (string header in headers)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(header, new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                table.AddCell(cell);
                            }

                            // Add table data
                            foreach (PRO02 product in lstPRO02)
                            {
                                PdfPCell cell1 = new PdfPCell(new Phrase(product.O02F01.ToString(), new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                                cell1.Border = Rectangle.LEFT_BORDER;
                                table.AddCell(cell1);

                                PdfPCell cell2 = new PdfPCell(new Phrase(product.O02F02, new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                                cell2.Border = Rectangle.LEFT_BORDER;
                                table.AddCell(cell2);

                                PdfPCell cell3 = new PdfPCell(new Phrase(product.O02F03.ToString(), new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                                cell3.Border = Rectangle.LEFT_BORDER;
                                table.AddCell(cell3);

                                PdfPCell cell4 = new PdfPCell(new Phrase(product.O02F04.ToString(), new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                                cell4.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                                table.AddCell(cell4);

                                PdfPCell cell5 = new PdfPCell(new Phrase((product.O02F04 * product.O02F03).ToString(), new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                                cell5.Border = Rectangle.RIGHT_BORDER;
                                table.AddCell(cell5);
                            }

                            for (int i = 0; i < 5; i++)
                            {
                                PdfPCell cell1 = new PdfPCell(new Phrase("\n", new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                                cell1.Border = Rectangle.LEFT_BORDER;
                                table.AddCell(cell1);

                                PdfPCell cell2 = new PdfPCell(new Phrase("\n", new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                                cell2.Border = Rectangle.LEFT_BORDER;
                                table.AddCell(cell2);

                                PdfPCell cell3 = new PdfPCell(new Phrase("\n", new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                                cell3.Border = Rectangle.LEFT_BORDER;
                                table.AddCell(cell3);

                                PdfPCell cell4 = new PdfPCell(new Phrase("\n", new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                                cell4.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                                table.AddCell(cell4);

                                PdfPCell cell5 = new PdfPCell(new Phrase("\n", new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                                cell5.Border = Rectangle.RIGHT_BORDER;
                                table.AddCell(cell5);
                            }

                            // Add total and tax details
                            PdfPCell totalCell = new PdfPCell(new Phrase("Total ", new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));
                            totalCell.Colspan = 4;
                            totalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            table.AddCell(totalCell);

                            PdfPCell totalValueCell = new PdfPCell(new Phrase(total.ToString(), new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));
                            totalValueCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.AddCell(totalValueCell);

                            // Calculate the amount in words
                            string amountInWords = ConvertAmountToWords(CalculateTotal(currentCMP01, objBIL01));

                            // Create the PdfPCell for amount in words
                            Paragraph amountWordsParagraph = new Paragraph();
                            amountWordsParagraph.Add(new Phrase("Amount in words: ", new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));

                            amountWordsParagraph.Add(new Phrase(amountInWords, new Font(Font.FontFamily.TIMES_ROMAN, 12)));
                            PdfPCell amountWordsCell = new PdfPCell(amountWordsParagraph);
                            amountWordsCell.Colspan = 3; // Assuming you want it to span across 3 columns
                            amountWordsCell.Rowspan = 2; // Assuming you want it to span across 2 rows

                            // Add the amount in words cell to the table
                            table.AddCell(amountWordsCell);

                            PdfPCell sgstCell = new PdfPCell(new Phrase("SGST " + sgst + "% ", new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));
                            sgstCell.Colspan = 1;
                            sgstCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            table.AddCell(sgstCell);

                            PdfPCell sgstValueCell = new PdfPCell(new Phrase(((total * sgst) / 100).ToString(), new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));
                            sgstValueCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.AddCell(sgstValueCell);

                            PdfPCell cgstCell = new PdfPCell(new Phrase("CGST " + cgst + "% ", new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));
                            cgstCell.Colspan = 1;
                            cgstCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            table.AddCell(cgstCell);

                            PdfPCell cgstValueCell = new PdfPCell(new Phrase(((total * cgst) / 100).ToString(), new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD)));
                            cgstValueCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.AddCell(cgstValueCell);

                            PdfPCell grandTotalCell = new PdfPCell(new Phrase("Grand Total ", new Font(Font.FontFamily.TIMES_ROMAN, 13, Font.BOLD)));
                            grandTotalCell.Colspan = 4;
                            grandTotalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            table.AddCell(grandTotalCell);

                            PdfPCell grandTotalValueCell = new PdfPCell(new Phrase(CalculateTotal(currentCMP01, objBIL01).ToString(), new Font(Font.FontFamily.TIMES_ROMAN, 13, Font.BOLD)));
                            grandTotalValueCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.AddCell(grandTotalValueCell);
                            doc.Add(table);

                            // Add signature
                            Paragraph signature = new Paragraph(string.Format("Signature: {0}", currentCMP01.P01F02), new Font(Font.FontFamily.TIMES_ROMAN, 12));
                            signature.Alignment = Element.ALIGN_RIGHT;
                            doc.Add(signature);

                            // Close the document
                            doc.Close();
                        }
                    } 
                }
                else
                {
                    response.isError = true;
                    response.message = "Invalid bill ID";
                }
            }
            return response;
        }

        /// <summary>
        /// Generates amount in words
        /// </summary>
        /// <param name="amount">Amount of bill</param>
        /// <returns>String contating amount in words</returns>
        public string ConvertAmountToWords(double amount)
        {
            // Array of units, tens, and hundreds as words
            string[] ones = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };

            string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

            string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            string[] thousandsGroups = { "", " Thousand", " Million", " Billion" };

            // Convert the amount to words
            string words = "";

            if (amount == 0)
            {
                words = "Zero";
            }
            else
            {
                int thousandsCount = 0;

                // Split the amount into groups of thousands
                while (amount > 0)
                {
                    int thousands = (int)(amount % 1000);

                    if (thousands != 0)
                    {
                        string thousandsWords = "";

                        // Convert the current group of thousands to words
                        if (thousands % 100 < 10)
                        {
                            thousandsWords = ones[thousands % 100];
                        }
                        else if (thousands % 100 < 20)
                        {
                            thousandsWords = teens[thousands % 10];
                        }
                        else
                        {
                            thousandsWords = tens[thousands % 100 / 10] + " " + ones[thousands % 10];
                        }

                        // Add the word "Thousand" if necessary
                        if (thousands >= 100)
                        {
                            thousandsWords = ones[thousands / 100] + " Hundred " + thousandsWords;
                        }

                        if (thousandsCount > 0)
                        {
                            words = thousandsWords + thousandsGroups[thousandsCount] + " " + words;
                        }
                        else
                        {
                            words = thousandsWords + words;
                        }
                    }

                    thousandsCount++;

                    amount /= 1000;
                }
            }

            return words.Trim();
        }

        #endregion

    }
}

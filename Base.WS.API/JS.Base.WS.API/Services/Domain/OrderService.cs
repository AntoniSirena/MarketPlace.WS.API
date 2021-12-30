using iTextSharp.text;
using iTextSharp.text.pdf;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Services.IServices.IDomain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JS.Base.WS.API.Services.Domain
{
    public class OrderService : IOrderService
    {

        MyDBcontext db;


        public OrderService()
        {
            db = new MyDBcontext();
        }


        public string GeneratePDF(long orderId)
        {

            var currentOrder = db.PurchaseTransactions.Where(x => x.Id == orderId).FirstOrDefault();


            MemoryStream ms = new MemoryStream();

            Document document = new Document(iTextSharp.text.PageSize.LETTER, 20, 20, 20, 0);
            PdfWriter pdfW = PdfWriter.GetInstance(document, ms);

            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            var labelFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
            var labelFontValue = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var labelFontDetail = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8);
            var labelFontValueDetail = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var variableNameFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLDOBLIQUE, 12);
            var textFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            //Document open
            document.Open();


            Paragraph Title = new Paragraph("Detalle de la orden # " + orderId.ToString(), titleFont);
            Title.Alignment = Element.ALIGN_CENTER;
            document.Add(Chunk.NEWLINE);
            document.Add(Title);

            document.Add(Chunk.NEWLINE);


            //Header
            PdfPTable tbHeader = new PdfPTable(2);
            tbHeader.WidthPercentage = 100;
            float[] headerWidths = new float[] { 20f, 80f };
            tbHeader.SetWidths(headerWidths);


            PdfPCell clDescription = new PdfPCell(new Phrase("# Orden", labelFont));
            clDescription.HorizontalAlignment = Element.ALIGN_LEFT;
            clDescription.BorderWidth = 0;

            PdfPCell clValue = new PdfPCell(new Phrase(currentOrder.Id.ToString(), labelFontValue));
            clValue.HorizontalAlignment = Element.ALIGN_LEFT;
            clValue.BorderWidth = 0;

            tbHeader.AddCell(clDescription);
            tbHeader.AddCell(clValue);

            clDescription = new PdfPCell(new Phrase("Clave", labelFont));
            clDescription.HorizontalAlignment = Element.ALIGN_LEFT;
            clDescription.BorderWidth = 0;

            clValue = new PdfPCell(new Phrase(currentOrder.Key, labelFontValue));
            clValue.HorizontalAlignment = Element.ALIGN_LEFT;
            clValue.BorderWidth = 0;

            tbHeader.AddCell(clDescription);
            tbHeader.AddCell(clValue);


            clDescription = new PdfPCell(new Phrase("Fecha de creación", labelFont));
            clDescription.HorizontalAlignment = Element.ALIGN_LEFT;
            clDescription.BorderWidth = 0;

            clValue = new PdfPCell(new Phrase(currentOrder.FormattedDate, labelFontValue));
            clValue.HorizontalAlignment = Element.ALIGN_LEFT;
            clValue.BorderWidth = 0;

            tbHeader.AddCell(clDescription);
            tbHeader.AddCell(clValue);


            clDescription = new PdfPCell(new Phrase("Estado", labelFont));
            clDescription.HorizontalAlignment = Element.ALIGN_LEFT;
            clDescription.BorderWidth = 0;

            clValue = new PdfPCell(new Phrase(currentOrder.Status.ClientStatusDescription, labelFontValue));
            clValue.HorizontalAlignment = Element.ALIGN_LEFT;
            clValue.BorderWidth = 0;

            tbHeader.AddCell(clDescription);
            tbHeader.AddCell(clValue);

            clDescription = new PdfPCell(new Phrase("Método de pago", labelFont));
            clDescription.HorizontalAlignment = Element.ALIGN_LEFT;
            clDescription.BorderWidth = 0;

            clValue = new PdfPCell(new Phrase(currentOrder?.PaymentMethod?.Description, labelFontValue));
            clValue.HorizontalAlignment = Element.ALIGN_LEFT;
            clValue.BorderWidth = 0;

            tbHeader.AddCell(clDescription);
            tbHeader.AddCell(clValue);

            clDescription = new PdfPCell(new Phrase("Dirección", labelFont));
            clDescription.HorizontalAlignment = Element.ALIGN_LEFT;
            clDescription.BorderWidth = 0;

            clValue = new PdfPCell(new Phrase(currentOrder.Address, labelFontValue));
            clValue.HorizontalAlignment = Element.ALIGN_LEFT;
            clValue.BorderWidth = 0;

            tbHeader.AddCell(clDescription);
            tbHeader.AddCell(clValue);

            clDescription = new PdfPCell(new Phrase("Cliente", labelFont));
            clDescription.HorizontalAlignment = Element.ALIGN_LEFT;
            clDescription.BorderWidth = 0;

            clValue = new PdfPCell(new Phrase(currentOrder.Client.Name + " " + currentOrder.Client.Surname, labelFontValue));
            clValue.HorizontalAlignment = Element.ALIGN_LEFT;
            clValue.BorderWidth = 0;

            tbHeader.AddCell(clDescription);
            tbHeader.AddCell(clValue);

            clDescription = new PdfPCell(new Phrase("Teléfono", labelFont));
            clDescription.HorizontalAlignment = Element.ALIGN_LEFT;
            clDescription.BorderWidth = 0;

            clValue = new PdfPCell(new Phrase(currentOrder.Client.PhoneNumber.Insert(3, "-").Insert(7, "-"), labelFontValue));
            clValue.HorizontalAlignment = Element.ALIGN_LEFT;
            clValue.BorderWidth = 0;

            tbHeader.AddCell(clDescription);
            tbHeader.AddCell(clValue);

            clDescription = new PdfPCell(new Phrase("Comentario", labelFont));
            clDescription.HorizontalAlignment = Element.ALIGN_LEFT;
            clDescription.BorderWidth = 0;

            clValue = new PdfPCell(new Phrase(currentOrder.Comment, labelFontValue));
            clValue.HorizontalAlignment = Element.ALIGN_LEFT;
            clValue.BorderWidth = 0;

            tbHeader.AddCell(clDescription);
            tbHeader.AddCell(clValue);


            document.Add(tbHeader);
            document.Add(new Paragraph("\n"));


            //Details

            Paragraph titleDetail = new Paragraph("Detalles de artículos", labelFont);
            titleDetail.Alignment = Element.ALIGN_CENTER;
            document.Add(titleDetail);
            document.Add(new Paragraph("\n"));

            PdfPTable tbDetails = new PdfPTable(7);
            tbDetails.WidthPercentage = 100;
            float[] ddetailWidth = new float[] { 30f, 10f, 10f, 10f, 10f, 10f, 20f };
            tbDetails.SetWidths(ddetailWidth);


            PdfPCell clDescriptionDetail = new PdfPCell(new Phrase("Descripción", labelFontDetail));
            clDescriptionDetail.HorizontalAlignment = Element.ALIGN_LEFT;
            clDescriptionDetail.BorderWidth = 1;

            PdfPCell clQuantity = new PdfPCell(new Phrase("Cantidad", labelFontDetail));
            clQuantity.HorizontalAlignment = Element.ALIGN_LEFT;
            clQuantity.BorderWidth = 1;

            PdfPCell clPrice = new PdfPCell(new Phrase("Precio", labelFontDetail));
            clPrice.HorizontalAlignment = Element.ALIGN_LEFT;
            clPrice.BorderWidth = 1;

            PdfPCell clSultotal = new PdfPCell(new Phrase("Subtotal", labelFontDetail));
            clSultotal.HorizontalAlignment = Element.ALIGN_LEFT;
            clSultotal.BorderWidth = 1;

            PdfPCell clTax = new PdfPCell(new Phrase("ITBIS", labelFontDetail));
            clTax.HorizontalAlignment = Element.ALIGN_LEFT;
            clTax.BorderWidth = 1;

            PdfPCell clTotal = new PdfPCell(new Phrase("Total", labelFontDetail));
            clTotal.HorizontalAlignment = Element.ALIGN_LEFT;
            clTotal.BorderWidth = 1;

            PdfPCell clComment = new PdfPCell(new Phrase("Comentario", labelFontDetail));
            clComment.HorizontalAlignment = Element.ALIGN_LEFT;
            clComment.BorderWidth = 1;


            tbDetails.AddCell(clDescriptionDetail);
            tbDetails.AddCell(clQuantity);
            tbDetails.AddCell(clPrice);
            tbDetails.AddCell(clSultotal);
            tbDetails.AddCell(clTax);
            tbDetails.AddCell(clTotal);
            tbDetails.AddCell(clComment);


            foreach (var item in currentOrder.ArticlesDetails)
            {
                clDescriptionDetail = new PdfPCell(new Phrase(item.Article.Title, labelFontValueDetail));
                clDescriptionDetail.HorizontalAlignment = Element.ALIGN_LEFT;
                clDescriptionDetail.BorderWidth = 1;

                clQuantity = new PdfPCell(new Phrase(item.Quantity.ToString(), labelFontValueDetail));
                clQuantity.HorizontalAlignment = Element.ALIGN_LEFT;
                clQuantity.BorderWidth = 1;

                clPrice = new PdfPCell(new Phrase(item.Price.ToString(), labelFontValueDetail));
                clPrice.HorizontalAlignment = Element.ALIGN_LEFT;
                clPrice.BorderWidth = 1;

                clSultotal = new PdfPCell(new Phrase(item.Amount.ToString(), labelFontValueDetail));
                clSultotal.HorizontalAlignment = Element.ALIGN_LEFT;
                clSultotal.BorderWidth = 1;

                clTax = new PdfPCell(new Phrase(item.Tax.ToString(), labelFontValueDetail));
                clTax.HorizontalAlignment = Element.ALIGN_LEFT;
                clTax.BorderWidth = 1;

                clTotal = new PdfPCell(new Phrase(item.TotalAmount.ToString(), labelFontValueDetail));
                clTotal.HorizontalAlignment = Element.ALIGN_LEFT;
                clTotal.BorderWidth = 1;

                clComment = new PdfPCell(new Phrase(item.Comment, labelFontValueDetail));
                clComment.HorizontalAlignment = Element.ALIGN_LEFT;
                clComment.BorderWidth = 1;

                tbDetails.AddCell(clDescriptionDetail);
                tbDetails.AddCell(clQuantity);
                tbDetails.AddCell(clPrice);
                tbDetails.AddCell(clSultotal);
                tbDetails.AddCell(clTax);
                tbDetails.AddCell(clTotal);
                tbDetails.AddCell(clComment);
            }

            document.Add(tbDetails);
            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph("\n"));


            //Summary
            PdfPTable tbSummary = new PdfPTable(2);
            tbSummary.WidthPercentage = 30;
            float[] summaryWidths = new float[] { 20f, 10f };
            tbSummary.SetWidths(summaryWidths);
            tbSummary.HorizontalAlignment = Element.ALIGN_RIGHT;


            PdfPCell clSummaryDescription = new PdfPCell(new Phrase("Subtotal", labelFont));
            clSummaryDescription.HorizontalAlignment = Element.ALIGN_LEFT;
            clSummaryDescription.BorderWidth = 1;

            PdfPCell clSummaryValue = new PdfPCell(new Phrase(currentOrder.Amount.ToString(), labelFontValue));
            clSummaryValue.HorizontalAlignment = Element.ALIGN_LEFT;
            clSummaryValue.BorderWidth = 1;

            tbSummary.AddCell(clSummaryDescription);
            tbSummary.AddCell(clSummaryValue);


            clSummaryDescription = new PdfPCell(new Phrase("Descuento", labelFont));
            clSummaryDescription.HorizontalAlignment = Element.ALIGN_LEFT;
            clSummaryDescription.BorderWidth = 1;

            clSummaryValue = new PdfPCell(new Phrase(currentOrder.Discount.ToString(), labelFontValue));
            clSummaryValue.HorizontalAlignment = Element.ALIGN_LEFT;
            clSummaryValue.BorderWidth = 1;

            tbSummary.AddCell(clSummaryDescription);
            tbSummary.AddCell(clSummaryValue);


            clSummaryDescription = new PdfPCell(new Phrase("ITBIS", labelFont));
            clSummaryDescription.HorizontalAlignment = Element.ALIGN_LEFT;
            clSummaryDescription.BorderWidth = 1;

            clSummaryValue = new PdfPCell(new Phrase(currentOrder.Tax.ToString(), labelFontValue));
            clSummaryValue.HorizontalAlignment = Element.ALIGN_LEFT;
            clSummaryValue.BorderWidth = 1;

            tbSummary.AddCell(clSummaryDescription);
            tbSummary.AddCell(clSummaryValue);


            clSummaryDescription = new PdfPCell(new Phrase("Total", labelFont));
            clSummaryDescription.HorizontalAlignment = Element.ALIGN_LEFT;
            clSummaryDescription.BorderWidth = 1;

            clSummaryValue = new PdfPCell(new Phrase(currentOrder.TotalAmount.ToString(), labelFontValue));
            clSummaryValue.HorizontalAlignment = Element.ALIGN_LEFT;
            clSummaryValue.BorderWidth = 1;

            tbSummary.AddCell(clSummaryDescription);
            tbSummary.AddCell(clSummaryValue);


            document.Add(tbSummary);
            document.Add(new Paragraph("\n"));



            document.Close();
            //Document close

            byte[] bytesSt = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(bytesSt, 0, bytesSt.Length);
            ms.Position = 0;

            //Save document
            var fileName = string.Concat("Orden_", Guid.NewGuid());
            var filePath = Path.Combine(Global.Constants.ConfigurationParameter.PathReportOrder, fileName) + ".pdf";

            File.WriteAllBytes(filePath, bytesSt);

            return filePath;
        }


        public void SendOrderDetail(long orderId, string subject)
        {
            var currentOrder = db.PurchaseTransactions.Where(x => x.Id == orderId).FirstOrDefault();

            string documentPath = GeneratePDF(orderId);

            string template = AlertService.Alert.GetOperation("OrderDetail");


            var requestAlert = new AlertService.DTO.Request.Mail
            {
                MailAddresses = currentOrder.Client.EmailAddress,
                Subject = subject,
                Body = template,
                SendFileAttach = true,
                PathFileAttach = documentPath,
            };

            var responseAlert = AlertService.Alert.SendMail(requestAlert);
        }

    }

}
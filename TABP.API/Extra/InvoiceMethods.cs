using System;
using System.Collections.Generic;
using System.Text;
using TABP.Application.DTOs.BookingDtos;
using Infrastructure.EmailServices;

namespace TAABP.API.Extra
{
    public static class InvoiceMethods
    {
        public static string GenerateInvoiceForUser(InvoiceDto invoice, string userName)
        {
            var sb = new StringBuilder();

            sb.Append(@"<!DOCTYPE html>
                        <html lang=""en"">
                        <head>
                            <meta charset=""UTF-8"">
                            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                            <title>Hotel Invoice</title>
                            <style>
                                body {
                                    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                                    margin: 0;
                                    padding: 20px;
                                    background-color: #f9f9f9;
                                    color: #333;
                                }
                                .invoice-container {
                                    max-width: 800px;
                                    margin: 0 auto;
                                    background: #fff;
                                    padding: 30px;
                                    border-radius: 10px;
                                    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
                                }
                                .invoice-header {
                                    text-align: center;
                                    margin-bottom: 30px;
                                }
                                .invoice-header h1 {
                                    font-size: 36px;
                                    color: #2c3e50;
                                    margin: 0;
                                }
                                .invoice-details {
                                    margin-bottom: 30px;
                                }
                                .invoice-details p {
                                    margin: 5px 0;
                                    font-size: 16px;
                                    color: #555;
                                }
                                .invoice-details strong {
                                    color: #2c3e50;
                                }
                                .invoice-table {
                                    width: 100%;
                                    border-collapse: collapse;
                                    margin-bottom: 30px;
                                }
                                .invoice-table th, .invoice-table td {
                                    padding: 12px;
                                    text-align: left;
                                    border-bottom: 1px solid #ddd;
                                }
                                .invoice-table th {
                                    background-color: #2c3e50;
                                    color: #fff;
                                    font-weight: bold;
                                }
                                .invoice-table tbody tr:hover {
                                    background-color: #f1f1f1;
                                }
                                .invoice-table tfoot td {
                                    font-weight: bold;
                                    background-color: #f9f9f9;
                                    border-top: 2px solid #ddd;
                                }
                                .total {
                                    font-size: 18px;
                                    color: #2c3e50;
                                }
                                .footer {
                                    text-align: center;
                                    margin-top: 30px;
                                    font-size: 14px;
                                    color: #777;
                                }
                            </style>
                        </head>
                        <body>
                            <div class=""invoice-container"">
                                <div class=""invoice-header"">
                                    <h1>Hotel Invoice</h1>
                                </div>
                                <div class=""invoice-details"">
                                    <p>Booking Number: <strong>#")
                                    .Append(invoice.Id).Append(@"</strong></p>
                                    <p>Booking Date: <strong>")
                                    .Append(invoice.BookingDate.ToString("yyyy/MM/dd"))
                                    .Append(@"</strong></p>
                                    <p>Hotel Name: <strong>")
                                    .Append(invoice.HotelName).Append(@"</strong></p>
                                    <p>Guest Name: <strong>")
                                    .Append(userName).Append(@"</strong></p>
                                </div>
                                <table class=""invoice-table"">
                                    <thead>
                                        <tr>
                                            <th>Description</th>
                                            <th>Quantity</th>
                                            <th>Unit Price</th>
                                            <th>Total</th>
                                        </tr>
                                    </thead>
                                    <tbody>");

            sb.Append($@"<tr>
                            <td>Room Charge</td>
                            <td>1</td>
                            <td>${invoice.Price}</td>
                            <td>${invoice.Price}</td>
                         </tr>");

            sb.Append($@"</tbody>
                                    <tfoot>
                                        <tr>
                                            <td colspan=""3"" style=""text-align: right;"">Total:</td>
                                            <td class=""total"">${invoice.Price}</td>
                                        </tr>
                                    </tfoot>
                                </table>
                                <div class=""footer"">
                                    <p>Thank you for choosing {invoice.HotelName}!</p>
                                    <p>Best regards,<br>{invoice.OwnerName}</p>
                                </div>
                            </div>
                        </body>
                        </html>");

            return sb.ToString();
        }

        public static EmailMessageDetails CreateInvoiceEmailMessage(Guid bookingId, string email, string name, InvoiceDto invoice)
        {
            return new EmailMessageDetails(
                new List<string> { email },
                "Your Invoice is Ready! 🎉",
                $"Dear {name},\n\n" +
                $"We are pleased to provide you with the invoice for your recent booking (Booking ID: {bookingId}). " +
                $"You can find the details of your stay at {invoice.HotelName} below.\n\n" +
                $"Thank you for choosing {invoice.HotelName} for your accommodation. We truly appreciate your trust in us and hope you had a wonderful experience.\n\n" +
                $"If you have any questions or need further assistance, please feel free to reach out to us. We're always here to help!\n\n" +
                $"Warm regards,\n" +
                $"{invoice.OwnerName}\n" +
                $"{invoice.HotelName}"
            );
        }
    }
}
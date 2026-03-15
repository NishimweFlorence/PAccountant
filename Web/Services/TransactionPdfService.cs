using Application.Interfaces;
using Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PAccountant.Services
{
    public class TransactionPdfService : ITransactionPdfService
    {
        public async Task<byte[]> GenerateTransactionPdfAsync(Transaction transaction, string signedBy)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontColor(Colors.Grey.Darken3));

                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("PAccountant").FontSize(24).SemiBold().FontColor(Colors.Blue.Medium);
                            col.Item().Text("Personal Financial Management").FontSize(10).Italic();
                        });

                        row.RelativeItem().AlignRight().Column(col =>
                        {
                            col.Item().Text("TRANSACTION RECEIPT").FontSize(18).SemiBold();
                            col.Item().Text($"ID: #{transaction.Id}").FontSize(12);
                            col.Item().Text($"Date: {transaction.TransactionDate:dd MMM yyyy}").FontSize(12);
                        });
                    });

                    page.Content().PaddingVertical(1, Unit.Centimetre).Column(col =>
                    {
                        col.Item().PaddingBottom(5).Text("Transaction Summary").FontSize(14).SemiBold();
                        col.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingBottom(10).Text(transaction.Description ?? "No description provided");

                        col.Item().PaddingTop(15).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(150);
                                columns.RelativeColumn();
                                columns.ConstantColumn(80);
                                columns.ConstantColumn(80);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Account");
                                header.Cell().Element(CellStyle).Text("Category");
                                header.Cell().Element(CellStyle).AlignRight().Text("Debit");
                                header.Cell().Element(CellStyle).AlignRight().Text("Credit");

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container.DefaultTextStyle(x => x.SemiBold())
                                                    .PaddingVertical(5)
                                                    .BorderBottom(1)
                                                    .BorderColor(Colors.Black);
                                }
                            });

                            foreach (var entry in transaction.Entries)
                            {
                                table.Cell().Element(CellStyle).Text(entry.Account?.Name ?? "-");
                                table.Cell().Element(CellStyle).Text(entry.Category?.Name ?? "-");
                                table.Cell().Element(CellStyle).AlignRight().Text(entry.Debit > 0 ? entry.Debit.ToString("N2") : "");
                                table.Cell().Element(CellStyle).AlignRight().Text(entry.Credit > 0 ? entry.Credit.ToString("N2") : "");

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container.BorderBottom(1)
                                                    .BorderColor(Colors.Grey.Lighten3)
                                                    .PaddingVertical(5);
                                }
                            }
                        });

                        var totalAmount = transaction.Entries.Sum(e => e.Debit);
                        col.Item().AlignRight().PaddingTop(10).Text(x =>
                        {
                            x.Span("Total Amount: ").SemiBold();
                            x.Span($"{totalAmount:N2} {transaction.Currency?.Code}").SemiBold().FontColor(Colors.Blue.Medium);
                        });

                        col.Item().PaddingTop(40).Row(row =>
                        {
                            row.RelativeItem().Column(innerCol =>
                            {
                                innerCol.Item().Text("Digital Certification").FontSize(12).SemiBold();
                                innerCol.Item().PaddingTop(5).Border(1).BorderColor(Colors.Blue.Medium).Padding(10).Column(stamp =>
                                {
                                    stamp.Item().Text("DIGITALLY SIGNED BY").FontSize(8).FontColor(Colors.Blue.Medium);
                                    stamp.Item().Text(signedBy.ToUpper()).FontSize(12).SemiBold().FontColor(Colors.Blue.Darken2);
                                    stamp.Item().Text($"TIMESTAMP: {DateTime.Now:yyyy-MM-dd HH:mm:ss}").FontSize(7).FontColor(Colors.Grey.Medium);
                                    stamp.Item().Text("VERIFIED BY PACCOUNTANT ADMIN").FontSize(7).Italic().FontColor(Colors.Green.Medium);
                                });
                            });
                            row.RelativeItem();
                        });
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
                });
            });

            using var stream = new MemoryStream();
            document.GeneratePdf(stream);
            return stream.ToArray();
        }
    }
}

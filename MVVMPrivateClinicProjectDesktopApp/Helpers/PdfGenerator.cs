using System.Diagnostics;
using System.IO;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace MVVMPrivateClinicProjectDesktopApp.Helpers;

public static class PdfGenerator {

    public static void GenerateReferralPdf(ReferralDetailsDto referralDetailsDto){
        QuestPDF.Settings.License = LicenseType.Community;

        var filePath = $"Referral_{referralDetailsDto.Id}.pdf";

        Document.Create(container => {
            container.Page(page => {
                page.Margin(50);

                page.Header()
                    .Text($"Referral Nr: #{referralDetailsDto.Id}")
                    .FontSize(20)
                    .SemiBold()
                    .AlignCenter();

                page.Content()
                    .PaddingVertical(10)
                    .Column(column => {
                        column.Spacing(8);

                        column.Item().Text(text => {
                            text.Span("Referral Title: ").Bold().FontSize(12);
                            text.Span($"{referralDetailsDto.Name}").FontSize(12);
                        });

                        column.Item().Text(text => {
                            text.Span("Date Issued: ").Bold().FontSize(12);
                            text.Span($"{referralDetailsDto.DateIssued:yyyy-MM-dd, HH:mm}").FontSize(12);
                        });

                        column.Item().LineHorizontal(0.5f);

                        column.Item().Text(text => {
                            text.Span("Issued by Doctor: ").Bold().FontSize(12);
                            text.Span(
                                    $"{referralDetailsDto.DoctorDtoBase.FirstName} {referralDetailsDto.DoctorDtoBase.LastName}")
                                .FontSize(12);
                        });

                        column.Item().Text(text => {
                            text.Span("Doctor Specialization: ").Bold().FontSize(12);
                            text.Span($"{referralDetailsDto.DoctorDtoBase.DoctorSpecialization}").FontSize(12);
                        });

                        column.Item().LineHorizontal(0.5f);

                        column.Item().Text(text => {
                            text.Span("Patient: ").Bold().FontSize(12);
                            text.Span(
                                    $"{referralDetailsDto.PatientDetailsDto.FirstName} {referralDetailsDto.PatientDetailsDto.LastName}")
                                .FontSize(12);
                        });

                        column.Item().Text(text => {
                            text.Span("Patient Code: ").Bold().FontSize(12);
                            text.Span($"#{referralDetailsDto.PatientDetailsDto.PatientCode}").FontSize(12);
                        });

                        column.Item().LineHorizontal(0.5f);

                        column.Item().Text(text => {
                            text.Span("Disease: ").Bold().FontSize(12);
                            text.Span($"{referralDetailsDto.DiseaseDetailsDto.Name}").FontSize(12);
                        });

                        column.Item().Text(text => {
                            text.Span("Disease Code: ").Bold().FontSize(12);
                            text.Span($"#{referralDetailsDto.DiseaseDetailsDto.DiseaseCode}").FontSize(12);
                        });

                        column.Item().LineHorizontal(0.5f);

                        column.Item().Text(text => {
                            text.Span("Referral Test: ").Bold().FontSize(12);
                            text.Span($"{referralDetailsDto.ReferralTestDetailsDto.Name}").FontSize(12);
                        });

                        column.Item().Text("Referral Test Description:").Bold().FontSize(12);

                        column.Item().Border(1).Padding(5).Background("#F5F5F5")
                            .Text(referralDetailsDto.ReferralTestDetailsDto.Description)
                            .FontSize(12)
                            .AlignLeft();
                    });


                page.Footer()
                    .AlignCenter()
                    .Text(txt => {
                        txt.Span("Page ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);
                        txt.Span(" of ").FontSize(10);
                        txt.TotalPages().FontSize(10);
                    });
            });
        }).GeneratePdf(filePath);

        OpenPdf(filePath);
    }

    public static void GeneratePrescriptionPdf(PrescriptionDetailsDto prescriptionDetailsDto){
        QuestPDF.Settings.License = LicenseType.Community;
        var filePath = $"Prescription_{prescriptionDetailsDto.Id}.pdf";

        Document.Create(container => {
            container.Page(page => {
                page.Margin(50);

                page.Header()
                    .Text($"Prescription Nr: #{prescriptionDetailsDto.Id}")
                    .FontSize(20)
                    .SemiBold()
                    .AlignCenter();

                page.Content()
                    .PaddingVertical(10)
                    .Column(column => {
                        column.Spacing(8);

                        column.Item().Text("Prescription Description:").Bold().FontSize(12);

                        column.Item().Border(1).Padding(5).Background("#F5F5F5")
                            .Text(prescriptionDetailsDto.PrescriptionDescription)
                            .FontSize(12)
                            .AlignLeft();

                        column.Item().Text(text => {
                            text.Span("Prescription Code: ").Bold().FontSize(12);
                            text.Span($"{prescriptionDetailsDto.PrescriptionCode}").FontSize(12);
                        });

                        column.Item().Text(text => {
                            text.Span("Date Issued: ").Bold().FontSize(12);
                            text.Span($"{prescriptionDetailsDto.DateIssued:yyyy-MM-dd}").FontSize(12);
                        });

                        column.Item().Text(text => {
                            text.Span("Expiration Date: ").Bold().FontSize(12);
                            text.Span($"{prescriptionDetailsDto.ExpirationDate:yyyy-MM-dd}").FontSize(12);
                        });

                        column.Item().LineHorizontal(0.5f);

                        column.Item().Text(text => {
                            text.Span("Issued by Doctor: ").Bold().FontSize(12);
                            text.Span(
                                    $"{prescriptionDetailsDto.DoctorDtoBase.FirstName} {prescriptionDetailsDto.DoctorDtoBase.LastName}")
                                .FontSize(12);
                        });

                        column.Item().Text(text => {
                            text.Span("Doctor Specialization: ").Bold().FontSize(12);
                            text.Span($"{prescriptionDetailsDto.DoctorDtoBase.DoctorSpecialization}").FontSize(12);
                        });

                        column.Item().LineHorizontal(0.5f);

                        column.Item().Text(text => {
                            text.Span("Patient: ").Bold().FontSize(12);
                            text.Span(
                                    $"{prescriptionDetailsDto.PatientDetailsDto.FirstName} {prescriptionDetailsDto.PatientDetailsDto.LastName}")
                                .FontSize(12);
                        });

                        column.Item().Text(text => {
                            text.Span("Patient Code: ").Bold().FontSize(12);
                            text.Span($"#{prescriptionDetailsDto.PatientDetailsDto.PatientCode}").FontSize(12);
                        });

                        column.Item().LineHorizontal(1);

                        column.Item().PaddingTop(10).Text("Medicines List").SemiBold().FontSize(14).AlignCenter();

                        column.Item().Table(table => {
                            table.ColumnsDefinition(columns => {
                                columns.ConstantColumn(35);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(3);
                            });

                            table.Header(header => {
                                header.Cell().BorderBottom(1).BorderColor("#000000").PaddingVertical(4).Text("#").SemiBold().FontSize(12).AlignCenter();
                                header.Cell().BorderBottom(1).BorderColor("#000000").PaddingVertical(4).Text("Name").SemiBold().FontSize(12);
                                header.Cell().BorderBottom(1).BorderColor("#000000").PaddingVertical(4).Text("Description").SemiBold().FontSize(12);
                            });

                            if (prescriptionDetailsDto.MedicinesDto != null) {
                                var index = 1;
                                foreach (var medicine in prescriptionDetailsDto.MedicinesDto) {
                                    table.Cell()
                                        .BorderBottom(0.5f)
                                        .BorderColor("#CCCCCC")
                                        .PaddingVertical(5)
                                        .Text(index.ToString())
                                        .FontSize(12)
                                        .AlignCenter();

                                    table.Cell()
                                        .BorderBottom(0.5f)
                                        .BorderColor("#CCCCCC")
                                        .PaddingVertical(5)
                                        .Text(medicine.Name)
                                        .FontSize(12);

                                    table.Cell()
                                        .BorderBottom(0.5f)
                                        .BorderColor("#CCCCCC")
                                        .PaddingVertical(5)
                                        .Text(medicine.Description)
                                        .FontSize(12);

                                    index++;
                                }
                            }
                            else {
                                table.Cell().ColumnSpan(3)
                                    .PaddingVertical(5)
                                    .BorderBottom(1)
                                    .BorderColor("#CCCCCC")
                                    .Text("No medicines prescribed.")
                                    .FontSize(12)
                                    .AlignCenter();
                            }
                        });
                    });


                page.Footer()
                    .AlignCenter()
                    .Text(txt => {
                        txt.Span("Page ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);
                        txt.Span(" of ").FontSize(10);
                        txt.TotalPages().FontSize(10);
                    });
            });
        }).GeneratePdf(filePath);

        OpenPdf(filePath);
    }
    
    private static void OpenPdf(string filePath)
    {
        var fullPath = Path.GetFullPath(filePath);
        if (!File.Exists(fullPath)) return;
        
        var process = new Process
        {
            StartInfo = new ProcessStartInfo(fullPath)
            {
                UseShellExecute = true,
                Verb = "open"
            }
        };
        
        process.Start();
    }
}
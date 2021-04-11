using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Configuration
    {
        public int? Licence { get; set; }
        public string Domain { get; set; }
        public int? NetworkUserAutLogOffTime { get; set; }
        public string NetworkAdminUserName { get; set; }
        public string NetworkAdminUserPassword { get; set; }
        public int? MaximumPasswordLength { get; set; }
        public int? MinimumPasswordLength { get; set; }
        public int? PlctagLoggingCycleTime { get; set; }
        public int? ScreensaverInterval { get; set; }
        public string ScreenshotFolder { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
        public string GeareportLogoPath { get; set; }
        public string ClientReportLogoPath { get; set; }
        public string GealogoPath { get; set; }
        public string ClientLogoPath { get; set; }
        public string PdffolderPath { get; set; }
        public string CsvfolderPath { get; set; }
        public string ClientPressName { get; set; }
        public string NoCommunicationEventCategory { get; set; }
        public string ClientName { get; set; }
        public int? StartupDelay { get; set; }
        public int? WorkingHoursPerDay { get; set; }
        public string Csvseparator { get; set; }
        public int? OpcserverMaximumConnections { get; set; }
        public int? OpcserverMaximumSubscriptions { get; set; }
        public string PrimaryBackupPath { get; set; }
        public string SecondaryBackupPath { get; set; }
        public int? PressModelId { get; set; }
        public int? SoftwareRevisionId { get; set; }
        public string PressName { get; set; }
        public int? ScreensaverSlot1TagId { get; set; }
        public int? ScreensaverSlot2TagId { get; set; }
        public int? ScreensaverSlot3TagId { get; set; }
        public int? ScreensaverSlot4TagId { get; set; }
        public int? ScreensaverSlot5TagId { get; set; }
        public int? ScreensaverSlot6TagId { get; set; }
        public int? ScreensaverSlot7TagId { get; set; }
        public int? ScreensaverSlot8TagId { get; set; }
        public int? NoCommunicationEventId { get; set; }
        public int? PlcversClassificationTagId { get; set; }
        public int? PlcversionMajorTagId { get; set; }
        public int? PlcversionMinorTagId { get; set; }
        public int? PlcversionBuildTagId { get; set; }
        public int? LanguageId { get; set; }
        public int? LogLanguageId { get; set; }

        public int Id { get; set; }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ConfigTool.Models;

namespace ConfigTool.DataAccess
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ConfigTool.Models.Action> Action { get; set; }
        public virtual DbSet<BarGraph> BarGraph { get; set; }
        public virtual DbSet<Batch> Batch { get; set; }
        public virtual DbSet<BatchStatus> BatchStatus { get; set; }
        public virtual DbSet<BuckValveProcedure> BuckValveProcedure { get; set; }
        public virtual DbSet<CalibrationParameter> CalibrationParameter { get; set; }
        public virtual DbSet<Configuration> Configuration { get; set; }
        public virtual DbSet<Control> Control { get; set; }
        public virtual DbSet<CsvlogFile> CsvlogFile { get; set; }
        public virtual DbSet<DataBlock> DataBlock { get; set; }
        public virtual DbSet<Ecm> Ecm { get; set; }
        public virtual DbSet<Ecmparameter> Ecmparameter { get; set; }
        public virtual DbSet<EcmparameterValue> EcmparameterValue { get; set; }
        public virtual DbSet<Engineering> Engineering { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventCategory> EventCategory { get; set; }
        public virtual DbSet<Graph> Graph { get; set; }
        public virtual DbSet<GraphItem> GraphItem { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<LayerSide> LayerSide { get; set; }
        public virtual DbSet<MachineStatus> MachineStatus { get; set; }
        public virtual DbSet<Manual> Manual { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<OpcserverTag> OpcserverTag { get; set; }
        public virtual DbSet<PerformanceLog> PerformanceLog { get; set; }
        public virtual DbSet<Plcconfiguration> Plcconfiguration { get; set; }
        public virtual DbSet<PlcfixedTags> PlcfixedTags { get; set; }
        public virtual DbSet<Plcmapping> Plcmapping { get; set; }
        public virtual DbSet<PlcsamplingCycleTags> PlcsamplingCycleTags { get; set; }
        public virtual DbSet<PlcsamplingResultTags> PlcsamplingResultTags { get; set; }
        public virtual DbSet<Plctag> Plctag { get; set; }
        public virtual DbSet<PressModel> PressModel { get; set; }
        public virtual DbSet<PressParameter> PressParameter { get; set; }
        public virtual DbSet<PressParameterType> PressParameterType { get; set; }
        public virtual DbSet<Procedure> Procedure { get; set; }
        public virtual DbSet<ProcessValue> ProcessValue { get; set; }
        public virtual DbSet<ProcessValueCalculationType> ProcessValueCalculationType { get; set; }
        public virtual DbSet<ReadWriteType> ReadWriteType { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<RecipeParameter> RecipeParameter { get; set; }
        public virtual DbSet<RecipeParameterValue> RecipeParameterValue { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<ReportSubReport> ReportSubReport { get; set; }
        public virtual DbSet<SamplingCycleType> SamplingCycleType { get; set; }
        public virtual DbSet<SamplingElement> SamplingElement { get; set; }
        public virtual DbSet<SamplingOutput> SamplingOutput { get; set; }
        public virtual DbSet<ScreenParameter> ScreenParameter { get; set; }
        public virtual DbSet<SingleSampling> SingleSampling { get; set; }
        public virtual DbSet<SoftwareRevision> SoftwareRevision { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<SubReport> SubReport { get; set; }
        public virtual DbSet<Text> Text { get; set; }
        public virtual DbSet<TextLanguage> TextLanguage { get; set; }
        public virtual DbSet<Tooling> Tooling { get; set; }
        public virtual DbSet<ToolingParameter> ToolingParameter { get; set; }
        public virtual DbSet<ToolingParameterValue> ToolingParameterValue { get; set; }
        public virtual DbSet<TurretProcedure> TurretProcedure { get; set; }
        public virtual DbSet<TurretProcedureStep> TurretProcedureStep { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<UnitCategory> UnitCategory { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserAccessMatrix> UserAccessMatrix { get; set; }
        public virtual DbSet<ConfigTool.Models.ValueType> ValueType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseFirebird("Server=localhost;Database=C:\\Users\\gea.halle.cni\\Source\\Repos\\ConfigTool\\Config\\MC5_CONFIG.FDB;user id=SYSDBA;password=SYSDBA;character set=UTF8;DIALECT=3");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfigTool.Models.Action>(entity =>
            {
                entity.ToTable("Action                         ");

                entity.HasIndex(e => e.GroupTextId)
                    .HasName("FK_ACTION_GROUPTEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("PK_ACTION");

                entity.HasIndex(e => e.MessageId)
                    .HasName("FK_ACTION_MESSAGEID")
                    .IsUnique();

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_ACTION_TEXTID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GroupTextId).HasColumnName("GroupTextID");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(6);

                

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.Visible).HasMaxLength(250);
            });

            modelBuilder.Entity<BarGraph>(entity =>
            {
                entity.ToTable("BarGraph                       ");

                entity.HasIndex(e => e.ActualTagId)
                    .HasName("UQ_BARGRAPHNAME");

                entity.HasIndex(e => e.AverageTagId)
                    .HasName("FK_BARGRAPH_AVERAGETAGID")
                    .IsUnique();

                entity.HasIndex(e => e.CorrectionMinTagId)
                    .HasName("FK_BARGRAPH_CORRECTIONMINTAGID")
                    .IsUnique();

                entity.HasIndex(e => e.CorrectionPlusTagId)
                    .HasName("FK_BARGRAPH_CORRECTIONPLUSTAGID")
                    .IsUnique();

                entity.HasIndex(e => e.EnabledPunchesTagId)
                    .HasName("FK_BARGRAPH_ENABLEDPUNCHESTAGID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1201");

                entity.HasIndex(e => e.LowerDeviationTagId)
                    .HasName("FK_BARGRAPH_LOWERDEVIATIONTAGID")
                    .IsUnique();

                entity.HasIndex(e => e.MaxTagId)
                    .HasName("FK_BARGRAPH_MAXTAGID")
                    .IsUnique();

                entity.HasIndex(e => e.MinTagId)
                    .HasName("FK_BARGRAPH_MINTAGID")
                    .IsUnique();

                entity.HasIndex(e => e.NominalTagId)
                    .HasName("FK_BARGRAPH_NOMINALTAGID")
                    .IsUnique();

                entity.HasIndex(e => e.NumberOfPunchesTagId)
                    .HasName("FK_BARGRAPH_NUMBEROFPUNCHESTAGI")
                    .IsUnique();

                entity.HasIndex(e => e.RejectMinTagId)
                    .HasName("FK_BARGRAPH_REJECTMINTAGID")
                    .IsUnique();

                entity.HasIndex(e => e.RejectPlusTagId)
                    .HasName("FK_BARGRAPH_REJECTPLUSTAGID")
                    .IsUnique();

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_BARGRAPH_TEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.UpperDeviationTagId)
                    .HasName("FK_BARGRAPH_UPPERDEVIATIONTAGID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActualTagId).HasColumnName("ActualTagID");

                entity.Property(e => e.AverageTagId).HasColumnName("AverageTagID");

                entity.Property(e => e.CorrectionMinTagId).HasColumnName("CorrectionMinTagID");

                entity.Property(e => e.CorrectionPlusTagId).HasColumnName("CorrectionPlusTagID");

                entity.Property(e => e.EnabledPunchesTagId).HasColumnName("EnabledPunchesTagID");

                entity.Property(e => e.LowerDeviationTagId).HasColumnName("LowerDeviationTagID");

                entity.Property(e => e.MaxTagId).HasColumnName("MaxTagID");

                entity.Property(e => e.MinTagId).HasColumnName("MinTagID");

                entity.Property(e => e.NominalTagId).HasColumnName("NominalTagID");

                entity.Property(e => e.NumberOfPunchesTagId).HasColumnName("NumberOfPunchesTagID");

                entity.Property(e => e.RejectMinTagId).HasColumnName("RejectMinTagID");

                entity.Property(e => e.RejectPlusTagId).HasColumnName("RejectPlusTagID");

                

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.UpperDeviationTagId).HasColumnName("UpperDeviationTagID");

                entity.Property(e => e.Visible).HasMaxLength(250);
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.ToTable("Batch                          ");

                entity.HasIndex(e => e.BatchStatusId)
                    .HasName("FK_BATCH_BATCHSTATUSID")
                    .IsUnique();

                entity.HasIndex(e => e.Ecmid)
                    .HasName("FK_BATCH_ECMID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY937");

                entity.HasIndex(e => e.RecipeId)
                    .HasName("FK_BATCH_RECIPEID")
                    .IsUnique();

                entity.HasIndex(e => e.ToolingId)
                    .HasName("FK_BATCH_TOOLINGID")
                    .IsUnique();

                entity.HasIndex(e => new { e.LotNumber, e.Name })
                    .HasName("UQ_BATCH99");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BatchStatusId).HasColumnName("BatchStatusID");

                entity.Property(e => e.Comment).HasMaxLength(100);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreationDate).HasDefaultValueSql("DEFAULT 19000101000000000");

                entity.Property(e => e.Ecmid).HasColumnName("ECMID");

                entity.Property(e => e.EndDateTime).HasDefaultValueSql("DEFAULT 19000101000000000");



                entity.Property(e => e.LotNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

     

                entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

                

                entity.Property(e => e.StartDateTime).HasDefaultValueSql("DEFAULT 19000101000000000");

                entity.Property(e => e.ToolingId).HasColumnName("ToolingID");

                entity.Property(e => e.Weight).HasDefaultValueSql("DEFAULT '-9999.0'");
            });

            modelBuilder.Entity<BatchStatus>(entity =>
            {
                entity.ToTable("BatchStatus                    ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY938");

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_BATCHSTATUS_TEXTID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                

                entity.Property(e => e.TextId).HasColumnName("TextID");
            });

            modelBuilder.Entity<BuckValveProcedure>(entity =>
            {
                entity.ToTable("BuckValveProcedure             ");

                entity.HasIndex(e => e.GroupTextId)
                    .HasName("FK_BUCKVALVEPROCEDURE_GROUPTEXT")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY912");

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_BUCKVALVEPROCEDURE_TEXTID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Enable).HasMaxLength(250);

                entity.Property(e => e.GroupTextId).HasColumnName("GroupTextID");

                

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.Visible).HasMaxLength(250);
            });

            modelBuilder.Entity<CalibrationParameter>(entity =>
            {
                entity.ToTable("CalibrationParameter           ");

                entity.HasIndex(e => e.CalibrationBusyTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_CALIBRA")
                    .IsUnique();

                entity.HasIndex(e => e.Code)
                    .HasName("UQ_CALIBPCODE");

                entity.HasIndex(e => e.EcmshiftBiLayerTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_ECMSHIF")
                    .IsUnique();

                entity.HasIndex(e => e.EcmshiftSingleLayerTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_ECMS0")
                    .IsUnique();

                entity.HasIndex(e => e.GlobalShiftTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_GLOBALS")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY931");

                entity.HasIndex(e => e.MaxHmitagId)
                    .HasName("FK_CALIBRATIONPARAMETER_MAXHMIT")
                    .IsUnique();

                entity.HasIndex(e => e.MaxSetPointTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_MAXSETP")
                    .IsUnique();

                entity.HasIndex(e => e.MaxToolTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_MAXTOOL")
                    .IsUnique();

                entity.HasIndex(e => e.MinHmitagId)
                    .HasName("FK_CALIBRATIONPARAMETER_MINHMIT")
                    .IsUnique();

                entity.HasIndex(e => e.MinSetPointTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_MINSETP")
                    .IsUnique();

                entity.HasIndex(e => e.MinToolTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_MINTOOL")
                    .IsUnique();

                entity.HasIndex(e => e.NullingOffsetTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_NULLING")
                    .IsUnique();

                entity.HasIndex(e => e.NullingReferenceTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_NULL0")
                    .IsUnique();

                entity.HasIndex(e => e.NullingTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_NULL1")
                    .IsUnique();

                entity.HasIndex(e => e.PositioningTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_POSITIO")
                    .IsUnique();

                entity.HasIndex(e => e.PositioningValueTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_POSI0")
                    .IsUnique();

                entity.HasIndex(e => e.RejectEncoderTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_REJECTE")
                    .IsUnique();

                entity.HasIndex(e => e.RejectRequestTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_REJECTR")
                    .IsUnique();

                entity.HasIndex(e => e.RejectShift1TagId)
                    .HasName("FK_CALIBRATIONPARAMETER_REJECTS")
                    .IsUnique();

                entity.HasIndex(e => e.RejectShift2TagId)
                    .HasName("FK_CALIBRATIONPARAMETER_REJE0")
                    .IsUnique();

                entity.HasIndex(e => e.RejectTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_REJECTT")
                    .IsUnique();

                entity.HasIndex(e => e.RejectWindowTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_REJECTW")
                    .IsUnique();

                entity.HasIndex(e => e.SensorShiftZeroEncValueTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_SENSORS")
                    .IsUnique();

                entity.HasIndex(e => e.SensorShiftZeroMsrValueTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_SENS0")
                    .IsUnique();

                entity.HasIndex(e => e.SensorShiftZeroPunchValueTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_SENS1")
                    .IsUnique();

                entity.HasIndex(e => e.SensorShiftZeroShiftValueTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_SENS2")
                    .IsUnique();

                entity.HasIndex(e => e.SensorShiftZeroTakeValueTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_SENS3")
                    .IsUnique();

                entity.HasIndex(e => e.SensorShiftZeroValueTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_SENS4")
                    .IsUnique();

                entity.HasIndex(e => e.SensorShiftZeroZeroValueTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_SENS5")
                    .IsUnique();

                entity.HasIndex(e => e.ShiftCorrectionTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_SHIFTCO")
                    .IsUnique();

                entity.HasIndex(e => e.SingleValueTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_SINGLEV")
                    .IsUnique();

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_CALIBRATIONPARAMETER_TAGID")
                    .IsUnique();

                entity.HasIndex(e => e.TurretRemovalCorrectionTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_TURRETR")
                    .IsUnique();

                entity.HasIndex(e => e.ZeroCorrectionTagId)
                    .HasName("FK_CALIBRATIONPARAMETER_ZEROCOR")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CalibrationBusyTagId).HasColumnName("CalibrationBusyTagID");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Dependency).HasMaxLength(50);

                entity.Property(e => e.EcmshiftBiLayer).HasColumnName("ECMShiftBiLayer");

                entity.Property(e => e.EcmshiftBiLayerTagId).HasColumnName("ECMShiftBiLayerTagID");

                entity.Property(e => e.EcmshiftSingleLayer).HasColumnName("ECMShiftSingleLayer");

                entity.Property(e => e.EcmshiftSingleLayerTagId).HasColumnName("ECMShiftSingleLayerTagID");

                entity.Property(e => e.GlobalShiftTagId).HasColumnName("GlobalShiftTagID");

                entity.Property(e => e.HasNullingReferencePosition).HasMaxLength(250);

                entity.Property(e => e.HasPositioningValue).HasMaxLength(250);

                entity.Property(e => e.MaxHmitagId).HasColumnName("MaxHMITagID");

                entity.Property(e => e.MaxHmivalue).HasColumnName("MaxHMIValue");

                entity.Property(e => e.MaxSetPointTagId).HasColumnName("MaxSetPointTagID");

                entity.Property(e => e.MaxToolTagId).HasColumnName("MaxToolTagID");

                entity.Property(e => e.MinHmitagId).HasColumnName("MinHMITagID");

                entity.Property(e => e.MinHmivalue).HasColumnName("MinHMIValue");

                entity.Property(e => e.MinSetPointTagId).HasColumnName("MinSetPointTagID");

                entity.Property(e => e.MinToolTagId).HasColumnName("MinToolTagID");

                entity.Property(e => e.Nulling).HasMaxLength(250);

                entity.Property(e => e.NullingOffsetTagId).HasColumnName("NullingOffsetTagID");

                entity.Property(e => e.NullingReferenceTagId).HasColumnName("NullingReferenceTagID");

                entity.Property(e => e.NullingTagId).HasColumnName("NullingTagID");

                entity.Property(e => e.PositioningTagId).HasColumnName("PositioningTagID");

                entity.Property(e => e.PositioningValueTagId).HasColumnName("PositioningValueTagID");

                entity.Property(e => e.Reject).HasMaxLength(250);

                entity.Property(e => e.RejectEncoderTagId).HasColumnName("RejectEncoderTagID");

                entity.Property(e => e.RejectRequestTagId).HasColumnName("RejectRequestTagID");

                entity.Property(e => e.RejectShift1TagId).HasColumnName("RejectShift1TagID");

                entity.Property(e => e.RejectShift2TagId).HasColumnName("RejectShift2TagID");

                entity.Property(e => e.RejectTagId).HasColumnName("RejectTagID");

                entity.Property(e => e.RejectWindowTagId).HasColumnName("RejectWindowTagID");

                

                entity.Property(e => e.SensorShiftZero).HasMaxLength(250);

                entity.Property(e => e.SensorShiftZeroEncValueTagId).HasColumnName("SensorShiftZeroEncValueTagID");

                entity.Property(e => e.SensorShiftZeroMsrValueTagId).HasColumnName("SensorShiftZeroMsrValueTagID");

                entity.Property(e => e.SensorShiftZeroPunchValueTagId).HasColumnName("SensorShiftZeroPunchValueTagID");

                entity.Property(e => e.SensorShiftZeroShiftValueTagId).HasColumnName("SensorShiftZeroShiftValueTagID");

                entity.Property(e => e.SensorShiftZeroTakeValueTagId).HasColumnName("SensorShiftZeroTakeValueTagID");

                entity.Property(e => e.SensorShiftZeroValueTagId).HasColumnName("SensorShiftZeroValueTagID");

                entity.Property(e => e.SensorShiftZeroZeroValueTagId).HasColumnName("SensorShiftZeroZeroValueTagID");

                entity.Property(e => e.ShiftCorrectionTagId).HasColumnName("ShiftCorrectionTagID");

                entity.Property(e => e.SingleValueTagId).HasColumnName("SingleValueTagID");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.TurretRemovalCorrectionTagId).HasColumnName("TurretRemovalCorrectionTagID");

                entity.Property(e => e.Visible).HasMaxLength(250);

                entity.Property(e => e.ZeroCorrectionTagId).HasColumnName("ZeroCorrectionTagID");
            });

            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.ToTable("Configuration                  ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1203");

                entity.HasIndex(e => e.LanguageId)
                    .HasName("FK_CONFIGURATION_LANGUAGEID")
                    .IsUnique();

                entity.HasIndex(e => e.LogLanguageId)
                    .HasName("FK_CONFIGURATION_LOGLANGUAGEID")
                    .IsUnique();

                entity.HasIndex(e => e.NoCommunicationEventId)
                    .HasName("FK_CONFIGURATION_NOCOMMUNICATIO")
                    .IsUnique();

                entity.HasIndex(e => e.PlcversClassificationTagId)
                    .HasName("FK_CONFIGURATION_PLCVERSCLASSIF")
                    .IsUnique();

                entity.HasIndex(e => e.PlcversionBuildTagId)
                    .HasName("FK_CONFIGURATION_PLCVERSIONBUIL")
                    .IsUnique();

                entity.HasIndex(e => e.PlcversionMajorTagId)
                    .HasName("FK_CONFIGURATION_PLCVERSIONMAJO")
                    .IsUnique();

                entity.HasIndex(e => e.PlcversionMinorTagId)
                    .HasName("FK_CONFIGURATION_PLCVERSIONMINO")
                    .IsUnique();

                entity.HasIndex(e => e.PressModelId)
                    .HasName("FK_CONFIGURATION_PRESSMODELID")
                    .IsUnique();

                entity.HasIndex(e => e.ScreensaverSlot1TagId)
                    .HasName("FK_CONFIGURATION_SCREENSAVERSLO")
                    .IsUnique();

                entity.HasIndex(e => e.ScreensaverSlot2TagId)
                    .HasName("FK_CONFIGURATION_SCREENSAVER0")
                    .IsUnique();

                entity.HasIndex(e => e.ScreensaverSlot3TagId)
                    .HasName("FK_CONFIGURATION_SCREENSAVER1")
                    .IsUnique();

                entity.HasIndex(e => e.ScreensaverSlot4TagId)
                    .HasName("FK_CONFIGURATION_SCREENSAVER2")
                    .IsUnique();

                entity.HasIndex(e => e.ScreensaverSlot5TagId)
                    .HasName("FK_CONFIGURATION_SCREENSAVER3")
                    .IsUnique();

                entity.HasIndex(e => e.ScreensaverSlot6TagId)
                    .HasName("FK_CONFIGURATION_SCREENSAVER4")
                    .IsUnique();

                entity.HasIndex(e => e.ScreensaverSlot7TagId)
                    .HasName("FK_CONFIGURATION_SCREENSAVER5")
                    .IsUnique();

                entity.HasIndex(e => e.ScreensaverSlot8TagId)
                    .HasName("FK_CONFIGURATION_SCREENSAVER6")
                    .IsUnique();

                entity.HasIndex(e => e.SoftwareRevisionId)
                    .HasName("FK_CONFIGURATION_SOFTWAREREVISI")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientLogoPath).HasMaxLength(50);

                entity.Property(e => e.ClientName).HasMaxLength(50);

                entity.Property(e => e.ClientPressName).HasMaxLength(15);

                entity.Property(e => e.ClientReportLogoPath).HasMaxLength(50);

                entity.Property(e => e.CsvfolderPath)
                    .HasColumnName("CSVFolderPath")
                    .HasMaxLength(100);

                entity.Property(e => e.Csvseparator)
                    .HasColumnName("CSVSeparator")
                    .HasMaxLength(1);

                entity.Property(e => e.DateFormat).HasMaxLength(15);

                entity.Property(e => e.Domain).HasMaxLength(50);

                entity.Property(e => e.GealogoPath)
                    .HasColumnName("GEALogoPath")
                    .HasMaxLength(50);

                entity.Property(e => e.GeareportLogoPath)
                    .HasColumnName("GEAReportLogoPath")
                    .HasMaxLength(50);

                entity.Property(e => e.LanguageId).HasColumnName("LanguageID");

                entity.Property(e => e.LogLanguageId).HasColumnName("LogLanguageID");

                entity.Property(e => e.NetworkAdminUserName).HasMaxLength(50);

                entity.Property(e => e.NetworkAdminUserPassword).HasMaxLength(500);

                entity.Property(e => e.NoCommunicationEventCategory).HasMaxLength(25);

                entity.Property(e => e.NoCommunicationEventId).HasColumnName("NoCommunicationEventID");

                entity.Property(e => e.OpcserverMaximumConnections).HasColumnName("OPCServerMaximumConnections");

                entity.Property(e => e.OpcserverMaximumSubscriptions).HasColumnName("OPCServerMaximumSubscriptions");

                entity.Property(e => e.PdffolderPath)
                    .HasColumnName("PDFFolderPath")
                    .HasMaxLength(100);

                entity.Property(e => e.PlctagLoggingCycleTime).HasColumnName("PLCTagLoggingCycleTime");

                entity.Property(e => e.PlcversClassificationTagId).HasColumnName("PLCVersClassificationTagID");

                entity.Property(e => e.PlcversionBuildTagId).HasColumnName("PLCVersionBuildTagID");

                entity.Property(e => e.PlcversionMajorTagId).HasColumnName("PLCVersionMajorTagID");

                entity.Property(e => e.PlcversionMinorTagId).HasColumnName("PLCVersionMinorTagID");

                entity.Property(e => e.PressModelId).HasColumnName("PressModelID");

                entity.Property(e => e.PressName).HasMaxLength(50);

                entity.Property(e => e.PrimaryBackupPath).HasMaxLength(100);

                

                entity.Property(e => e.ScreensaverSlot1TagId).HasColumnName("ScreensaverSlot1TagID");

                entity.Property(e => e.ScreensaverSlot2TagId).HasColumnName("ScreensaverSlot2TagID");

                entity.Property(e => e.ScreensaverSlot3TagId).HasColumnName("ScreensaverSlot3TagID");

                entity.Property(e => e.ScreensaverSlot4TagId).HasColumnName("ScreensaverSlot4TagID");

                entity.Property(e => e.ScreensaverSlot5TagId).HasColumnName("ScreensaverSlot5TagID");

                entity.Property(e => e.ScreensaverSlot6TagId).HasColumnName("ScreensaverSlot6TagID");

                entity.Property(e => e.ScreensaverSlot7TagId).HasColumnName("ScreensaverSlot7TagID");

                entity.Property(e => e.ScreensaverSlot8TagId).HasColumnName("ScreensaverSlot8TagID");

                entity.Property(e => e.ScreenshotFolder).HasMaxLength(100);

                entity.Property(e => e.SecondaryBackupPath).HasMaxLength(100);

                entity.Property(e => e.SoftwareRevisionId).HasColumnName("SoftwareRevisionID");

                entity.Property(e => e.TimeFormat).HasMaxLength(15);
            });

            modelBuilder.Entity<Control>(entity =>
            {
                entity.ToTable("Control                        ");

                entity.HasIndex(e => e.ActionId)
                    .HasName("FK_CONTROL_ACTIONID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("PK_CONTROL");

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_CONTROL_TAGID")
                    .IsUnique();

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_CONTROL_TEXTID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionId).HasColumnName("ActionID");

                entity.Property(e => e.EnableCondition)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.IconPath).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.VisibleCondition).HasMaxLength(500);
            });

            modelBuilder.Entity<CsvlogFile>(entity =>
            {
                entity.ToTable("CSVLogFile                     ");

                entity.HasIndex(e => e.ColumnTextId)
                    .HasName("FK_CSVLOGFILE_TEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY940");

                entity.HasIndex(e => e.LogTriggerTagId)
                    .HasName("FK_CSVLOGFILE_LOGTRIGGERTAGID")
                    .IsUnique();

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_CSVLOGFILE_TAGID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ColumnTextId).HasColumnName("ColumnTextID");

                entity.Property(e => e.CsvfileName)
                    .HasColumnName("CSVFileName")
                    .HasMaxLength(100);

                entity.Property(e => e.Enable).HasMaxLength(250);

                entity.Property(e => e.LogTriggerTagId).HasColumnName("LogTriggerTagID");

                entity.Property(e => e.NewFile).HasMaxLength(250);

                

                entity.Property(e => e.TagId).HasColumnName("TagID");
            });

            modelBuilder.Entity<DataBlock>(entity =>
            {
                entity.ToTable("DataBlock                      ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY910");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_DATABLOCKNAME");

                entity.HasIndex(e => e.Number)
                    .HasName("UQ_DATABLOCKNUMBER");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(120);

                entity.Property(e => e.Number).HasMaxLength(120);

                
            });

            modelBuilder.Entity<Ecm>(entity =>
            {
                entity.ToTable("ECM                            ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1205");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment).HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.SerialNumber).HasMaxLength(20);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Version).HasMaxLength(50);
            });

            modelBuilder.Entity<Ecmparameter>(entity =>
            {
                entity.ToTable("ECMParameter                   ");

                entity.HasIndex(e => e.GroupTextId)
                    .HasName("FK_ECMPARAMETER_GROUPTEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1207");

                entity.HasIndex(e => e.ReadWriteTypeId)
                    .HasName("FK_ECMPARAMETER_READWRITETYPEID")
                    .IsUnique();

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_ECMPARAMETER_TAGID")
                    .IsUnique();

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_ECMPARAMETER_TEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.ValueTypeId)
                    .HasName("FK_ECMPARAMETER_VALUETYPEID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DefaultValue).HasMaxLength(250);

                entity.Property(e => e.Enable).HasMaxLength(250);

                entity.Property(e => e.GroupTextId).HasColumnName("GroupTextID");

                entity.Property(e => e.IconPath).HasMaxLength(100);

                entity.Property(e => e.Max).HasMaxLength(250);

                entity.Property(e => e.Min).HasMaxLength(250);

                entity.Property(e => e.ReadWriteTypeId).HasColumnName("ReadWriteTypeID");

                

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.ValueTypeId).HasColumnName("ValueTypeID");

                entity.Property(e => e.Visible).HasMaxLength(250);
            });

            modelBuilder.Entity<EcmparameterValue>(entity =>
            {
                entity.ToTable("ECMParameterValue              ");

                entity.HasIndex(e => e.Ecmid)
                    .HasName("FK_ECMPARAMETERVALUE_ECMID")
                    .IsUnique();

                entity.HasIndex(e => e.EcmparameterId)
                    .HasName("FK_ECMPARAMETERVALUE_ECMPARAMET")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("PK_ECMPARAMETERVALUE");

                entity.HasIndex(e => e.UnitId)
                    .HasName("FK_ECMPARAMETERVALUE_UNITID")
                    .IsUnique();

                entity.HasIndex(e => new { e.Ecmid, e.EcmparameterId })
                    .HasName("UQ_ECMPARAMETERVALUEECMP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ecmid).HasColumnName("ECMID");

                entity.Property(e => e.EcmparameterId).HasColumnName("ECMParameterID");

                entity.Property(e => e.Format).HasMaxLength(20);

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.Value).HasMaxLength(100);
            });

            modelBuilder.Entity<Engineering>(entity =>
            {
                entity.ToTable("Engineering                    ");

                entity.HasIndex(e => e.GroupTextId)
                    .HasName("FK_ENGINEERING_GROUPTEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1209");

                entity.HasIndex(e => e.ReadWriteTypeId)
                    .HasName("FK_ENGINEERING_READWRITETYPEID")
                    .IsUnique();

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_ENGINEERING_TAGID")
                    .IsUnique();

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_ENGINEERING_TEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.ValueTypeId)
                    .HasName("FK_ENGINEERING_VALUETYPEID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Enable).HasMaxLength(250);

                entity.Property(e => e.GroupTextId).HasColumnName("GroupTextID");

                entity.Property(e => e.IconPath).HasMaxLength(100);

                entity.Property(e => e.Max).HasMaxLength(250);

                entity.Property(e => e.Min).HasMaxLength(250);

                entity.Property(e => e.ReadWriteTypeId).HasColumnName("ReadWriteTypeID");

                

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.Value).HasMaxLength(250);

                entity.Property(e => e.ValueTypeId).HasColumnName("ValueTypeID");

                entity.Property(e => e.Visible).HasMaxLength(250);
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.ToTable("Equipment                      ");

                entity.HasIndex(e => e.GroupTextId)
                    .HasName("FK_EQUIPMENT_GROUPTEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1211");

                entity.HasIndex(e => e.ReadWriteTypeId)
                    .HasName("FK_EQUIPMENT_READWRITETYPEID")
                    .IsUnique();

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_EQUIPMENT_TAGID")
                    .IsUnique();

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_EQUIPMENT_TEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.ValueTypeId)
                    .HasName("FK_EQUIPMENT_VALUETYPEID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Enable).HasMaxLength(250);

                entity.Property(e => e.GroupTextId).HasColumnName("GroupTextID");

                entity.Property(e => e.IconPath).HasMaxLength(100);

                entity.Property(e => e.Max).HasMaxLength(250);

                entity.Property(e => e.Min).HasMaxLength(250);

                entity.Property(e => e.ReadWriteTypeId).HasColumnName("ReadWriteTypeID");

                

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.Value).HasMaxLength(250);

                entity.Property(e => e.ValueTypeId).HasColumnName("ValueTypeID");

                entity.Property(e => e.Visible).HasMaxLength(250);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event                          ");

                entity.HasIndex(e => e.EventCategoryId)
                    .HasName("FK_EVENT_EVENTCATEGORYID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY936");

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_EVENT_TAGID")
                    .IsUnique();

                entity.HasIndex(e => e.TagId1)
                    .HasName("FK_EVENT_TAGID1")
                    .IsUnique();

                entity.HasIndex(e => e.TagId10)
                    .HasName("FK_EVENT_TAGID10")
                    .IsUnique();

                entity.HasIndex(e => e.TagId2)
                    .HasName("FK_EVENT_TAGID2")
                    .IsUnique();

                entity.HasIndex(e => e.TagId3)
                    .HasName("FK_EVENT_TAGID3")
                    .IsUnique();

                entity.HasIndex(e => e.TagId4)
                    .HasName("FK_EVENT_TAGID4")
                    .IsUnique();

                entity.HasIndex(e => e.TagId5)
                    .HasName("FK_EVENT_TAGID5")
                    .IsUnique();

                entity.HasIndex(e => e.TagId6)
                    .HasName("FK_EVENT_TAGID6")
                    .IsUnique();

                entity.HasIndex(e => e.TagId7)
                    .HasName("FK_EVENT_TAGID7")
                    .IsUnique();

                entity.HasIndex(e => e.TagId8)
                    .HasName("FK_EVENT_TAGID8")
                    .IsUnique();

                entity.HasIndex(e => e.TagId9)
                    .HasName("FK_EVENT_TAGID9")
                    .IsUnique();

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_EVENT_TEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.TextId1)
                    .HasName("FK_EVENT_TEXTID1")
                    .IsUnique();

                entity.HasIndex(e => e.TextId10)
                    .HasName("FK_EVENT_TEXTID10")
                    .IsUnique();

                entity.HasIndex(e => e.TextId2)
                    .HasName("FK_EVENT_TEXTID2")
                    .IsUnique();

                entity.HasIndex(e => e.TextId3)
                    .HasName("FK_EVENT_TEXTID3")
                    .IsUnique();

                entity.HasIndex(e => e.TextId4)
                    .HasName("FK_EVENT_TEXTID4")
                    .IsUnique();

                entity.HasIndex(e => e.TextId5)
                    .HasName("FK_EVENT_TEXTID5")
                    .IsUnique();

                entity.HasIndex(e => e.TextId6)
                    .HasName("FK_EVENT_TEXTID6")
                    .IsUnique();

                entity.HasIndex(e => e.TextId7)
                    .HasName("FK_EVENT_TEXTID7")
                    .IsUnique();

                entity.HasIndex(e => e.TextId8)
                    .HasName("FK_EVENT_TEXTID8")
                    .IsUnique();

                entity.HasIndex(e => e.TextId9)
                    .HasName("FK_EVENT_TEXTID9")
                    .IsUnique();

                entity.HasIndex(e => new { e.EventCategoryId, e.Number })
                    .HasName("UQ_EVENTIDNAME");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EventCategoryId).HasColumnName("EventCategoryID");

                

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.TagId1).HasColumnName("TagID1");

                entity.Property(e => e.TagId10).HasColumnName("TagID10");

                entity.Property(e => e.TagId2).HasColumnName("TagID2");

                entity.Property(e => e.TagId3).HasColumnName("TagID3");

                entity.Property(e => e.TagId4).HasColumnName("TagID4");

                entity.Property(e => e.TagId5).HasColumnName("TagID5");

                entity.Property(e => e.TagId6).HasColumnName("TagID6");

                entity.Property(e => e.TagId7).HasColumnName("TagID7");

                entity.Property(e => e.TagId8).HasColumnName("TagID8");

                entity.Property(e => e.TagId9).HasColumnName("TagID9");

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.TextId1).HasColumnName("TextID1");

                entity.Property(e => e.TextId10).HasColumnName("TextID10");

                entity.Property(e => e.TextId2).HasColumnName("TextID2");

                entity.Property(e => e.TextId3).HasColumnName("TextID3");

                entity.Property(e => e.TextId4).HasColumnName("TextID4");

                entity.Property(e => e.TextId5).HasColumnName("TextID5");

                entity.Property(e => e.TextId6).HasColumnName("TextID6");

                entity.Property(e => e.TextId7).HasColumnName("TextID7");

                entity.Property(e => e.TextId8).HasColumnName("TextID8");

                entity.Property(e => e.TextId9).HasColumnName("TextID9");
            });

            modelBuilder.Entity<EventCategory>(entity =>
            {
                entity.ToTable("EventCategory                  ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY935");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_EVENTCATEGORYNAME");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);

                
            });

            modelBuilder.Entity<Graph>(entity =>
            {
                entity.ToTable("Graph                          ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1213");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BatchLot).HasMaxLength(100);

                entity.Property(e => e.BatchName).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                
            });

            modelBuilder.Entity<GraphItem>(entity =>
            {
                entity.ToTable("GraphItem                      ");

                entity.HasIndex(e => e.GraphId)
                    .HasName("FK_GRAPHITEM_GRAPHID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("PK_GRAPHITEM");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Axis)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GraphId).HasColumnName("GraphID");

                
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group                          ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1215");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_GROUPNAME");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);

                
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language                       ");

                entity.HasIndex(e => e.CultureCode)
                    .HasName("UQ_LANGUAGENAME");

                entity.HasIndex(e => e.Id)
                    .HasName("LANGUAGE_PK");

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_LANGUAGE_TEXTID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CultureCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CultureIdentifier).HasMaxLength(10);

                

                entity.Property(e => e.TextId).HasColumnName("TextID");
            });

            modelBuilder.Entity<LayerSide>(entity =>
            {
                entity.ToTable("LAYER_SIDE                     ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1217");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Desc)
                    .HasColumnName("DESC")
                    .HasMaxLength(200);

                
            });

            modelBuilder.Entity<MachineStatus>(entity =>
            {
                entity.ToTable("MachineStatus                  ");

                entity.HasIndex(e => e.ActiveTagId)
                    .HasName("FK_MACHINESTATUS_ACTIVETAGID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY911");

                entity.HasIndex(e => e.SetActiveTagId)
                    .HasName("FK_MACHINESTATUS_SETACTIVETAGID")
                    .IsUnique();

                entity.HasIndex(e => e.SetInActiveTagId)
                    .HasName("FK_MACHINESTATUS_SETINACTIVETAG")
                    .IsUnique();

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_MACHINESTATUS_TEXTID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActiveTagId).HasColumnName("ActiveTagID");

                entity.Property(e => e.Color).HasMaxLength(15);

                entity.Property(e => e.Enable).HasMaxLength(250);

                

                entity.Property(e => e.SetActiveTagId).HasColumnName("SetActiveTagID");

                entity.Property(e => e.SetInActiveTagId).HasColumnName("SetInActiveTagID");

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.Visible).HasMaxLength(250);
            });

            modelBuilder.Entity<Manual>(entity =>
            {
                entity.ToTable("Manual                         ");

                entity.HasIndex(e => e.GroupTextId)
                    .HasName("FK_MANUAL_GROUPTEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1219");

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_MANUAL_TEXTID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GroupTextId).HasColumnName("GroupTextID");

                entity.Property(e => e.Path).HasMaxLength(250);

                

                entity.Property(e => e.TextId).HasColumnName("TextID");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message                        ");

                entity.HasIndex(e => e.DisplayTextId)
                    .HasName("FK_MESSAGE_DISPLAYTEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1221");

                entity.HasIndex(e => e.LogCategoryTextId)
                    .HasName("FK_MESSAGE_LOGCATEGORYTEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.LogSubCategoryTextId)
                    .HasName("FK_MESSAGE_LOGSUBCATEGORYTEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.LogTextId)
                    .HasName("FK_MESSAGE_LOGTEXTID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category).HasMaxLength(100);

                entity.Property(e => e.DisplayTextId).HasColumnName("DisplayTextID");

                entity.Property(e => e.LogCategoryTextId).HasColumnName("LogCategoryTextID");

                entity.Property(e => e.LogSubCategoryTextId).HasColumnName("LogSubCategoryTextID");

                entity.Property(e => e.LogTextId).HasColumnName("LogTextID");

                
            });

            modelBuilder.Entity<OpcserverTag>(entity =>
            {
                entity.ToTable("OPCServerTag                   ");

                entity.HasIndex(e => e.Id)
                    .HasName("PK_OPCSERVERTAGID");

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_OPCSERVERTAG_TAGID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(100);

                

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.Visible).HasMaxLength(250);
            });

            modelBuilder.Entity<PerformanceLog>(entity =>
            {
                entity.ToTable("PerformanceLog                 ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1231");

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_PERFORMANCELOG_TAGID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.Visible).HasMaxLength(250);
            });

            modelBuilder.Entity<Plcconfiguration>(entity =>
            {
                entity.ToTable("PLCConfiguration               ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1223");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(20);

                entity.Property(e => e.Plctype)
                    .HasColumnName("PLCType")
                    .HasMaxLength(25);

                
            });

            modelBuilder.Entity<PlcfixedTags>(entity =>
            {
                entity.ToTable("PLCFixedTags                   ");

                entity.HasIndex(e => e.ActualSpeedTagId)
                    .HasName("FK_PLCFIXEDTAGS_ACTUALSPEEDTAGI")
                    .IsUnique();

                entity.HasIndex(e => e.AlarmActiveTagId)
                    .HasName("FK_PLCFIXEDTAGS_ALARMACTIVETAGI")
                    .IsUnique();

                entity.HasIndex(e => e.AutomaticProductionModeOnTagId)
                    .HasName("FK_PLCFIXEDTAGS_AUTOMATICPRODUC")
                    .IsUnique();

                entity.HasIndex(e => e.BiLayerTagId)
                    .HasName("FK_PLCFIXEDTAGS_BILAYERTAGID")
                    .IsUnique();

                entity.HasIndex(e => e.CriticalAlarmActiveTagId)
                    .HasName("FK_PLCFIXEDTAGS_CRITICALALARMAC")
                    .IsUnique();

                entity.HasIndex(e => e.CurrentDayTagId)
                    .HasName("FK_PLCFIXEDTAGS_CURRENTDAYTAGID")
                    .IsUnique();

                entity.HasIndex(e => e.CurrentHourTagId)
                    .HasName("FK_PLCFIXEDTAGS_CURRENTHOURTAGI")
                    .IsUnique();

                entity.HasIndex(e => e.CurrentMinuteTagId)
                    .HasName("FK_PLCFIXEDTAGS_CURRENTMINUTETA")
                    .IsUnique();

                entity.HasIndex(e => e.CurrentMonthTagId)
                    .HasName("FK_PLCFIXEDTAGS_CURRENTMONTHTAG")
                    .IsUnique();

                entity.HasIndex(e => e.CurrentSecondTagId)
                    .HasName("FK_PLCFIXEDTAGS_CURRENTSECONDTA")
                    .IsUnique();

                entity.HasIndex(e => e.CurrentWeekDayTagId)
                    .HasName("FK_PLCFIXEDTAGS_CURRENTWEEKDAYT")
                    .IsUnique();

                entity.HasIndex(e => e.CurrentYearTagId)
                    .HasName("FK_PLCFIXEDTAGS_CURRENTYEARTAGI")
                    .IsUnique();

                entity.HasIndex(e => e.EcmparametersSentTagId)
                    .HasName("FK_PLCFIXEDTAGS_ECMPARAMETERSSE")
                    .IsUnique();

                entity.HasIndex(e => e.EngineeringParametersSentTagId)
                    .HasName("FK_PLCFIXEDTAGS_ENGINEERINGPARA")
                    .IsUnique();

                entity.HasIndex(e => e.EquipmentParametersSentTagId)
                    .HasName("FK_PLCFIXEDTAGS_EQUIPMENTPARAME")
                    .IsUnique();

                entity.HasIndex(e => e.FirstCombiTesterInArrayTagId)
                    .HasName("FK_PLCFIXEDTAGS_FIRSTCOMBITESTE")
                    .IsUnique();

                entity.HasIndex(e => e.FirstCombiTesterOutArrayTagId)
                    .HasName("FK_PLCFIXEDTAGS_FIRSTCOMBITE0")
                    .IsUnique();

                entity.HasIndex(e => e.FirstLayerTagId)
                    .HasName("FK_PLCFIXEDTAGS_FIRSTLAYERTAGID")
                    .IsUnique();

                entity.HasIndex(e => e.HmitoPlclifeBeatTagId)
                    .HasName("FK_PLCFIXEDTAGS_HMITOPLCLIFEBEA")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1225");

                entity.HasIndex(e => e.NumberOfAcceptedTabletsTagId)
                    .HasName("FK_PLCFIXEDTAGS_NUMBEROFACCEPTE")
                    .IsUnique();

                entity.HasIndex(e => e.PlctoHmilifeBeatTagId)
                    .HasName("FK_PLCFIXEDTAGS_PLCTOHMILIFEBEA")
                    .IsUnique();

                entity.HasIndex(e => e.RecipeParametersSentTagId)
                    .HasName("FK_PLCFIXEDTAGS_RECIPEPARAMETER")
                    .IsUnique();

                entity.HasIndex(e => e.SecondCombiTesterInArrayTagId)
                    .HasName("FK_PLCFIXEDTAGS_SECONDCOMBITEST")
                    .IsUnique();

                entity.HasIndex(e => e.SecondCombiTesterOutArrayTagId)
                    .HasName("FK_PLCFIXEDTAGS_SECONDCOMBIT0")
                    .IsUnique();

                entity.HasIndex(e => e.SecondLayerTagId)
                    .HasName("FK_PLCFIXEDTAGS_SECONDLAYERTAGI")
                    .IsUnique();

                entity.HasIndex(e => e.ShutDownDelayTagId)
                    .HasName("FK_PLCFIXEDTAGS_SHUTDOWNDELAYTA")
                    .IsUnique();

                entity.HasIndex(e => e.ShutDownTagId)
                    .HasName("FK_PLCFIXEDTAGS_SHUTDOWNTAGID")
                    .IsUnique();

                entity.HasIndex(e => e.SingleLayerTagId)
                    .HasName("FK_PLCFIXEDTAGS_SINGLELAYERTAGI")
                    .IsUnique();

                entity.HasIndex(e => e.StartedBatchWeightTagId)
                    .HasName("FK_PLCFIXEDTAGS_STARTEDBATCHWEI")
                    .IsUnique();

                entity.HasIndex(e => e.TheoriticalSpeedTagId)
                    .HasName("FK_PLCFIXEDTAGS_THEORITICALSPEE")
                    .IsUnique();

                entity.HasIndex(e => e.ToolingParametersSentTagId)
                    .HasName("FK_PLCFIXEDTAGS_TOOLINGPARAMETE")
                    .IsUnique();

                entity.HasIndex(e => e.TotalNumberOfTabletsTagId)
                    .HasName("FK_PLCFIXEDTAGS_TOTALNUMBEROFTA")
                    .IsUnique();

                entity.HasIndex(e => e.WarningActiveTagId)
                    .HasName("FK_PLCFIXEDTAGS_WARNINGACTIVETA")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActualSpeedTagId).HasColumnName("ActualSpeedTagID");

                entity.Property(e => e.AlarmActiveTagId).HasColumnName("AlarmActiveTagID");

                entity.Property(e => e.AutomaticProductionModeOnTagId).HasColumnName("AutomaticProductionModeOnTagID");

                entity.Property(e => e.BiLayerTagId).HasColumnName("BiLayerTagID");

                entity.Property(e => e.CriticalAlarmActiveTagId).HasColumnName("CriticalAlarmActiveTagID");

                entity.Property(e => e.CurrentDayTagId).HasColumnName("CurrentDayTagID");

                entity.Property(e => e.CurrentHourTagId).HasColumnName("CurrentHourTagID");

                entity.Property(e => e.CurrentMinuteTagId).HasColumnName("CurrentMinuteTagID");

                entity.Property(e => e.CurrentMonthTagId).HasColumnName("CurrentMonthTagID");

                entity.Property(e => e.CurrentSecondTagId).HasColumnName("CurrentSecondTagID");

                entity.Property(e => e.CurrentWeekDayTagId).HasColumnName("CurrentWeekDayTagID");

                entity.Property(e => e.CurrentYearTagId).HasColumnName("CurrentYearTagID");

                entity.Property(e => e.EcmparametersSentTagId).HasColumnName("ECMParametersSentTagID");

                entity.Property(e => e.EngineeringParametersSentTagId).HasColumnName("EngineeringParametersSentTagID");

                entity.Property(e => e.EquipmentParametersSentTagId).HasColumnName("EquipmentParametersSentTagID");

                entity.Property(e => e.FirstCombiTesterComport)
                    .HasColumnName("FirstCombiTesterCOMPort")
                    .HasMaxLength(20);

                entity.Property(e => e.FirstCombiTesterInArrayTagId).HasColumnName("FirstCombiTesterInArrayTagID");

                entity.Property(e => e.FirstCombiTesterOutArrayTagId).HasColumnName("FirstCombiTesterOutArrayTagID");

                entity.Property(e => e.FirstLayerTagId).HasColumnName("FirstLayerTagID");

                entity.Property(e => e.HmitoPlclifeBeatTagId).HasColumnName("HMIToPLCLifeBeatTagID");

                entity.Property(e => e.NumberOfAcceptedTabletsTagId).HasColumnName("NumberOfAcceptedTabletsTagID");

                entity.Property(e => e.PlctoHmilifeBeatTagId).HasColumnName("PLCToHMILifeBeatTagID");

                entity.Property(e => e.RecipeParametersSentTagId).HasColumnName("RecipeParametersSentTagID");

                

                entity.Property(e => e.SecondCombiTesterComport)
                    .HasColumnName("SecondCombiTesterCOMPort")
                    .HasMaxLength(20);

                entity.Property(e => e.SecondCombiTesterInArrayTagId).HasColumnName("SecondCombiTesterInArrayTagID");

                entity.Property(e => e.SecondCombiTesterOutArrayTagId).HasColumnName("SecondCombiTesterOutArrayTagID");

                entity.Property(e => e.SecondLayerTagId).HasColumnName("SecondLayerTagID");

                entity.Property(e => e.ShutDownDelayTagId).HasColumnName("ShutDownDelayTagID");

                entity.Property(e => e.ShutDownTagId).HasColumnName("ShutDownTagID");

                entity.Property(e => e.SingleLayerTagId).HasColumnName("SingleLayerTagID");

                entity.Property(e => e.StartedBatchWeightTagId).HasColumnName("StartedBatchWeightTagID");

                entity.Property(e => e.TheoriticalSpeedTagId).HasColumnName("TheoriticalSpeedTagID");

                entity.Property(e => e.ToolingParametersSentTagId).HasColumnName("ToolingParametersSentTagID");

                entity.Property(e => e.TotalNumberOfTabletsTagId).HasColumnName("TotalNumberOfTabletsTagID");

                entity.Property(e => e.WarningActiveTagId).HasColumnName("WarningActiveTagID");
            });

            modelBuilder.Entity<Plcmapping>(entity =>
            {
                entity.ToTable("PLCMapping                     ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1227");

                entity.HasIndex(e => e.LayerSideId)
                    .HasName("FK_PLCMAPPING_LAYER_SIDEID")
                    .IsUnique();

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_PLCMAPPING_TAGID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ElDesingCode)
                    .HasColumnName("EL DESING CODE")
                    .HasMaxLength(50);

                entity.Property(e => e.LayerSideId).HasColumnName("Layer_SideID");

                entity.Property(e => e.Name).HasMaxLength(250);

                

                entity.Property(e => e.TagId).HasColumnName("TagID");
            });

            modelBuilder.Entity<PlcsamplingCycleTags>(entity =>
            {
                entity.ToTable("PLCSamplingCycleTags           ");

                entity.HasIndex(e => e.FeederAngleTagId)
                    .HasName("FK_PLCSAMPLINGCYCLETAGS_FEEDERA")
                    .IsUnique();

                entity.HasIndex(e => e.FeederSpeedTagId)
                    .HasName("FK_PLCSAMPLINGCYCLETAGS_FEEDERS")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY925");

                entity.HasIndex(e => e.SamplingCycleAbortedTagId)
                    .HasName("FK_PLCSAMPLINGCYCLETAGS_SAMP0")
                    .IsUnique();

                entity.HasIndex(e => e.SamplingOutputId)
                    .HasName("FK_PLCSAMPLINGCYCLETAGS_SAMP1")
                    .IsUnique();

                entity.HasIndex(e => e.SamplingResultNumberTagId)
                    .HasName("FK_PLCSAMPLINGCYCLETAGS_SAMPLIN")
                    .IsUnique();

                entity.HasIndex(e => e.StarWheelSpeedTagId)
                    .HasName("FK_PLCSAMPLINGCYCLETAGS_STARWHE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FeederAngleTagId).HasColumnName("FeederAngleTagID");

                entity.Property(e => e.FeederSpeedTagId).HasColumnName("FeederSpeedTagID");

                

                entity.Property(e => e.SamplingCycleAbortedTagId).HasColumnName("SamplingCycleAbortedTagID");

                entity.Property(e => e.SamplingOutputId).HasColumnName("SamplingOutputID");

                entity.Property(e => e.SamplingResultNumberTagId).HasColumnName("SamplingResultNumberTagID");

                entity.Property(e => e.StarWheelSpeedTagId).HasColumnName("StarWheelSpeedTagID");
            });

            modelBuilder.Entity<PlcsamplingResultTags>(entity =>
            {
                entity.ToTable("PLCSamplingResultTags          ");

                entity.HasIndex(e => e.AcceptedTabletsOutsideT1tagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_ACCEPT")
                    .IsUnique();

                entity.HasIndex(e => e.AmountOfResultsTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_AMOUNT")
                    .IsUnique();

                entity.HasIndex(e => e.AmountOfTabletsOutsideT1tagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_AMO3")
                    .IsUnique();

                entity.HasIndex(e => e.AmountOfTabletsOutsideT2tagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_AMO4")
                    .IsUnique();

                entity.HasIndex(e => e.AvailableTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_AVAILA")
                    .IsUnique();

                entity.HasIndex(e => e.AverageTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_AVERAG")
                    .IsUnique();

                entity.HasIndex(e => e.CorrectionLimitTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_COR2")
                    .IsUnique();

                entity.HasIndex(e => e.CorrectionTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_CORREC")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY926");

                entity.HasIndex(e => e.IsMeasuredTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_ISMEAS")
                    .IsUnique();

                entity.HasIndex(e => e.MaximumTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_MAXIMU")
                    .IsUnique();

                entity.HasIndex(e => e.MinimumTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_MINIMU")
                    .IsUnique();

                entity.HasIndex(e => e.NominalTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_NOMINA")
                    .IsUnique();

                entity.HasIndex(e => e.RequestedNumberOfResultsTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_REQUES")
                    .IsUnique();

                entity.HasIndex(e => e.ResultTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_RESULT")
                    .IsUnique();

                entity.HasIndex(e => e.SamplingElementId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_SAM4")
                    .IsUnique();

                entity.HasIndex(e => e.SamplingOutputId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_SAM3")
                    .IsUnique();

                entity.HasIndex(e => e.SamplingResultIdtagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_SAMPLI")
                    .IsUnique();

                entity.HasIndex(e => e.SigmaAbsoluteTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_SIGMAA")
                    .IsUnique();

                entity.HasIndex(e => e.SigmaAbsoluteToleranceTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_SIG5")
                    .IsUnique();

                entity.HasIndex(e => e.SigmaRelativeTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_SIGMAR")
                    .IsUnique();

                entity.HasIndex(e => e.SigmaRelativeToleranceTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_SIG4")
                    .IsUnique();

                entity.HasIndex(e => e.StarwheelSpeedTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_STARWH")
                    .IsUnique();

                entity.HasIndex(e => e.T1minTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_T1MINT")
                    .IsUnique();

                entity.HasIndex(e => e.T1plusTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_T1PLUS")
                    .IsUnique();

                entity.HasIndex(e => e.T2minTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_T2MINT")
                    .IsUnique();

                entity.HasIndex(e => e.T2plusTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_T2PLUS")
                    .IsUnique();

                entity.HasIndex(e => e.ToleranceMinMinTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_TOL6")
                    .IsUnique();

                entity.HasIndex(e => e.ToleranceMinTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_TOL5")
                    .IsUnique();

                entity.HasIndex(e => e.TolerancePlusPlusTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_TOLERA")
                    .IsUnique();

                entity.HasIndex(e => e.TolerancePlusTagId)
                    .HasName("FK_PLCSAMPLINGRESULTTAGS_TOL4")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AcceptedTabletsOutsideT1tagId).HasColumnName("AcceptedTabletsOutsideT1TagID");

                entity.Property(e => e.AmountOfResultsTagId).HasColumnName("AmountOfResultsTagID");

                entity.Property(e => e.AmountOfTabletsOutsideT1tagId).HasColumnName("AmountOfTabletsOutsideT1TagID");

                entity.Property(e => e.AmountOfTabletsOutsideT2tagId).HasColumnName("AmountOfTabletsOutsideT2TagID");

                entity.Property(e => e.AvailableTagId).HasColumnName("AvailableTagID");

                entity.Property(e => e.AverageTagId).HasColumnName("AverageTagID");

                entity.Property(e => e.CorrectionLimitTagId).HasColumnName("CorrectionLimitTagID");

                entity.Property(e => e.CorrectionTagId).HasColumnName("CorrectionTagID");

                entity.Property(e => e.IsMeasuredTagId).HasColumnName("IsMeasuredTagID");

                entity.Property(e => e.MaximumTagId).HasColumnName("MaximumTagID");

                entity.Property(e => e.MinimumTagId).HasColumnName("MinimumTagID");

                entity.Property(e => e.NominalTagId).HasColumnName("NominalTagID");

                

                entity.Property(e => e.RequestedNumberOfResultsTagId).HasColumnName("RequestedNumberOfResultsTagID");

                entity.Property(e => e.ResultTagId).HasColumnName("ResultTagID");

                entity.Property(e => e.SamplingElementId).HasColumnName("SamplingElementID");

                entity.Property(e => e.SamplingOutputId).HasColumnName("SamplingOutputID");

                entity.Property(e => e.SamplingResultIdtagId).HasColumnName("SamplingResultIDTagID");

                entity.Property(e => e.SigmaAbsoluteTagId).HasColumnName("SigmaAbsoluteTagID");

                entity.Property(e => e.SigmaAbsoluteToleranceTagId).HasColumnName("SigmaAbsoluteToleranceTagID");

                entity.Property(e => e.SigmaRelativeTagId).HasColumnName("SigmaRelativeTagID");

                entity.Property(e => e.SigmaRelativeToleranceTagId).HasColumnName("SigmaRelativeToleranceTagID");

                entity.Property(e => e.StarwheelSpeedTagId).HasColumnName("StarwheelSpeedTagID");

                entity.Property(e => e.T1minTagId).HasColumnName("T1MinTagID");

                entity.Property(e => e.T1plusTagId).HasColumnName("T1PlusTagID");

                entity.Property(e => e.T2minTagId).HasColumnName("T2MinTagID");

                entity.Property(e => e.T2plusTagId).HasColumnName("T2PlusTagID");

                entity.Property(e => e.ToleranceMinMinTagId).HasColumnName("ToleranceMinMinTagID");

                entity.Property(e => e.ToleranceMinTagId).HasColumnName("ToleranceMinTagID");

                entity.Property(e => e.TolerancePlusPlusTagId).HasColumnName("TolerancePlusPlusTagID");

                entity.Property(e => e.TolerancePlusTagId).HasColumnName("TolerancePlusTagID");
            });

            modelBuilder.Entity<Plctag>(entity =>
            {
                entity.ToTable("PLCTag                         ");

                entity.HasIndex(e => e.DataBlockId)
                    .HasName("FK_PLCTAG_DATABLOCKID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1229");

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_PLCTAG_TEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.UnitCategoryId)
                    .HasName("FK_PLCTAG_UNITCATEGORYID")
                    .IsUnique();

                entity.HasIndex(e => e.ValueTypeId)
                    .HasName("FK_PLCTAG_VALUETYPEID")
                    .IsUnique();

                entity.HasIndex(e => new { e.DataBlockId, e.Name })
                    .HasName("UQ_PLCTAGNAMEBDID");

                entity.HasIndex(e => new { e.DataBlockId, e.Number })
                    .HasName("UQ_PLCTAGDBIDNUMBER");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DataBlockId).HasColumnName("DataBlockID");

                entity.Property(e => e.DefaultValue).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(120);

                entity.Property(e => e.Number).HasMaxLength(120);

                

                entity.Property(e => e.Screensaver).HasMaxLength(250);

                entity.Property(e => e.Statistics).HasMaxLength(250);

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.UnitCategoryId).HasColumnName("UnitCategoryID");

                entity.Property(e => e.ValueTypeId).HasColumnName("ValueTypeID");
            });

            modelBuilder.Entity<PressModel>(entity =>
            {
                entity.ToTable("PressModel                     ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY920");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_PRESSMODELNAME");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(40);

                
            });

            modelBuilder.Entity<PressParameter>(entity =>
            {
                entity.ToTable("PressParameter                 ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1233");

                entity.HasIndex(e => e.LayerSideId)
                    .HasName("FK_PRESSPARAMETER_LAYER_SIDEID")
                    .IsUnique();

                entity.HasIndex(e => e.PressParameterTypeId)
                    .HasName("FK_PRESSPARAMETER_PRESSPARAMETE")
                    .IsUnique();

                entity.HasIndex(e => e.PlctagId)
                    .HasName("FK_PRESSPARAMETER_PLCTAGID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LayerSideId).HasColumnName("Layer_SideID");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.PressParameterTypeId).HasColumnName("PressParameterTypeID");

                

                entity.Property(e => e.PlctagId).HasColumnName("PlctagID");
            });

            modelBuilder.Entity<PressParameterType>(entity =>
            {
                entity.ToTable("PressParameterType             ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY919");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_PRESSPARAMETERTYPENAME");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);

                
            });

            modelBuilder.Entity<Procedure>(entity =>
            {
                entity.ToTable("Procedure                      ");

                entity.HasIndex(e => e.GroupTextId)
                    .HasName("FK_PROCEDURE_GROUPTEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1235");

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_PROCEDURE_TEXTID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GroupTextId).HasColumnName("GroupTextID");

                entity.Property(e => e.Path).HasMaxLength(250);

                

                entity.Property(e => e.TextId).HasColumnName("TextID");
            });

            modelBuilder.Entity<ProcessValue>(entity =>
            {
                entity.ToTable("ProcessValue                   ");

                entity.HasIndex(e => e.GroupTextId)
                    .HasName("FK_PROCESSVALUE_GROUPTEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1237");

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_PROCESSVALUE_TAGID")
                    .IsUnique();

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_PROCESSVALUE_TEXTID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GroupTextId).HasColumnName("GroupTextID");

                entity.Property(e => e.ProcessValueCalculationTypeId).HasColumnName("ProcessValueCalculationTypeID");

                

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.Visible).HasMaxLength(250);
            });

            modelBuilder.Entity<ProcessValueCalculationType>(entity =>
            {
                entity.ToTable("ProcessValueCalculationType    ");

                entity.HasIndex(e => e.Id)
                    .HasName("PROCESSVALUECALCULATIONTYPE_PK");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_PROCESSVCALCTYPENAME");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(30);

                
            });

            modelBuilder.Entity<ReadWriteType>(entity =>
            {
                entity.ToTable("ReadWriteType                  ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY933");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_READWRITETYPENAME");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(16);

                
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("Recipe                         ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1239");

                entity.HasIndex(e => e.StatusId)
                    .HasName("FK_RECIPE_STATUSID")
                    .IsUnique();

                entity.HasIndex(e => new { e.Name, e.StatusId, e.Version })
                    .HasName("UQ_RECIPE99");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment).HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Version).HasMaxLength(50);
            });

            modelBuilder.Entity<RecipeParameter>(entity =>
            {
                entity.ToTable("RecipeParameter                ");

                entity.HasIndex(e => e.GroupTextId)
                    .HasName("FK_RECIPEPARAMETER_GROUPTEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1241");

                entity.HasIndex(e => e.ReadWriteTypeId)
                    .HasName("FK_RECIPEPARAMETER_READWRITETYP")
                    .IsUnique();

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_RECIPEPARAMETER_TAGID")
                    .IsUnique();

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_RECIPEPARAMETER_TEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.ValueTypeId)
                    .HasName("FK_RECIPEPARAMETER_VALUETYPEID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DefaultValue).HasMaxLength(250);

                entity.Property(e => e.Enable).HasMaxLength(250);

                entity.Property(e => e.GroupTextId).HasColumnName("GroupTextID");

                entity.Property(e => e.IconPath).HasMaxLength(100);

                entity.Property(e => e.Max).HasMaxLength(250);

                entity.Property(e => e.Min).HasMaxLength(250);

                entity.Property(e => e.ReadWriteTypeId).HasColumnName("ReadWriteTypeID");

                

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.ValueTypeId).HasColumnName("ValueTypeID");

                entity.Property(e => e.Visible).HasMaxLength(250);
            });

            modelBuilder.Entity<RecipeParameterValue>(entity =>
            {
                entity.ToTable("RecipeParameterValue           ");

                entity.HasIndex(e => e.Id)
                    .HasName("PK_RECIPEPARAMETERVALUE");

                entity.HasIndex(e => e.RecipeId)
                    .HasName("FK_RECIPEPARAMETERVALUE_RECIPEI")
                    .IsUnique();

                entity.HasIndex(e => e.RecipeParameterId)
                    .HasName("FK_RECIPEPARAMETERVALUE_RECIPEP")
                    .IsUnique();

                entity.HasIndex(e => e.UnitId)
                    .HasName("FK_RECIPEPARAMETERVALUE_UNITID")
                    .IsUnique();

                entity.HasIndex(e => new { e.RecipeId, e.RecipeParameterId })
                    .HasName("UQ_Recipe_RecipeParameter");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Format).HasMaxLength(20);

                entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

                entity.Property(e => e.RecipeParameterId).HasColumnName("RecipeParameterID");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.Value).HasMaxLength(100);
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report                         ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY918");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                
            });

            modelBuilder.Entity<ReportSubReport>(entity =>
            {
                entity.ToTable("ReportSubReport                ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY917");

                entity.HasIndex(e => e.ReportId)
                    .HasName("FK_REPORTSUBREPORT_REPORTID")
                    .IsUnique();

                entity.HasIndex(e => e.SubReportId)
                    .HasName("FK_REPORTSUBREPORT_SUBREPORTID")
                    .IsUnique();

                entity.HasIndex(e => new { e.ReportId, e.SubReportId })
                    .HasName("UQ_REPORTSR");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.SubReportId).HasColumnName("SubReportID");
            });

            modelBuilder.Entity<SamplingCycleType>(entity =>
            {
                entity.ToTable("SamplingCycleType              ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY923");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_SAMPLINGCYCLETYPENAME");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(30);

                
            });

            modelBuilder.Entity<SamplingElement>(entity =>
            {
                entity.ToTable("SamplingElement                ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY922");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_SAMPLINGELEMENTNAME");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(30);

                
            });

            modelBuilder.Entity<SamplingOutput>(entity =>
            {
                entity.ToTable("SamplingOutput                 ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY924");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_SAMPLINGOUTPUTNAME");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(30);

                
            });

            modelBuilder.Entity<ScreenParameter>(entity =>
            {
                entity.ToTable("ScreenParameter                ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1243");

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_SCREENPARAMETER_TAGID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);

                

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.Value).HasMaxLength(10);
            });

            modelBuilder.Entity<SingleSampling>(entity =>
            {
                entity.ToTable("SingleSampling                 ");

                entity.HasIndex(e => e.GroupTextId)
                    .HasName("FK_SINGLESAMPLING_GROUPTEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1245");

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_SINGLESAMPLING_TEXTID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Enable).HasMaxLength(250);

                entity.Property(e => e.GroupTextId).HasColumnName("GroupTextID");

                

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.Visible).HasMaxLength(250);
            });

            modelBuilder.Entity<SoftwareRevision>(entity =>
            {
                entity.ToTable("SoftwareRevision               ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY932");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Hmiversion)
                    .HasColumnName("HMIVersion")
                    .HasMaxLength(30);

                entity.Property(e => e.Plcversion)
                    .HasColumnName("PLCVersion")
                    .HasMaxLength(30);

                
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status                         ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY939");

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_STATUS_TEXTID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                

                entity.Property(e => e.TextId).HasColumnName("TextID");
            });

            modelBuilder.Entity<SubReport>(entity =>
            {
                entity.ToTable("SubReport                      ");

                entity.HasIndex(e => e.GroupTextId)
                    .HasName("FK_SUBREPORT_GROUPTEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1247");

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_SUBREPORT_TEXTID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GroupTextId).HasColumnName("GroupTextID");

                

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.Visible).HasMaxLength(250);
            });

            modelBuilder.Entity<Text>(entity =>
            {
                entity.ToTable("Text                           ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY916");

                entity.Property(e => e.Id).HasColumnName("ID");

                
            });

            modelBuilder.Entity<TextLanguage>(entity =>
            {
                entity.ToTable("TextLanguage                   ");

                entity.HasIndex(e => e.Id)
                    .HasName("TEXTLANGUAGE_PK");

                entity.HasIndex(e => e.LanguageId)
                    .HasName("FK_TEXTLANGUAGE_LANGUAGEID")
                    .IsUnique();

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_TEXTLANGUAGE_TEXTID")
                    .IsUnique();

                entity.HasIndex(e => new { e.LanguageId, e.Text })
                    .HasName("UQ_TLLANGUAGE_TEXT");

                entity.HasIndex(e => new { e.LanguageId, e.TextId })
                    .HasName("UQ_TLLANGUAGE_TEXTID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LanguageId).HasColumnName("LanguageID");

                entity.Property(e => e.Text).HasMaxLength(250);

                entity.Property(e => e.TextId).HasColumnName("TextID");
            });

            modelBuilder.Entity<Tooling>(entity =>
            {
                entity.ToTable("Tooling                        ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1251");

                entity.HasIndex(e => e.StatusId)
                    .HasName("FK_TOOLING_STATUSID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment).HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.SerialNumber).HasMaxLength(20);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Version).HasMaxLength(50);
            });

            modelBuilder.Entity<ToolingParameter>(entity =>
            {
                entity.ToTable("ToolingParameter               ");

                entity.HasIndex(e => e.GroupTextId)
                    .HasName("FK_TOOLINGPARAMETER_GROUPTEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1253");

                entity.HasIndex(e => e.ReadWriteTypeId)
                    .HasName("FK_TOOLINGPARAMETER_READWRITETY")
                    .IsUnique();

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_TOOLINGPARAMETER_TAGID")
                    .IsUnique();

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_TOOLINGPARAMETER_TEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.ValueTypeId)
                    .HasName("FK_TOOLINGPARAMETER_VALUETYPEID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DefaultValue).HasMaxLength(250);

                entity.Property(e => e.Enable).HasMaxLength(250);

                entity.Property(e => e.GroupTextId).HasColumnName("GroupTextID");

                entity.Property(e => e.IconPath).HasMaxLength(100);

                entity.Property(e => e.Max).HasMaxLength(250);

                entity.Property(e => e.Min).HasMaxLength(250);

                entity.Property(e => e.ReadWriteTypeId).HasColumnName("ReadWriteTypeID");

                

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.ValueTypeId).HasColumnName("ValueTypeID");

                entity.Property(e => e.Visible).HasMaxLength(250);
            });

            modelBuilder.Entity<ToolingParameterValue>(entity =>
            {
                entity.ToTable("ToolingParameterValue          ");

                entity.HasIndex(e => e.Id)
                    .HasName("PK_TOOLINGPARAMETERVALUE");

                entity.HasIndex(e => e.ToolingId)
                    .HasName("FK_TOOLINGPARAMETERVALUE_TOOLIN")
                    .IsUnique();

                entity.HasIndex(e => e.ToolingParameterId)
                    .HasName("FK_TOOLINGPARAMETERVALUE_TOO0")
                    .IsUnique();

                entity.HasIndex(e => new { e.ToolingId, e.ToolingParameterId })
                    .HasName("UQ_TOOLINGPVTOO");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Format).HasMaxLength(20);

                entity.Property(e => e.ToolingId).HasColumnName("ToolingID");

                entity.Property(e => e.ToolingParameterId).HasColumnName("ToolingParameterID");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.Value).HasMaxLength(250);
            });

            modelBuilder.Entity<TurretProcedure>(entity =>
            {
                entity.ToTable("TurretProcedure                ");

                entity.HasIndex(e => e.GroupTextId)
                    .HasName("FK_TURRETPROCEDURE_GROUPTEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1255");

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_TURRETPROCEDURE_TEXTID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Enable).HasMaxLength(250);

                entity.Property(e => e.GroupTextId).HasColumnName("GroupTextID");

                

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.Visible).HasMaxLength(250);
            });

            modelBuilder.Entity<TurretProcedureStep>(entity =>
            {
                entity.ToTable("TurretProcedureStep            ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY928");

                entity.HasIndex(e => e.TurretProcedureId)
                    .HasName("FK_TURRETPROCEDURESTEP_TURRETPR")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(200);

                

                entity.Property(e => e.TurretProcedureId).HasColumnName("TurretProcedureID");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("Unit                           ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1257");

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_UNIT_TEXTID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                

                entity.Property(e => e.TextId).HasColumnName("TextID");
            });

            modelBuilder.Entity<UnitCategory>(entity =>
            {
                entity.ToTable("UnitCategory                   ");

                entity.HasIndex(e => e.ActiveUnitId)
                    .HasName("FK_UNITCATEGORY_ACTIVEUNITID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY929");

                entity.HasIndex(e => e.PrimaryUnitActiveTagId)
                    .HasName("FK_UNITCATEGORY_PRIMARYUNITACTI")
                    .IsUnique();

                entity.HasIndex(e => e.PrimaryUnitId)
                    .HasName("FK_UNITCATEGORY_PRIMARYUNITID")
                    .IsUnique();

                entity.HasIndex(e => e.SecondaryUnitId)
                    .HasName("FK_UNITCATEGORY_SECONDARYUNITID")
                    .IsUnique();

                entity.HasIndex(e => e.TextId)
                    .HasName("FK_UNITCATEGORY_TEXTID")
                    .IsUnique();

                entity.HasIndex(e => e.UnitConversionFactorTagId)
                    .HasName("FK_UNITCATEGORY_UNITCONVERSIONF")
                    .IsUnique();

                entity.HasIndex(e => new { e.Name, e.SubCategoryNumber })
                    .HasName("UQ_UNITCATEGORYNAMESC");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActiveUnitId).HasColumnName("ActiveUnitID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PrimaryFormat).HasMaxLength(20);

                entity.Property(e => e.PrimaryUnitActiveTagId).HasColumnName("PrimaryUnitActiveTagID");

                entity.Property(e => e.PrimaryUnitId).HasColumnName("PrimaryUnitID");

                

                entity.Property(e => e.SecondaryFormat).HasMaxLength(20);

                entity.Property(e => e.SecondaryUnitId).HasColumnName("SecondaryUnitID");

                entity.Property(e => e.TextId).HasColumnName("TextID");

                entity.Property(e => e.UnitConversionFactorTagId).HasColumnName("UnitConversionFactorTagID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User                           ");

                entity.HasIndex(e => e.GroupId)
                    .HasName("FK_USER_GROUPID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY1259");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_USERNAME");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comments).HasMaxLength(50);

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(150);

                entity.Property(e => e.PreviousPassword1).HasMaxLength(150);

                entity.Property(e => e.PreviousPassword2).HasMaxLength(150);

                entity.Property(e => e.PreviousPassword3).HasMaxLength(150);

                entity.Property(e => e.PreviousPassword4).HasMaxLength(150);

                entity.Property(e => e.PreviousPassword5).HasMaxLength(150);

                
            });

            modelBuilder.Entity<UserAccessMatrix>(entity =>
            {
                entity.ToTable("UserAccessMatrix               ");

                entity.HasIndex(e => e.ActionId)
                    .HasName("FK_USERACCESSMATRIX_ACTIONID")
                    .IsUnique();

                entity.HasIndex(e => e.FirstSignatureGroupId)
                    .HasName("FK_USERACCESSMATRIX_FIRSTSIGNAT")
                    .IsUnique();

                entity.HasIndex(e => e.GroupId)
                    .HasName("FK_USERACCESSMATRIX_GROUPID")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY930");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionId).HasColumnName("ActionID");

                entity.Property(e => e.FirstSignatureGroupId).HasColumnName("FirstSignatureGroupID");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                
            });

            modelBuilder.Entity<ConfigTool.Models.ValueType>(entity =>
            {
                entity.ToTable("ValueType                      ");

                entity.HasIndex(e => e.Id)
                    .HasName("RDB$PRIMARY934");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_VALUETYPENAME");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(16);

                
            });
        }
    }
}

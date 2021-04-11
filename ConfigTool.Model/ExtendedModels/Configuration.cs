using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Configuration
    {
        public Language Language { get; set; }
        public Language LogLanguage { get; set; }

        public bool PrintProcessValuesOnBatchStart { get; set; }

        public bool AutoBatchBackup { get; set; }
        public bool AutoBatchBackupZipped { get; set; }
        public bool PrintToPrinter { get; set; }
        public bool PrintToPDF { get; set; }
        public bool ManualSampleCSVLog { get; set; }
        public bool NetworkAuthentication { get; set; }
        public bool NetworkUserAutLogOffEnabled { get; set; }



        public bool OPCServer { get; set; }

        public Plctag PLCVersionMajorTag { get; set; }

        public Plctag PLCVersionMinorTag { get; set; }

        public Plctag PLCVersionBuildTag { get; set; }

        public Plctag ScreenSaverSlot1Tag { get; set; }
        public Plctag ScreenSaverSlot2Tag { get; set; }
        public Plctag ScreenSaverSlot3Tag { get; set; }
        public Plctag ScreenSaverSlot4Tag { get; set; }
        public Plctag ScreenSaverSlot5Tag { get; set; }
        public Plctag ScreenSaverSlot6Tag { get; set; }
        public Plctag ScreenSaverSlot7Tag { get; set; }
        public Plctag ScreenSaverSlot8Tag { get; set; }



        public Plctag PLCVersClassificationTag { get; set; }

 


    }
}

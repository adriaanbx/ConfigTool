using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class PlcfixedTags
    {
        public string FirstCombiTesterComport { get; set; }
        public string SecondCombiTesterComport { get; set; }
        public int? HmitoPlclifeBeatTagId { get; set; }
        public int? PlctoHmilifeBeatTagId { get; set; }
        public int? ShutDownTagId { get; set; }
        public int? ShutDownDelayTagId { get; set; }
        public int? EngineeringParametersSentTagId { get; set; }
        public int? EquipmentParametersSentTagId { get; set; }
        public int? RecipeParametersSentTagId { get; set; }
        public int? ToolingParametersSentTagId { get; set; }
        public int? EcmparametersSentTagId { get; set; }
        public int? SingleLayerTagId { get; set; }
        public int? BiLayerTagId { get; set; }
        public int? FirstLayerTagId { get; set; }
        public int? SecondLayerTagId { get; set; }
        public int? AutomaticProductionModeOnTagId { get; set; }
        public int? ActualSpeedTagId { get; set; }
        public int? TheoriticalSpeedTagId { get; set; }
        public int? NumberOfAcceptedTabletsTagId { get; set; }
        public int? TotalNumberOfTabletsTagId { get; set; }
        public int? CurrentYearTagId { get; set; }
        public int? CurrentMonthTagId { get; set; }
        public int? CurrentDayTagId { get; set; }
        public int? CurrentWeekDayTagId { get; set; }
        public int? CurrentHourTagId { get; set; }
        public int? CurrentMinuteTagId { get; set; }
        public int? CurrentSecondTagId { get; set; }
        public int? CriticalAlarmActiveTagId { get; set; }
        public int? AlarmActiveTagId { get; set; }
        public int? WarningActiveTagId { get; set; }
        public int? FirstCombiTesterInArrayTagId { get; set; }
        public int? FirstCombiTesterOutArrayTagId { get; set; }
        public int? SecondCombiTesterInArrayTagId { get; set; }
        public int? SecondCombiTesterOutArrayTagId { get; set; }
        public int? StartedBatchWeightTagId { get; set; }
        public int Id { get; set; }

    }
}

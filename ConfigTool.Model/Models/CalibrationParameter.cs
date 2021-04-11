using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class CalibrationParameter
    {
        public string Code { get; set; }
        public string Dependency { get; set; }
        public int Id { get; set; }
        public int? TextId { get; set; }

        public string Visible { get; set; }
        public int? CalibrationBusyTagId { get; set; }
        public int? EcmshiftBiLayerTagId { get; set; }
        public int? EcmshiftSingleLayerTagId { get; set; }
        public int? GlobalShiftTagId { get; set; }
        public int? MaxHmitagId { get; set; }
        public int? MaxSetPointTagId { get; set; }
        public int? MaxToolTagId { get; set; }
        public int? MinHmitagId { get; set; }
        public int? MinSetPointTagId { get; set; }
        public int? MinToolTagId { get; set; }
        public int? NullingOffsetTagId { get; set; }
        public int? NullingReferenceTagId { get; set; }
        public int? NullingTagId { get; set; }
        public int? PositioningTagId { get; set; }
        public int? PositioningValueTagId { get; set; }
        public int? RejectEncoderTagId { get; set; }
        public int? RejectRequestTagId { get; set; }
        public int? RejectShift1TagId { get; set; }
        public int? RejectShift2TagId { get; set; }
        public int? RejectTagId { get; set; }
        public int? RejectWindowTagId { get; set; }
        public int? SensorShiftZeroEncValueTagId { get; set; }
        public int? SensorShiftZeroMsrValueTagId { get; set; }
        public int? SensorShiftZeroPunchValueTagId { get; set; }
        public int? SensorShiftZeroShiftValueTagId { get; set; }
        public int? SensorShiftZeroTakeValueTagId { get; set; }
        public int? SensorShiftZeroValueTagId { get; set; }
        public int? SensorShiftZeroZeroValueTagId { get; set; }
        public int? ShiftCorrectionTagId { get; set; }
        public int? SingleValueTagId { get; set; }
        public int? TagId { get; set; }
        public int? TurretRemovalCorrectionTagId { get; set; }
        public int? ZeroCorrectionTagId { get; set; }
        public float? MaxHmivalue { get; set; }
        public float? MaxSetPointValue { get; set; }
        public float? MaxToolValue { get; set; }
        public float? MinHmivalue { get; set; }
        public float? MinSetPointValue { get; set; }
        public float? MinToolValue { get; set; }
        public float? NullingOffset { get; set; }
        public float? NullingReference { get; set; }
        public float? PositioningValue { get; set; }
        public float? ShiftCorrection { get; set; }
        public float? SingleValue { get; set; }
        public float? TurretRemovalCorrection { get; set; }
        public float? ZeroCorrection { get; set; }
        public float? EcmshiftBiLayer { get; set; }
        public float? EcmshiftSingleLayer { get; set; }
        public float? GlobalShift { get; set; }
        public string HasNullingReferencePosition { get; set; }
        public string HasPositioningValue { get; set; }
        public string Nulling { get; set; }
        public string Reject { get; set; }
        public string SensorShiftZero { get; set; }
        public int? SortPosition { get; set; }
    }
}

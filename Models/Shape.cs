using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeamAnalysisApp.Models;

public class BeamShape
{

    public ObjectId Id { get; set; }

    [BsonElement("Type")]
    public string? Type { get; set; }

    [BsonElement("EDI_Std_Nomenclature")]
    public string? EDIStdNomenclature { get; set; }

    [BsonElement("AISC_Manual_Label")]
    public string? AISCManualLabel { get; set; }

    [BsonElement("T_F")]
    public string? HasSpecialNote { get; set; }

    [BsonElement("W")]
    public double? Weight { get; set; }

    [BsonElement("A")]
    public double? Area { get; set; }

    [BsonElement("d")]
    public double? Depth { get; set; }

    [BsonElement("ddet")]
    public string? DepthDetail { get; set; }

    [BsonElement("Ht")]
    public double? HSSDepthTotal { get; set; }

    [BsonElement("h")]
    public double? DepthFlat { get; set; }

    [BsonElement("OD")]
    public double? PipeRoundOuterDiameter { get; set; }

    [BsonElement("bf")]
    public double? FlangeWidth { get; set; }

    [BsonElement("bfdet")]
    public string? FlangeWidthDetail { get; set; }

    [BsonElement("B")]
    public double? HSSDepthWidth { get; set; }

    [BsonElement("b")]
    public double? WidthFlatOrLeg { get; set; }

    [BsonElement("ID")]
    public double? PipeRoundInnerDiameter { get; set; }

    [BsonElement("tw")]
    public double? WebThickness { get; set; }

    [BsonElement("twdet")]
    public string? WebThicknessDetail { get; set; }

    [BsonElement("twdet/2")]
    public string? WebThicknessDetailHalf { get; set; }

    [BsonElement("tf")]
    public double? FlangeThickness { get; set; }

    [BsonElement("tfdet")]
    public string? FlangeThicknessDetail { get; set; }

    [BsonElement("t")]
    public double? AngleThickness { get; set; }

    [BsonElement("tnom")]
    public double? TubeThicknessNom { get; set; }

    [BsonElement("tdes")]
    public double? TubeThicknessDesign { get; set; }

    [BsonElement("kdes")]
    public double? KRegionDesign { get; set; }

    [BsonElement("kdet")]
    public string? KRegionDetail { get; set; }

    [BsonElement("k1")]
    public string? K1RegionDetail { get; set; }

    [BsonElement("x")]
    public double? HorizEdgeToCentroid { get; set; }

    [BsonElement("y")]
    public double? VertEdgeToCentroid { get; set; }

    [BsonElement("eo")]
    public double? HorizEdgeToShearCenter { get; set; }

    [BsonElement("xp")]
    public double? HorizEdgeToPNA { get; set; }

    [BsonElement("yp")]
    public double? VertEdgeToPNA { get; set; }

    [BsonElement("bf/2tf")]
    public double? WOrHPSlendernessRatio { get; set; }

    [BsonElement("b/t")]
    public double? LOrCSlendernessRatio { get; set; }

    [BsonElement("b/tdes")]
    public double? TubeSlendernessRatioDesign { get; set; }

    [BsonElement("h/tw")]
    public double? WebSlendernessRatio { get; set; }

    [BsonElement("h/tdes")]
    public double? TubeSlendernessRatio { get; set; }

    [BsonElement("D/t")]
    public double? PipeRoundSlendernessRatio { get; set; }

    [BsonElement("Ix")]
    public double? InertiaX { get; set; }

    [BsonElement("Zx")]
    public double? PlasticSectionModulusX { get; set; }

    [BsonElement("Sx")]
    public double? SectionModulusX { get; set; }

    [BsonElement("rx")]
    public double? RadiusOfGyrationX { get; set; }

    [BsonElement("Iy")]
    public double? InertiaY { get; set; }

    [BsonElement("Zy")]
    public double? PlasticSectionModulusY { get; set; }

    [BsonElement("Sy")]
    public double? SectionModulusY { get; set; }

    [BsonElement("ry")]
    public double? RadiusOfgyrationY { get; set; }

    [BsonElement("Iz")]
    public double? InertiaZ { get; set; }

    [BsonElement("rz")]
    public double? RadiusOfGyrationZ { get; set; }

    [BsonElement("Sz")]
    public double? SectionModulusZ { get; set; }

    [BsonElement("J")]
    public double? TorsionInertia { get; set; }

    [BsonElement("Cw")]
    public double? WarpingConstant { get; set; }

    [BsonElement("C")]
    public double? TubeTorsionInsertia { get; set; }

    [BsonElement("Wno")]
    public double? NormWarpFunc { get; set; }

    [BsonElement("Sw1")]
    public double? WarpStaticMoment1 { get; set; }

    [BsonElement("Sw2")]
    public double? WarpStaticMoment2 { get; set; }

    [BsonElement("Sw3")]
    public double? WarpStaticMoment3 { get; set; }

    [BsonElement("Qf")]
    public double? WebStaticMoment { get; set; }

    [BsonElement("Qw")]
    public double? MidDepthStaticMoment { get; set; }

    [BsonElement("ro")]
    public double? RadiusOfGyrationPolar { get; set; }

    [BsonElement("H")]
    public double? FlexureConstant { get; set; }

    [BsonElement("tan(α)")]
    public double? TangentAngle { get; set; }

    [BsonElement("Iw")]
    public double? InertiaAngleCrossAxis { get; set; }

    [BsonElement("zA")]
    public double? AngleCGZAxisToA { get; set; }

    [BsonElement("zB")]
    public double? AngleCGZAxisToB { get; set; }

    [BsonElement("zC")]
    public double? AngleCGZAxisToC { get; set; }

    [BsonElement("wA")]
    public double? AngleCGWAxisToA { get; set; }

    [BsonElement("wB")]
    public double? AngleCGWAxisToB { get; set; }

    [BsonElement("wC")]
    public double? AngleCGWAxisToC { get; set; }

    [BsonElement("SwA")]
    public double? AngleSectionModulusWAxisA { get; set; }

    [BsonElement("SwB")]
    public double? AngleSectionModulusWAxisB { get; set; }

    [BsonElement("SwC")]
    public double? AngleSectionModulusWAxisC { get; set; }

    [BsonElement("SzA")]
    public double? AngleSectionModulusZAxisA { get; set; }

    [BsonElement("SzB")]
    public double? AngleSectionModulusZAxisB { get; set; }

    [BsonElement("SzC")]
    public double? AngleSectionModulusZAxisC { get; set; }

    [BsonElement("rts")]
    public double? RadiusOfGyrationEffective { get; set; }

    [BsonElement("ho")]
    public double? FlangeCentroidDistance { get; set; }

    [BsonElement("PA")]
    public double? AngleShapePerimeterMinusOneFlange { get; set; }

    [BsonElement("PA2")]
    public double? AngleShapePerimeterMinusOneLeg { get; set; }

    [BsonElement("PB")]
    public double? ShapePerimeter { get; set; }

    [BsonElement("PC")]
    public double? BoxPerimeterMinusOneFlange { get; set; }

    [BsonElement("PD")]
    public double? BoxPerimeter { get; set; }

    [BsonElement("T")]
    public string? WebToeFilletDistance { get; set; }

    [BsonElement("WGi")]
    public string? WorkableGageInner { get; set; }

    [BsonElement("WGo")]
    public double? WorkableGageInnerOuter { get; set; }
}
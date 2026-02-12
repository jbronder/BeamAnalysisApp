using System;
using System.Collections.ObjectModel;
using LiveChartsCore.Defaults;

namespace BeamAnalysisApp.Models;


public record BeamLoadCase(string ComboName, string ImageSrc, string LoadKind, int BlcId);
public class DiagramResults
{
    public ObservableCollection<ObservablePoint> MomentSeries { get; set; } = new()
    {
        new ObservablePoint(0, 0),
    };

    public ObservableCollection<ObservablePoint> ShearSeries { get; set; } = new()
    {
        new ObservablePoint(0,0),
    };

    public ObservableCollection<ObservablePoint> DeflectionSeries { get; set; } = new()
    {
        new ObservablePoint(0,0),
    };
}

/// <summary>
/// Collects the inputs from the View to be consumed by the .ComputeExtremes and .ComputeDiagrams methods.
/// </summary>
public class BeamInputs
{
    public double BeamLoad { get; set; } = 0;
    public double BeamLength { get; set; } = 0;
    public double BeamInertia { get; set; } = 0;
    public double BeamElasticity { get; set; } = 0;

    public void Deconstruct(out double load, out double length, out double inertia, out double elasticity)
    {
        load = BeamLoad;
        length = BeamLength;
        inertia = BeamInertia;
        elasticity = BeamElasticity;
    }
}

/// <summary>
/// Class that collects the max/min results of the beam load case to update the
/// View from the ViewModel that 
/// </summary>
public class MaxBeamResults
{
    public double MaxShear { get; set; } = 0;
    public double MaxPositiveMoment { get; set; } = 0;
    public double MaxNegativeMoment { get; set; } = 0;
    public double MaxDeflection { get; set; } = 0;
    public double LOverValue { get; set; } = 0;

    public MaxBeamResults() { }
    public MaxBeamResults(double shear, double positiveMoment, double negativeMoment, double deflection, double lOverValue) { }
}

public class BeamCalculatorModel
{

    /// <summary>
    /// Constant that sets the number of points to generate when computing the
    /// shear, moment, and deflection diagrams.
    /// </summary>
    private const int NUM_POINTS = 1000;
    public MaxBeamResults ComputeExtremes(int blcId, BeamInputs beamInputs)
    {
        switch (blcId)
        {
            case 1:
                return ComputeExtremesBLC1(beamInputs);
            case 23:
                return ComputeExtremesBLC23(beamInputs);
            default:
                Console.WriteLine("Unhandled Beam Load Case!");
                return new MaxBeamResults(0, 0, 0, 0, 0);
        }
    }

    public DiagramResults ComputeDiagrams(int blcId, BeamInputs beamInputs)
    {
        switch (blcId)
        {
            case 1:
                return ComputeDiagramsBLC1(beamInputs);
            case 23:
                return ComputeDiagramsBLC23(beamInputs);
            default:
                Console.WriteLine("Unhandled Diagram Beam Load Case!");
                return new DiagramResults();
        }
    }


    private MaxBeamResults ComputeExtremesBLC1(BeamInputs inputs)
    {
        var results = new MaxBeamResults();
        var (load, length, inertia, elasticity) = inputs;
        results.MaxPositiveMoment = load * length * length / 8;
        results.MaxNegativeMoment = 0;
        results.MaxShear = load * length / 2;
        results.MaxDeflection = ((5 * load * Math.Pow((length), 4)) / (384 * elasticity * inertia)) * Math.Pow(12, 3);
        results.LOverValue = length * 12 / results.MaxDeflection;
        return results;
    }

    private MaxBeamResults ComputeExtremesBLC23(BeamInputs inputs)
    {
        var results = new MaxBeamResults();
        var (load, length, inertia, elasticity) = inputs;
        results.MaxPositiveMoment = load * length * length / 24;
        results.MaxNegativeMoment = load * length * length / 12;
        results.MaxShear = load * length / 2;
        results.MaxDeflection = ((load * Math.Pow((length), 4)) / (384 * elasticity * inertia)) * Math.Pow(12, 3);
        results.LOverValue = length * 12 / results.MaxDeflection;
        return results;
    }

    private DiagramResults ComputeDiagramsBLC1(BeamInputs inputs)
    {
        var results = new DiagramResults();
        var (load, length, inertia, elasticity) = inputs;
        var delta = length / NUM_POINTS;

        double bi = 0;
        var momentSeries = new ObservableCollection<ObservablePoint>();
        var shearSeries = new ObservableCollection<ObservablePoint>();
        var deflectionSeries = new ObservableCollection<ObservablePoint>();

        for (int i = 0; i <= NUM_POINTS; i++)
        {
            double mi = (load * bi / 2) * (length - bi);
            double vi = load * ((length / 2) - bi);
            double di = ((load * bi) * Math.Pow(12, 3) / (24 * elasticity * inertia)) * (Math.Pow(length, 3) - (2 * length * Math.Pow(bi, 2)) + Math.Pow(bi, 3));

            momentSeries.Add(new ObservablePoint(bi, mi));
            shearSeries.Add(new ObservablePoint(bi, vi));
            deflectionSeries.Add(new ObservablePoint(bi, di));
            bi += delta;
        }

        results.MomentSeries = momentSeries;
        results.ShearSeries = shearSeries;
        results.DeflectionSeries = deflectionSeries;
        return results;
    }

    private DiagramResults ComputeDiagramsBLC23(BeamInputs inputs)
    {
        var results = new DiagramResults();
        var (load, length, inertia, elasticity) = inputs;
        var delta = length / NUM_POINTS;

        double bi = 0;
        var momentSeries = new ObservableCollection<ObservablePoint>();
        var shearSeries = new ObservableCollection<ObservablePoint>();
        var deflectionSeries = new ObservableCollection<ObservablePoint>();

        for (int i = 0; i <= NUM_POINTS; i++)
        {
            double mi = (load / 12) * (6 * length * bi - Math.Pow(length, 2) - (6 * Math.Pow(bi, 2)));
            double vi = (load) * ((length / 2) - bi);
            double di = (load * Math.Pow(bi, 2) * Math.Pow(12, 3) / (24 * elasticity * inertia)) * (Math.Pow(length - bi, 2));

            momentSeries.Add(new ObservablePoint(bi, mi));
            shearSeries.Add(new ObservablePoint(bi, vi));
            deflectionSeries.Add(new ObservablePoint(bi, di));
            bi += delta;
        }

        results.MomentSeries = momentSeries;
        results.ShearSeries = shearSeries;
        results.DeflectionSeries = deflectionSeries;
        return results;
    }
}
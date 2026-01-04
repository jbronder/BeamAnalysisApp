using System;
using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SkiaSharp;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using BeamAnalysisApp.Models;
using LiveChartsCore.Defaults;


namespace BeamAnalysisApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private BeamLoadCase? _blc;
    private Bitmap? _currentImage;
    private string? _strBeamLoad;
    private string? _strBeamLength;
    private string? _strBeamInertia;
    private string? _strBeamElasticity;
    private bool _isOtherBLC0 = false;

    private BeamInputs _currentBeamInputs;
    private MaxBeamResults _maxBeamResults;

    private ObservableCollection<ObservablePoint> _momentData;
    private ObservableCollection<ObservablePoint> _shearData;
    private ObservableCollection<ObservablePoint> _deflectionData;

    public ObservableCollection<ISeries> MomentSeries { get; set; }
    public ObservableCollection<ISeries> ShearSeries { get; set; }
    public ObservableCollection<ISeries> DeflectionSeries { get; set; }

    public bool IsOtherBLC0
    {
        get => _isOtherBLC0;
        set => SetProperty(ref _isOtherBLC0, value);
    }
    private BeamCalculatorModel _bcm;

    public MaxBeamResults MaxResults
    {
        get => _maxBeamResults;
        set => SetProperty(ref _maxBeamResults, value);
    }

    public ObservableCollection<ObservablePoint> MomentData
    {
        get => _momentData;
        set
        {
            SetProperty(ref _momentData, value);
            Console.WriteLine("SetProp called");
        }
    }
    public ObservableCollection<ObservablePoint> ShearData
    {
        get => _shearData;
        set => SetProperty(ref _shearData, value);
    }
    public ObservableCollection<ObservablePoint> DeflectionData
    {
        get => _deflectionData;
        set => SetProperty(ref _deflectionData, value);
    }

    public string? BeamLoadLabel
    {
        get
        {
            if (SelectedBeamLoadCase?.LoadKind == "UNIFORM")
            {
                return "Uniform Load, w (k/ft)";
            }
            else if (SelectedBeamLoadCase?.LoadKind == "POINT")
            {
                return "Point Load, P (k)";
            }
            return null;
        }
    }
    public string? BeamLoad
    {
        get => _strBeamLoad;
        set
        {
            SetProperty(ref _strBeamLoad, value);
            if (double.TryParse(_strBeamLoad, out double load))
            {
                _currentBeamInputs.BeamLoad = load;
            }
            // TODO: Needs error handling here?
            OnInputUpdate();
        }
    }

    public string? BeamLength
    {
        get => _strBeamLength;
        set
        {
            SetProperty(ref _strBeamLength, value);
            if (double.TryParse(_strBeamLength, out double length))
            {
                _currentBeamInputs.BeamLength = length;
            }
            // TODO: Needs error handling here?
            OnInputUpdate();
        }
    }

    public string? BeamInertia
    {
        get => _strBeamInertia;
        set
        {
            SetProperty(ref _strBeamInertia, value);
            if (double.TryParse(_strBeamInertia, out double inertia))
            {
                _currentBeamInputs.BeamInertia = inertia;
            }
            // TODO: Needs error handling here?
            OnInputUpdate();
        }
    }

    public string? BeamElasticity
    {
        get => _strBeamElasticity;
        set
        {
            SetProperty(ref _strBeamElasticity, value);
            if (double.TryParse(_strBeamElasticity, out double elasticity))
            {
                _currentBeamInputs.BeamElasticity = elasticity;
            }
            // TODO: Needs error handling here?
            OnInputUpdate();
        }
    }

    public Bitmap? CurrentImage
    {
        get => _currentImage;
        set => SetProperty(ref _currentImage, value);
    }

    public BeamLoadCase? SelectedBeamLoadCase
    {
        get => _blc;
        set
        {
            SetProperty(ref _blc, value);
            Console.WriteLine($"Selected: {_blc}");
            UpdateImage();
            OnPropertyChanged(nameof(BeamLoadLabel));
            OnInputUpdate();
        }
    }
    public ObservableCollection<BeamLoadCase> BeamLoadCases { get; } = new()
    {
        // Add Load Cases here.
        new BeamLoadCase("Simple Beam - Uniform Load", "avares://BeamAnalysisApp/Assets/blc1.png", "UNIFORM", 1),
        new BeamLoadCase("Beam Fixed At Both Ends - Uniform Load", "avares://BeamAnalysisApp/Assets/blc23.png", "UNIFORM", 23),
    };

    /// <summary>
    /// Runs the calculations for the current beam configuration.
    /// </summary>
    private void OnInputUpdate()
    {
        IsOtherBLC0 = true;
        MaxResults = _bcm.ComputeExtremes(SelectedBeamLoadCase.BlcId, _currentBeamInputs);
        var diagramData = _bcm.ComputeDiagrams(SelectedBeamLoadCase.BlcId, _currentBeamInputs);

        MomentData.Clear();
        foreach (var ms in diagramData.MomentSeries)
        {
            MomentData.Add(ms);
        }

        ShearData.Clear();
        foreach (var ss in diagramData.ShearSeries)
        {
            ShearData.Add(ss);
        }

        DeflectionData.Clear();
        foreach (var ds in diagramData.DeflectionSeries)
        {
            DeflectionData.Add(ds);
        }
    }

    private void UpdateImage()
    {
        try
        {
            if (!string.IsNullOrEmpty(SelectedBeamLoadCase?.ImageSrc))
            {
                CurrentImage = LoadImage(SelectedBeamLoadCase.ImageSrc);
            }
            else
            {
                // Retrieve default image of unloaded beam.
                CurrentImage = LoadImage("avares://BeamAnalysisApp/Assets/blc0.png");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading image: {e.Message}");
            CurrentImage = null;
        }
    }

    private Bitmap LoadImage(string resource)
    {
        try
        {
            var uri = new Uri(resource);
            return new Bitmap(AssetLoader.Open(uri));
        }
        catch (Exception)
        {
            throw;
        }
    }

    public MainWindowViewModel()
    {
        // Initialize CurrentImage with a simple beam on start-up.
        CurrentImage = LoadImage("avares://BeamAnalysisApp/Assets/blc0.png");

        // Simple Instantiation to get a model up and running.
        // TODO: Refactor to use Dependency Injection.
        _bcm = new BeamCalculatorModel();

        _currentBeamInputs = new BeamInputs();
        _maxBeamResults = new MaxBeamResults();

        _momentData = new ObservableCollection<ObservablePoint> { new ObservablePoint(0, 0) };
        _shearData = new ObservableCollection<ObservablePoint> { new ObservablePoint(0, 0) };
        _deflectionData = new ObservableCollection<ObservablePoint> { new ObservablePoint(0, 0) };

        MomentSeries = new ObservableCollection<ISeries> {
            new LineSeries<ObservablePoint> {
                Values = MomentData,
                Stroke = new SolidColorPaint(SKColors.CornflowerBlue) { StrokeThickness = 2 },
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0,
            }
        };

        ShearSeries = new ObservableCollection<ISeries> {
            new LineSeries<ObservablePoint> {
                Values = ShearData,
                Stroke = new SolidColorPaint(SKColors.CornflowerBlue) { StrokeThickness = 2 },
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0,
            }
        };

        DeflectionSeries = new ObservableCollection<ISeries> {
            new LineSeries<ObservablePoint> {
                Values = DeflectionData,
                Stroke = new SolidColorPaint(SKColors.CornflowerBlue) { StrokeThickness = 2 },
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0,
            }
        };
    }
}

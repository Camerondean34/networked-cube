﻿namespace FaceViewer.ViewModels;
using System.Windows.Input;
using LibCubeIntegration.GetCubeStrategies;
using LibNetCube;

public class MainViewModel : BaseViewModel
{
    int _faceIndex;

    readonly List<string> Faces = ["Front", "Right", "Back", "Left", "Top", "Bottom"];

    public MainViewModel(IGetCubeStrategy getCubeStrategy)
    {
        var faceVal = GetFaceEnum();
        Face = new FaceViewModel(faceVal, getCubeStrategy);
        RotateFace = new RelayCommand(RotateFaceDisplayed);
    }

    public FaceViewModel Face { get; set; }
    public string FaceName => Faces[_faceIndex];

    public ICommand RotateFace { get; }

    void UpdateFace()
    {
        Face.Face = GetFaceEnum();
        _ = Face.Update();
    }

    CubeFace GetFaceEnum()
    {
        return FaceName switch
        {
            "Front" => CubeFace.Front,
            "Right" => CubeFace.Right,
            "Back" => CubeFace.Back,
            "Left" => CubeFace.Left,
            "Top" => CubeFace.Top,
            _ => CubeFace.Bottom
        };
    }

    void RotateFaceDisplayed()
    {
        _faceIndex = (_faceIndex + 1) % Faces.Count;
        UpdateFace();
        OnPropertyChanged("FaceName");
    }
}

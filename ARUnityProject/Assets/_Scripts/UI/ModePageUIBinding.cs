using UnityEngine;
using UnityEngine.UI;

public class ModePageUIBinding : MonoBehaviour
{
    [SerializeField] private Button _freeModeButton;
    [SerializeField] private Button _moveModeButton;
    [SerializeField] private Button _rotateModeButton;
    [SerializeField] private Button _scaleModeButton;
    [SerializeField] private Button _resetButton;

    private void Awake()
    {
        _freeModeButton.onClick.AddListener(OnFreeModeButtonClicked);
        _moveModeButton.onClick.AddListener(OnMoveModeButtonClicked);
        _rotateModeButton.onClick.AddListener(OnRotateModeButtonClicked);
        _scaleModeButton.onClick.AddListener(OnScaleModeButtonClicked);
        _resetButton.onClick.AddListener(OnResetButtonClicked);
    }

    private void OnFreeModeButtonClicked() => ARCarController. currentEditMode = EditMode.Free;
    private void OnMoveModeButtonClicked() => ARCarController.currentEditMode = EditMode.Move;
    private void OnRotateModeButtonClicked() => ARCarController.currentEditMode = EditMode.Rotate;
    private void OnScaleModeButtonClicked() => ARCarController.currentEditMode = EditMode.Scale;
    private void OnResetButtonClicked() => ARCarController.RequestReset();
}

public enum EditMode
{
    Free,
    Move,
    Rotate,
    Scale
}
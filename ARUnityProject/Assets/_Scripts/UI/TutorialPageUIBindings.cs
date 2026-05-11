using UnityEngine;
using UnityEngine.UI;

public class TutorialPageUIBindings : MonoBehaviour
{
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private GameObject[] _slides;

    private int _currentSlideIndex = 0;

    void Start()
    {
        UpdateSlideVisibility();
        UpdateButtonInteractability();
        _leftButton.onClick.AddListener(OnLeftButtonClicked);
        _rightButton.onClick.AddListener(OnRightButtonClicked);
    }

    private void OnLeftButtonClicked()
    {
        if (_currentSlideIndex > 0)
        {
            _currentSlideIndex--;
            UpdateSlideVisibility();
        }

        UpdateButtonInteractability();
    }

    private void OnRightButtonClicked()
    {
        if (_currentSlideIndex < _slides.Length - 1)
        {
            _currentSlideIndex++;
            UpdateSlideVisibility();
        }

        UpdateButtonInteractability();
    }

    private void UpdateSlideVisibility()
    {
        for (int i = 0; i < _slides.Length; i++)
        {
            _slides[i].SetActive(i == _currentSlideIndex);
        }
    }

    private void UpdateButtonInteractability()
    {
        _leftButton.interactable = _currentSlideIndex != 0;
        _rightButton.interactable = _currentSlideIndex != _slides.Length - 1;
    }
}

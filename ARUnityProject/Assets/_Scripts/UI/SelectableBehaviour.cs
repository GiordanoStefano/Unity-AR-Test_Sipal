using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectableBehaviour : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Image[] _targetImages;
    [SerializeField] private Image _startingImage;

    private void Start()
    {
        if (_startingImage != null)
            SetSelected(_startingImage);
    }

    public void SetSelected(Image target)
    {
        foreach (var image in _targetImages)
        {
            image.color = image == target ? Color.white : Color.clear;
        }
    }

    public void AddSelectable(Image target)
    {
        if (target == null) return;

        foreach (var image in _targetImages)
        {
            if (image == target) return;
        }

        _targetImages = _targetImages.Append(target).ToArray();
    }
}
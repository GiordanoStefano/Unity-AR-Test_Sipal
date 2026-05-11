using UnityEngine;

public class PlacementPageUIBinding : MonoBehaviour
{
    [SerializeField] private GameObject _placementPage;
    [SerializeField] private GameObject _transformPage;

    private void OnEnable()
    {
        ARCarController.CarPlaced += OnCarPlaced;
    }

    private void OnDisable()
    {
        ARCarController.CarPlaced -= OnCarPlaced;
    }

    private void OnCarPlaced()
    {
        _placementPage.SetActive(false);
        _transformPage.SetActive(true);
    }
}
using UnityEngine;

[CreateAssetMenu(fileName = "Element_", menuName = "Report/Element")]
public class ElementSO : ScriptableObject
{
    [SerializeField] private string _elemento;
    [SerializeField] private DefectSO[] _difetti;

    // Properties
    public string Elemento => _elemento;
    public DefectSO[] Difetti => _difetti;
}
using UnityEngine;

[CreateAssetMenu(fileName = "Defect_", menuName = "Report/Defect")]
public class DefectSO : ScriptableObject
{
    [Header("Info")]
    [SerializeField] private string _codice;
    [SerializeField] private string _descrizione;
    [Range(0f, 5f)]
    [SerializeField] private int _gravità;

    [Header("Fields (TRUE = editable)")]
    [SerializeField] private bool _hasVisto         = true;
    [Space(20)]
    [SerializeField] private bool _hasEstensione02;
    [SerializeField] private bool _hasEstensione05;
    [SerializeField] private bool _hasEstensione1   = true;
    [Space(20)]
    [SerializeField] private bool _hasIntensità02;
    [SerializeField] private bool _hasIntensità05;
    [SerializeField] private bool _hasIntensità1    = true;
    [Space(20)]
    [SerializeField] private bool _hasPS;
    [SerializeField] private bool _hasNA            = true;
    [SerializeField] private bool _hasNR            = true;
    [SerializeField] private bool _hasNP            = true;

    // Properties
    public string Codice => _codice;
    public string Descrizione => _descrizione;
    public int Gravità => _gravità;
    public bool HasVisto => _hasVisto;
    public bool HasEstensione02 => _hasEstensione02;
    public bool HasEstensione05 => _hasEstensione05;
    public bool HasEstensione1 => _hasEstensione1;
    public bool HasIntensità02 => _hasIntensità02;
    public bool HasIntensità05 => _hasIntensità05;
    public bool HasIntensità1 => _hasIntensità1;
    public bool HasPS => _hasPS;
    public bool HasNA => _hasNA;
    public bool HasNR => _hasNR;
    public bool HasNP => _hasNP;
}
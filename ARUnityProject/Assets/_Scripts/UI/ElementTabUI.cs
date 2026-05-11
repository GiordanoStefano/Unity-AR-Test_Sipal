using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class ElementTabUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _selectableImage;

    public Image SelectableImage => _selectableImage;
    private SelectableBehaviour _selectableBehaviour;

    private ElementSO _elementSO;
    private List<DefectUI> _defectUIs = new();

    private static Action ClearDefects;


    // Setup
    private void OnEnable() => ClearDefects += ClearDefectUIs;
    private void OnDisable() => ClearDefects -= ClearDefectUIs;

    public void Initialize(ElementSO elementSO)
    {
        _elementSO = elementSO;
        _text.text = _elementSO.Elemento;
        _button.onClick.AddListener(OnButtonClicked);
        _selectableBehaviour = GetComponentInParent<SelectableBehaviour>();
    }

    
    // Button
    public void OnButtonClicked()
    {
        ClearDefects?.Invoke();
        ShowDefectUIs(true);
        _selectableBehaviour.SetSelected(_selectableImage);
    }


    // Defect UIs
    public void AddDefectUI(DefectUI defectUI) => _defectUIs.Add(defectUI);
    private void ClearDefectUIs() => ShowDefectUIs(false);
        private void ShowDefectUIs(bool show)
    {
        foreach (var defectUI in _defectUIs)
        {
            defectUI.gameObject.SetActive(show);
        }
    }


    // Report Creation
    public GruppoDifetti GetGruppoDifetti()
    {
        var gruppoDifetti = new GruppoDifetti
        {
            Elemento = _elementSO.Elemento,
            RigheDifetto = new List<RigaDifetto>()
        };

        foreach (var defectUI in _defectUIs)
        {
            var rigaDifetto = defectUI.GetRigaDifetto();
            if (rigaDifetto != null)
            {
                gruppoDifetti.RigheDifetto.Add(rigaDifetto);
            }
        }

        return gruppoDifetti;
    }
}
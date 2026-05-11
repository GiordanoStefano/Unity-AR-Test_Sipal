using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefectUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _codeText;
    [SerializeField] private TMP_Text _gText;
    [SerializeField] private Toggle _vistoToggle;
    [SerializeField] private Toggle _extension02Toggle;
    [SerializeField] private Toggle _extension05Toggle;
    [SerializeField] private Toggle _extension10Toggle;
    [SerializeField] private Toggle _intensity02Toggle;
    [SerializeField] private Toggle _intensity05Toggle;
    [SerializeField] private Toggle _intensity10Toggle;
    [SerializeField] private Toggle _PSToggle;
    [SerializeField] private Toggle _NAToggle;
    [SerializeField] private Toggle _NRToggle;
    [SerializeField] private Toggle _NPToggle;

    private DefectSO _defectSO;

    public void Initialize(DefectSO defectSO)
    {
        _defectSO = defectSO;
        _nameText.text = defectSO.Descrizione;
        _codeText.text = defectSO.Codice;
        _gText.text = "G : " + defectSO.Gravità.ToString();

        SetToggleInteractable(_vistoToggle, defectSO.HasVisto);
        SetToggleInteractable(_extension02Toggle, defectSO.HasEstensione02);
        SetToggleInteractable(_extension05Toggle, defectSO.HasEstensione05);
        SetToggleInteractable(_extension10Toggle, defectSO.HasEstensione1);
        SetToggleInteractable(_intensity02Toggle, defectSO.HasIntensità02);
        SetToggleInteractable(_intensity05Toggle, defectSO.HasIntensità05);
        SetToggleInteractable(_intensity10Toggle, defectSO.HasIntensità1);
        SetToggleInteractable(_PSToggle, defectSO.HasPS);
        SetToggleInteractable(_NAToggle, defectSO.HasNA);
        SetToggleInteractable(_NRToggle, defectSO.HasNR);
        SetToggleInteractable(_NPToggle, defectSO.HasNP);
    }


    private void SetToggleInteractable(Toggle toggle, bool isInteractable)
    {
        toggle.interactable = isInteractable;
        if (!isInteractable)
        {
            var text = toggle.GetComponentInChildren<TMP_Text>();
            if (text != null) text.color = new Color32(255, 255, 255, 20);
        }
    }

    private bool? GetToggleValue(Toggle toggle)
    {
        if (!toggle.IsInteractable())
            return null;
        return toggle.isOn;
    }


    // Crea un oggetto RigaDifetto basato sui valori attuali dell'interfaccia
    public RigaDifetto GetRigaDifetto()
    {
        return new RigaDifetto()
        {
            Descrizione = _defectSO.Descrizione,
            Codice = _defectSO.Codice,
            Gravità = _defectSO.Gravità,

            Visto = GetToggleValue(_vistoToggle),

            Estensione02 = GetToggleValue(_extension02Toggle),
            Estensione05 = GetToggleValue(_extension05Toggle),
            Estensione1 = GetToggleValue(_extension10Toggle),
            Intensità02 = GetToggleValue(_intensity02Toggle),
            Intensità05 = GetToggleValue(_intensity05Toggle),
            Intensità1 = GetToggleValue(_intensity10Toggle),

            PS = GetToggleValue(_PSToggle),
            NA = GetToggleValue(_NAToggle),
            NR = GetToggleValue(_NRToggle),
            NP = GetToggleValue(_NPToggle)
        };
    }
}
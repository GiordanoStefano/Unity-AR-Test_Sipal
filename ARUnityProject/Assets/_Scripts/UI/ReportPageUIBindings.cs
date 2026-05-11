using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ReportPageUIBindings : MonoBehaviour
{
    public static Action<ElementSO> OnElementTabClicked;

    [Header("UI Elements")]
    [SerializeField] private Transform _elementTabParent;
    [SerializeField] private Transform _defectParent;
    [SerializeField] private TMP_Text _saveAlertPopup;
    [SerializeField] private SelectableBehaviour _selectableBehaviour;

    [Header("Prefabs")]
    [SerializeField] private GameObject _elementTabPrefab;
    [SerializeField] private GameObject _defectPrefab;

    [Header("ScriptableObjects")]
    [SerializeField] private ElementSO[] _elementSOs;

    private List<ElementTabUI> _elementTabUIs = new();
    private string _filePath;

    void Start()
    {
        PopulatePage();
    }

    private void PopulatePage()
    {
        foreach (var elementSO in _elementSOs)
        {
            var tabInstance = Instantiate(_elementTabPrefab, _elementTabParent);
            if (tabInstance.TryGetComponent(out ElementTabUI tabUI))
            {
                tabUI.Initialize(elementSO);
                _elementTabUIs.Add(tabUI);
                _selectableBehaviour.AddSelectable(tabUI.SelectableImage);

                foreach (DefectSO defect in elementSO.Difetti)
                {
                    var defectInstance = Instantiate(_defectPrefab, _defectParent);
                    if (defectInstance.TryGetComponent(out DefectUI defectUI))
                    {
                        defectUI.Initialize(defect);
                        tabUI.AddDefectUI(defectUI);
                    }
                }
            }
        }

        _selectableBehaviour.SetSelected(_elementTabUIs[0].SelectableImage);
        _elementTabUIs[0].OnButtonClicked();
    }

    // This method can be called when the save button is clicked in the UI
    // creates a json report document based on the current state of the UI
    public void SaveButtonClicked()
    {
        _filePath = Application.persistentDataPath + "/report.json";
        var report = CreateReportModule();

        string jsonReport = JsonConvert.SerializeObject(report, Formatting.Indented);
        File.WriteAllText(_filePath, jsonReport);
        
        _saveAlertPopup.text = "Report saved successfully!" +
            "\n\nFile path: " + _filePath;
    }

    public void PROVA()
    {
    }

    private ReportDocument CreateReportModule()
    {
        ReportDocument report = new()
        {
            Intestazione = new IntestazioneReport().DefaultHeader(),
            Gruppi = new List<GruppoDifetti>()
        };
        foreach (var tabUI in _elementTabUIs)
        {
            report.Gruppi.Add(tabUI.GetGruppoDifetti());
        }
        return report;
    }
}
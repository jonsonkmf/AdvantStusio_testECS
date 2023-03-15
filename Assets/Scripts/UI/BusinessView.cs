using System;
using System.Collections;
using System.Collections.Generic;
using StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BusinessView : MonoBehaviour
{
    public event Action<BusinessType> LevelUpButtonClickedEvent;
    public event Action<BusinessType,int> UpgradeButtonClickedEvent;
    
    public BusinessType BusinessType;
    
    [SerializeField] private TMP_Text _BusinessNameText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Image _incomeProgressFill;
    [SerializeField] private TMP_Text _incomeText;
    [SerializeField] private UpgradeWidget _upgradeWidget;
    [SerializeField] private LevelUpWidget _levelUpWidget;
    [SerializeField] private Button _levelUpButton;
    
    public TMP_Text BusinessNameText => _BusinessNameText;
    public TMP_Text LevelText => _levelText;
    public Image IncomeProgressFill => _incomeProgressFill;

    public TMP_Text IncomeText => _incomeText;

    public UpgradeWidget UpgradeWidget => _upgradeWidget;

    public LevelUpWidget LevelUpWidget => _levelUpWidget;

    private void OnEnable()
    {
        _levelUpButton.onClick.AddListener(LevelUpButtonClicked);
    }

    private void OnDisable()
    {
        _levelUpButton.onClick.RemoveListener(LevelUpButtonClicked);
    }

    private void LevelUpButtonClicked()
    {
        LevelUpButtonClickedEvent?.Invoke(BusinessType);
    }
}

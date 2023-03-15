using System;
using System.Collections;
using System.Collections.Generic;
using StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWidgetElement : MonoBehaviour
{
    public event Action<UpgradeWidgetElement,BusinessType, int> BuyUpgradeButtonClick;
    private BusinessType _businessType;
    private int _number;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _incomeBoostText;
    [SerializeField] private TMP_Text _priceText;

    public Button BuyButton => _buyButton;

    public TMP_Text NameText => _nameText;

    public TMP_Text IncomeBoostText => _incomeBoostText;

    public TMP_Text PriceText => _priceText;


    public void Init(BusinessType businessType, int number)
    {
        _businessType = businessType;
        _number = number;
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(ButtonClick);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(ButtonClick);
    }

    private void ButtonClick()
    {
        BuyUpgradeButtonClick?.Invoke(this,_businessType,_number);
    }
}

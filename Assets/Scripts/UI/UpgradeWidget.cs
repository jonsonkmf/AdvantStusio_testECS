using System.Collections;
using System.Collections.Generic;
using StaticData;
using UnityEngine;

public class UpgradeWidget : MonoBehaviour
{
    public List<UpgradeWidgetElement> Elements => _elements;
    
    [SerializeField] private UpgradeWidgetElement _upgradeWidgetElement;

    private List<UpgradeWidgetElement> _elements = new List<UpgradeWidgetElement>();
    
    public void Create(string name, string income, string price, BusinessType businessType, int number,
        bool upgradeUpgradeIsBuy)
    {
        var element = Instantiate(_upgradeWidgetElement, transform);
        element.NameText.text = name;
        element.IncomeBoostText.text = $"Доход: +{income}%";
        element.PriceText.text = upgradeUpgradeIsBuy?"Куплено":$"Цена: {price}$";
        element.Init(businessType, number);
        Elements.Add(element);
    }
}

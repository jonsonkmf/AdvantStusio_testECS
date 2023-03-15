using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpWidget : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelPriceText;
    [SerializeField] private Button _levelUpButton;

    public TMP_Text LevelPriceText => _levelPriceText;

    public Button UpButton => _levelUpButton;
    
    public void ChangeData(string price)
    {
        _levelPriceText.text = price;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDView : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;

    public TMP_Text MoneyText => _moneyText;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContainer : MonoBehaviour
{
    [SerializeField] private HUDView _hudHUDView;
    [SerializeField] private MainView _mainView;

    public HUDView HUDView => _hudHUDView;

    public MainView MainView => _mainView;
    
    
}

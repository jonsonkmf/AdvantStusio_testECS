using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainView : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private BusinessView _viewPrefab;

    public BusinessView ViewPrefab => _viewPrefab;

    public Transform Container => _container;
}

using System.Collections.Generic;
using Components;
using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/BusinessConfiguration", fileName = "BusinessConfiguration", order = 51)]
public class BusinessConfiguration : ScriptableObject
{
    public List<BusinessComponent> _business = new List<BusinessComponent>();
}
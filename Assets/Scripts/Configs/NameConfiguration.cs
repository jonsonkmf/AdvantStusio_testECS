using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/NameConfiguration", fileName = "NameConfiguration", order = 51)]
public class NameConfiguration : ScriptableObject
{
    public BusinessName[] BusinessNames;
    public UpgradeName[] UpgradeNames;
}
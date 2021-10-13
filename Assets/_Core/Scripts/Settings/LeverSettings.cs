using UnityEngine;

[CreateAssetMenu(fileName = "LeverSettings", menuName = "Custom/Settings/LeverSettings")]
public class LeverSettings : ScriptableObject
{
    [SerializeField] private float leverSensitiveDegrees;
    [SerializeField] private float maximalAngleDegrees;
    [SerializeField] private float springPower;
    [SerializeField] private float damperPower;


    public float LeverSensitiveDegrees => leverSensitiveDegrees;

    public float MaximalAngleDegrees => maximalAngleDegrees;

    public float SpringPower => springPower;

    public float DamperPower => damperPower;
}
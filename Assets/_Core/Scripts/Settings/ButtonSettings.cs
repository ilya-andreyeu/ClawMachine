using UnityEngine;

[CreateAssetMenu(fileName = "ButtonSettings", menuName = "Custom/Settings/ButtonSettings")]
public class ButtonSettings : ScriptableObject
{
    [SerializeField] private float pressureSensitive;
    [SerializeField] private float movementLimit;
    [SerializeField] private float springPower;
    [SerializeField] private float damperPower;

    public float PressureSensitive => pressureSensitive;

    public float MovementLimit => movementLimit;

    public float SpringPower => springPower;

    public float DamperPower => damperPower;
}
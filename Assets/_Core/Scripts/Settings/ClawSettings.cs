using UnityEngine;

[CreateAssetMenu(fileName = "ClawSettings", menuName = "Custom/Settings/ClawSettings")]
public class ClawSettings : ScriptableObject
{
    [SerializeField] private float minimalAngle;
    [SerializeField] private float maximalAngle;
    [SerializeField] private float driveVelocity;
    [SerializeField] private float driveForce;

    public float MinimalAngle => minimalAngle;

    public float MaximalAngle => maximalAngle;

    public float DriveVelocity => driveVelocity;

    public float DriveForce => driveForce;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    private const float openFactor = 1f;
    private const float closeFactor = -1f;

    [SerializeField] private HingeJoint joint;
    [SerializeField] private ClawSettings settings;

    private void Awake()
    {
        CheckNull();
    }

    public void Initialize(Hand hand)
    {
        var motor = joint.motor;
        motor.force = settings.DriveForce;
        motor.targetVelocity = settings.DriveVelocity;
        joint.motor = motor;

        var limits = joint.limits;
        limits.min = settings.MinimalAngle;
        limits.max = settings.MaximalAngle;
        joint.limits = limits;

        joint.useMotor = true;
        joint.useLimits = true;

        hand.SetCallbacks(Open, Close);
    }

    private void CheckNull()
    {
        if(joint == null) Debug.LogError("Joint is null");
        if(settings == null) Debug.LogError("Settings is null");
    }

    private void Open()
    {
        var motor = joint.motor;
        motor.targetVelocity = settings.DriveVelocity * openFactor;
        joint.motor = motor;
    }

    private void Close()
    {
        var motor = joint.motor;
        motor.targetVelocity = settings.DriveVelocity * closeFactor;
        joint.motor = motor;
    }
}
using System;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private ConfigurableJoint leverJoint;
    [SerializeField] private LeverSettings settings;

    private float xAngleToPercent;
    private float zAngleToPercent;

    private Vector3 yDirection;
    private Vector3 xDirection;
    private Vector3 zDirection;

    private Vector2 leverValue = Vector3.zero;

    private event Action<Vector2> OnLeverValueChanged;

    private bool IsLeverTilted => Vector3.Angle(yDirection, transform.up) > settings?.LeverSensitiveDegrees;

    public Vector2 LeverValue => leverValue;

    public void Initialize(Action<Vector2> callback)
    {
        NullCheck();
        SetupJoint();
        SetMaxAngles();
        GetLeverDirections();
        OnLeverValueChanged = callback;
    }

    private void NullCheck()
    {
        if(settings == null) Debug.LogError("Settings is null");
        if(leverJoint == null) Debug.LogError("Joint is null");
    }

    private void SetupJoint()
    {
        leverJoint.xMotion = ConfigurableJointMotion.Locked;
        leverJoint.yMotion = ConfigurableJointMotion.Locked;
        leverJoint.zMotion = ConfigurableJointMotion.Locked;
        leverJoint.angularXMotion = ConfigurableJointMotion.Limited;
        leverJoint.angularYMotion = ConfigurableJointMotion.Limited;
        leverJoint.angularZMotion = ConfigurableJointMotion.Limited;

        var lowAngularXLimit = leverJoint.lowAngularXLimit;
        lowAngularXLimit.limit = settings.MaximalAngleDegrees * -1;
        leverJoint.lowAngularXLimit = lowAngularXLimit;

        var highAngularXLimit = leverJoint.highAngularXLimit;
        highAngularXLimit.limit = settings.MaximalAngleDegrees;
        leverJoint.highAngularXLimit = highAngularXLimit;

        var angularZLimit = leverJoint.angularZLimit;
        angularZLimit.limit = settings.MaximalAngleDegrees;
        leverJoint.angularZLimit = angularZLimit;

        var angularDrive = leverJoint.angularXDrive;
        angularDrive.positionSpring = settings.SpringPower;
        angularDrive.positionDamper = settings.DamperPower;
        
        leverJoint.angularXDrive = angularDrive;
        leverJoint.angularYZDrive = angularDrive;
    }

    private void GetLeverDirections()
    {
        yDirection = transform.up;
        xDirection = transform.right;
        zDirection = transform.forward;
    }

    private void SetMaxAngles()
    {
        xAngleToPercent = 1f / Mathf.Sin(settings.MaximalAngleDegrees);
        zAngleToPercent = 1f / Mathf.Sin(settings.MaximalAngleDegrees);
    }

    private void CalculateLeverValue()
    {
        if (IsLeverTilted)
        {
            var xValue = Vector3.ProjectOnPlane(transform.up, zDirection).x * xAngleToPercent;
            var zValue = Vector3.ProjectOnPlane(transform.up, xDirection).z * zAngleToPercent;

            leverValue = new Vector2(xValue, zValue);
            OnLeverValueChanged?.Invoke(leverValue);
        }
        else
        {
            leverValue = Vector3.zero;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        CalculateLeverValue();
    }
}
using System;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private ConfigurableJoint buttonJoint;
    [SerializeField] private ButtonSettings settings;

    private Vector3 startPosition;

    private event Action OnButtonPressed;
    private bool isButtonPressed;

    public void Initialize(Action callback)
    {
        NullCheck();
        SetupJoint();
        SetStartButtonPosition();
        OnButtonPressed = callback;
    }

    private void NullCheck()
    {
        if (settings == null) Debug.LogError("Settings is null");
        if (buttonJoint == null) Debug.LogError("Joint is null");
    }

    private void SetupJoint()
    {
        buttonJoint.xMotion = ConfigurableJointMotion.Locked;
        buttonJoint.yMotion = ConfigurableJointMotion.Limited;
        buttonJoint.zMotion = ConfigurableJointMotion.Locked;
        buttonJoint.angularXMotion = ConfigurableJointMotion.Locked;
        buttonJoint.angularYMotion = ConfigurableJointMotion.Locked;
        buttonJoint.angularZMotion = ConfigurableJointMotion.Locked;

        var linearLimit = buttonJoint.linearLimit;
        linearLimit.limit = settings.MovementLimit;
        buttonJoint.linearLimit = linearLimit;

        var yDrive = buttonJoint.yDrive;
        yDrive.positionSpring = settings.SpringPower;
        yDrive.positionDamper = settings.DamperPower;
        buttonJoint.yDrive = yDrive;
    }

    private void SetStartButtonPosition()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        CheckButtonPressed();
    }

    private void CheckButtonPressed()
    {
        if (Vector3.Distance(transform.position, startPosition) > settings.PressureSensitive)
        {
            if(!isButtonPressed)
            {
                isButtonPressed = true;
                OnButtonPressed?.Invoke();
            }
        }
        else
        {
            isButtonPressed = false;
        }
    }
}
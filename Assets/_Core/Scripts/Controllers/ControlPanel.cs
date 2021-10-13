using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    private const float moveDownFactor = -1f;
    private const float moveUpFactor = 1f;

    [SerializeField] private Hand hand;
    [SerializeField] private Lever lever;
    [SerializeField] private Button button;
    [SerializeField] private ControlPanelSettings settings;

    private bool isGrabbing;
    private Vector2 parkingPosition;
    
    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        CheckNull();

        lever.Initialize(OnLeverMoveHandler);
        button.Initialize(OnButtonPressedHandler);
        hand.Initialize(settings.HandBounds);
        parkingPosition = hand.RootPosition;
    }

    private void CheckNull()
    {
        if(hand == null) Debug.LogError("Hand is null");
        if(lever == null) Debug.LogError("Lever is null");
        if(button == null) Debug.LogError("Button is null");
        if(settings == null) Debug.LogError("Settings is null");
    }

    private void OnLeverMoveHandler(Vector2 value)
    {
        if (!isGrabbing)
        {
            MoveHandHorizontal(value.x, value.y);
        }
    }

    private void MoveHandHorizontal(float x, float z)
    {
        x *= Time.deltaTime * settings.HorizontalSpeed;
        z *= Time.deltaTime * settings.HorizontalSpeed;

        hand.MoveHorizontal(x, z);
    }


    private void MoveHandVertical(float y)
    {
        y *= Time.deltaTime * settings.VerticalSpeed;
        hand.MoveVertical(y);
    }

    private void OnButtonPressedHandler()
    {
        Grab();
    }

    private void Grab()
    {
        if(!isGrabbing)
        {
            StartCoroutine(GrabRoutine());
        }
    }

    private IEnumerator GrabRoutine()
    {
        isGrabbing = true;
        while (hand.RootHeight > settings.MinimalRootHeight)
        {
            float y = moveDownFactor;
            MoveHandVertical(y);
            yield return null;
        }

        hand.Close();
        yield return new WaitForSeconds(2f);

        while (hand.RootHeight < settings.MaximalRootHeight)
        {
            float y = moveUpFactor;
            MoveHandVertical(y);
            yield return null;
        }

        while (Vector2.Distance(hand.RootPosition, parkingPosition) > 1f)
        {
            float x = parkingPosition.x - hand.RootPosition.x;
            if(x != 0)
            {
                x /= Mathf.Abs(x);
            }
            float z = parkingPosition.y - hand.RootPosition.y;
            if (z != 0)
            {
                z /= Mathf.Abs(z);
            }

            MoveHandHorizontal(x, z);

            yield return null;
        }

        hand.Open();
        yield return new WaitForSeconds(2f);
        isGrabbing = false;
    }
}
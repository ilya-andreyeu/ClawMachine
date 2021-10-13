using System;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private List<Claw> claws = new List<Claw>();
    [SerializeField] private ChainLink rootLink;
    [SerializeField] private GameObject root;
    [SerializeField] private Transform magicBox;

    private event Action OnOpen;
    private event Action OnClose;

    private XZBounds handBounds;

    private float initialRootHeight;

    private float chainSize = 3f;

    public bool IsHandOpened { get; private set; }


    public float RootHeight => root.transform.position.y;

    public Vector2 RootPosition => new Vector2(root.transform.position.x, root.transform.position.z);

    public Vector3 MagicBoxPosition => magicBox.transform.position;

    private void CheckNull()
    {
        if(rootLink == null) Debug.LogError("RootLink is null");
        if (root == null) Debug.LogError("Root is null");
        if(magicBox == null) Debug.LogError("BagicBox is null");
    }

    public void Initialize(XZBounds handBounds)
    {
        CheckNull();
        InitializeClaws();
        SetupChain();
        Open();

        this.handBounds = handBounds;
    }

    private void SetupChain()
    {
        initialRootHeight = rootLink.transform.position.y;

        rootLink.DisableChainToTop();
    }

    private void InitializeClaws()
    {
        foreach (var claw in claws)
        {
            claw.Initialize(this);
        }
    }

    public void Close()
    {
        OnClose?.Invoke();
        IsHandOpened = false;
    }

    public void Open()
    {
        OnOpen?.Invoke();
        IsHandOpened = true;
    }

    public void MoveHorizontal(float x, float z)
    {
        float xOffset = CheckBounds(x, magicBox.transform.position.x, handBounds.xBounds);
        float zOffset = CheckBounds(z, magicBox.transform.position.z, handBounds.zBounds);
        rootLink.transform.Translate(xOffset, 0, zOffset);
        magicBox.transform.Translate(xOffset, 0, zOffset);
    }

    private float CheckBounds(float offset, float currentPosition, Vector2 limits)
    {
        offset = (currentPosition + offset < limits.x) ? limits.x - currentPosition : offset;
        offset = (currentPosition + offset > limits.y) ? limits.y - currentPosition : offset;
        return offset;
    }


    public void MoveVertical(float y)
    {
        rootLink.transform.Translate(0, y, 0);
    }

    public void SetCallbacks(Action openCallback, Action closeCallback)
    {
        OnOpen += openCallback;
        OnClose += closeCallback;
    }

    private void Update()
    {
        CheckRootChanges();
    }

    private void CheckRootChanges()
    {
        if (rootLink.transform.position.y > initialRootHeight + chainSize / 2f)
        {
            if (rootLink.ChildLink != null)
            {
                rootLink = rootLink.ChildLink;
                rootLink.Disable();
            }
        }

        if (rootLink.transform.position.y < initialRootHeight - chainSize / 2f)
        {
            if (rootLink.ParentLink != null)
            {
                rootLink.Enable();
                rootLink = rootLink.ParentLink;
            }
        }
    }
}
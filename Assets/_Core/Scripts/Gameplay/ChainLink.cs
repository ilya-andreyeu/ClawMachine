using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLink : MonoBehaviour
{
    [SerializeField] private ChainLink parentLink;
    [SerializeField] private ChainLink childLink;

    [SerializeField] private ConfigurableJoint joint;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Collider collider;

    [SerializeField] private Hand hand;

    public ChainLink ParentLink => parentLink;
    public ChainLink ChildLink => childLink;

    public Rigidbody Rigidbody => rigidbody;

    private bool isDisabled;

    private Vector3 connectedAnchor;
    private float distanceForParent;

    private void Awake()
    {
        CheckNull();
        Initialize();
    }

    private void CheckNull()
    {
        if (joint == null) Debug.LogError("Joint is null");
        if (rigidbody == null) Debug.LogError("Rigidbody is null");
        if (meshRenderer == null) Debug.LogError("MeshRenderer is null");
        if (collider == null) Debug.LogError("Collider is null");
    }

    private void Initialize()
    {
        connectedAnchor = joint.connectedAnchor;
        distanceForParent = parentLink != null ? parentLink.transform.position.y - transform.position.y : 0f;
    }

    public void Disable()
    {
        joint.xMotion = ConfigurableJointMotion.Free;
        joint.yMotion = ConfigurableJointMotion.Free;
        joint.zMotion = ConfigurableJointMotion.Free;

        transform.rotation = Quaternion.identity;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        joint.connectedBody = null;
        meshRenderer.enabled = false;
        collider.enabled = false;
        isDisabled = true;
        transform.position = new Vector3(hand.MagicBoxPosition.x, transform.position.y, hand.MagicBoxPosition.z);
    }

    public void Enable()
    {
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;

        rigidbody.constraints = RigidbodyConstraints.None;
        joint.connectedBody = parentLink.Rigidbody;
        joint.connectedAnchor = connectedAnchor;
        meshRenderer.enabled = true;
        collider.enabled = true;

        isDisabled = false;
    }

    private void Update()
    {
        UpdateDisabledPosition();
    }

    private void UpdateDisabledPosition()
    {
        if (isDisabled)
        {
            if (parentLink != null)
            {
                parentLink.transform.position = new Vector3(hand.MagicBoxPosition.x, transform.position.y + distanceForParent,
                    hand.MagicBoxPosition.z);
            }
        }
    }

    public void DisableChainToTop()
    {
        if (parentLink != null)
        {
            Disable();
            parentLink.DisableChainToTop();
        }
    }
}

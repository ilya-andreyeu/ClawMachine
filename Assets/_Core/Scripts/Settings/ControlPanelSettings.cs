using UnityEngine;

[CreateAssetMenu(fileName = "ControlPanelSettings", menuName = "Custom/Settings/ControlPanelSettings")]
public class ControlPanelSettings : ScriptableObject
{
    [SerializeField] private float _minimalRootHeight;
    [SerializeField] private float _maximalRootHeight;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    
    [SerializeField] private XZBounds handBounds;

    public float MinimalRootHeight => _minimalRootHeight;

    public float MaximalRootHeight => _maximalRootHeight;

    public float HorizontalSpeed => horizontalSpeed;

    public float VerticalSpeed => verticalSpeed;

    public XZBounds HandBounds => handBounds;
}
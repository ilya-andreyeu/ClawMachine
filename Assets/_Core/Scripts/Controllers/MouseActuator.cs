using UnityEngine;

public class MouseActuator : MonoBehaviour
{
    private const float downFactor = -1;

    [SerializeField] private float force;
    [SerializeField] private float maxForce;
    [SerializeField] private float pushButtonForce;

    private Vector3 hitPoint;
    private bool isHitLever;
    private Rigidbody leverRigidbody;

    private void FixedUpdate()
    {
        CastRay();
    }

    private void CastRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                DetectLever(hit);

                DetectButton(hit);
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (isHitLever)
            {
                MoveLever();
            }
        }
    }

    private void MoveLever()
    {
        Vector3 newPoint = Input.mousePosition;
        Vector3 direction = (newPoint - hitPoint);
        direction = new Vector3(direction.x, 0, direction.y) * force;
        if (direction.magnitude > maxForce)
        {
            direction = direction / direction.magnitude * maxForce;
        }

        leverRigidbody.AddForce(direction, ForceMode.Force);
    }

    private void DetectButton(RaycastHit hit)
    {
        if (hit.transform.GetComponent<Button>() != null)
        {
            PushButton(hit.rigidbody);
        }
    }

    private void PushButton(Rigidbody button)
    {
        button.AddForce(button.transform.up * downFactor * pushButtonForce, ForceMode.Impulse);
    }

    private void DetectLever(RaycastHit hit)
    {
        if (hit.transform.GetComponent<Lever>() != null)
        {
            hitPoint = Input.mousePosition;
            isHitLever = true;
            leverRigidbody = hit.rigidbody;
        }
        else
        {
            isHitLever = false;
            leverRigidbody = null;
        }
    }
}

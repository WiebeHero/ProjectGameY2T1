using UnityEngine;

public sealed class Car : MonoBehaviour
{
    [SerializeField]
    private GameObject speedometerDial;
    
    private float speed;
    private Vector3 rotateAxis;

    private void Start()
    {
        if (speedometerDial == null) Debug.LogError("No dial attached");

        Vector3 lookForwardVector = speedometerDial.transform.forward;
        rotateAxis =  Quaternion.Euler(0,-90, 0) * lookForwardVector;

        Debug.LogWarning(lookForwardVector);
        Debug.LogWarning(rotateAxis);
    }

    private void FixedUpdate() => speedometerDial.transform.Rotate(speedometerDial.transform.forward, 0.1f);
}

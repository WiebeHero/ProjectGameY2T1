
using TerrainMovement;
using UnityEngine;

public sealed class Car : MonoBehaviour
{
    [SerializeField]
    private GameObject speedometerDial;
    
    public float speed;

    private void Start()
    {
        if (speedometerDial == null) Debug.LogError("No dial attached");
    }

    private void FixedUpdate()
    {
        speed += 0.01f;
        MovingTerrainManager.Speed = speed;
    }
}

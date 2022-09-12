
using TerrainMovement;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private GameObject speedometerDial;
    
    public float speed;

    private void Start()
    {
        if (speedometerDial == null) Debug.LogError("No dial attached");
        
        speed = 1.0f;
    }

    private void FixedUpdate()
    {
        speed += 1f;
        MovingTerrainManager.Speed = speed;
    }
}

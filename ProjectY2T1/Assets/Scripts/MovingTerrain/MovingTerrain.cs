using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTerrain : MonoBehaviour
{
    [SerializeField]
    protected GameObject god;
    [SerializeField]
    protected GameObject road;
    [SerializeField]

    [HideInInspector]
    protected MovingTerrainManager manager;


    public void Start()
    {
        if (this.god != null)
        {
            MovingTerrainManager manager = this.god.GetComponent<MovingTerrainManager>();
            if (manager != null)
            {
                this.manager = manager;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            if (this.manager != null)
            {
                List<GameObject> roads = this.manager.Roads;
                int roadCount = roads.Count;
                if (roads.Contains(this.gameObject))
                {
                    Debug.Log("Road removed!");
                    roads.Remove(this.gameObject);
                    MeshRenderer meshRenderer = this.road.GetComponent<MeshRenderer>();
                    Vector3 vector = this.transform.position;
                    Debug.Log(meshRenderer.bounds.size.x);
                    Debug.Log(roadCount);
                    Debug.Log(vector.x + meshRenderer.bounds.size.x * (float)roadCount);
                    vector.x = vector.x + meshRenderer.bounds.size.x * (float)roadCount;
                    this.transform.position = vector;
                    roads.Add(this.gameObject);
                }
            }
        }
    }

    public GameObject Road
    {
        get
        {
            return this.road;
        }
        set
        {
            this.road = value;
        }
    }
}

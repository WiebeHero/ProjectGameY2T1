using System.Collections.Generic;
using UnityEngine;

public class MovingTerrainManager : MonoBehaviour
{
    private List<GameObject> queue;

    private static float speed;
    private bool addRoad;

    [SerializeField]
    private Vector3 moveDirection;
        

    void Start()
    {
        this.queue = new List<GameObject>();
        this.queue.AddRange(GameObject.FindGameObjectsWithTag("Road"));
    }

    void FixedUpdate()
    {
        for (int i = 0; i < this.queue.Count; i++)
        {
            MovingTerrain terrainTemp = this.queue[i].GetComponent<MovingTerrain>();
            if (terrainTemp != null)
            {
                if (!terrainTemp.IsDisabled)
                {
                    this.queue[i].transform.position += moveDirection * speed;
                }
            }
        }
    }

    public static float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    public List<GameObject> Roads
    {
        get
        {
            return this.queue;
        }
        set
        {
            this.queue = value;
        }
    }
}

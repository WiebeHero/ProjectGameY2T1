using System.Collections.Generic;
using UnityEngine;

public class MovingTerrainManager : MonoBehaviour
{
    private List<GameObject> queue;

    private static float speed;
    private bool addRoad;
    private long timer;

    [SerializeField]
    private Vector3 moveDirection;
    [SerializeField]
    private GameObject roadWithSign;
        

    void Start()
    {
        this.timer = System.DateTimeOffset.Now.ToUnixTimeMilliseconds() + 15000L;
        this.queue = new List<GameObject>();
        this.queue.AddRange(GameObject.FindGameObjectsWithTag("Road"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < this.queue.Count; i++)
        {
            GameObject gameObject = this.queue[i];
            MovingTerrain terrain = gameObject.GetComponent<MovingTerrain>();
            if (terrain != null)
            {
                gameObject.transform.position += moveDirection * speed;
            }
            MovingTerrainTemp terrainTemp = gameObject.GetComponent<MovingTerrainTemp>();
            if (terrainTemp != null)
            {
                if (terrainTemp.IsDisabled)
                {
                    gameObject.transform.position += moveDirection * speed;
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

using System.Collections.Generic;
using UnityEngine;

public class MovingTerrainManager : MonoBehaviour
{
    private List<GameObject> queue;

    private static float speed;
    private bool end;

    [SerializeField]
    private Vector3 moveDirection;
        

    void Start()
    {
        queue = new List<GameObject>();
        queue.AddRange(GameObject.FindGameObjectsWithTag("Road"));
    }

    void FixedUpdate()
    {
        if (!end)
        {
            Debug.Log(Roads.Count);
            for (int i = 0; i < queue.Count; i++)
            {
                MovingTerrain terrainTemp = queue[i].GetComponent<MovingTerrain>();
                if (terrainTemp != null)
                {
                    if (!terrainTemp.IsDisabled)
                    {
                        queue[i].transform.position += moveDirection * speed;
                    }
                }
            }
        }
    }

    public static float Speed
    {
        get => speed;
        set => speed = value;
    }

    public List<GameObject> Roads
    {
        get => queue;
        set => queue = value;
    }

    public bool End
    {
        get => end;
        set => end = value;
    }
}

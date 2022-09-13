using System.Collections.Generic;
using UnityEngine;

public class MovingTerrain : MonoBehaviour
{
    public MovingTerrainManager manager;
    public GameObject god;
    public GameObject road;

    public SettingType settingType;
    public long timeToSpawn;
    public int spawnAfterTile;
    public bool removeOnDespawn, disabled, added;

    public void Start()
    {
        if (this.god != null)
        {
            MovingTerrainManager manager = this.god.GetComponent<MovingTerrainManager>();
            if (manager != null)
            {
                this.manager = manager;
                if (settingType == SettingType.SpawnAfterTimePassed)
                {
                    timeToSpawn = System.DateTimeOffset.Now.ToUnixTimeMilliseconds() + timeToSpawn;
                }
            }
        }
    }

    public void FixedUpdate()
    {
        if (!added && System.DateTimeOffset.Now.ToUnixTimeMilliseconds() >= this.timeToSpawn && timeToSpawn > 0)
        {
            added = true;
            List<GameObject> queue = manager.Roads;
            disabled = false;
            MeshRenderer meshRenderer = road.GetComponent<MeshRenderer>();
            GameObject last = queue[queue.Count - 1];
            gameObject.transform.position = last.transform.position;
            Vector3 vector = gameObject.transform.position;
            queue.Add(gameObject);
            vector.x = vector.x + meshRenderer.bounds.size.x;
            gameObject.transform.position = vector;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car") && (gameObject.CompareTag("Road") || gameObject.CompareTag("TerrainSign")))
        {
            if (manager != null)
            {
                List<GameObject> roads = manager.Roads;
                int roadCount = roads.Count;
                if (roads.Contains(gameObject))
                {
                    if (removeOnDespawn)
                    {
                        gameObject.SetActive(false);
                        roads.Remove(gameObject);
                    }
                    else
                    {
                        roads.Remove(gameObject);
                        MeshRenderer meshRenderer = road.GetComponent<MeshRenderer>();
                        Vector3 vector = transform.position;
                        vector.x = vector.x + meshRenderer.bounds.size.x * (float)roadCount;
                        transform.position = vector;
                        roads.Add(gameObject);
                    }
                }
            }
        }
        else if (gameObject.CompareTag("CrashSequence"))
        {
            if (manager != null)
            {
                manager.End = true;
            }
        }
    }
    public enum SettingType
    {
        None,
        SpawnAfterTimePassed,
        SpawnAfterTilesPassed
    }

    public bool IsDisabled
    {
        get
        {
            return this.disabled;
        }
        set
        {
            this.disabled = value;
        }
    }

    public bool RemoveOnDespawn
    {
        get => removeOnDespawn;
        set => this.removeOnDespawn = value;
    }

    public GameObject Road
    {
        get => road;
    }
}

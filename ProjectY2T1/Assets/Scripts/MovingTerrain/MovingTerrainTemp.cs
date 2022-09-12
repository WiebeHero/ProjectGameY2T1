using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTerrainTemp : MovingTerrain
{
    public long timeToSpawn;
    [SerializeField]
    private bool removeOnDespawn, disabled;

    public SpawnType spawnType;

    private bool added;

    public new void Start()
    {
        if (this.god != null)
        {
            MovingTerrainManager manager = this.god.GetComponent<MovingTerrainManager>();
            if (manager != null)
            {
                this.manager = manager;
                this.timeToSpawn = System.DateTimeOffset.Now.ToUnixTimeMilliseconds() + this.timeToSpawn;
            }
        }
    }

    public void FixedUpdate()
    {
        if (!added && System.DateTimeOffset.Now.ToUnixTimeMilliseconds() >= this.timeToSpawn)
        {
            added = true;
            List<GameObject> queue = this.manager.Roads;
            GameObject roadWithSign = GameObject.FindGameObjectWithTag("TerrainSign");
            MovingTerrainTemp terrain = roadWithSign.GetComponent<MovingTerrainTemp>();
            terrain.IsDisabled = false;
            MeshRenderer meshRenderer = terrain.Road.GetComponent<MeshRenderer>();
            GameObject last = queue[queue.Count - 1];
            roadWithSign.transform.position = last.transform.position;
            Vector3 vector = roadWithSign.transform.position;
            queue.Add(roadWithSign);
            vector.x = vector.x + meshRenderer.bounds.size.x;
            roadWithSign.transform.position = vector;
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
                    if (this.removeOnDespawn)
                    {
                        this.gameObject.SetActive(false);
                        roads.Remove(this.gameObject);
                    }
                    else
                    {
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
    }
    public enum SpawnType
    {
        Timer,
        Spawn_After_Tiles_Passed
    }

    public SpawnType GetSpawnType()
    {
        return this.spawnType;
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
        get
        {
            return this.removeOnDespawn;
        }
        set
        {
            this.removeOnDespawn = value;
        }
    }
}

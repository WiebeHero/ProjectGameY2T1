using System;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainMovement
{
    public sealed class MovingTerrain : MonoBehaviour
    {
        [SerializeField] private MovingTerrainManager manager;
        [SerializeField] private SettingType settingType;
        [SerializeField] private CarCrash carCrash;
        [SerializeField] private long timeToSpawn;
        [SerializeField] private int spawnAfterTile;

        public GameObject road;
        public bool removeOnDespawn, disabled, added;

        private MeshRenderer meshRenderer;

        
        public void Start()
        {
            if (manager == null) throw new Exception("No MovingTerrainManager attached");
            if (road == null) throw new Exception("No road is attached");
            if (carCrash == null) throw new Exception("MovingTerrain has no CarCrash script attached");

            meshRenderer = road.GetComponent<MeshRenderer>();
            if (meshRenderer == null) throw new Exception("The road doesn't have a mesh renderer");


            switch (settingType)
            {
                case SettingType.None:
                    break;
                case SettingType.SpawnAfterTimePassed:
                    timeToSpawn = DateTimeOffset.Now.ToUnixTimeMilliseconds() + timeToSpawn;
                    break;
                case SettingType.SpawnAfterTilesPassed:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void FixedUpdate()
        {
            if (added || DateTimeOffset.Now.ToUnixTimeMilliseconds()
                < timeToSpawn || timeToSpawn <= 0) return;
            
            added = true;
            disabled = false;
            
            List<GameObject> queue = manager.roads;
            Vector3 lastPosition = queue[^1].transform.position; //^1 means index from end, useful information for future]
            Debug.Log(lastPosition);
            lastPosition.x += meshRenderer.bounds.size.x;

            GameObject thisObject = gameObject;
            thisObject.transform.position = lastPosition;
            queue.Add(thisObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Car")) return; //Prevent collision with self 
            
            if (manager.roads.Contains(gameObject) && (gameObject.CompareTag("Road") || gameObject.CompareTag("TerrainSign")))
            {
                List<GameObject> roads = manager.roads;
                int roadCount = roads.Count; 
                if (removeOnDespawn)
                {
                    GameObject thisObject = gameObject;
                    thisObject.SetActive(false);
                    roads.Remove(thisObject);
                }
                else
                {
                    roads.Remove(gameObject);
                    
                    Vector3 vector = transform.position;
                    
                    #if UNITY_EDITOR
                        Debug.Log($"MeshRendderer sizeX: {meshRenderer.bounds.size.x}");
                        Debug.Log("Road Count: " + roadCount);
                        Debug.Log(vector.x + meshRenderer.bounds.size.x * roadCount);
                    #endif
                    
                    vector.x += meshRenderer.bounds.size.x * roadCount;
                    transform.position = vector;
                    roads.Add(gameObject);
                }
            }
            else if(gameObject.CompareTag("CrashSequence"))
            {
                carCrash.Run();
            }
        }
        public enum SettingType
        {
            None,
            SpawnAfterTimePassed,
            SpawnAfterTilesPassed
        }
    }
}

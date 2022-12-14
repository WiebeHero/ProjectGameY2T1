using System;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainMovement
{
    public class MovingTerrainManager : MonoBehaviour
    {
        [NonSerialized]
        public static MovingTerrainManager i;
        public static SpeedMode speedMode;
        public static bool canBrake;
        public static float recordedSpeed;
        public static float speed { set; get; }
        public static bool active { set; get; }
        
        private bool addRoad;
        private float tempSpeed;

        [SerializeField] private Vector3 moveDirection;
        
        public List<GameObject> roads { get; private set; }

        private float t;
        private bool first;
        
        private void Awake()
        {
            if (i != null && i != this) Destroy(this);
            i = this;
            canBrake = true;
        }

        private void Start()
        {
            active = true;
            roads = new List<GameObject>();
            roads.AddRange(GameObject.FindGameObjectsWithTag("Road"));
            if (roads.Count == 0) Debug.LogWarning("MovingTerrainManager hasn't found any roads in scene!");
            speedMode = SpeedMode.Normal;
            speed = 2.00F;
        }

        private void FixedUpdate()
        {
            if (!active) return;
            
            switch (speedMode)
            {
                case SpeedMode.Normal:
                    if (speed < 4.99 && canBrake) speed += 0.005F;
                    break;
                case SpeedMode.Slow:
                    if (!first)
                    {
                        first = true;
                        recordedSpeed = speed;
                        tempSpeed = 5.0F;
                    }

                    if (t <= 0.98F) t += recordedSpeed <= 4.0F ? 0.01F : 0.02F;
                    else t = 1.0F;
                    speed = Mathf.Lerp(recordedSpeed <= 4.0F ? recordedSpeed : tempSpeed, 0.05F, t);
                    if (speed <= 0.06F && recordedSpeed <= 4.0F) speedMode = SpeedMode.Frozen;
                    break;
                case SpeedMode.Frozen:
                    speed = 0.0F;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            foreach (GameObject road in roads)
            {
                MovingTerrain terrainTemp = road.GetComponent<MovingTerrain>();
                if (terrainTemp == null) continue;
                if (!terrainTemp.disabled) 
                    road.transform.position += moveDirection * speed;
            }
        }
        
        public enum SpeedMode
        {
            Normal,
            Slow,
            Frozen
        }
    }
}

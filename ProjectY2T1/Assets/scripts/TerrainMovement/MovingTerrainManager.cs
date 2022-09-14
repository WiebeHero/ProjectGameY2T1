using System.Collections.Generic;
using UnityEngine;

namespace TerrainMovement
{
    public class MovingTerrainManager : MonoBehaviour
    {
        public static SpeedMode speedMode;
        private static float speed;
        private bool addRoad;

        [SerializeField] private Vector3 moveDirection;
        
        public List<GameObject> roads { get; private set; }


        void Start()
        {
            roads = new List<GameObject>();
            roads.AddRange(GameObject.FindGameObjectsWithTag("Road"));
            speedMode = SpeedMode.Normal;
        }

        void FixedUpdate()
        {
            if (speedMode == SpeedMode.Normal)
            {
                if (speed < 4.99)
                {
                    speed += 0.005F;
                }
            }

            else if (speedMode == SpeedMode.Slow)
            {
                speed = Mathf.MoveTowards(speed, 0.05F, 0.07F);
            }

            for (int i = 0; i < roads.Count; i++)
            {
                MovingTerrain terrainTemp = roads[i].GetComponent<MovingTerrain>();
                if (terrainTemp != null)
                {
                    if (!terrainTemp.disabled)
                    {
                        roads[i].transform.position += moveDirection * speed;
                    }
                }
            }
        }

        public static float Speed
        {
            set => speed = value;
            get => speed;
        }


        public enum SpeedMode
        {
            Normal,
            Slow
        }
    }
}

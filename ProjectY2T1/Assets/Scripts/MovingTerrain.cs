using UnityEngine;

namespace Scenes
{
    public class MovingTerrain : MonoBehaviour
    {
        private static int amountOfRoads = 3;
        private static float speed;

        [SerializeField]
        private Vector3 moveDirection;
        [SerializeField]
        private GameObject road;
        

        void Start()
        {
            if (this.road == null)
            {
                Debug.LogWarning("No road has been taken into account for size! Assign a road to this script!");
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            this.transform.position += moveDirection * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Car"))
            {
                if (this.road != null)
                {
                    MeshRenderer meshRenderer = this.road.GetComponent<MeshRenderer>();
                    Vector3 vector = this.transform.position;
                    vector.x = vector.x + meshRenderer.bounds.size.x * amountOfRoads;
                    this.transform.position = vector;
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
    }
}

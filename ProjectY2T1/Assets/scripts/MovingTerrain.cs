using UnityEngine;

namespace Scenes
{
    public class MovingTerrain : MonoBehaviour
    {
        [SerializeField]
        private Vector3 moveDirection;

        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.position += moveDirection;

        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Test");
        }
    }
}

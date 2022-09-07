using UnityEngine;

namespace Scenes
{
    public class MovingTerrain : MonoBehaviour
    {
        [SerializeField]
        private Vector3 moveDirection;
        [SerializeField]
        private Vector3 moveTo;
        

        void Start()
        {
        
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            this.transform.position += moveDirection;

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("MainCamera"))
            {
                this.transform.position = moveTo;
            }
        }
    }
}

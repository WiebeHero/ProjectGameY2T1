using System;
using UnityEngine;

namespace Deprecated
{
    [Obsolete]
    public class SpeedOMeter : MonoBehaviour
    {
        [SerializeField]
        private GameObject carObject;

        private Car car;
        private void Start()
        {
            if (car == null) throw new Exception("No car assigned to Speedometer script");

            car = carObject.GetComponent<Car>();

            if (car == null) throw new Exception("Car has no Car script component"); 
        }

        private void FixedUpdate()
        {
            // Transform thisTransform = transform;
            // Quaternion quaternion = thisTransform.rotation;
            //
            // transform.Rotate(0,1,0,Space.Self);
            //
            // quaternion.y = car.speed;
            // thisTransform.rotation = quaternion;
        }
    }
}

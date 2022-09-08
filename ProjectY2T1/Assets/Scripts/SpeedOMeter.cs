using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedOMeter : MonoBehaviour
{
    [SerializeField]
    private GameObject carObject;

    private float speedPerHour;
    private Car car;

    private void Start()
    {
        if (this.carObject != null)
        {
            Car car = this.carObject.GetComponent<Car>();
            if (car != null)
            {
                this.car = car;
            }
            else
            {
                Debug.LogWarning("This car does not have a 'Car' script! Assign a car script to this object.");
            }
        }
        else
        {
            Debug.LogWarning("There is no car assigned to value 'carObject' assign the car to this script!");
        }
    }


    // Update is called once per frame
    void Update()
    {
        /*Quaternion quaternion = this.transform.rotation;
        quaternion.SetLookRotation(new Vector3(0, 0, 0), new Vector3(0, 0.3F, 0));
        this.transform.rotation = quaternion;*/
        
    }

    public float KMH
    {
        get
        {
            return this.speedPerHour;
        }
        set
        {
            this.speedPerHour = value;
            MovingTerrainManager.Speed = value;
        }
    }

    public Car Car
    {
        get
        {
            return this.car;
        }
        set
        {
            this.car = value;
        }
    }
}

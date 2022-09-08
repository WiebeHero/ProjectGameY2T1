using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private GameObject speedOMeterObject;
    
    private SpeedOMeter speedOMeter;

    void Start()
    {
        if (this.speedOMeterObject != null)
        {
            SpeedOMeter speed = this.speedOMeterObject.GetComponent<SpeedOMeter>();
            if (speed != null)
            {
                this.speedOMeter = speed;
            }
            else
            {
                Debug.LogWarning("The current 'speedOMeterObject' does not contain a 'speedOMeter' script!");
            }
        }
        else
        {
            Debug.LogWarning("The current car script does not contain a 'speedOMeterObject' assign it!");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.speedOMeter.KMH == 0.0F)
        {
            this.speedOMeter.KMH = 1.0F;
        }
        else
        {
            this.speedOMeter.KMH = this.speedOMeter.KMH += 0.0001F;
        }
    }
}

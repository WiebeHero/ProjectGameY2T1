using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;

public class ArtsieTextExperiment : MonoBehaviour
{
    public Volume theVolume;
    public Material theMaterial;
    public float transitionSpeed;

    bool schmoovin;
    float current;

    // Start is called before the first frame update
    void Start()
    {
        theMaterial.SetFloat("_Text", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (schmoovin && current <= 1)
        {
            theMaterial.SetFloat("_Text", current);
            theVolume.weight = current;
            current += Time.deltaTime / transitionSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            schmoovin = true;            
        }
    }

    public void StartSchmoovin()
    {
        schmoovin = true;
    }
}

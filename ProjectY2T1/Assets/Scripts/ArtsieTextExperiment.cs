using UnityEngine.Rendering;
using UnityEngine;

public class ArtsieTextExperiment : MonoBehaviour
{
    public Volume theVolume;
    public Material theMaterial;
    public float transitionSpeed;

    private bool schmoovin;
    private float current;
    private static readonly int Text = Shader.PropertyToID("_Text");

    private void Start() => theMaterial.SetFloat(Text, 0f);

    private void Update()
    {
        if (schmoovin && current <= 1)
        {
            theMaterial.SetFloat(Text, current);
            theVolume.weight = current;
            current += Time.deltaTime / transitionSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
            schmoovin = true;
    }

    public void StartSchmoovin() => schmoovin = true;
}

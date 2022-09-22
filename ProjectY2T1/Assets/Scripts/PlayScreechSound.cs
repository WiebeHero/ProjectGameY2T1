using UnityEngine;

public class PlayScreechSound : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    private bool played;
    
    public void PlayScreech(int i)
    {
        if (!played)
        {
            source.Play();
            played = true;
        }
    }
}

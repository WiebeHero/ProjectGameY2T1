using UnityEngine;

public sealed class TestScript : MonoBehaviour
{
	[SerializeField] private CarCrash carCrash;
	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.H))
		{
			carCrash.Initiate();
		}
	}
}
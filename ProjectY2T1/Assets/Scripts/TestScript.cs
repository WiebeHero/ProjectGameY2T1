using System;
using UnityEngine;

public class TestScript : MonoBehaviour
{ 
	public Timer timer;
	
	private void Update()
	{
		if (timer == null) throw new Exception("No timer found!");

		timer.Finished += Test;
		if (Input.GetKeyDown(KeyCode.H))
		{
			Debug.LogWarning("Started");
			timer.Run();
		}
	}

	private void Test() => Debug.LogWarning("Done!");
}
using UnityEngine;

public class MoveCarTest : MonoBehaviour
{
	private void Update() => transform.Translate(Vector3.forward * 0.1f);
}

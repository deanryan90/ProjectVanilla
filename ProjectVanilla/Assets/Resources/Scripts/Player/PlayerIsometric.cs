using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerIsometric : MonoBehaviour
{

	private Rigidbody2D rb;
	private float speed = 1f;
	private float amplitude = 3f;
	private Vector3 originalScale;

	private void Start()
	{
		//rb = GetComponent<Rigidbody2D>();
		originalScale = transform.localScale;
		
	}

	private void FixedUpdate()
	{
		
	}
}
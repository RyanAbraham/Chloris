using UnityEngine;
using System.Collections;

public class Done_BGScroller : MonoBehaviour
{
	private float scrollSpeed = 5;
	private float tileSizeZ = 1;

	private Vector3 startPosition;

	void Start ()
	{
		startPosition = transform.position;
	}

	void Update ()
	{
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}

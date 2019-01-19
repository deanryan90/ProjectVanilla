using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrdering : MonoBehaviour {

	private const int IsometricRangePerYUnit = 100;

	void Update()
	{
		var renderer = GetComponent<Renderer>();
		renderer.sortingOrder = -(int)(transform.position.y * IsometricRangePerYUnit);
	}
}

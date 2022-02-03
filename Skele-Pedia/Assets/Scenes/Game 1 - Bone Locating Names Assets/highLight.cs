using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highLight : MonoBehaviour
{
    public Color basicColor;
		public Color hoverColor = Color.magenta;
		public Renderer renderer;

		public Game1Runner g1;
 
	void Start() {
		g1 = FindObjectOfType<Game1Runner>();
		renderer = GetComponent<Renderer>();
		basicColor = renderer.material.color;
	}
 
	void OnMouseEnter() {
		renderer.material.color = hoverColor;
		g1.SetCurrentBone(gameObject.name);
	}
 
	void OnMouseExit() {
		renderer.material.color = basicColor;
	}
}

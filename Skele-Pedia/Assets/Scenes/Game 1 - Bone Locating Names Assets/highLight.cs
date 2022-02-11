using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highLight : MonoBehaviour
{
    public Color basicColor;
		public Color hoverColor = Color.magenta;
		public Renderer renderer;

		public Game1Runner g1;
		//public TriviaGame g3;
 
		public bool hover = true; //if its for highlighting or for when basic
		public bool simpleHighlight = true; //set this to false to make the glowy effect

		//the better looking, more advanced material - we can remove the color one later if needed.
		public Material basicMaterial;
		public Material hoverMaterial;

	void Start() {
		renderer = GetComponent<Renderer>();
		basicColor = renderer.material.color;
			
		g1 = FindObjectOfType<Game1Runner>();
			/*
			g3 = FindObjectOfType<TriviaGame>();
			if(g3!=null){ // if Game 3 exists, for now disable hover
				hover = false;
			}
			*/
	}
 
	void OnMouseEnter() {
			if(hover == true){
					highLightThis();

					if(g1!=null) //if this is even a game1runner game
						g1.SetCurrentBone(gameObject.name);
			}
	}
 
	void OnMouseExit() {
			if(hover == true){
					deHighLight();
			}
	}

	public void highLightThis(){ //made public so other scripts can use
			
			renderer.material.color = hoverColor;
			
	}
	public void deHighLight(){
			
			renderer.material.color = basicColor;
			
	}

	public void complexHighlight(){
			
	}
	public void complexDeHighlight(){
			
	}
	
}

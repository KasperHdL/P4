using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InformationHandler : MonoBehaviour {

	public 	Text text;
	public 	Text infoText;

	private Body body;
	private float screenWidth;
	private float screenHeight;

	private RectTransform rt;
	private float boxWidth;
	private float boxHeight;

	// Use this for initialization
	void Start () {
		screenWidth 	= Screen.width;
		screenHeight 	= Screen.height;
		rt 				= transform as RectTransform;
		boxWidth		= rt.rect.width;
		boxHeight 		= rt.rect.height;
	}
	
	// Update is called once per frame
	void Update () {
		if(body != null){
			setText();
			if(body.type == Body.Type.Planet)
				rt.sizeDelta = new Vector2(boxWidth, 56.5f);
			else
				rt.sizeDelta = new Vector2(boxWidth, 77.25f);
		}
	}

	public void setText(){
		if(body != null){
			if(body.type == Body.Type.DwarfStar){
				text.text = 
				"<color=lightblue>Type: </color>"			+ 
				"<color=lightblue>\nClass: </color>"		+
				"<color=lightblue>\nTemperature: </color>"	+
				"<color=lightblue>\nRadius: </color>"		+
				"<color=lightblue>\nMass: </color>"			+
				"<color=lightblue>\nMass: </color>"
				;

				infoText.text =
						body.type.ToString() 						+ 
				"\n" + 	body.classification							+
				"\n" + 	body.temperature.ToString("F0") 			+ " kelvin" +
				"\n" + 	body.radius.ToString("F0") 					+ " earths" +
				"\n" + 	body.mass.ToString("F1") 					+ " suns" 	+
				"\n" + 	(body.mass*333060.402).ToString("F0")		+ " earths"
				;
			} else{
				text.text = 
				"<color=lightblue>Type: </color>" 			+
				"<color=lightblue>\nRadius: </color>" 		+
				"<color=lightblue>\nMass: </color>"			+
				"<color=lightblue>\nMass: </color>" 	
				;

				infoText.text =
						body.type.ToString() 						+ 
				"\n" + 	body.radius 								+ " earths" +
				"\n" + 	(body.mass*0.0000030024584).ToString("F6") 	+ " suns"	+
				"\n" + 	body.mass									+ " earths"
				;
			}


		} else
			Debug.LogError("INFORMATIONHANDLER: BODY IS NOT INSTANTIATED");
	}

	public void setBody(Body body){
		this.body = body;
	}
}

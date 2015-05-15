using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InformationHandler : MonoBehaviour {

	public 	Text text;

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
		}
	}

	public void setText(){
		if(body != null){
			if(body.type == Body.Type.DwarfStar)
				text.text = 
				"Type: " 			+ body.type.ToString() 					+ 
				"\nTemperature: " 	+ body.temperature.ToString("F0") 		+ " kelvin" +
				"\nCategory: "		+ "categoryPlaceHolder"					+
				"\nRadius: " 		+ body.radius.ToString("F0") 			+ " earths" +
				"\nMass: " 			+ body.mass.ToString("F0") 				+ " earths" +
				"\nDensity: "		+ body.density 							
				;
			else
				text.text = 
				"Type: " 			+ body.type.ToString() 	+ 
				"\nRadius: " 		+ body.radius 			+ " earths" +
				"\nMass: " 			+ body.mass				+ " earths" +
				"\nDensity: "		+ body.density 			
				;


		} else
			Debug.LogError("INFORMATIONHANDLER: BODY IS NOT INSTANTIATED");
	}

	public void setBody(Body body){
		this.body = body;
	}
}

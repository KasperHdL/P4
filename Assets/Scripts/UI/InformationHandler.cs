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
				"Type: " 			+ body.type.ToString() 							+ 
				"\nCategory: "		+ "categoryPlaceHolder"							+
				"\nTemperature: " 	+ body.temperature.ToString("F0") 				+ " kelvin" +
				//"\nLuminosity: "	+ body.luminosity.ToString("F3")				+ " suns"	+
				"\nRadius: " 		+ body.radius.ToString("F0") 					+ " earths" +
				"\nMass: " 			+ body.mass.ToString("F1") 						+ " suns" 	+
				"\nMass: "			+ (body.mass*333060.402).ToString("F0")			+ " earths"	+
				"\nMass: "			+ (body.mass*333060.402*5972.19).ToString("F1")	+ " Yg" 	+
				"\nDensity: "		+ body.density 							
				;
			else
				text.text = 
				"Type: " 			+ body.type.ToString() 	+ 
				"\nRadius: " 		+ body.radius 			+ " earths" +
				"\nMass: " 			+ body.mass				+ " earths" +
				"\nDensity: "		+ body.density 			+
				"\nAcceleration: "	+ "accPlaceHolder"		+
				"\nvelocity: "		+ "velPlaceHolder"
				;


		} else
			Debug.LogError("INFORMATIONHANDLER: BODY IS NOT INSTANTIATED");
	}

	public void setBody(Body body){
		this.body = body;
	}
}

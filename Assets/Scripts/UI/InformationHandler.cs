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
			setPosition();
			setText();
		}
	}

	public void setText(){
		if(body != null){
			if(body.type == Body.Type.DwarfStar)
				text.text = 
				"Type: " 			+ body.type.ToString() 	+ 
				"\nTemperature: " 	+ body.temperature 		+ " kelvin" +
				"\nCategory: "		+ "categoryPlaceHolder"	+
				"\nRadius: " 		+ body.radius/100 		+ " suns" +
				"\nMass: " 			+ body.mass/100 		+ " suns" +
				"\nDensity: "		+ body.density 			+
				"\nRotation Speed: "+ body.rotSpeed
				;
			else
				text.text = 
				"Type: " 			+ body.type.ToString() 	+ 
				"\nRadius: " 		+ body.radius 			+ " earths" +
				"\nMass: " 			+ body.mass				+ " earths" +
				"\nDensity: "		+ body.density 			+
				"\nRotation Speed: "+ body.rotSpeed;
				;


		} else
			Debug.LogError("INFORMATIONHANDLER: BODY IS NOT INSTANTIATED");
	}

	public void setBody(Body body){
		this.body = body;
	}

	public void setPosition(){
		if(body != null){
			if(body.type == Body.Type.DwarfStar)
				transform.localPosition = new Vector3(body.radius/7+boxWidth/2, body.radius/7+boxHeight/2, 0);
			else
				transform.localPosition = new Vector3(body.transform.localScale.x/4+boxWidth/2, body.transform.localScale.y/4+boxHeight/2, 0);
		} else
			Debug.LogError("INFORMATIONHANDLER: BODY IS NOT INSTANTIATED");
	}
}

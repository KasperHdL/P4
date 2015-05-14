using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MassSlider : SliderWithText {

	public string unit = "";

	public new void Start(){
		base.Start();
		text.text = value + " kg";

	}

	public void updateExtremes(Body.Type type){
		switch(type){
			case Body.Type.Planet:
				maxValue = Settings.Planet.MASS_MAX_VALUE;
				minValue = Settings.Planet.MASS_MIN_VALUE;
				unit = "Earths";
			break;
		}
	}

	public override void ValueChanged(){
		if(controller != null)
			controller.updateMass(value);
		setText(value);
	}

	public override void setText(float val){
		text.text = val + " " + unit;
	}
}

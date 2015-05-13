using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadiusSlider : SliderWithText {

	private string unit = "";

	public new void Start(){
		base.Start();

	}

	public void updateExtremes(Body.Type type){
		switch(type){
			case Body.Type.Planet:
				minValue = Settings.Planet.RADIUS_MIN_VALUE;
				maxValue = Settings.Planet.RADIUS_MAX_VALUE;
				unit = Settings.Planet.RADIUS_UNIT;
			break;

			case Body.Type.DwarfStar:
				minValue = Settings.Star.Dwarf.RADIUS_MIN_VALUE;
				maxValue = Settings.Star.Dwarf.RADIUS_MAX_VALUE;
				unit = Settings.Star.Dwarf.RADIUS_UNIT;
			break;
		}
		setText(value);
	}

	public override void ValueChanged(){
		if(controller != null)
			controller.updateRadius(value);
		setText(value);
	}

	public override void setText(float val){

		text.text = val + " " + unit;

	}
}

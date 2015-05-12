using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadiusSlider : SliderWithText {

	private string unit = "";

	public new void Start(){
		base.Start();
		text.text = value + " Mm";

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
	}
	public override void OnDrag(PointerEventData eventData){
		controller.updateRadius(value);
		setText(value);
		base.OnDrag(eventData);
	}

	public void setText(float val){

		text.text = val + " " + unit;

	}
}

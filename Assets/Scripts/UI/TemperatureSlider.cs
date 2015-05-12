using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TemperatureSlider : SliderWithText {

	public new void Start(){
		base.Start();
		text.text = value + " Mm";
	}

	public void updateExtremes(Body.Type type){
		switch(type){
			case Body.Type.DwarfStar:
				maxValue = Settings.Star.Dwarf.TEMPERATURE[Settings.Star.Dwarf.TEMPERATURE.Length - 1];
				minValue = Settings.Star.Dwarf.TEMPERATURE[0];
			break;
		}
	}

	public override void OnDrag(PointerEventData eventData){
		controller.updateTemperature(value);
		setText(value);
		base.OnDrag(eventData);
	}

	public void setText(double val){
		text.text = val + "";
	}
}

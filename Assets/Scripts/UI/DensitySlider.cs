using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DensitySlider : SliderWithText {

	public new void Start(){
		base.Start();
		text.text = (int)value + " kg/m3";
	}

	public override void OnDrag(PointerEventData eventData){
		controller.updateDensity(value);
		if(value < 100)
			text.text = value + " kg/m3";
		else
			text.text = value + " kg/m3";

		base.OnDrag(eventData);
	}
}


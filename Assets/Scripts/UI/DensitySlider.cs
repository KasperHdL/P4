using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DensitySlider : SliderWithText {

	public new void Start(){
		base.Start();
//		text.text = (value/100)*maxVal + " kg/m3";

	}

	public override void OnDrag(PointerEventData eventData){
		//controller.updateDensity((value/100)*difVal);
		base.OnDrag(eventData);
	}

	public void setText(double val){
		if(val < 100)
			text.text = val + " kg/m3";
		else
			text.text = val + " kg/m3";
	}
}


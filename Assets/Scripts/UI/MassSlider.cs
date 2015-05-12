using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MassSlider : SliderWithText {

	public new void Start(){
		base.Start();
		text.text = value + " kg";

	}

	public void updateExtremes(Body.Type type){
		switch(type){
			case Body.Type.Planet:
				maxValue = Settings.Planet.MASS_MAX_VALUE;
				minValue = Settings.Planet.MASS_MIN_VALUE;
			break;
		}
	}

	public override void OnDrag(PointerEventData eventData){
		controller.updateMass(value);
		setText(value);
		base.OnDrag(eventData);
	}

	public void setText(double val){
		/*
		Teragram (Tg) 10e9
		Petagram (Pg) 10e12
		Exagram (Eg) 10e15
		Zettagram (Zg) 10e18
		Yottagram (Yg) 10e21
		*/
		if(val > Mathf.Pow(10,24)){
			text.text = valueFormating(30, 2, val) + "E+30 kg";
		} else if(val > Mathf.Pow(10,27)){
			text.text = valueFormating(27, 2, val) + "E+27 kg";
		} else if(val > Mathf.Pow(10,24)){
			text.text = valueFormating(24, 2, val) + "E+24 kg";
		} else if(val > Mathf.Pow(10,21)){
			text.text = valueFormating(21, 2, val) + "E+21 kg";
		} else if(val > Mathf.Pow(10, 18)){
			text.text = valueFormating(18, 2, val) + "E+18 kg";
		} else if(val > Mathf.Pow(10, 15)){
			text.text = valueFormating(15, 2, val) + "E+15 kg";
		} else if(val > Mathf.Pow(10, 12)){
			text.text = valueFormating(12, 2, val) + "E+12 kg";
		} else if(val > Mathf.Pow(10, 9)){
			text.text = valueFormating(9, 2, val) + "E+9 kg";
		}
	}
}

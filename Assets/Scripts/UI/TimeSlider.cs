using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TimeSlider : SliderWithText {

	public override void ValueChanged(){
		if(value == 0)
			Time.timeScale = 0;
		else
			Time.timeScale = value/10;
		setText(value);
	}

	public override void setText(float val){
		text.text = value.ToString("F1") + "x speed";
	}
}

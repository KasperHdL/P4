using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TimeSlider : SliderWithText {

	public override void ValueChanged(){
		Time.timeScale = value;
		setText(value);
	}

	public override void setText(float val){
		text.text = value.ToString("F1") + "x speed";
	}
}

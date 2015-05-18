using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TimeSlider : SliderWithText {

	private float lastTimeScale = 0;

	public override void ValueChanged(){
		if(value == 0)
			Time.timeScale = 0;
		else
			Time.timeScale = value/10;
		setText(value);
	}

	public void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			if(lastTimeScale == 0 && Time.timeScale != 0){
				lastTimeScale = Time.timeScale;
				Time.timeScale = 0;

			}else if(lastTimeScale != 0){
				Time.timeScale = lastTimeScale;
				lastTimeScale = 0;
			}
			value = Time.timeScale * 10;
		}
	}

	public override void setText(float val){
		text.text = value.ToString("F1") + "x speed";
	}
}

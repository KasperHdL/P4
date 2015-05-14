using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderWithText : Slider {

	public UIController controller;
	public Text text;

	public new void Start(){
		base.Start();
		onValueChanged.AddListener (delegate {ValueChanged();});
	}


	public virtual void setText(float value){}

	public virtual void ValueChanged(){
	}
}

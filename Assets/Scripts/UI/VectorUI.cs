using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VectorUI : MonoBehaviour{

	public Vector2 value;

	public UIController controller;

	public RectTransform line;
	public RectTransform head;

	void Start(){

	}

	void Update () {
        if (Input.GetMouseButton(0)){
			Vector2 input = Input.mousePosition;

			Vector2 screen = new Vector2(Screen.width,Screen.height);
			input -= new Vector2(screen.x,screen.y)/2;

			Vector2 lB = controller.leftButton.GetComponent<RectTransform>().sizeDelta;
			Vector2 rB = controller.rightButton.GetComponent<RectTransform>().sizeDelta;

			/*
			Debug.Log(string.Format("screen: ({0},{1}) - lB: ({2},{3}) - rB: ({4},{5}) - input: ({6},{7})",
				screen.x,screen.y,
				lB.x,lB.y,
				rB.x,rB.y,
				input.x,input.y));
			*/

			if(!((input.x < -screen.x/2 + lB.x || input.x > screen.x/2 - rB.x) && input.y > screen.y/2 - lB.y)){
				updateVelocity(input * 0.2f);
				controller.updateVelocity(value);
			}
		}
	}

	public void updateVelocity(Vector2 vel){
		value = vel;

		head.anchoredPosition = value;
		line.anchoredPosition = value/2;
		line.sizeDelta = new Vector2(vel.magnitude,10);
		float a = Vector2.Angle(new Vector2(1,0),vel);

		if(vel.y < 0)
			a = -a;

		line.localRotation = Quaternion.Euler(0,0,a);


	}
}

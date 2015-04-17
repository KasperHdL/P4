using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VectorUI : MonoBehaviour{

	public Vector2 velocity;
	public Vector2 center = new Vector2(158,106);

	public BodyController controller;

	public RectTransform line;
	public RectTransform head;

	void Update () {
		for(int i = 0;i<Input.touchCount;i++){
			Touch touch = Input.GetTouch(i);
			if((touch.position.x < 60 && touch.position.y > 180) ||
				(touch.position.x > 265 && touch.position.y > 180))
				continue;
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved){
				Vector2 input = touch.position;

				input -= center;
				updateVelocity(input);
			}
		}
	}

	public void updateVelocity(Vector2 vel){
		velocity = vel;

		head.anchoredPosition = velocity;
		line.anchoredPosition = velocity/2;
		line.sizeDelta = new Vector2(vel.magnitude,10);
		float a = Vector2.Angle(new Vector2(1,0),vel);

		if(vel.y < 0)
			a = -a;

		line.localRotation = Quaternion.Euler(0,0,a);


	}
}

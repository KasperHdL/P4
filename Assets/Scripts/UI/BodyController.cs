using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BodyController : MonoBehaviour {

	public Body body;

	public VectorUI velocity;

	public RadiusSlider radiusSlider;
	public MassSlider massSlider;
	public DensitySlider densitySlider;

	public Button leftButton;
	public Text leftButtonText;

	public Button rightButton;
	public Text rightButtonText;

	float canvasWidth;
	float canvasHeight;
	State state = 0;

	enum State{
		PropState,
		VeloState
	}

	// Use this for initialization
	void Start () {
		RectTransform objectRectTransform = gameObject.GetComponent<RectTransform> ();
		canvasWidth = objectRectTransform.rect.width;
		canvasHeight = objectRectTransform.rect.height;
		radiusSlider.controller = this;
		massSlider.controller = this;
		velocity.controller = this;
		densitySlider.controller = this;
		densitySlider.maxValue = massSlider.maxValue/(((4/3)*Mathf.PI)*Mathf.Pow(radiusSlider.minValue, 3));
		densitySlider.minValue = massSlider.minValue/(((4/3)*Mathf.PI)*Mathf.Pow(radiusSlider.maxValue, 3));


		radiusSlider.gameObject.SetActive(true);
		massSlider.gameObject.SetActive(true);
		velocity.gameObject.SetActive(false);

		leftButton.gameObject.SetActive(false);
		rightButton.gameObject.SetActive(true);

		updateMass(massSlider.value);
		updateRadius(radiusSlider.value);

		RectTransform rtL = leftButton.transform as RectTransform;
		rtL.anchoredPosition = new Vector2(0+rtL.rect.width/2, 0-rtL.rect.height/2);
		rtL.sizeDelta = new Vector2(50,30);

		RectTransform rt = rightButton.transform as RectTransform;
		rt.anchoredPosition = new Vector2(canvasWidth-rt.rect.width/2, 0-rt.rect.height/2);
		rt.sizeDelta = new Vector2(50,30);
	}

	public void handleButton(int i){
		//setup for setting the vel

		switch(state){
			case State.PropState:{
				state = State.VeloState;

				radiusSlider.gameObject.SetActive(false);
				massSlider.gameObject.SetActive(false);
				densitySlider.gameObject.SetActive(false);
				velocity.gameObject.SetActive(true);

				leftButton.gameObject.SetActive(true);
				rightButton.gameObject.SetActive(true);

				RectTransform rt = rightButton.transform as RectTransform;
				rt.anchoredPosition = new Vector2(canvasWidth-rt.rect.width/2,0-rt.rect.height/2);
				rt.sizeDelta = new Vector2(50,30);

				rightButtonText.text = "Finish";
			}break;
			case State.VeloState:{
				if(i == 1){
					//finish

					radiusSlider.gameObject.SetActive(false);
					massSlider.gameObject.SetActive(false);
					densitySlider.gameObject.SetActive(false);
					velocity.gameObject.SetActive(false);

					leftButton.gameObject.SetActive(false);
					rightButton.gameObject.SetActive(false);

					body.startVelocity = velocity.velocity;

				}else{
					state = State.PropState;

					radiusSlider.gameObject.SetActive(true);
					massSlider.gameObject.SetActive(true);
					densitySlider.gameObject.SetActive(true);
					velocity.gameObject.SetActive(false);

					leftButton.gameObject.SetActive(false);
					rightButton.gameObject.SetActive(true);

					RectTransform rt = rightButton.transform as RectTransform;
					rt.anchoredPosition = new Vector2(canvasWidth-rt.rect.width/2,0-rt.rect.height/2);
					rt.sizeDelta = new Vector2(40,30);

					rightButtonText.text = "Next";
				}

			}break;
		}

	}


	public void updateMass(float value){
		body.updateMass(value);
		float p = value/body.volume;
		updateDensity(p);
	}

	public void updateRadius(float value){
		body.updateRadius(value);
		updateVolume(value);
	}

	public void updateVolume(float r){
		float vol;
		vol = ((4/3)*Mathf.PI)*Mathf.Pow(r, 3);
		body.updateVolume(vol);
		float p = body.mass/vol;
		updateDensity(p);
	}

	public void updateDensity(float value){
		body.updateDensity(value);
		Debug.Log(value);
		densitySlider.value = value;
	}

}

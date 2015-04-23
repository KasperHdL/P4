using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BodyController : MonoBehaviour {

	private Camera cam;
	private CameraMovement cm;

	public Body body;

	public GravitySystem gs;

	public VectorUI velocity;

	public RadiusSlider radiusSlider;
	public MassSlider massSlider;
	public DensitySlider densitySlider;

	public Button leftButton;
	public Text leftButtonText;

	public Button rightButton;
	public Text rightButtonText;

	public float canvasWidth;
	public float canvasHeight;
	State state = 0;

	enum State{
		PropState,
		VeloState,
		SimState
	}

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		cm = cam.GetComponent<CameraMovement>();
		gs.uiHold = true;
		RectTransform objectRectTransform = gameObject.GetComponent<RectTransform> ();
		canvasWidth = objectRectTransform.rect.width;
		canvasHeight = objectRectTransform.rect.height;
		radiusSlider.controller = this;
		massSlider.controller = this;
		velocity.controller = this;

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

		setState(State.PropState);
	}


	private void setState(State s){
		switch(s){
			case State.PropState:{
				radiusSlider.gameObject.SetActive(true);
				massSlider.gameObject.SetActive(true);
				densitySlider.gameObject.SetActive(true);
				velocity.gameObject.SetActive(false);

				leftButton.gameObject.SetActive(false);
				rightButton.gameObject.SetActive(true);

				RectTransform rt = rightButton.transform as RectTransform;
				rt.anchoredPosition = new Vector2(canvasWidth-rt.rect.width/2,0-rt.rect.height/2);
				rt.sizeDelta = new Vector2(40,30);


	        	cm.allowMouseDrag = false;
	        	cm.setTarget(body.transform);

				rightButtonText.text = "Next";
			}break;
			case State.VeloState:{
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
			case State.SimState:{
				radiusSlider.gameObject.SetActive(false);
				massSlider.gameObject.SetActive(false);
				densitySlider.gameObject.SetActive(false);
				velocity.gameObject.SetActive(false);

				leftButton.gameObject.SetActive(false);
				rightButton.gameObject.SetActive(false);

				gs.reInitChildren();
				gs.uiHold = false;
	        	cm.allowMouseDrag = true;
	        	cm.setTarget(null);

			}break;
		}
		state = s;
	}

	public void handleButton(int i){
		//setup for setting the vel
		// 0 is left button - 1 is right button
		switch(state){
			case State.PropState:{
				setState(State.VeloState);
			}break;
			case State.VeloState:{
				if(i == 0)
					setState(State.PropState);
				else
					setState(State.SimState);
			}break;
		}

	}

	public void Update(){
		if(state == State.SimState && Input.GetMouseButtonDown(0)){
	        Ray ray = cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
	        RaycastHit hitinfo;
	        if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, 1<<8)){
				gs.uiHold = true;


	        	body = hitinfo.transform.GetComponent<Body>();
	        	setState(State.PropState);
	        }
		}
	}

	public void updateVelocity(Vector2 value){
		body.startVelocity = value;
		//update Dots
	}

	public void updateMass(double value){
		massSlider.setText(value);
		body.updateMass(value);
	}

	public void updateRadius(double value){
		radiusSlider.setText(value);
		body.updateRadius(value);
	}

	public void updateDensity(float value){
		body.updateDensity(value);
		densitySlider.value = value;
	}
}

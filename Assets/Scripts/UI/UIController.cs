using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour {

	public Camera cam;
	public CameraMovement cm;

	public Body body;

	public GravitySystem gs;

	public PlanetSwitcher planetSwitcher;

	public VectorUI velocity;

	public TimeSlider timeSlider;

	public RadiusSlider radiusSlider;
	public MassSlider massSlider;
	public DensitySlider densitySlider;
	public TemperatureSlider temperatureSlider;

	public GameObject typeSelector;

	public Button leftButton;
	public Text leftButtonText;

	public Button rightButton;
	public Text rightButtonText;

	private float canvasWidth;
	private float canvasHeight;

	public State state = State.SimState;
	public Body.Type selectedType = Body.Type.None;
	public ActiveSliders activeSliders;

	public float maxDistortion;

	public enum State{
		PropState,
		VeloState,
		SimState
	}


#region Unity Methods
	public void Start () {
		gs.uiHold = true;
		RectTransform objectRectTransform = gameObject.GetComponent<RectTransform> ();
		canvasWidth = objectRectTransform.rect.width;
		canvasHeight = objectRectTransform.rect.height;
		radiusSlider.controller = this;
		massSlider.controller = this;
		temperatureSlider.controller = this;
		velocity.controller = this;

		setState(state);

		maxDistortion = Settings.MAX_DISTORTION;
	}

	public void Update(){
		//Chose planet for entering propertyState
		if(Input.GetMouseButtonDown(0) && state == State.SimState){

			Vector2 input = Input.mousePosition;
			Vector2 screen = new Vector2(Screen.width,Screen.height);

	        Ray ray = cam.ScreenPointToRay(new Vector3(input.x, input.y, 0));
	        RaycastHit hitinfo;
	        if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, 1<<8)){
				gs.uiHold = true;

	        	body = hitinfo.transform.GetComponent<Body>();
	        	setBody(body);
	        }
		}
	}
#endregion
	public void newBody(Body body){
		planetSwitcher.newBody(body);
		setBody(body);
	}

	public void setBody(Body body){
		this.body = body;

		updateType(body.type);
		setState(State.PropState);

		updateMass(massSlider.value);
		updateRadius(radiusSlider.value);
		updateTemperature(temperatureSlider.value);
	}

	public void setState(State s){
		switch(s){
			case State.PropState:{
				updateActiveSliders(activeSliders);

				velocity.gameObject.SetActive(false);
				timeSlider.gameObject.SetActive(false);

				typeSelector.SetActive(true);

				leftButton.gameObject.SetActive(false);
				rightButton.gameObject.SetActive(true);

				RectTransform rt = rightButton.transform as RectTransform;
				rt.anchoredPosition = new Vector2(canvasWidth-rt.rect.width/2,0-rt.rect.height/2);
				rt.sizeDelta = new Vector2(40,30);


	        	cm.setTarget(body.transform);

				rightButtonText.text = "Next";
			}break;
			case State.VeloState:{
				updateActiveSliders(ActiveSliders.None);

				velocity.gameObject.SetActive(true);
				timeSlider.gameObject.SetActive(false);
				typeSelector.SetActive(false);

				leftButton.gameObject.SetActive(true);
				rightButton.gameObject.SetActive(true);

				RectTransform rt = rightButton.transform as RectTransform;
				rt.anchoredPosition = new Vector2(canvasWidth-rt.rect.width/2,0-rt.rect.height/2);
				rt.sizeDelta = new Vector2(50,30);

				rightButtonText.text = "Finish";
			}break;
			case State.SimState:{
				updateActiveSliders(ActiveSliders.None);
				typeSelector.SetActive(false);
				timeSlider.gameObject.SetActive(true);

				velocity.gameObject.SetActive(false);

				leftButton.gameObject.SetActive(false);
				rightButton.gameObject.SetActive(false);

				gs.uiHold = false;

			}break;
		}
		state = s;
	}



#region Button Handlers
	public void handleNavigationalButton(int i){
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

	public void handleTypeButton(int i ){
		updateType((Body.Type)i);
	}

#endregion


	private void updateActiveSliders(ActiveSliders activeSliders){
		//activate appropiate sliders
		radiusSlider.gameObject.SetActive((activeSliders & ActiveSliders.Radius) == ActiveSliders.Radius);
		massSlider.gameObject.SetActive((activeSliders & ActiveSliders.Mass) == ActiveSliders.Mass);
		//densitySlider.gameObject.SetActive((activeSliders & ActiveSliders.Density) == ActiveSliders.Density);
		temperatureSlider.gameObject.SetActive((activeSliders & ActiveSliders.Temperature) == ActiveSliders.Temperature);

		//update extremes
		radiusSlider.updateExtremes(selectedType);
		massSlider.updateExtremes(selectedType);
		temperatureSlider.updateExtremes(selectedType);

		//update location
		int c = 0;
		if(radiusSlider.gameObject.activeSelf){
			RectTransform rt = radiusSlider.transform as RectTransform;
			rt.anchoredPosition = new Vector2(-35  ,c++ * 40 + 20);
		}
		if(massSlider.gameObject.activeSelf){
			RectTransform rt = massSlider.transform as RectTransform;
			rt.anchoredPosition = new Vector2(-35  ,c++ * 40 + 20);
		}
		if(temperatureSlider.gameObject.activeSelf){
			RectTransform rt = temperatureSlider.transform as RectTransform;
			rt.anchoredPosition = new Vector2(-35  ,c++ * 40 + 20);
		}
	}

	private void updateType(Body.Type type){
		selectedType = type;
		body.setType(type);
		switch(type){
			case Body.Type.Planet:{
				activeSliders = Settings.Planet.SLIDERS;
				resetBodySoundValues(body);
			}break;
			case Body.Type.DwarfStar:{
				activeSliders = Settings.Star.Dwarf.SLIDERS;
				resetBodySoundValues(body);
			}break;
		}
		updateActiveSliders(activeSliders);
	}

#region Slider Calls

	public void updateVelocity(Vector2 value){
		body.startVelocity = new Vector3(value.x,0,value.y);
		body.construct();
		//update Dots
		gs.calcFuturePositions();
	}

	public void updateMass(double value){
		body.updateMass(value);
		gs.calcFuturePositions();
	}

	public void updateRadius(float value){
		body.updateRadius(value);
	}

	public void updateDensity(float value){
		body.updateDensity(value);
	}

	public void updateTemperature(float value){
		for(int i = 1;i<Settings.Star.Dwarf.TEMPERATURE.Length;i++){
			float ct = Settings.Star.Dwarf.TEMPERATURE[i];
			float lt = Settings.Star.Dwarf.TEMPERATURE[i-1];
			if(value < ct && value >= lt){
				//found
				float step = (value-lt)/(ct-lt);
				body.starLight.color = Color.Lerp(Settings.Star.Dwarf.COLORS[i-1],Settings.Star.Dwarf.COLORS[i],step);
				break;
			}
		}
		body.temperature = value;
	}
#endregion

	public void resetBodySoundValues(Body body){
		body.sound.volume = Settings.RESET_VOLUME;
		body.sound.pitch = Settings.RESET_PITCH;
	}

}

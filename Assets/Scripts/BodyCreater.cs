using UnityEngine;
using System.Collections;

public class BodyCreater : MonoBehaviour {

	GameObject canvas;
	private Camera cam;
	public GravitySystem gs;

	public Body body;

	public float 	halfWidth,
					halfHeight;



	// Use this for initialization
	void Start () {
		canvas = GameObject.Find("Canvas");
		halfWidth = Screen.width/2;
		halfHeight = Screen.height/2;
	}
	
	// Update is called once per frame
	void Update () {
		cam = Camera.main;
		
		if(Input.GetMouseButtonDown(1) && canvas.GetComponent<UIController>().state == UIController.State.SimState){
			createBodyAtPosition(Input.mousePosition);
			Debug.Log("x: " + Input.mousePosition.x + " y: " + Input.mousePosition);
		}
	}

	//Make new planet
	public void createBodyAtPosition(Vector2 input){
		Vector2 screen = new Vector2(Screen.width,Screen.height);

        Ray ray = cam.ScreenPointToRay(new Vector3(input.x, input.y, 0));
        Debug.DrawRay(ray.origin,ray.direction,Color.white,1f);
        RaycastHit hitinfo;

        Vector2 planetPosition = new Vector2((input.x-halfWidth)*100, (input.y-halfHeight)*100);

        body = Object.Instantiate(body, planetPosition, Quaternion.identity) as Body;
        body.transform.parent = gs.transform;

		gs.uiHold = true;
		GetComponent<CameraMovement>().setTarget(body.transform);
		canvas.GetComponent<UIController>().body = this.body;
    	canvas.GetComponent<UIController>().setState(UIController.State.PropState);
	}
}

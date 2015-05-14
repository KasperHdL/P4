using UnityEngine;
using System.Collections;

public class BodyCreator : MonoBehaviour {

	public UIController controller;
	private Camera cam;
	public GravitySystem gs;

	public CameraMovement cameraMovement;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(1) && controller.state == UIController.State.SimState){

			Ray ray = cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
	        RaycastHit hitinfo;
	        if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, 1<<8 | 1<<9)){
				createBodyAtPosition(hitinfo.point);
	        }
		}
	}

	//Make new planet
	public void createBodyAtPosition(Vector3 pos){
		pos.y = 0;

        GameObject g = Instantiate(gs.bodyPrefab, pos, Quaternion.identity) as GameObject;
        g.transform.parent = gs.transform;

        Body body = g.GetComponent<Body>();
        body.type = Body.Type.Planet;

		gs.uiHold = true;
		cameraMovement.setTarget(g.transform);
		controller.newBody(body);
    	gs.addBody(body);
	}
}

using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Camera cam;
	public GravitySystem gs;

	public Transform target;
	private Body body;
	public float offset;

	void Start(){
		cam.transform.rotation = Quaternion.LookRotation(-Vector3.up);
		if(target != null){
			body = target.GetComponent<Body>();
			setTarget(target);
		}
	}

	void Update () {
		if(target == null){
			cam.transform.localPosition = Vector3.up * offset;
		}else{
			transform.position = body.positions[(gs.uiHold ? 0 : (int)(Time.timeScale*10))];
			cam.transform.localPosition = Vector3.up * (offset + body.radius);
		}
	}

	public void setTarget(Transform t){
		//if(body != null)
			//body.sound.enabled = true;

		target = t;
		body = target.GetComponent<Body>();
		//body.sound.enabled = false;

		transform.position = target.position;
		cam.transform.localPosition = Vector3.up * (offset + body.radius);

	}
}

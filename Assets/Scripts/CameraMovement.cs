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
		body = target.GetComponent<Body>();
		setTarget(target);
	}

	void Update () {
		transform.position = target.position;
		cam.transform.localPosition = Vector3.up * (offset + body.radius);
	}

	public void setTarget(Transform t){
		target = t;
		body = target.GetComponent<Body>();
	}
}

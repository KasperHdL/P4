using UnityEngine;
using System.Collections;

public class THAW_Texture : MonoBehaviour {

	public static bool isCalibrated = false;
	private bool showingPosition = false;

	public Texture2D calibrate_texture;
	public Texture2D position_texture;
	public Color[] colors;


	// Use this for initialization
	void Start () {
		position_texture = new Texture2D(100,100);
		position_texture.filterMode = FilterMode.Point;

		colors = createTexture(position_texture.width,position_texture.height);

		position_texture.SetPixels(colors);
		position_texture.Apply();

		colors = new Color[4] {Color.black,Color.green,Color.red,Color.yellow};

		calibrate_texture = new Texture2D(2,2);
		calibrate_texture.filterMode = FilterMode.Point;
		calibrate_texture.SetPixels(colors);
		calibrate_texture.Apply();

		GetComponent<Renderer>().material.mainTexture = calibrate_texture;

	}

	void Update(){

		if(isCalibrated != showingPosition){
			if(isCalibrated){
				GetComponent<Renderer>().material.mainTexture = position_texture;
			}else{
				GetComponent<Renderer>().material.mainTexture = calibrate_texture;
			}
			isCalibrated = showingPosition;
		}
	}

	Color[] createTexture(int w,int h){
		Color[] colors = new Color[w*h];
		for(int i = 0;i<h;i++){
			for(int j = 0;j<w;j++){
				float r = (float)i/h;
				float g = (float)j/w;
				colors[i*w+j] = new Color(r,g,0f,1f);
			}
		}
		return colors;
	}

	public static void calibrationComplete(){
		isCalibrated = true;
	}


}

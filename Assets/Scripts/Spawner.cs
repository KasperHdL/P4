using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Spawner : MonoBehaviour {

	public GameObject low_pitch;
	public GameObject high_pitch;
	private GameObject obj;

	private readonly bool drawCube = true;


	public Transform direction;

	private bool roundStarted = false;

	private bool waitForRestart = false;

	public string directory = "D:/KasperHdL/Desktop/TestFiles/";
	private readonly string filenamePrepend = "participant_";
	private readonly string filenameAppend = ".txt";
	private int nextNum = 0;

	private readonly int maxRounds = 10;

	private int round = 0;
	private float[] correctAngles = new float[10];
	private float[] angles = new float[10];

	private float correctAngle;

	private float offset = 5;


	void Start(){
		init();
	}

	void init(){
		findNextFileNumber();
		round = 0;

		correctAngles = new float[10];
		angles = new float[10];
		roundStarted = false;
		waitForRestart = false;
	}

	void findNextFileNumber(){
		DirectoryInfo dir = new DirectoryInfo(directory);
		List<int> numbersUsed = new List<int>();

		foreach (FileInfo file in dir.GetFiles()){
			string name = file.Name;
			if(name.IndexOf(filenamePrepend) != -1 && name.IndexOf(filenameAppend) != -1){
				name = name.Substring(filenamePrepend.Length,name.Length-(filenameAppend.Length+filenamePrepend.Length));
				numbersUsed.Add(int.Parse(name));
			}
		}

		int max = 0;

		numbersUsed.Sort();

		foreach(int n in numbersUsed){
			if(n == max)
				max++;
		}

		nextNum = max;
	}

	// Update is called once per frame
	void Update () {
		if(roundStarted){
			//update and wait for click

	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

	        RaycastHit hitinfo;
	        if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, 1<<8)){
				Vector3 mPos = hitinfo.point;

				float angle = Vector3.Angle(Vector3.right,mPos);
				direction.LookAt(mPos);

				 if(Input.GetMouseButtonDown(0)){
		        	//clicked
				 	correctAngles[round-1] = correctAngle;

				 	angles[round-1] = (angle/180)*Mathf.PI * (mPos.y>0 ? -1:1);
				 	Destroy(obj);
				 	roundStarted = false;
		        }
	        }
		}else if(waitForRestart){
			if(Input.GetKeyDown(KeyCode.Space)){
				init();
			}
		}else{
			if(round == maxRounds){
				string s = "angle,\t\tcorrect angle;\r\n";
				for(int i = 0;i<maxRounds;i++){
					s += (angles[i] <0 ? "":" ");
					s += angles[i].ToString("F10") + ",\t";
					s += (correctAngles[i] <0 ? "":" ");
					s += correctAngles[i].ToString("F10") + ";\r\n";
				}
 				File.WriteAllText(directory + filenamePrepend + nextNum + filenameAppend, s);

 				findNextFileNumber();
 				waitForRestart = true;
			}else{
				newRound();
				roundStarted = true;
			}
		}



	}

	private void newRound(){
		float angle = UnityEngine.Random.Range(-Mathf.PI,Mathf.PI);

		correctAngle = angle;

		obj = Instantiate((UnityEngine.Random.Range(0,2) == 1 ? low_pitch:high_pitch), new Vector3(Mathf.Cos(angle)*offset,(drawCube ? 0:-10),Mathf.Sin(angle)*offset), Quaternion.identity) as GameObject;
		round++;
	}

}

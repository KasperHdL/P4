using UnityEngine;
using System.Collections.Generic;
using LibPDBinding;
using System.Runtime.InteropServices;
using System;
using System.IO;

public class lipd4unityTest : MonoBehaviour {

	// C# pointer stuff
	private GCHandle dataHandle;
	private IntPtr dataPtr;

	// Patch handle, create one for each patch
	private int SPatch;

	// Pd related
	private bool islibpdready;
	private int numberOfTicks;

	// Public, patch name
	public string nameOfPatch;
	bool banged = false;

	// Use this for initialization
	void Start () {
		nameOfPatch = "Radio.pd";
		
		PluginUtils.ResolvePath();

		// Delegate for 'print' 
//		LibPD.Print += ReceivePrint;

		// Follow this sequence of initialisation to avoid destruction of the universe
		if (!islibpdready) {
			if(openPd() == 0) {
				SPatch = loadPatch (nameOfPatch);
				LibPD.ComputeAudio (true);
			}
			else Debug.LogError("Error opening libpd");
		}

		//LibPD.OpenPatch(path + file_Name);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			banged = true;
		}
	}

	public void OnAudioFilterRead (float[] data, int channels){
		
		if(dataPtr == IntPtr.Zero)
		{
			dataHandle = GCHandle.Alloc(data,GCHandleType.Pinned);
			dataPtr = dataHandle.AddrOfPinnedObject();
		}
		
		if (islibpdready) {
			LibPD.Process(numberOfTicks, dataPtr, dataPtr);
		}

		if(banged){
			Debug.Log("Bang send: " + LibPD.SendBang("&0switch"));

			Debug.Log("HighPass send float: " + LibPD.SendFloat("&highPass", 260));
			
			Debug.Log("LowPass send float: " + LibPD.SendFloat("&lowPass", 260));
			
			Debug.Log("LowPass send float: " + LibPD.SendFloat("&volume", 1.5f));
			banged = false;
		}

	}

	// Initialise Pd with Unity's sample rate and calculate number of ticks. Returns 0 on success.
	public int openPd ()
	{
		int bufferSize;
		int noOfBuffers;
		AudioSettings.GetDSPBufferSize (out bufferSize, out noOfBuffers);
		
		// Calculate number of ticks for process callback
		numberOfTicks = bufferSize / LibPD.BlockSize;
		
		// Get Unity's sample rate
		int unitySR = AudioSettings.outputSampleRate;
		
		// Initialise Pd with 2 ins and outs and Unity's samplerate. Project dependent.
		int pdOpen = -1;
		pdOpen = LibPD.OpenAudio (2,2,48000);
		if (pdOpen == 0) islibpdready = true;
		
		return pdOpen;
	}

	public int loadPatch (string patchName)
	{
		string assetsPath = Application.streamingAssetsPath + "/PdAssets/";
		
		string path = assetsPath + patchName;
		
		// Android voodoo to load the patch. TODO: use Android APIs to copy whole folder?
		#if UNITY_ANDROID && !UNITY_EDITOR
		string patchJar = Application.persistentDataPath + "/" + patchName;
		
		if (File.Exists(patchJar))
		{
			Debug.Log("Patch already unpacked");
			File.Delete(patchJar);
			
			if (File.Exists(patchJar))
			{
				Debug.Log("Couldn't delete");				
			}
		}
		
		WWW dataStream = new WWW(path);
		
		
		// Hack to wait till download is done
		while(!dataStream.isDone) 
		{
		}
		
		if (!String.IsNullOrEmpty(dataStream.error))
		{
			Debug.Log("### WWW ERROR IN DATA STREAM:" + dataStream.error);
		}
		
		File.WriteAllBytes(patchJar, dataStream.bytes);
		
		path = patchJar;
		#endif

		Debug.Log("Loading patch:" + path);
		return LibPD.OpenPatch (path);
	}
}

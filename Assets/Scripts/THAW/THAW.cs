using System;
using UnityEngine;
using System.Collections.Generic;

public class THAW : MonoBehaviour
{
    /// <summary>
    /// Meta reference to the camera
    /// </summary>
    public Material CameraMaterial = null;

    /// <summary>
    /// The selected device index
    /// </summary>
    public int m_indexDevice = -1;
    private int m_currentIndex = -1;

    /// <summary>
    /// The web cam texture
    /// </summary>
    private WebCamTexture m_texture = null;

    public Color[] colors;
    private int colorIndex = 0;

    // Use this for initialization
    void Start()
    {
    	colors = new Color[4];
        if (null == CameraMaterial)
        {
            throw new ApplicationException("Missing camera material reference");
        }

        Application.RequestUserAuthorization(UserAuthorization.WebCam);
        setCamera();
    }

    public void setCamera(){
    	for(int i = 0;i<WebCamTexture.devices.Length;i++){
    		Debug.Log(i + ": " + WebCamTexture.devices[i].name);
    	}
    	if(m_indexDevice < 0 || m_indexDevice > WebCamTexture.devices.Length){
        	Debug.LogWarning("Index out of bounds");
            return;
    	}

    	var device = WebCamTexture.devices[m_indexDevice];
        if (string.IsNullOrEmpty(device.name))
        {
        	Debug.LogWarning("Illegal Camera index");
            return;
        }

        // stop playing
        if (null != m_texture)
        {
            if (m_texture.isPlaying)
            {
                m_texture.Stop();
            }
        }

        // destroy the old texture
        if (null != m_texture)
        {
            UnityEngine.Object.DestroyImmediate(m_texture, true);
        }

        m_currentIndex = m_indexDevice;

        // use the device name
        m_texture = new WebCamTexture(device.name);

        // start playing
        m_texture.Play();

        // assign the texture
        CameraMaterial.mainTexture = m_texture;
    }

    // Update is called once per frame
    private void Update()
    {
    	if(m_currentIndex != m_indexDevice){
    		setCamera();
    	}
        if (null != m_texture &&
            m_texture.didUpdateThisFrame){
            CameraMaterial.mainTexture = m_texture;
            if(THAW_Texture.isCalibrated){
            	calculatePosition(m_texture.GetPixels());
            }else if(Input.GetMouseButtonDown(0)){
            	colors[colorIndex++] = getColor(m_texture.GetPixels());
            	if(colorIndex == 4)
            		THAW_Texture.isCalibrated = true;
            }
        }
    }

    public void calculatePosition(Color[] colors){
    	Color c = getColor(colors);

    	Debug.Log(c);
    }

    public Color getColor(Color[] colors){
    	float r = 0,g = 0, b = 0;

    	for(int i = 0;i<colors.Length;i++){
    		r += colors[i].r;
    		g += colors[i].g;
    		b += colors[i].b;
    	}

    	r /= colors.Length;
    	g /= colors.Length;
    	b /= colors.Length;

    	return new Color(r,g,b);
    }
}

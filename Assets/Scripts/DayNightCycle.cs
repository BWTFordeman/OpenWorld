using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {
    public double dt = 0.0;
    public float secPerCycle = 1f;

    public Color fogDayColor;
    public Color fogNightColor;
    public Color targetColor;
    public Color currentColor;

    public float maxLight = 0.75f;
    public float minLight = 0f;
    public float currentLight = 0.75f;

    public float skyboxMax = 1.25f;
    public float skyboxMin = 0f;
    public float skyboxCurrent = 1.25f;

    public float sine;
    public float cosine;
    public float temp2;

    public Light sun;
    public Material skybox;

    public int sunState;

	// Use this for initialization
	void Start () {
        RenderSettings.fog = true;
	}
	
	// Update is called once per frame
	void Update () {
        dt += Time.deltaTime * Mathf.PI / (secPerCycle / 2f);
        sine = Mathf.Sin((float)dt);
        cosine = Mathf.Cos((float)dt);

        if ((sine > -1 && sine < 0) && (cosine > 0 && cosine < 1))
            sunState = 0;  
        if ((sine > 0 && sine < 1) && (cosine > 0 && cosine < 1))
            sunState = 1;   
        if ((sine > 0 && sine < 1) && (cosine > -1 && cosine < 0))
            sunState = 2;    
        if ((sine > -1 && sine < 0) && (cosine > -1 && cosine < 0))
            sunState = 3;  

        /// Day/Night cycle determination
        if (sunState == 1)
        {
            temp2 += (Time.deltaTime * Mathf.PI * 5f) / (secPerCycle / 2f);
            temp2 = Mathf.Clamp(temp2, 0, 1);

            currentLight = Mathf.Lerp(minLight, maxLight, temp2);
            skyboxCurrent = Mathf.Lerp(skyboxMin, skyboxMax, temp2);
            targetColor = fogDayColor;
        }
        else if (sunState == 2 && transform.eulerAngles.x < 20f)
        {
            temp2 -= (Time.deltaTime * Mathf.PI) / (secPerCycle / 2f);
            temp2 = Mathf.Clamp(temp2, 0, 1);

            currentLight = Mathf.Lerp(minLight, maxLight, temp2);
            skyboxCurrent = Mathf.Lerp(skyboxMin, skyboxMax, temp2);
        }
        else if (sunState == 3)
        {
            temp2 -= (Time.deltaTime * Mathf.PI * 2f) / (secPerCycle / 2f);
            temp2 = Mathf.Clamp(temp2, 0, 1);

            currentLight = Mathf.Lerp(minLight, maxLight, temp2);
            skyboxCurrent = Mathf.Lerp(skyboxMin, skyboxMax, temp2);
            targetColor = fogNightColor;
        }

        /// Updating based on which part of the day/night cycle
        transform.position = new Vector3(Mathf.Cos((float)dt), Mathf.Sin((float)dt), 0);
        transform.LookAt(Vector3.zero);

        sun.intensity = currentLight;
        skybox.SetFloat("_AtmosphereThickness", skyboxCurrent);

        /// Fog
        currentColor = RenderSettings.fogColor;
        RenderSettings.fogColor = Color.Lerp(currentColor, targetColor, 0.25f * Time.deltaTime);
	}

    float Interpolate(float a, float b, float factor)
    {
        return (1.0f - factor) * a + factor * b;
    }
}

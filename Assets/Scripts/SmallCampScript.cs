using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCampScript : MonoBehaviour {
    public AudioClip clip;
    public bool played = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!played)
            {
                SoundManager.instance.Fade(clip, 0.1f, 0.5f);
                played = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (SoundManager.instance.bgmSource.clip == clip)
            {
                played = false;
                SoundManager.instance.Fade(SoundManager.instance.defaultClip, 0.1f, 0.5f);
            }
        }
    }
}

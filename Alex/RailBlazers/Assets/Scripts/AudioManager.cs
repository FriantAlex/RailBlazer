using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public static AudioManager aud;

    public AudioSource audioSources;
    /*
    0 = Sound Effects
    1 = Looping Audio
    2 = BG Music
    */
    public int arrLength;
    public AudioClip[] audioClips = new AudioClip[5];
	public AudioClip[] hitClips = new AudioClip[5];
    public AudioClip hitSound;
    public AudioClip transformSound;
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void PlayTransformSound()
    {
        audioSources.clip = transformSound;
        audioSources.Play();
    }//ends PlaTransformSound()

    public void PlayHitSound()
    {
		audioSources.PlayOneShot(hitClips[ (int) Random.Range(0, hitClips.Length) ], 1f);
    }//ends PlayHitSound()

    public void PlaySound(int i)
    {
        if (i > audioClips.Length)
            return;

        audioSources.clip = audioClips[i];
        audioSources.PlayOneShot(audioClips[i],1f);
    }//ends PlaySound()

    void StopMusic()
    {
        //audioSources[2].Stop();
    }//StopMusic()

    void StartMusic()
    {
        //audioSources[2].Play();
    }//StartMusic()

}

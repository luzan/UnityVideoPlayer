using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


/// <summary>
/// Unity VideoPlayer Script for Unity 5.6 (currently in beta 0b11 as of March 15, 2017)
/// YouTube Video Link: 
/// StackOverflow Disscussion:
/// Code Contiburation: StackOverflow - Programmer
/// </summary>


public class StreamVideo : MonoBehaviour {

    public RawImage image;

    public VideoClip videoToPlay;

    private VideoPlayer videoPlayer;
    private VideoSource videoSource;

    private AudioSource audioSource;
	
    // Use this for initialization
	void Start () {
        Application.runInBackground = true;
        StartCoroutine(playVideo());
	}

    IEnumerator playVideo()
    {
      
        //Add VideoPlayer to the GameObject
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //Add AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        //We want to play from video clip not from url
        
        videoPlayer.source = VideoSource.VideoClip;

        // Vide clip from Url
        //videoPlayer.source = VideoSource.Url;
        //videoPlayer.url = "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4";


        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        //Wait until video is prepared
        WaitForSeconds waitTime = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            //Prepare/Wait for 5 sceonds only
            yield return waitTime;
            //Break out of the while loop after 5 seconds wait
            break;
        }

        Debug.Log("Done Preparing Video");

        //Assign the Texture from Video to RawImage to be displayed
        image.texture = videoPlayer.texture;

        //Play Video
        videoPlayer.Play();

        //Play Sound
        audioSource.Play();

        Debug.Log("Playing Video");
        while (videoPlayer.isPlaying)
        {
            Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }

        Debug.Log("Done Playing Video");
    }

    // Update is called once per frame
    void Update () {
		
	}
}

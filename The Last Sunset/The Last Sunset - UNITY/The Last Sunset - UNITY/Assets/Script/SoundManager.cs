using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public static AudioSource musicSource;
    [SerializeField] AudioClip musicaInicial;
    public int startingPitch = 1;

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.pitch = startingPitch;
    }

    private void Update()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.clip = musicaInicial;
            musicSource.Play();
            GameController.instancia.FitaTocando(false);
        }
    }

    public void RandomPlay(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Set the clip to the clip at our randomly chosen index.
        musicSource.clip = clips[randomIndex];

        //Play the clip.
        musicSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static bool MusicControllerCreated = false;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private int newAudioDelay;
    private int musicIndex = 0;
    private float waitTime;

    private void Awake()
    {
        if (!MusicControllerCreated)
        {
            MusicControllerCreated = true;
        }

        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(PlayBackgroundMusic());
    }

    IEnumerator PlayBackgroundMusic()
    {
        while (audioClips.Count > 0)
        {
            waitTime = audioClips[musicIndex].length + newAudioDelay;
            audioSource.PlayOneShot(audioClips[musicIndex]);
            musicIndex++;

            if (musicIndex >= audioClips.Count)
            {
                musicIndex = 0;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}

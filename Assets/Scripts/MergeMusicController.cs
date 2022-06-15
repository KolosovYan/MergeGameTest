using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeMusicController : MonoBehaviour
{
    [SerializeField] private CreationPlaceManager placeManager;
    [SerializeField] private MergeManager mergeManager;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        placeManager.OnObjectCreated += PlaySound;
    }

    public void PlaySound()
    {
            audioSource.Play(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeMusicController : MonoBehaviour
{
    [SerializeField] private MergeManager mergeManager;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        mergeManager.onMergeSucces += PlaySound;
    }

    public void PlaySound(int mergeLvl) // mergeLvl может быть использован для воспроизведения разных звуков для разного урвоня слияния.
    {
            audioSource.Play(0);
    }
}

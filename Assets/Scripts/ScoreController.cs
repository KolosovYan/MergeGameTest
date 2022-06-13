using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] private Text scoreText;
    [SerializeField] private MergeManager mergeManager;

    private void Awake()
    {
        mergeManager.onMergeSucces += ChangeScore;
        scoreText.text = "Score: " + score;
    }

    private void ChangeScore(int mergeLevel)
    {
        score += 5 * mergeLevel;
        scoreText.text = "Score: " + score;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreLimitText;
    [SerializeField] private MergeManager mergeManager;
    [SerializeField] private CreationPlaceManager placeManager;

    private void Awake()
    {
        mergeManager.onMergeSucces += ChangeScore;
        ChangeScoreText();
        ChangeScoreLimitText();
        ChangeUpgradeButtonSprite(0);
    }

    private int score = 0;
    [SerializeField] private int scoreLimit = 200;

    private void CheckScore()
    {
        if (score >= scoreLimit)
        {
            upgradeButton.interactable = true;
            scoreLimitText.text = "Ready";
        }
    }

    private void ChangeScore(int mergeLevel)
    {
        score += 5 * mergeLevel;
        ChangeScoreText();
        ChangeScoreLimitText();
        CheckScore();
    }

    [SerializeField] private List<Sprite> upgradeButtonSprites;
    [SerializeField] private Image upgradeButtonImage;
    private int upgradeButtonIndex = 0;

    public void OnUpgradeButtonPush()
    {
        scoreLimit += scoreLimit;
        upgradeButton.interactable = false;
        ChangeScoreLimitText();
        placeManager.ChangeCooldownTime();
        CheckScore();

        if (upgradeButtonIndex != upgradeButtonSprites.Count)
        {
            upgradeButtonIndex += 1;
            ChangeUpgradeButtonSprite(upgradeButtonIndex);
        }
    }

    private void ChangeScoreLimitText()
    {
        scoreLimitText.text = "Need " + (scoreLimit - score) + " score";
    }

    private void ChangeScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    private void ChangeUpgradeButtonSprite(int index)
    {
        upgradeButtonImage.sprite = upgradeButtonSprites[upgradeButtonIndex];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI scoreText = null;

    private ScoreManager scoreManager = null;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("Unable to find scoreManager");
        }
        if (scoreText == null)
        {
            Debug.LogError("No scoreText assigned");
        }
    }

    void Update()
    {
        if (scoreText == null)
        {
            return;
        }
        scoreText.text = scoreManager.GetScore().ToString();
    }
}

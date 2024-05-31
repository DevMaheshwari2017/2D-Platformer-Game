using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    private TextMeshProUGUI Score_Text;
    int Score = 0;
    private void Awake()
    {
        Score_Text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        IncreasingScoreInUI();
    }
    public void Update_Score(int increase )
    {
        Score += increase;
        IncreasingScoreInUI(); 
    }

    private void IncreasingScoreInUI()
    {
        Score_Text.text = "Score: " + Score;
    }
}

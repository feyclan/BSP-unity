using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI txt;
    
    public void AddXP(int toAdd)
    {
        score += toAdd;
        UpdateXP();
    }

    public void ResetXP()
    {
        
    }

    public void UpdateXP()
    {
        txt.text = $"Score: {score}";
    }
}

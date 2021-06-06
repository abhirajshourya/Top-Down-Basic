using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    GameSession gameSession;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }
    void Update()
    {
        scoreText.SetText(gameSession.GetScore().ToString());
    }
}

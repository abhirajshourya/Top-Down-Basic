using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHeath : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerHPText;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Health();
    }

    private void Health()
    {
        var healthText = "Health: " + player.GetPlayerHP().ToString();
        playerHPText.SetText(healthText);
    }
}

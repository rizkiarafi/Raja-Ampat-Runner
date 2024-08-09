using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static int numberOfCoins;
    private static float _batteryPower;

    public static float batteryPower
    {
        get { return _batteryPower; }
        set { _batteryPower = Mathf.Clamp(value, 0f, 1f); }
    }

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI batteryText;

    void Start()
    {
        numberOfCoins = 0;
        batteryPower = 1f;
    }

    private void Update()
    {
        coinText.text = "Coins: " + numberOfCoins;
        batteryText.text = (batteryPower * 100).ToString("F0") + "%";
    }
}
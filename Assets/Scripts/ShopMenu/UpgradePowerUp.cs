using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePowerUp : MonoBehaviour
{
    [SerializeField] MenuManager menuManagerScr;
    [SerializeField] List<PowerUpSO> powerUpSO;

    [SerializeField] GameObject[] levelBars;
    [SerializeField] TMP_Text[] costTexts;

    [SerializeField] Sprite filledLevelBar;


    private void Start()
    {
        for (int i = 0; i < powerUpSO.Count; i++)
        {
            powerUpSO[i].level = PlayerPrefs.GetInt("LevelIndex" + i, 1);
        }

        for (int i = 0; i < levelBars.Length; i++)
        {
            for (int j = 0; j < powerUpSO[i].level - 1; j++)
            {
                ChangeBarSprite(i, j);
            }

            ChangeCostText(i);
        }
    }

    public void LevelUpPowerUp(int index)
    {
        PowerUpSO powerUp = powerUpSO[index];
        if (menuManagerScr.GetNumberOfCoins() >= powerUp.cost[powerUp.level - 1])
        {
            if (powerUp.level >= 4) 
                UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;

            menuManagerScr.SubstractCoins(powerUp.cost[powerUp.level - 1]);
            powerUpSO[index].level += 1;
            SaveLevel(index);
            SetLevelBar(index);
        }
    }

    private void SetLevelBar(int index)
    {
        ChangeBarSprite(index, powerUpSO[index].level - 2);

        ChangeCostText(index);
    }

    private void SaveLevel(int index)
    {
        PlayerPrefs.SetInt("LevelIndex" + index, powerUpSO[index].level);
    }

    private void ChangeCostText(int i)
    {
        if (powerUpSO[i].level <= 4)
            costTexts[i].text = (powerUpSO[i].cost[powerUpSO[i].level - 1].ToString());
        else
        {
            costTexts[i].text = (powerUpSO[i].cost[powerUpSO.Count - 1].ToString());
            costTexts[i].GetComponentInParent<Button>().interactable = false;
        }
    }

    private void ChangeBarSprite(int i, int j)
    {
        Image[] levelBarsImage = levelBars[i].transform.GetComponentsInChildren<Image>();
        levelBarsImage[j].sprite = filledLevelBar;
    }
}

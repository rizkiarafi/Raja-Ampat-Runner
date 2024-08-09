using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private string coinKey = "NumberOfCoins";

    void Start()
    {
        UpdateCoinText();
    }

    // Method untuk mengambil jumlah koin dari PlayerPrefs
    public int GetNumberOfCoins()
    {
        return PlayerPrefs.GetInt(coinKey, 0);
    }

    // Method untuk menyimpan jumlah koin ke PlayerPrefs
    public void SaveNumberOfCoins(int numberOfCoins)
    {
        PlayerPrefs.SetInt(coinKey, numberOfCoins);
        PlayerPrefs.Save();
    }

    // Method untuk menambah jumlah koin dan menyimpannya
    public void AddCoins(int amount)
    {
        int currentCoins = GetNumberOfCoins();
        currentCoins += amount;
        SaveNumberOfCoins(currentCoins);

        // Pembaruan teks jumlah koin pada UI setelah ditambahkan
        UpdateCoinText();
    }

    public void SubstractCoins(int amount)
    {
        int currentCoins = GetNumberOfCoins();
        currentCoins -= amount;
        SaveNumberOfCoins(currentCoins);

        // Pembaruan teks jumlah koin pada UI setelah ditambahkan
        UpdateCoinText();
    }

    // Method untuk mengupdate teks jumlah koin pada UI
    public void UpdateCoinText()
    {
        coinText.text = "Coins: " + GetNumberOfCoins().ToString();
    }

    // Method untuk menyetel jumlah koin menjadi 0
    public void ResetCoins()
    {
        SaveNumberOfCoins(0);
        UpdateCoinText();
    }
}

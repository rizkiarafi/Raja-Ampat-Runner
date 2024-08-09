using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingManager : MonoBehaviour
{
    public float initialIntensity = 2f; // Intensitas awal cahaya
    public float decreaseRate = 0.0001f; // Tingkat pengurangan intensitas per detik

    private Light2D playerLight; // Komponen Light2D dari objek player
    private PlayerManager playerManager; // Persentase baterai, 1 artinya penuh, 0 artinya habis

    void Start()
    {
        playerLight = GetComponentInChildren<Light2D>();
        playerManager = FindObjectOfType<PlayerManager>();
        playerLight.intensity = initialIntensity;
    }

    void Update()
    {
            PlayerManager.batteryPower -= decreaseRate * Time.deltaTime;

            PlayerManager.batteryPower = Mathf.Clamp01(PlayerManager.batteryPower);

            playerLight.intensity = initialIntensity * PlayerManager.batteryPower;
    }
}

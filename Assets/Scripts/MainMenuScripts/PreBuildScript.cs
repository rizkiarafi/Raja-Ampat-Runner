using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreBuildScript : MonoBehaviour
{
    void Awake()
    {
        FindObjectOfType<MenuManager>().ResetCoins();
    }
}

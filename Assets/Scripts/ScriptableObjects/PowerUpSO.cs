using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Power Up", menuName = "Power Up")]
public class PowerUpSO : ScriptableObject
{
    public string title;
    public int index;
    public int level;
    public float[] durationOrStack;
    public int[] cost;
    public Sprite sprite;
}

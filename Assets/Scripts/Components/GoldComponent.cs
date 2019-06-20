using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GoldComponent : MonoBehaviour {
    [HideInInspector] public int score;
    [HideInInspector] public int roomsCleared;

    public GameObject scoreText;
    public GameObject roomText;
}
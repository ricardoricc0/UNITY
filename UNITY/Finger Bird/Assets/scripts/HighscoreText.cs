﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class HighscoreText : MonoBehaviour
{
   Text score;

	void OnEnable() {
		score = GetComponent<Text>();
		score.text = "High Score: " +PlayerPrefs.GetInt("HighScore").ToString();
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    Text score;

	void OnEnable() {
		score = GetComponent<Text>();
		score.text = "Score: " + GameManager.Instance.scoreText;
	}
   
}

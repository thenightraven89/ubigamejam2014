﻿using UnityEngine;
using System.Collections;

public class GameEndedState : GameState
{
	public override void TransitionIn()
	{
        Debug.Log("qq");
		Time.timeScale = 0f;
        WaveManager.instance.SetGameOverText();
	}

	public override void TransitionOut()
	{
		Time.timeScale = 1f;
	}
}

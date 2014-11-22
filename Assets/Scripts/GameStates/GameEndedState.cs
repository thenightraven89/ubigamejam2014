using UnityEngine;
using System.Collections;

public class GameEndedState : GameState
{
	public override void TransitionIn()
	{
		Time.timeScale = 0f;
	}

	public override void TransitionOut()
	{
		Time.timeScale = 1f;
	}
}

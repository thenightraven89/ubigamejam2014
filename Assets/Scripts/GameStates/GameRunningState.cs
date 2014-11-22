using UnityEngine;
using System.Collections;

public class GameRunningState : GameState {
	private string sceneName;
	public GameRunningState(string sceneName):base()
	{
		this.sceneName = sceneName;
	}

	public override void TransitionIn()
	{
		Application.LoadLevel(sceneName);
	}

	public override void TransitionOut()
	{

	}
}

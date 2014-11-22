using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {
	public void OnClick()
	{
		GameManager.Instance.State = new GameRunningState("Main");
	}
}

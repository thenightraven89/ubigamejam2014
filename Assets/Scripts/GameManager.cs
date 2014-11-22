using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static GameManager instance;
	private GameState currentState;

	public static GameManager Instance { get{return instance;} }
	public GameState State 
	{
		get { return currentState; }
		set
		{
			SwitchState(value);
		}
	}

	void Awake()
	{
		instance = this;
		DontDestroyOnLoad(this.gameObject);
	}

	public void SwitchState(GameState state)
	{
		if(currentState != null)
		{
			currentState.TransitionOut();
		}
		currentState = state;
		currentState.TransitionIn();
	}
}

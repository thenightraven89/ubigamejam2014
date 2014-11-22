using UnityEngine;
using System.Collections;

public abstract class GameState
{
	public abstract void TransitionIn();
	public abstract void TransitionOut();
}

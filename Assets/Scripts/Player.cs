using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float keepEnemyInRange = .5f;
	[HideInInspector]
	public float initRange;
	public int hitPoints = 100;

	void Start()
	{
		initRange = keepEnemyInRange;
	}

	public void TakeDamage(int amount)
	{
		hitPoints = hitPoints - amount;
		if(hitPoints <= 0)
		{
			GameManager.Instance.State = new GameEndedState();
		}
	}

	public void Heal(int amount)
	{
		hitPoints += amount;
		if(hitPoints > 100)
		{
			hitPoints = 100;
		}
	}
}

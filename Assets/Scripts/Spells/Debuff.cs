using UnityEngine;
using System.Collections;

public abstract class Debuff 
{
	protected Enemy targetEnemy;
	protected float ttl;

	public float TimeToLive 
	{
		get {return ttl;}
		set {ttl = value;}
	}
	public bool RanOut {get { return ttl <=0f; } }

	public Debuff(float timeToLive, Enemy targetEnemy = null)
	{
		this.targetEnemy = targetEnemy;
		this.ttl = timeToLive;
	}

	public void DecreaseTtl(float value)
	{
		ttl-=value;
	}

	public void SetTarget(Enemy target)
	{
		targetEnemy = target;
	}

	public abstract void Apply();
	public abstract void Unapply();
}

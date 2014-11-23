using UnityEngine;
using System.Collections;

public class FreezeDebuff : Debuff 
{
	private float speedModifier;
	public FreezeDebuff(float ttl, float speedModifier, Enemy target = null):base(ttl, target)
	{
		this.speedModifier=speedModifier;
	}

	public override void Apply ()
	{
		targetEnemy.currentSpeedModifier = speedModifier;
		Material mat = targetEnemy.GetComponentInChildren<Renderer>().material;
		Color col = mat.color;
		Color newCol = new Color(col.r-0.4f, col.g-0.4f, col.b+0.1f);
		mat.SetColor("_Color", newCol);
	}

	public override void Unapply ()
	{
		targetEnemy.currentSpeedModifier = 1f;
        Material mat = targetEnemy.GetComponentInChildren<Renderer>().material;
		Color col = mat.color;
		Color newCol = new Color(col.r+0.4f, col.g+0.4f, col.b-0.1f);
		mat.SetColor("_Color", newCol);
	}
}

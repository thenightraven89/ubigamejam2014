using UnityEngine;
using System.Collections;

public class FrostNova : MonoBehaviour {

	public int damage = 5;
	public float radius = 15f;
	public float speedModifier = 0.5f;
	public float freezeTime = 5f;
	public float spreadTime = 0.5f;
	//private FreezeDebuff debuff;

	void Start () 
	{
		LeanTween.scale(gameObject, new Vector3(radius,radius,radius), spreadTime, new object[]{"onComplete", "DestroyNova"});
	}
	
	void DestroyNova()
	{
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Enemy"))
		{
			FreezeDebuff debuff = new FreezeDebuff(freezeTime, speedModifier);
			Enemy cmp = col.transform.GetComponent<Enemy>();
			cmp.ApplyDebuff(debuff);
			cmp.TakeDamage(damage);
		}
	}
}

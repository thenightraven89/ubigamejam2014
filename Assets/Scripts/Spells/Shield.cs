using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	
	public float radius = 10f;
	public float spreadTime = 0.2f;
	public float timeToLive = 10f;
	//private FreezeDebuff debuff;

	float range;
	Player pl;
	void Start () 
	{
		LeanTween.scale(gameObject, new Vector3(radius,radius,radius), spreadTime);
		StartCoroutine(DestroyShield());
		pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		range = pl.keepEnemyInRange;
		pl.keepEnemyInRange = radius-4f;
	}
	
	IEnumerator DestroyShield()
	{
		yield return new WaitForSeconds(timeToLive);
		pl.keepEnemyInRange = range;
		Destroy(gameObject);
	}

	void Update()
	{
		transform.position = pl.transform.position;
	}
}

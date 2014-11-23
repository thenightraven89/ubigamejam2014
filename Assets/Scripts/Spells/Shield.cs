using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	public static float stack = 0;
	public float radius = 10f;
	public float spreadTime = 0.2f;
	public float timeToLive = 10f;
	//private FreezeDebuff debuff;

	Player pl;
	void Start () 
	{
		stack++;
		LeanTween.scale(gameObject, new Vector3(radius,radius,radius), spreadTime);
		StartCoroutine(DestroyShield());
		pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		pl.keepEnemyInRange = radius-4f;
	}
	
	IEnumerator DestroyShield()
	{
		yield return new WaitForSeconds(timeToLive);
		stack--;
		if(stack<=0)
		{
			stack = 0;
			pl.keepEnemyInRange = pl.initRange;
		}
		Destroy(gameObject);
	}

	void Update()
	{
		transform.position = pl.transform.position;
	}
}

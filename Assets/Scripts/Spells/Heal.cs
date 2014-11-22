using UnityEngine;
using System.Collections;

public class Heal : MonoBehaviour {
	public int healAmount = 10;
	public float timeToLive = 7f;
	private Transform player;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player").transform;
		StartCoroutine(DestroyAfterTime());
		player.GetComponent<Player>().Heal(healAmount);
	}

	IEnumerator DestroyAfterTime()
	{
		yield return new WaitForSeconds(timeToLive);
		Destroy(gameObject);
	}

	void Update()
	{
		transform.position = player.position;
	}
}

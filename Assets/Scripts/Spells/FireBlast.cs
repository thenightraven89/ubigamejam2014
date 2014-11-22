using UnityEngine;
using System.Collections;

public class FireBlast : MonoBehaviour {
	public int damage = 20;
	public float speed = 25f;
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = transform.position + transform.forward * Time.deltaTime * speed;
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Enemy"))
		{
			col.transform.GetComponent<Enemy>().TakeDamage(damage);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}

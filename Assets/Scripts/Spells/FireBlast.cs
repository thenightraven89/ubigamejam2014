using UnityEngine;
using System.Collections;

public class FireBlast : MonoBehaviour {
	public int damage = 20;
	public float speed = 25f;

	void Start()
	{
		GameObject.Find("Main Camera").GetComponent<CameraShake>().shake = .1f;
	}

	// Update is called once per frame
	void Update () 
	{
		if(renderer.enabled)
		{
			transform.position = transform.position + transform.forward * Time.deltaTime * speed;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Enemy"))
		{
			col.transform.GetComponent<Enemy>().TakeDamage(damage);
		}
		else
		{
			StartCoroutine(DestroyDelay());
		}
	}

	IEnumerator DestroyDelay()
	{
		renderer.enabled = false;
		ParticleSystem[] sys = GetComponentsInChildren<ParticleSystem>();
		foreach(var s in sys)
			s.enableEmission = false;
		yield return new WaitForSeconds(10f);
		Destroy (gameObject);
	}
}

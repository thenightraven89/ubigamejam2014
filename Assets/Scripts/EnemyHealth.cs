using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	private float initialScale;
	public Enemy enm;
	private int initialHP;
	// Use this for initialization
	void Start () {
		initialScale = transform.localScale.x;
		initialHP = enm.hitPoints;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(enm.hitPoints == initialHP)
		{
			renderer.enabled = false;
		}
		else
		{
			renderer.enabled = true;
			float val = enm.hitPoints * initialScale / initialHP;
			transform.localScale = new Vector3(val,transform.localScale.y, transform.localScale.z);
		}
	}
}

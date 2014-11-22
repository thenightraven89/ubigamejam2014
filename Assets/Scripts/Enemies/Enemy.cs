using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Transform navTarget;
	// Use this for initialization
	void Start () {
		GetComponent<NavMeshAgent>().SetDestination(navTarget.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

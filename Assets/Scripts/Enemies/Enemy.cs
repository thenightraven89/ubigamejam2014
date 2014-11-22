using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	[HideInInspector]
	public Transform targetPrefab;
	public int healthPoints = 100;
	public float walkSpeed = 2.5f;
	public float runSpeed = 4.0f;
	public float maximumRoamRange = 1.5f;
	public float minimumRoamRange = -1.5f;
	public float roamMinTime = 0.5f;
	public float roamMaxTime = 2.0f;
	private Vector3 targetPosition;
	private float roamWaitTime;
	private EnemyState state;
	private NavMeshAgent agent;
	private Player playerComponent;
	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		SetState(EnemyState.Idle);
		StartCoroutine(ChangeRoamPosition());
	}

	IEnumerator ChangeRoamPosition()
	{
		yield return new WaitForSeconds(Random.Range(2f, 4f));
		while(true)
		{
			roamWaitTime = Random.Range (roamMinTime, roamMaxTime);
			if(state == EnemyState.Idle)
			{
				targetPosition = transform.position + new Vector3(Random.Range (minimumRoamRange,maximumRoamRange), 
				                             					  0, 
				                                                  Random.Range (minimumRoamRange,maximumRoamRange));
				
				agent.SetDestination(targetPosition);
			}
			yield return new WaitForSeconds(roamWaitTime);
		}
	}

	void FixedUpdate () 
	{
		if(state == EnemyState.Attacking)
		{
			Vector3 dir = (targetPrefab.position - transform.position).normalized * playerComponent.keepEnemyInRange;
			targetPosition = targetPrefab.position - dir;
			agent.SetDestination(targetPosition);
		}
	}

	void SetState(EnemyState newState)
	{
		if(newState == EnemyState.Idle)
			agent.speed = walkSpeed;
		if(newState == EnemyState.Attacking)
			agent.speed = runSpeed;
		state = newState;
	}

	void OnTriggerEnter(Collider cld)
	{
		if(cld.CompareTag("Player"))
		{
			targetPrefab = cld.transform;
			playerComponent = targetPrefab.GetComponent<Player>();
			SetState(EnemyState.Attacking);
		}
	}

	public enum EnemyState
	{
		Idle,
		Attacking
	}
}

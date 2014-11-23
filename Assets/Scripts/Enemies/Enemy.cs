using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enemy : MonoBehaviour {
	[HideInInspector]
	public Transform targetPrefab;
	public int hitPoints = 100;
	public float walkSpeed = 2.5f;
	public float runSpeed = 4.0f;
	public float maximumRoamRange = 1.5f;
	public float minimumRoamRange = -1.5f;
	public float roamMinTime = 0.5f;
	public float roamMaxTime = 2.0f;
	public float attackCooldown = 1.0f;
	public int attackDamage = 2;
	private Vector3 targetPosition;
	private float roamWaitTime;
	private EnemyState state;
	private NavMeshAgent agent;
	private Player playerComponent;
	private List<Debuff> debuffs;
	private float attackDistance;
	private float currentSpeed;

	public float currentSpeedModifier = 1f;
	// Use this for initialization
	void Start () 
	{
		currentSpeed = walkSpeed;
		agent = GetComponent<NavMeshAgent>();
		SetState(EnemyState.Idle);
		StartCoroutine(ChangeRoamPosition());
		debuffs = new List<Debuff>();
		attackDistance = GameObject.FindWithTag("Player").GetComponent<Player>().initRange;
	}

	IEnumerator ChangeRoamPosition()
	{
		//yield return new WaitForSeconds(Random.Range(2f, 4f));
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
		agent.speed = currentSpeed * currentSpeedModifier;
		if(state == EnemyState.Attacking)
		{
			Vector3 dir = (targetPrefab.position - transform.position).normalized * playerComponent.keepEnemyInRange;
			targetPosition = targetPrefab.position - dir;
			agent.SetDestination(targetPosition);
		}
	}

	void Update()
	{
		//I know, for in update, but it only executes if there are debuffs, so it is cool.
		for(int i=0; i<debuffs.Count; i++)
		{
			debuffs[i].DecreaseTtl(Time.deltaTime);
			if(debuffs[i].RanOut)
			{
				debuffs[i].Unapply();
				debuffs.RemoveAt(i);
			}
		}
	}

	Debuff CheckIfDebuffExists(Debuff nd)
	{
		foreach(var d in debuffs)
		{
			if(nd.GetType() == d.GetType())
				return d;
		}
		return null;
	}

	public void ApplyDebuff(Debuff newDebuff)
	{
		Debuff old = CheckIfDebuffExists(newDebuff);
		if(old == null)
		{
			newDebuff.SetTarget(this);
			debuffs.Add(newDebuff);
			newDebuff.Apply();
		}
		else
		{
			old.TimeToLive = newDebuff.TimeToLive;
		}
	}

	void SetState(EnemyState newState)
	{
		if(newState == EnemyState.Idle)
			currentSpeed = walkSpeed;
		if(newState == EnemyState.Attacking)
			currentSpeed = runSpeed;
		if(newState == EnemyState.Dead)
			Destroy(gameObject);
		state = newState;
	}

	void OnTriggerEnter(Collider cld)
	{
		if(cld.CompareTag("Player"))
		{
			targetPrefab = cld.transform;
			playerComponent = targetPrefab.GetComponent<Player>();
			SetState(EnemyState.Attacking);
			StartCoroutine(Attack());
		}
	}

	IEnumerator Attack()
	{
		while(state == EnemyState.Attacking)
		{
			float distance = Vector3.Distance(targetPrefab.position, transform.position);
			if(distance <= attackDistance+0.3f)
			{
				playerComponent.TakeDamage(attackDamage);
				Transform child = transform.GetChild(1);
				child.animation.Play ("Attack");
				child.animation.PlayQueued("Walk");
				yield return new WaitForSeconds(attackCooldown);
			}
			yield return new WaitForEndOfFrame();
		}
	}

	public void TakeDamage(int amount)
	{
		hitPoints = hitPoints - amount;
		if(hitPoints <= 0)
		{
			SetState(EnemyState.Dead);
		}
	}

	public enum EnemyState
	{
		Idle,
		Attacking,
		Dead
	}
}

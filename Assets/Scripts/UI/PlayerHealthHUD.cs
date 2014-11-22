using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthHUD : MonoBehaviour {

	private Player playerComponent;
	private Slider sliderComponent;
	// Use this for initialization
	void Start () 
	{
		playerComponent = FindObjectOfType(typeof(Player)) as Player;
		sliderComponent = GetComponent<Slider>();
	}

	void FixedUpdate () 
	{
		sliderComponent.value = playerComponent.hitPoints;
	}
}

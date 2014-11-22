using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public GameObject rotator;
	public float speed = 3.5f;
	public float rotateSpeed = 1f;
	public bool limitDiagonalSpeed = true;
	private CharacterController controller;

	void Start()
	{
		controller = GetComponent<CharacterController>();
	}

    void Update()
    {
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		Vector3 newRotation = rotator.transform.rotation.eulerAngles;
		if(Mathf.Abs(inputX) > 0.1f || Mathf.Abs(inputY) > 0.1f)
		{
			newRotation = new Vector3(inputX, 0, inputY).normalized;
		}
		if(newRotation != rotator.transform.rotation.eulerAngles)
		{
			rotator.transform.rotation = Quaternion.Slerp(rotator.transform.rotation, 
			                                              Quaternion.LookRotation(newRotation), 
			                                              Time.time * rotateSpeed);
		}
		// If both horizontal and vertical are used simultaneously, limit speed (if allowed), so the total doesn't exceed normal move speed
		float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f && limitDiagonalSpeed)? .7071f : 1.0f;
		Vector3 moveDirection = new Vector3(inputX * inputModifyFactor, 0f, inputY * inputModifyFactor) * speed;
		controller.Move(moveDirection * Time.deltaTime);
	}
}
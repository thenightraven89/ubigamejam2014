using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public Transform rotator;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rotator.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        if (Input.GetKey(KeyCode.S))
        {
            rotator.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        if (Input.GetKey(KeyCode.A))
        {
            rotator.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
        }

        if (Input.GetKey(KeyCode.D))
        {
            rotator.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }

    }
}
using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public Transform rotator;

    // true if we are expecting the player to press the left foot
    private bool leftFoot = true;
    private float stepTime = 0.5f;
    private float stepLength = 1f;

    private bool alreadyMoving = false;

    private bool canScheduleNextStep = true;

    void Update()
    {
        #region turn

        if (Input.GetKeyDown(KeyCode.W))
        {
            rotator.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            rotator.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            rotator.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            rotator.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }

        #endregion

        #region move

        bool doMove = false;

        if (leftFoot)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                doMove = true;
                
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                doMove = true;
            }
        }

        if (doMove && !alreadyMoving)
        {
            LeanTween.move(gameObject, gameObject.transform.position + rotator.forward * stepLength, stepTime, new object[] { "onComplete", "UnlockMovement", "onCompleteTarget", gameObject });
            leftFoot = !leftFoot;
            alreadyMoving = true;
        }

        #endregion move

    }

    private void UnlockMovement()
    {
        Debug.Log("been here");
        alreadyMoving = false;
    }
}
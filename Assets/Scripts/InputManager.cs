using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public GameObject rotator;

    private float rotateTime = 0.5f;

    // true if we are expecting the player to press the left foot
    private bool leftFoot = true;
    private float stepTime = 0.5f;
    private float stepLength = 2f;

    private bool alreadyMoving = false;

    private bool canScheduleNextStep = true;

    private Vector3 currentDirection = Vector3.forward;

    public ParticleSystem stumbleParticles;

    void Update()
    {
        #region turn

        if (Input.GetKeyDown(KeyCode.W))
        {
            LeanTween.rotate(rotator, new Vector3(0, 0, 0), rotateTime);
            currentDirection = new Vector3(0, 0, 1);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            LeanTween.rotate(rotator, new Vector3(0, 180, 0), rotateTime);
            currentDirection = new Vector3(0, 0, -1);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            LeanTween.rotate(rotator, new Vector3(0, 270, 0), rotateTime);
            currentDirection = new Vector3(-1, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            LeanTween.rotate(rotator, new Vector3(0, 90, 0), rotateTime);
            currentDirection = new Vector3(1, 0, 0);
        }

        #endregion

        #region move

        bool doMove = false;

        if (leftFoot)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                doMove = true;                
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                doMove = true;
            }
        }

        if (doMove && !alreadyMoving)
        {
            // then we can move
            LeanTween.move(gameObject, gameObject.transform.position + currentDirection * stepLength, stepTime, new object[] { "onComplete", "UnlockMovement", "onCompleteTarget", gameObject });
            leftFoot = !leftFoot;
            alreadyMoving = true;
        }
        else
        {
            if (doMove)
            {
                // then we will stumble
                isStumbled = true;
                stumbleParticles.enableEmission = true;
                StartCoroutine(RecoverFromStumble());
            }
        }
        #endregion move
    }

    private bool isStumbled = false;
    private float stumbleTime = 3f;

    private void UnlockMovement()
    {
        Debug.Log("been here");
        alreadyMoving = false;
    }

    private IEnumerator RecoverFromStumble()
    {
        yield return new WaitForSeconds(stumbleTime);
        isStumbled = false;
        stumbleParticles.enableEmission = false;
        yield return null;
    }
}
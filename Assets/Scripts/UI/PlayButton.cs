using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour
{
    public void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.Instance.State = new GameRunningState("Main");
        }
    }
}

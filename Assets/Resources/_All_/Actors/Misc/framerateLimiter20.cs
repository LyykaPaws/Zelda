using UnityEngine;
using System.Collections;

public class framerateLimiter20 : MonoBehaviour {

    void Awake()
    {
        Application.targetFrameRate = 20;   //Application targetFrameRate = 20 means 20fps
    }
}
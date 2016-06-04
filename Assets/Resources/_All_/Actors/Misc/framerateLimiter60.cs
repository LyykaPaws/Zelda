using UnityEngine;
using System.Collections;

public class framerateLimiter60 : MonoBehaviour {

    void Awake()
        {
          Application.targetFrameRate = 60;   //Application targetFrameRate = 60 means 60fps
        }
}
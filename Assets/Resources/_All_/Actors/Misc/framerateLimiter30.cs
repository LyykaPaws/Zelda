using UnityEngine;
using System.Collections;

public class framerateLimiter30 : MonoBehaviour {

    void Awake()
        {
            Application.targetFrameRate = 30;   //Application.targetFrameRate = 30 means 30fps
        }
}
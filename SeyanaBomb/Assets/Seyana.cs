using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seyana : MonoBehaviour
{
    bool isGrip;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrip)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isGrip = false;
            } 
            else
            {
                var screenPos = Input.mousePosition;
                screenPos.z = Mathf.Abs(mainCamera.transform.position.z);
                transform.position = mainCamera.ScreenToWorldPoint(screenPos);
            }

        }
    }

    public void Grip()
    {
        isGrip = true;
    }
}

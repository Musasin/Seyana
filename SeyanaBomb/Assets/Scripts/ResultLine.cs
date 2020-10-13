using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RectTransform>().localPosition = new Vector2(transform.localPosition.x, 113.4f - (40 * StaticValues.GetNowEnding()));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryText : MonoBehaviour
{
    public float storySpeed;
    public float storySizeZ;

    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.GetComponent<RectTransform>().anchoredPosition;

    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<RectTransform>().anchoredPosition.y < storySizeZ)
        {
            this.GetComponent<RectTransform>().anchoredPosition += Vector2.up * Time.deltaTime * storySpeed;
        }
        else
        {
            this.GetComponent<RectTransform>().anchoredPosition = startPos;
        }
    }
}

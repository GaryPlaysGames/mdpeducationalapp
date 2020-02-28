using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanObjectToToss : MonoBehaviour
{
    public GameObject camera;

    public Sprite chipBag;
    public Sprite[] crayons;
    public Sprite emptyBottle;
    public Sprite compost;

    [HideInInspector]
    public bool spanSprite;

    // Use this for initialization
    void Start ()
    {
        spanSprite = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (spanSprite)
        {
            switch (camera.GetComponent<GrabTrash>().phase)
            {
                case 1:
                case 5:
                case 7:
                    spanTrash();
                    break;
                case 2:
                case 4:
                    spanRecycling();
                    break;
                case 3:
                case 6:
                    spanCompost();
                    break;
            }
            spanSprite = false;
        }
    }

    void spanTrash()
    {
        GetComponent<SpriteRenderer>().sprite = chipBag;
    }

    void spanRecycling()
    {
        if(Random.Range(0.0f, 1.0f) <= 0.5f)
        {
            GetComponent<SpriteRenderer>().sprite = crayons[Random.Range(0, crayons.Length)];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = emptyBottle;
        }   
    }

    void spanCompost()
    {
        GetComponent<SpriteRenderer>().sprite = compost;
    }
}

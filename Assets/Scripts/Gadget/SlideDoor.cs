using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoor : Gadget
{
    public bool isOpen = false;

    public float Speed;
    public Transform OpenPoint;
    public Transform ClosePoint;
    private Vector3 currentPosition;

    private float moveDistance;
    private float startTime;

    public override void Action()
    {
        isOpen = !isOpen;

        currentPosition = transform.position;
        if (isOpen)
        {
            moveDistance = Vector3.Distance(OpenPoint.position, currentPosition);
        }
        else
        {
            moveDistance = Vector3.Distance(currentPosition, ClosePoint.position);
        }

        startTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = !isOpen;
        Action();
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = (Time.time - startTime) * Speed;
        if (distCovered <= 0)
            return;
            
        float fractionOfJourney = distCovered / moveDistance;
        if (!isOpen)
        {
            transform.position = Vector3.Lerp(currentPosition, ClosePoint.position, fractionOfJourney);
        }
        else
        {
            transform.position = Vector3.Lerp(currentPosition, OpenPoint.position, fractionOfJourney);
        }
    }
}
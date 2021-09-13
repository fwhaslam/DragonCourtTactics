using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateCamera : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // +1 or -1, positive turns right.
    public int plusOneForRight;

    //Detect current clicks on the GameObject (the one with the script attached)
    public void OnPointerDown(PointerEventData pointerEventData) {

        MainCameraHandler.AddTurning( plusOneForRight );
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData) {

        MainCameraHandler.AddTurning( -plusOneForRight );
    }

}

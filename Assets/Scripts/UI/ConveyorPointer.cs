using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorPointer : MonoBehaviour
{
    //private Transform targetTransform;
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;

    private void Awake()
    {
        //targetTransform = GameObject.FindGameObjectWithTag("Conveyor").transform;
        targetPosition = GameObject.FindGameObjectWithTag("Conveyor").transform.position;
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;

        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;

        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width ||
            targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;
        Debug.Log(isOffScreen + " " + targetPositionScreenPoint);

        if (isOffScreen)
        {
            int borderSize = 50;

            //pointerRectTransform.gameObject.SetActive(true);

            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize)
            {
                cappedTargetScreenPosition.x = borderSize;
            }
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize)
            {
                cappedTargetScreenPosition.x = Screen.width - borderSize;
            }
            if (cappedTargetScreenPosition.y <= borderSize)
            {
                cappedTargetScreenPosition.y = borderSize;
            }
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize)
            {
                cappedTargetScreenPosition.y = Screen.height - borderSize;
            }

            pointerRectTransform.position = cappedTargetScreenPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
        else
        {
            pointerRectTransform.position = targetPositionScreenPoint;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
    }
}

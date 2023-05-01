using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorPointer : MonoBehaviour
{
    [SerializeField]
    private List<Properties> convoyerProperties;

    [SerializeField]
    private List<Transform> targetTransform;

    [SerializeField]
    private List<RectTransform> pointerRectTransform;

    private int borderSize = 50;

    private void Update()
    {
        int i;
        for (i = 0; i < targetTransform.Count; i++)
        {
            if (convoyerProperties[i].HasBoxOn())
            {
                Vector3 toPosition = targetTransform[i].position;
                Vector3 fromPosition = Camera.main.transform.position;
                fromPosition.z = 0f;
                Vector3 dir = (toPosition - fromPosition).normalized;

                float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;

                pointerRectTransform[i].localEulerAngles = new Vector3(0, 0, angle);

                Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(toPosition);
                bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width ||
                    targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;

                if (isOffScreen)
                {
                    pointerRectTransform[i].gameObject.SetActive(true);

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

                    pointerRectTransform[i].position = cappedTargetScreenPosition;
                    pointerRectTransform[i].localPosition = new Vector3(pointerRectTransform[i].localPosition.x, pointerRectTransform[i].localPosition.y, 0f);
                }
                else
                {
                    pointerRectTransform[i].gameObject.SetActive(false);

                    //pointerRectTransform.position = targetPositionScreenPoint;
                    //pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
                }
            }
            else
            {
                pointerRectTransform[i].gameObject.SetActive(false);
            }
        }
        while (i < pointerRectTransform.Count)
        {
            pointerRectTransform[i].gameObject.SetActive(false);
            i++;
        }
    }
}

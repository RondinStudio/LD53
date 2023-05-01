using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalPointer : MonoBehaviour
{
    [SerializeField]
    private List<Transform> targetTransform;

    [SerializeField]
    private List<GoalController> goalControllers;

    [SerializeField]
    private RectTransform pointerRectTransform;

    private GrabCollisionDetector grabCollisionDetector;

    private int borderSize = 100;

    private void Start()
    {
        grabCollisionDetector = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GrabCollisionDetector>();
    }

    private void Update()
    {
        if (grabCollisionDetector.GetConnectedObject() != null)
        {
            BoxValues boxValues = grabCollisionDetector.GetConnectedObject().GetComponent<BoxController>().boxValues;

            Color arrowColor;
            switch(boxValues.color)
            {
                case EColor.Red:
                    arrowColor = new Color(228/255f, 59 / 255f, 68 / 255f);
                    break;
                case EColor.Green:
                    arrowColor = new Color(62 / 255f, 137 / 255f, 72 / 255f);
                    break;
                case EColor.Blue:
                    arrowColor = new Color(0 / 255f, 153 / 255f, 219 / 255f);
                    break;
                default:
                    arrowColor = new Color(62 / 255f, 137 / 255f, 72 / 255f);
                    break;
            }

            int indexColor = 0;
            for (int index = 0; index < goalControllers.Count; index++)
            {
                if (goalControllers[index].goalValues.color == boxValues.color)
                {
                    indexColor = index;
                    break;
                }
            }

            pointerRectTransform.gameObject.GetComponent<Image>().color = arrowColor;

            Vector3 toPosition = targetTransform[indexColor].position;
            Vector3 fromPosition = Camera.main.transform.position;
            fromPosition.z = 0f;
            Vector3 dir = (toPosition - fromPosition).normalized;

            float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;

            pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

            Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(toPosition);
            bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width ||
                targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;

            if (isOffScreen)
            {
                pointerRectTransform.gameObject.SetActive(true);

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
                pointerRectTransform.gameObject.SetActive(false);
            }
        }
        else
        {
            pointerRectTransform.gameObject.SetActive(false);
        }
    }
}

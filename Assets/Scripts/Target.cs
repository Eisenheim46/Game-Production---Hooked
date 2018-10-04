using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    public enum BillboardMode { lookAt, allign };
    public BillboardMode billboardMode;

    [SerializeField] private float targetSize = 1f;
    [SerializeField] private float maxHookDistance = 15f;
    [SerializeField] private Color nearTargetColor;
    [SerializeField] private Color nearWrongTargetColor;
    [SerializeField] private Color farTargetColor;
    [SerializeField] private GameObject HookOrigin;

    private float distance;

    private void LateUpdate()
    {
        if (billboardMode == BillboardMode.lookAt)
            transform.LookAt(Camera.main.transform.position, -Vector3.up);
        if (billboardMode == BillboardMode.allign)
            transform.forward = mainCamera.transform.forward;

        distance = Vector3.Distance(mainCamera.position, transform.position);
        transform.localScale = new Vector3(targetSize/100 * distance, targetSize/100 * distance, targetSize/100 * distance);

        if (distance <= maxHookDistance)
        {
            if (HookOrigin.GetComponent<HookOrigin>().IsWooden)
            {
                GetComponent<SpriteRenderer>().color = nearTargetColor;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = nearWrongTargetColor;
            }
            
        }
        else
        {
            GetComponent<SpriteRenderer>().color = farTargetColor;
        }
    }
}

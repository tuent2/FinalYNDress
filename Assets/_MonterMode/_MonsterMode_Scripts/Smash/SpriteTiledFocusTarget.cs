using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTiledFocusTarget : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Transform targetPointObject;
    private void Update()
    {
        var distanceToTarget = Vector2.Distance(transform.position, targetPointObject.position);
        distanceToTarget *= Mathf.Abs(transform.lossyScale.x) + .5f;//GMan .4f Origin
        var size = spriteRenderer.size;
        size.x = distanceToTarget;
        spriteRenderer.size = size;

        //var angle = Mathf.Atan2(targetPointObject.transform.position.y, targetPointObject.transform.position.x) * Mathf.Rad2Deg;
        //spriteRenderer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        var angle = Quaternion.LookRotation(Vector3.forward, targetPointObject.transform.position - spriteRenderer.transform.position);
        var eulerAngle = angle.eulerAngles;
        eulerAngle.z -= 90;
        spriteRenderer.transform.eulerAngles = eulerAngle;
    }
}

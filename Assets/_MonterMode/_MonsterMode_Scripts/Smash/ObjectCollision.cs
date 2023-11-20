using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ObjectCollision : MonoBehaviour
{
    UnityAction<Collision2D> callbackCollision;
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        callbackCollision?.Invoke(collision);
    }
    public void AddListenerCollision(UnityAction<Collision2D> callbackCollision)
    {
        this.callbackCollision = callbackCollision;
    }
}

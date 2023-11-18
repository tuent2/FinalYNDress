using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoroutineEverywhere : MonoBehaviour
{
    private static CoroutineEverywhere instance;
    public static CoroutineEverywhere Instance
    {
        get
        {
            if (instance == null)
            {
                var singletonObject = new GameObject();
                instance = singletonObject.AddComponent<CoroutineEverywhere>();
                singletonObject.name = "CorotineEveryWhere";
                DontDestroyOnLoad(singletonObject);
            }
            return instance;
        }
    }
    public Coroutine WaitingWithSecond(float second, UnityAction callFinish)
    {
        return StartCoroutine(WaitingWithSecondCorotine(second, callFinish));
    }
    IEnumerator WaitingWithSecondCorotine(float second, UnityAction callFinish)
    {
        yield return new WaitForSecondsRealtime(second);
        callFinish?.Invoke();
    }
    public Coroutine WaitingWithCondition(WaitUntil waitUntil, UnityAction callFinish)
    {
        return StartCoroutine(WaitingWithConditionCorotine(waitUntil, callFinish));
    }
    IEnumerator WaitingWithConditionCorotine(WaitUntil waitUntil, UnityAction callFinish)
    {
        yield return waitUntil;
        callFinish?.Invoke();
    }
    public Coroutine WaitingWithPassFrame(int numberOfFrame, UnityAction callFinish)
    {
        return StartCoroutine(WaitingWithPassFrameCorotine(numberOfFrame, callFinish));
    }
    IEnumerator WaitingWithPassFrameCorotine(int numberOfFrame, UnityAction callFinish)
    {
        //yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => Time.frameCount > numberOfFrame);
        callFinish?.Invoke();
    }
    public void StopCoroutineEverywhere(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }
}

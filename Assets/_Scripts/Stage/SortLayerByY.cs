using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class SortLayerByY : MonoBehaviour
{
    [SerializeField] SortingGroup sortingGroup;
    [SerializeField] int sortOrder;
    private void OnEnable()
    {
        InvokeRepeating(nameof(SortLayer), .1f, .1f);
    }
    private void OnDisable()
    {
        CancelInvoke(nameof(SortLayer));
    }
    private void SortLayer()
    {
        if (sortingGroup == null)
        {
            sortingGroup = GetComponentInChildren<SortingGroup>();
        }
        sortOrder = (int)(-transform.position.y * 1000);
        sortingGroup.sortingLayerName = "Monster";
        sortingGroup.sortingOrder = sortOrder;
    }
}

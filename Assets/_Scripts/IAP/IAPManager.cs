using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
public class IAPManager : MonoBehaviour
{
    private const string productId = "com.dressup.makeup.leftright.removeads";
    [SerializeField] GameObject[] native;


    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("removeAds", 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }
    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == productId)
        {
            IAPPurchaseString(productId);
            Destroy(gameObject);
            for (int i = 0; i < native.Length; i++)
            {
                if (native[i] == null)
                {
                    return;
                }
                else
                {
                    native[i].gameObject.SetActive(false);
                }

            }

        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
       // IAPRespond.THIS.RepondText.text = product + "failed because: " + failureReason + "";
        //IAPRespond.THIS.gameObject.SetActive(true);
    }

    public void IAPPurchaseString(string productID)
    {
        switch (productID)
        {
            case "com.dressup.makeup.leftright.removeads":
                //IronSouceController.THIS.DestroyBanner();

                //PlayerPrefs.SetInt("removeAds", 1);
                //FireBaseAnalysticsController.THIS.FireEvent("REMOVEADS_SUCCESS");
                break;
        }
    }
}

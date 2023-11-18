using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SetPositionPlaceCharacterInStage : MonoBehaviour
{
    public Collider2D limitCollider;
    [SerializeField] Collider2D thisCollider;
    //[SerializeField] LayerMask layerMask;
    [SerializeField] float distance;
    [SerializeField] Collider2D colliderMonster;
    [SerializeField] ContactFilter2D binContactFilter2D;
    [SerializeField] float offsetMoveOutSide;
    Collider2D[] bincollider2Ds = new Collider2D[1];
    //Collider2D[] boxStageLockcollider2Ds = new Collider2D[1];
    public void OnDrop()
    {
        //Nếu chạm bin thì sẽ destroy
        int countBin = Physics2D.OverlapCollider(colliderMonster, binContactFilter2D, bincollider2Ds);
      
        if (countBin >= 1)
        {
            Debug.Log(123);
            GameManager.THIS.uIStage.ComfirmDeletePopup.ShowWithTextCallback((isOk) => {
                if (isOk)
                {
                    gameObject.SetActive(false);
                    GameManager.THIS.stageManager.RemoveCharacterInStage(GetComponent<MainCharacterController>());
                    GameManager.THIS.stageManager.UpdateUIOfLimited();
                }
                else
                {
                    gameObject.SetActive(true);
                    GameManager.THIS.stageManager.BackPositionCharacterDragWhenNotRemove();

                }
                GameManager.THIS.stageManager.OnDisSelectMonster();

            });
        }

        CheckInCollider();
        GameManager.THIS.stageManager.UpdateUIOfLimited();
    }

    private void CheckInCollider()
    {
        //Kiểm tra xem nếu đang đứng ở vùng lock thì sẽ di chuyển ra rìa
        //int countBoxStage = Physics2D.OverlapCollider(checkCollider, stageCollider, boxStageLockcollider2Ds);
        //if (countBoxStage == 0)
        //{
        //    var colliderDetect = (BoxCollider2D)boxStageLockcollider2Ds[0];
        //    Vector2 posMonster = transform.position;
        //    //Vector2 posOutSideColliderDetect = colliderDetect.bounds.ClosestPoint(posMonster);
        //    //Vector2 posOutSideColliderDetect = Physics2D.ClosestPoint(posMonster, colliderDetect);
        //    Vector2 closestPoint = FindClosestEdgePoint(colliderDetect, posMonster);
        //    transform.position = closestPoint;

        //    //GameManager.Instance.notificationPopup.ShowWithText(LocalizationManager.GetTranslation("NeedToUnlockTheStage"));

        //}

        if (limitCollider != null && thisCollider != null)
        {
            if (!IsInsideLimitCollider())
            {
                Vector2 closestPointInside = limitCollider.ClosestPoint(thisCollider.bounds.center);
                Vector2 direction = closestPointInside - (Vector2)thisCollider.bounds.center;

                transform.position += (Vector3)direction;
            }
            else
            {
               
            }
        }

    }

    

    bool IsInsideLimitCollider()
    {
        if (thisCollider.bounds.min.x > limitCollider.bounds.min.x &&
            thisCollider.bounds.max.x < limitCollider.bounds.max.x &&
            thisCollider.bounds.min.y > limitCollider.bounds.min.y &&
            thisCollider.bounds.max.y < limitCollider.bounds.max.y)
        {
            return true; // Collider này nằm bên trong limitCollider
        }
        return false;
    }

   

}

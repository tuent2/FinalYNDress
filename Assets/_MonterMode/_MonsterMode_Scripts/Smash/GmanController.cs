using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
public class GmanController : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 screenBounds;
    [SerializeField] int dameage;
    [SerializeField] int powerForceToCharacter;
    public Rigidbody2D rb2d;
    private Vector3 currentPosition;
    public SkeletonAnimation skeletonAnimation;
    [SerializeField] Transform pointStartLaser;
    //[SerializeField] GameObject lazer;
    

    
    [SerializeField] Transform targetObject;
    [SerializeField] ContactFilter2D contactFilter;
    [SerializeField] GameObject[] laserRefObjects;
    //public Rigidbody2D rb;
    List<RaycastHit2D> hit2Ds = new List<RaycastHit2D>();
    bool canAttack = true;
    bool settedShooting;
    bool settedHiding;
    bool canShootLaser;
    bool canPlaySound = true;

    
    void Start()
    {
        settedShooting = false;
        UpdateScreenBounds();
        //lazer.SetActive(false);
        //skeletonAnimation.AnimationState.Event += HandleEvent;

        //skeletonAnimation.AnimationState.Start += delegate (TrackEntry trackEntry) {
        //    // You can also use an anonymous delegate.
        //    Debug.Log(string.Format("track {0} started a new animation.", trackEntry.TrackIndex));
        //};

        //skeletonAnimation.AnimationState.End += delegate {
        //    // ... or choose to ignore its parameters.
        //    Debug.Log("An animation ended!");
        //};
    }

    public void UpdateScreenBounds()
    {
        float screenHalfHeight = Camera.main.orthographicSize;
        float screenHalfWidth = screenHalfHeight * Camera.main.aspect;
        screenBounds = new Vector3(screenHalfWidth, screenHalfHeight, 0f);
    }

    public void PlayAnimationCharacter(int index, string animation, bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(index, animation, isLoop);
    }

    //void HandleEvent(TrackEntry trackEntry, Spine.Event e)
    //{
    //    // Play some sound if the event named "footstep" fired.
    //    if (e.Data.Name == "XuatHien")
    //    {
    //        PlayAnimationCharacter(0, "Attack_Loop", true);
    //        isAttack = true;
    //        lazer.SetActive(true);
    //        Debug.Log("123");
    //    }
    //    if (e.Data.Name == "TroLai")
    //    {
    //        PlayAnimationCharacter(0, "Idle", true);
    //        isAttack = false;
    //        lazer.SetActive(false);
    //    }


    //}

    void LateUpdate()
    {
#if !UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;
                
                transform.position = touchPosition;

            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x+0.5f, screenBounds.x-0.5f);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y + 2.5f, screenBounds.y-0.5f);

            transform.position = clampedPosition;
                
                   

                Collider2D playerCollider = GetComponent<Collider2D>();

                if (playerCollider.OverlapPoint(touchPosition))
                {
                    PlayAnimationCharacter(0, "XuatHien", false);

                    skeletonAnimation.state.SetAnimation(0, "XuatHien", false).Complete += delegate
                    {
                        SmashSoundController.THIS.PlayLazerClip();
                        canShootLaser = true;
                    };
                    skeletonAnimation.state.AddAnimation(0, "Attack_Loop", true, 0);
                    isDragging = true;
                     SmashCharacterManager.THIS.setStateOfColliderMain(true);
                }
            }
        }

        if (Input.touchCount == 0)
        {
            isDragging = false;
             SmashCharacterManager.THIS.setStateOfColliderMain(false);
        }

        if (isDragging)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(touch.position);
            newPosition.z = 0;
            transform.position = newPosition;

            settedHiding = false;
            if (!settedShooting)
            {
                settedShooting = true;
                skeletonAnimation.state.ClearTracks();
                skeletonAnimation.state.SetAnimation(0, "XuatHien", false).Complete += delegate
                {
                    SmashSoundController.THIS.PlayLazerClip();
                    for (int i = 0; i < laserRefObjects.Length; i++)
                    {
                        laserRefObjects[i].SetActive(true);
                    }
                    canShootLaser = true;
                };
                skeletonAnimation.state.AddAnimation(0, "Attack_Loop", true, 0);
            }

            if (transform.position.x >= SmashCharacterManager.THIS.transform.position.x)
            {
                transform.localEulerAngles = Vector3.zero;
            }
            else
            {
                transform.localEulerAngles = new Vector3(180, 0, 180);
            }
        }
        else
        {
            settedShooting = false;
            canShootLaser = false;

            if (!settedHiding)
            {
                SmashSoundController.THIS.ShutDownTheCurrentBGM();
                for (int i = 0; i < laserRefObjects.Length; i++)
                {
                    laserRefObjects[i].SetActive(false);
                }
                settedHiding = true;
                skeletonAnimation.state.ClearTracks();
                skeletonAnimation.state.SetAnimation(0, "TroLai", false);
                skeletonAnimation.state.AddAnimation(0, "Idle", true, 0);
            }
        }
#endif
        if (Input.GetMouseButtonDown(0))
        {
            skeletonAnimation.state.ClearTracks();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            currentPosition = gameObject.transform.position;
            Collider2D playerCollider = GetComponent<Collider2D>();


            if (playerCollider.OverlapPoint(mousePosition))
            {
                PlayAnimationCharacter(0, "XuatHien", false);

                skeletonAnimation.state.SetAnimation(0, "XuatHien", false).Complete += delegate
                {
                    //isAttack = true;
                    //lazer.SetActive(true);
                };
                skeletonAnimation.state.AddAnimation(0, "Attack_Loop", true, 0);
                isDragging = true;
                SmashCharacterManager.THIS.setStateOfColliderMain(true);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            SmashCharacterManager.THIS.setStateOfColliderMain(false);
        }

        if (isDragging)
        {   

            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;
            transform.position = newPosition;

            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x + 0.5f, screenBounds.x - 0.5f);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y + 2.5f, screenBounds.y - 0.5f);

            transform.position = clampedPosition;
          
            
            settedHiding = false;
            if (!settedShooting)
            {
                settedShooting = true;
                skeletonAnimation.state.ClearTracks();
                skeletonAnimation.state.SetAnimation(0, "XuatHien", false).Complete += delegate
                {   

                    SmashSoundController.THIS.PlayLazerClip();
                    for (int i = 0; i < laserRefObjects.Length; i++)
                    {
                        laserRefObjects[i].SetActive(true);
                    }
                    canShootLaser = true;
                };
               
                canShootLaser = true;
                skeletonAnimation.state.AddAnimation(0, "Attack_Loop", true, 0);
            }

            if (transform.position.x >= SmashCharacterManager.THIS.transform.position.x)//GameManager.Instance.smashManager.characterRagdoll.transform.position.x)
            {
                transform.localEulerAngles = Vector3.zero;
            }
            else
            {
                transform.localEulerAngles = new Vector3(180, 0, 180);
            }
        }
        else
        {
            settedShooting = false;
            canShootLaser = false;

            if (!settedHiding)
            {
                SmashSoundController.THIS.ShutDownTheCurrentBGM();
                for (int i = 0; i < laserRefObjects.Length; i++)
                {
                    laserRefObjects[i].SetActive(false);
                }
                settedHiding = true;
                skeletonAnimation.state.ClearTracks();
                skeletonAnimation.state.SetAnimation(0, "TroLai", false);
                skeletonAnimation.state.AddAnimation(0, "Idle", true, 0);
            }
        }

    }
    private void FixedUpdate()
    {
        if (canShootLaser)
        {
            int count = Physics2D.Raycast(pointStartLaser.position, pointStartLaser.right, contactFilter, hit2Ds);
            if (count > 0)
            {
                targetObject.position = hit2Ds[0].point;
                if (hit2Ds[0].collider.CompareTag("Player"))
                {
                    if (canAttack)
                    {
                        canAttack = false;
                        SmashCharacterManager.THIS.Dameage(dameage);
                        var direction = pointStartLaser.right.normalized;
                        direction.y = transform.position.y >= 0 ? -.5f : .5f;
                         SmashCharacterManager.THIS.AddForceDameage(direction * powerForceToCharacter, targetObject.position);
                        Invoke(nameof(SetCanAttack), .02f);
                        if (canPlaySound)
                        {
                            //SoundController.THIS.PlayGmanLazeClip(); 
                            
                            canPlaySound = false;
                            Invoke(nameof(SetCanPlaySound), .2f);
                        }
                    }
                }
            }
        }
    }

    void SetCanPlaySound()
    {
        canPlaySound = true;
    }

    void SetCanAttack()
    {
        canAttack = true;
    }

    private void OnDisable()
    {
        isDragging = false;
    }
}

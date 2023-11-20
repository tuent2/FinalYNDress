using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSmashController : MonoBehaviour
{
    private Vector3 screenBounds;
    [SerializeField] int damage;
    [SerializeField] int powerDameage;
    [SerializeField] GameObject SawFX;
    ParticleSystem touch;
    [SerializeField] ObjectCollision objectCollision;
    bool canPlaySound = true;

    private bool isDragging = false;
    [SerializeField] Rigidbody2D rbSaw;
    [SerializeField] float speed = 100f;
    [SerializeField] GameObject Saw;
    private void Start()
    {
        UpdateScreenBounds();
        touch = SawFX.GetComponent<ParticleSystem>();
        objectCollision.AddListenerCollision((collision) =>
        {
            
            if (!collision.collider.CompareTag("Player"))
            {
                return;
            }
            var velX = Mathf.Abs(collision.relativeVelocity.x);
            var velY = Mathf.Abs(collision.relativeVelocity.y);
            if (velX >= powerDameage || velY >= powerDameage)
            {
                
                if (canPlaySound)
                {
                    SmashSoundController.THIS.PlayTouchClip();
                    canPlaySound = false;
                    Invoke(nameof(SetCanPlaySound), .2f);
                    SawFX.transform.position = collision.contacts[0].point;
                    touch.Play();
                }
                var dameageAttackRatio = velX >= velY ? velX : velY;
                var dameageAttack = (int)(damage * (dameageAttackRatio / powerDameage));
                if (dameageAttack >= 20)
                {
                    dameageAttack = 20;
                }
                SmashCharacterManager.THIS.Dameage(dameageAttack);
            }
        });
    }

    public void UpdateScreenBounds()
    {
        float screenHalfHeight = Camera.main.orthographicSize;
        float screenHalfWidth = screenHalfHeight * Camera.main.aspect;
        screenBounds = new Vector3(screenHalfWidth, screenHalfHeight, 0f);
    }
    protected virtual void SetCanPlaySound()
    {
        canPlaySound = true;
    }
    private void OnDisable()
    {
        isDragging = false;
    }
    void Update()
    {

        rbSaw.MoveRotation(rbSaw.rotation + speed * Time.fixedDeltaTime);
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //    Collider2D playerCollider = Saw.GetComponent<Collider2D>();


        //    if (playerCollider.OverlapPoint(mousePosition))
        //    {

        //        isDragging = true;
        //        //offset = transform.position - hit.point;
        //        //Debug.Log("123");
        //        // SoundController.THIS.PlayInGameBGClip();
        //        SmashSoundController.THIS.PlaySawClip();
        //    }
        //}

        //if (Input.GetMouseButtonUp(0))
        //{   
        //    isDragging = false;
        //    SmashSoundController.THIS.ShutDownTheCurrentBGM();
        //}

        //if (isDragging)
        //{
        //    Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Saw.transform.position = newPosition;


        //}
#if !UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                Collider2D playerCollider = Saw.GetComponent<Collider2D>();

                if (playerCollider.OverlapPoint(touchPosition))
                {
                    isDragging = true;
                    SmashSoundController.THIS.PlaySawClip();
                     SmashCharacterManager.THIS.setStateOfColliderMain(true);
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
                SmashSoundController.THIS.ShutDownTheCurrentBGM();
                 SmashCharacterManager.THIS.setStateOfColliderMain(false);
            }

            if (isDragging)
            {
                Vector2 newPosition = Camera.main.ScreenToWorldPoint(touch.position);

                 
               Saw.transform.position = newPosition;

            Vector3 clampedPosition = Saw.transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x+0.5f, screenBounds.x-0.5f);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y + 2.5f, screenBounds.y-0.5f);

            Saw.transform.position = clampedPosition;
                
            }
        }
#endif
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D playerCollider = Saw.GetComponent<Collider2D>();


            if (playerCollider.OverlapPoint(mousePosition))
            {

                isDragging = true;
                //offset = transform.position - hit.point;
                SmashCharacterManager.THIS.setStateOfColliderMain(true);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            SmashSoundController.THIS.ShutDownTheCurrentBGM();
            SmashCharacterManager.THIS.setStateOfColliderMain(false);
        }

        if (isDragging)
        {
            Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Saw.transform.position = newPosition;

            Vector3 clampedPosition = Saw.transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x+0.5f, screenBounds.x-0.5f);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y + 2.5f, screenBounds.y-0.5f);

            Saw.transform.position = clampedPosition;
        }

    }

    //private void FixedUpdate()
    //{
    //    rbSaw.MoveRotation(rbSaw.rotation + speed * Time.fixedDeltaTime);
    //}

}

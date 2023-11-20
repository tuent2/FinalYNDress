using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickSmashController : MonoBehaviour
{
    public static StickSmashController THIS;

    private bool isDragging = false;
    //private Vector2 initialClickPosition;
    //public HingeJoint2D hingeJoint;
    //public Rigidbody2D rb2d;
    //private Vector3 currentPosition;

    [SerializeField] int damage;
    [SerializeField] int powerDameage;
    [SerializeField] GameObject touchFX;
    ParticleSystem touch;
    bool canPlaySound = true;
    [SerializeField] Collider2D objectMainCollider2D;
    [SerializeField] private Rigidbody2D objectMainRB;
    [SerializeField] ObjectCollision objectCollision;
    Rigidbody2D rb;
    private Vector2 offset;
    //public bool isDragging = false;
    private void OnDisable()
    {
        isDragging = false;
    }
    private void Awake()
    {
        THIS = this;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        objectMainRB.bodyType = RigidbodyType2D.Dynamic;
        objectMainRB.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        touch = touchFX.GetComponent<ParticleSystem>();
        objectCollision.AddListenerCollision((collision) =>
        {
            Debug.Log(collision.gameObject.tag);
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


                    touchFX.transform.position = collision.contacts[0].point;
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
    private void Update()
    {
#if !UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                if (objectMainCollider2D.OverlapPoint(touchPosition))
                {
                    isDragging = true;
                    SmashCharacterManager.THIS.setStateOfColliderMain(true);
                    offset = touchPosition - objectMainRB.position;
                    rb.position = (touchPosition - offset);
                    var hingeJoint = objectMainRB.gameObject.AddComponent<HingeJoint2D>();
                    hingeJoint.connectedBody = rb;
                    hingeJoint.autoConfigureConnectedAnchor = false;
                    var posFingerNew = objectMainRB.transform.InverseTransformPoint(touchPosition);
                    hingeJoint.anchor = posFingerNew;
                    hingeJoint.connectedAnchor = Vector2.zero;

                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
                var hingeJoint = objectMainRB.gameObject.GetComponent<HingeJoint2D>();
                 SmashCharacterManager.THIS.setStateOfColliderMain(false);
                if (hingeJoint)
                {
                    Destroy(hingeJoint);
                }
            }
        }

        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //hingeJoint.connectedAnchor = mousePosition;
            rb.MovePosition(mousePosition);
        }
#endif
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (objectMainCollider2D.OverlapPoint(mousePosition))
            {
                isDragging = true;
                SmashCharacterManager.THIS.setStateOfColliderMain(true);
                offset = mousePosition - objectMainRB.position;
                rb.position = (mousePosition - offset);
                var hingeJoint = objectMainRB.gameObject.AddComponent<HingeJoint2D>();
                hingeJoint.connectedBody = rb;
                hingeJoint.autoConfigureConnectedAnchor = false;
                var posFingerNew = objectMainRB.transform.InverseTransformPoint(mousePosition);
                hingeJoint.anchor = posFingerNew;
                hingeJoint.connectedAnchor = Vector2.zero;
                
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            SmashCharacterManager.THIS.setStateOfColliderMain(false);
            var hingeJoint = objectMainRB.gameObject.GetComponent<HingeJoint2D>();
            if (hingeJoint)
            {
                Destroy(hingeJoint);
            }
        }

        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //hingeJoint.connectedAnchor = mousePosition;
            rb.MovePosition(mousePosition);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log(collision.gameObject.tag);
    //    if (!collision.collider.CompareTag("Player"))
    //    {
    //        return;
    //    }
    //    var velX = Mathf.Abs(collision.relativeVelocity.x);
    //    var velY = Mathf.Abs(collision.relativeVelocity.y);
    //    if (velX >= powerDameage || velY >= powerDameage)
    //    {
    //        if (canPlaySound)
    //        {
    //            SoundController.THIS.PlayTouchClip();
    //            canPlaySound = false;
    //            Invoke(nameof(SetCanPlaySound), .2f);


    //            touchFX.transform.position = collision.contacts[0].point;
    //            touch.Play();
    //        }


    //        var dameageAttackRatio = velX >= velY ? velX : velY;
    //        var dameageAttack = (int)(damage * (dameageAttackRatio / powerDameage));
    //        if (dameageAttack >= 20)
    //        {
    //            dameageAttack = 20;
    //        }
    //        SmashCharacterManager.THIS.Dameage(dameageAttack);
    //    }
    //}

    protected virtual void SetCanPlaySound()
    {
        canPlaySound = true;
    }
}

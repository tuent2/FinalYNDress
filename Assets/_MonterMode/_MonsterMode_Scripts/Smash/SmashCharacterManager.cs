using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using DG.Tweening;
using Lean.Common;
using System.Linq;
using Lean.Touch;

    public class SmashCharacterManager : MonoBehaviour
    {
        public static SmashCharacterManager THIS;

        private void Awake()
        {
            THIS = this;
        }
        
    public void setStateOfColliderMain(bool state)
    {
        collider2DMain.enabled = state;
    }
        public void ResetTransform()
        {
            gameObject.transform.position = Vector3.zero;
             Smoke.Play();
        }

        public SkeletonAnimation GetSkeletonActive()
        {
            return MainSkeletonAnimation;
        }

    //[SerializeField] Collider2D mainCollider2D;
    [SerializeField] GameObject SmokeFx;
     ParticleSystem Smoke;
        [SerializeField] GameObject touchFx;
    ParticleSystem touch;
        [SerializeField] SkeletonAnimation MainSkeletonAnimation;
        [SerializeField] HealthProcesing healthProcesing;
        [SerializeField] int dameage;
        [SerializeField] int powerDameage = 10;
        [SerializeField] float powerForceUltimate;
        [SerializeField] Collider2D collider2DMain;
        [SerializeField] GameObject[] uDauObjects;
        [SerializeField] GameObject QuanTai;
    //[SerializeField] ParticleSystem smokeFX;
    //[SerializeField] ParticleSystem dustFX;
    //[SerializeField] EffectPooling touchFXPooling;
    public GameObject[] armsAll;
        
        IgnoreCollider ignoreCollisions;
        Rigidbody2D rb2DMain;
        BlanceCharacter balanceMain;
        SkeletonAnimation skeletonAnimation;
        List<BlanceCharacter> balancesList = new List<BlanceCharacter>();
        Collider2D[] childsCollider2D;
        Rigidbody2D[] childsRigidbody2D;
        HingeJoint2D[] childsHingeJoin2D;
        SkeletonUtilityBone[] skeletonUtilityBones;
        BoneStateWithSkin[] boneStateWithSkins;
        bool isDragging;
        bool isStandUp;
        bool isStandUpping;
        public bool isIgnoreDameageByCollision;
        bool isInitStarted;
        bool isAskUltimateShowed;
        bool isDirtyShowed;
        bool isBloodShowed;
        bool isUltimateShowing;
        List<SaveDataChildTransform> childBoneSaveList = new List<SaveDataChildTransform>();
        Vector2 inDirectionUltimate;
        private void OnDisable()
        {
            for (int i = 0; i < childBoneSaveList.Count; i++)
            {
                childBoneSaveList[i].SetReload();
            }
        }
        private void OnEnable()
        {
            isInitStarted = true;
        touch = touchFx.GetComponent<ParticleSystem>();
       
        ignoreCollisions = GetComponent<IgnoreCollider>();
            balanceMain = GetComponent<BlanceCharacter>();
            rb2DMain = GetComponent<Rigidbody2D>();
            skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            skeletonUtilityBones = GetComponentsInChildren<SkeletonUtilityBone>();
            boneStateWithSkins = GetComponentsInChildren<BoneStateWithSkin>();
            childsCollider2D = transform.GetChild(0).GetComponentsInChildren<Collider2D>();
            childsRigidbody2D = transform.GetChild(0).GetComponentsInChildren<Rigidbody2D>();
            childsHingeJoin2D = transform.GetChild(0).GetComponentsInChildren<HingeJoint2D>();
            for (int i = 0; i < childsRigidbody2D.Length; i++)
            {
                childsRigidbody2D[i].mass = 3;
                childsRigidbody2D[i].collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            }
        for (int i = 0; i < childsCollider2D.Length; i++)
        {
            var leanSelect = childsCollider2D[i].gameObject.AddComponent<LeanSelectableByFinger>();
            leanSelect.OnSelected.AddListener(StartDrag);
            leanSelect.OnDeselected.AddListener(EndDrag);
            var leanDrag = childsCollider2D[i].gameObject.AddComponent<LeanDragTranslateRigidbody2D>();
            leanDrag.Damping = 100;
            //childsCollider2D[i].gameObject.AddComponent<CheckOffScreen>();
        }
        if (balancesList.Count == 0)
            {
                balancesList = GetComponentsInChildren<BlanceCharacter>().ToList();
                for (int i = balancesList.Count - 1; i >= 0; i--)
                {
                    if (balancesList[i].enabled == false)
                    {
                        balancesList.RemoveAt(i);
                    }
                }
            }
           
            var childTransforms = transform.GetComponentsInChildren<Transform>();
            for (int i = 0; i < childTransforms.Length; i++)
            {
                SaveDataChildTransform saveDataChildTransform = new SaveDataChildTransform();
                saveDataChildTransform.Save(childTransforms[i]);
                childBoneSaveList.Add(saveDataChildTransform);
            }
        Smoke = SmokeFx.GetComponent<ParticleSystem>();
        Reload();
        }
        private void FixedUpdate()
        {
            if (!isDragging && !isStandUp)
            {
                Vector2 posCheck = new Vector2(collider2DMain.bounds.center.x, collider2DMain.bounds.min.y);
                Vector2 posTarget = posCheck;
                posTarget.y -= .5f;
                var rayCast = Physics2D.Linecast(posCheck, posTarget, LayerMask.GetMask("Ground"));
                if (rayCast.collider != null)
                {
                    isStandUp = true;
                    StandUp();
                }
            }
            if (isStandUpping)
            {
                Vector2 posCheck = new Vector2(collider2DMain.bounds.center.x, collider2DMain.bounds.min.y);
                Vector2 posTarget = posCheck;
                posTarget.y -= .5f;

                var rayCast = Physics2D.Linecast(posCheck, posTarget, LayerMask.GetMask("Ground"));
                if (rayCast.collider == null)
                {
                    isStandUpping = false;
                    isStandUp = false;
                    DOTween.Kill("RemainOnCol");
                }
            }
        }
        public void Reload()
        {
            if (isInitStarted == false)
            {
                return;
            }
            skeletonAnimation.gameObject.SetActive(true);

            healthProcesing.Reload();
            ignoreCollisions.IgnoreColliders();
            skeletonAnimation.GetComponent<MeshRenderer>().enabled = true;
            rb2DMain.position = new Vector2(0, .3f);
            transform.position = new Vector2(0, .3f);

            for (int i = 0; i < childsCollider2D.Length; i++)
            {
                childsCollider2D[i].isTrigger = false;
            }
            for (int i = 0; i < childsHingeJoin2D.Length; i++)
            {
                childsHingeJoin2D[i].enabled = true;
            }
            for (int i = 0; i < balancesList.Count; i++)
            {
                balancesList[i].enabled = true;
                balancesList[i].gameObject.tag = "Player";
            }
            rb2DMain.isKinematic = false;
            for (int i = 0; i < childsRigidbody2D.Length; i++)
            {
                childsRigidbody2D[i].isKinematic = false;
            }

            for (int i = 0; i < childsRigidbody2D.Length; i++)
            {
                childsRigidbody2D[i].isKinematic = false;
            }

        bool isBoneShowed = false;
        for (int i = 0; i < boneStateWithSkins.Length; i++)
        {
            var isShowed = boneStateWithSkins[i].ReloadShowBone();
            if (isShowed && !isBoneShowed)
            {
                isBoneShowed = true;
            }
        }
        if (isBoneShowed)
        {
            for (int i = 0; i < armsAll.Length; i++)
            {
                armsAll[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < armsAll.Length; i++)
            {
                armsAll[i].SetActive(true);
            }
        }

        collider2DMain.isTrigger = false;
            isStandUp = true;
            isDragging = false;
        collider2DMain.enabled = false;
        //GameManager.Instance.smashManager.isDead = false;
        isIgnoreDameageByCollision = false;
            isStandUpping = false;
            isAskUltimateShowed = false;
            isDirtyShowed = false;
            isBloodShowed = false;
            isUltimateShowing = false;
        }
        public void StartDrag(LeanSelect leanSelect)
        {
            DOTween.Kill("RemainOnCol");
            for (int i = 0; i < childsHingeJoin2D.Length; i++)
            {
                childsHingeJoin2D[i].enabled = true;
            }
            for (int i = 0; i < balancesList.Count; i++)
            {
                balancesList[i].enabled = true;
            }
            for (int i = 0; i < skeletonUtilityBones.Length; i++)
            {
                skeletonUtilityBones[i].mode = SkeletonUtilityBone.Mode.Override;
            }
            isDragging = true;
            isStandUp = false;
            collider2DMain.enabled = true;
            }
        public void EndDrag(LeanSelect leanSelect)
        {
            isDragging = false;
            collider2DMain.enabled = false;
    }
        public void StandUp()
        {
            isStandUpping = true;
            DOTween.Kill("RemainOnCol");
            DOTween.Sequence()
                .SetId("    ")
                .AppendInterval(1)
                .AppendCallback(() =>
                {
                    for (int i = 0; i < skeletonUtilityBones.Length; i++)
                    {
                        skeletonUtilityBones[i].mode = SkeletonUtilityBone.Mode.Follow;
                    }
                    skeletonAnimation.skeleton.SetBonesToSetupPose();
                    for (int i = 0; i < childBoneSaveList.Count; i++)
                    {
                        if (childBoneSaveList[i].transform.GetInstanceID() == transform.GetInstanceID())
                        {
                            continue;
                        }
                        childBoneSaveList[i].SetReload();
                    }
                    Smoke.Play();
                })
                .AppendInterval(.1f)
                .OnComplete(() =>
                {

                    for (int i = 0; i < skeletonUtilityBones.Length; i++)
                    {
                        skeletonUtilityBones[i].mode = SkeletonUtilityBone.Mode.Override;
                    }
                    //gameObject.transform.position = Vector3.zero;
                    balanceMain.enabled = true;
                    isStandUpping = false;
                });
        }
        public void Dameage(int dameage)
        {
        if (SmashController.THIS.isDead)
        {
            return;
        }
        //Debug.LogError(dameage);
        //Vibration.Vibrate(DataGame.numberPowerVibration);
        int health = healthProcesing.Dameage(dameage);
            if (health == 0)
            {
                SmashController.THIS.isDead = true;
                Dead();
                SmashController.THIS.HideAllTrap();
            }
            else if (!isDirtyShowed && health <= (85f / 100) * healthProcesing.healthMax)
            {
                isDirtyShowed = true;
                //GameManager.Instance.smashManager.SetMaterialOfCharacterDirty();
            }
            else if (!isBloodShowed && health <= (40f / 100) * healthProcesing.healthMax)
            {
                isBloodShowed = true;
                //GameManager.Instance.smashManager.SetMaterialOfCharacterBlood();
            }
            else if (!isAskUltimateShowed && health <= (30f / 100) * healthProcesing.healthMax)
            {
                setStateOfColliderMain(false);
                isAskUltimateShowed = true;
                SmashController.THIS.HideAllTrap();
                SmashSoundController.THIS.ShutDownTheCurrentBGM();
                touch.Stop();
            //Time.timeScale = 0f;

            MonsterModeGameManager.THIS.SmashAskCanvas.gameObject.SetActive(true);
            }



            if (health <= (90f / 100) * healthProcesing.healthMax)
            {
                uDauObjects[0].SetActive(true);
            }
            if (health <= (55f / 100) * healthProcesing.healthMax)
            {
                uDauObjects[1].SetActive(true);
            }
        }
        public void AddForceDameage(Vector2 force, Vector2 pos)
        {
        if (SmashController.THIS.isDead)
        {
            return;
        }
        rb2DMain.AddForceAtPosition(force, pos);
        }
        public void Dead()
        {
            isUltimateShowing = false;
            collider2DMain.isTrigger = true;
            for (int i = 0; i < childsCollider2D.Length; i++)
            {
                childsCollider2D[i].isTrigger = true;
            }
            for (int i = 0; i < childsHingeJoin2D.Length; i++)
            {
                childsHingeJoin2D[i].enabled = false;
            }
            for (int i = 0; i < balancesList.Count; i++)
            {
                balancesList[i].enabled = false;
            }
            skeletonAnimation.gameObject.SetActive(false);
            SmashSoundController.THIS.ShutDownTheCurrentBGM();
            QuanTai.gameObject.SetActive(true);
            StartCoroutine(WaitQuanTaiAnimation());
        }

    private IEnumerator WaitQuanTaiAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        SmashSoundController.THIS.PlayKOClip();
        MonsterModeGameManager.THIS.ShowFinishedCanvas();
    }
    public Vector2 RandomVector2(float angle, float angleMin)
        {
            float random = UnityEngine.Random.value * angle + angleMin;
            return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
        }
        public void UltimateShow()
        {
            isUltimateShowing = true;
            inDirectionUltimate = RandomVector2(0, 360).normalized;
            rb2DMain.AddForce(inDirectionUltimate * powerForceUltimate);
             StartCoroutine(KillTheCharacter());
    }
    private IEnumerator KillTheCharacter()
    {
         yield return new WaitForSeconds(0.3f);
        SmashController.THIS.isDead = true;
        healthProcesing.Die();
        Dead();
        SmashController.THIS.HideAllTrap();
    }
    private void OnCollisionEnter2D(Collision2D collision)
        {
        if (SmashController.THIS.isDead)
        {
            return;
        }
        if (!collision.collider.CompareTag("Ground") && !collision.collider.CompareTag("Box"))
            {
                return;
            }
            if (isUltimateShowing)
            {
                var contactPoint = collision.contacts[0].point;
                //Vector2 ballLocation = transform.position;
                Vector2 ballLocation = collider2DMain.bounds.center;
                var inNormal = (ballLocation - contactPoint).normalized;
                inDirectionUltimate = Vector2.Reflect(inDirectionUltimate, inNormal);
                rb2DMain.AddForce(inDirectionUltimate * powerForceUltimate);

                //var touchACs = AudioManager.Instance.audioClipData.touchAudioClips;
                //AudioManager.Instance.PlayOneShot(touchACs.GetRandom());
                //AudioManager.Instance.PlayOneShot(AudioManager.Instance.audioClipData.vaChamAudioClip);
                if (collision.collider.CompareTag("Ground"))
                {
                    //dustFX.transform.position = collision.contacts[0].point;
                    //dustFX.Play();
                }
                //var touchFX = touchFXPooling.GetObjectInPooling();
                //touchFX.transform.position = collision.contacts[0].point;
                //touchFX.Play();
                //touchFXPooling.ReturnObjectToPooling(touchFX, 4);
                Dameage(dameage * 2);
                return;
            }
            if (isIgnoreDameageByCollision)
            {
                return;
            }
            var velX = Mathf.Abs(collision.relativeVelocity.x);
            var velY = Mathf.Abs(collision.relativeVelocity.y);
            if (velX >= powerDameage || velY >= powerDameage)
            {
                SmashSoundController.THIS.PlayTouchClip(); 
                

                if (collision.collider.CompareTag("Ground"))
                {
                    touchFx.transform.position = collision.contacts[0].point;
                    touch.Play();
                }
            touchFx.transform.position = collision.contacts[0].point;
            touch.Play();

            var dameageAttackRatio = velX >= velY ? velX : velY;
                var dameageAttack = (int)(dameage * (dameageAttackRatio / powerDameage));
                if (dameageAttack >= 20)
                {
                    dameageAttack = 20;
                }
                Dameage(dameageAttack);
            }
        }
    }

    [SerializeField]
    public class SaveDataChildTransform
    {
        public Transform transform;
        public Vector3 localPosition;
        public Vector3 localRotation;
        public Vector3 localScale;
        public void Save(Transform transform)
        {
            this.transform = transform;
            localPosition = transform.localPosition;
            localRotation = transform.localEulerAngles;
            localScale = transform.localScale;
        }
        public void SetReload()
        {
            transform.localPosition = localPosition;
            transform.localEulerAngles = localRotation;
            transform.localScale = localScale;
        }
    }

    


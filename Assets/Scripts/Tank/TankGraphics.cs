using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TankGraphics : MonoBehaviour
{
    public List<TankSlotGraphics> TankSlotsGraphics;

    public GameObject BulletPrefab;
    public Transform EnemyTank;
    public Transform ShootStartingPoint;
    public AnimationCurve ShootAnimationCurve;
    public float ShootHeight;
    public float ProjectileTravelTime = 5f;

    //public TankSlotGraphics tankSlotPrefab;
    public TankController tankController;
    private bool canShoot, isHolding;

    public Team team;
    public TankGraphics enemyTank;

    private Prop holdingProp;

    public Animator animator;
    public GameObject repairIcon, shotIcon;
    private Coroutine sliderRoutine;
    public Slider tankSlider;
    public Image lifeFill;

    public ParticleSystem particleSystem;

    public static readonly int shoot = Animator.StringToHash("Shoot");
    public static readonly int reset = Animator.StringToHash("Reset");
    public static readonly int intro = Animator.StringToHash("Intro");

    public string underAttackBgm
    {
        get
        {
            switch (tankController.CurrentLife)
            {
                case 3:
                    return "bgm_plantao_globo";
                case 2:
                    return "bgm_ameno";
                case 1:
                    return "bgm_ameno_super";
                default:
                    return "avisa o Gavinhos que deu ruim";
            }
        }
    }

    private void Awake()
    {
        tankController = new TankController(this, team);

        if (team == Team.Blue)
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        else
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        LoadTankSlots();
    }

    private void LoadTankSlots()
    {
        for (int i = 0; i < TankSlotsGraphics.Count; i++)
        {
            tankController.TankSlots.Add(TankSlotsGraphics[i].tankSlot);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == GameConstants.PLAYER_TAG && sliderRoutine == null)
        {
            var playerController = other.GetComponent<PlayerView>().playerController;
            if (playerController.playerTeam == team && playerController.holdingProp is TankPiece &&
                (playerController.holdingProp as TankPiece).team == team)
            {
                holdingProp = playerController.holdingProp;
                repairIcon.SetActive(true);
                SoundManager.Instance.PlaySFX("sfx_tank_repair", true);

                if(particleSystem == null)
                    particleSystem = ParticleManager.Instance.InstantiateParticle("FX_Repair", this.transform).GetComponent<ParticleSystem>();

                sliderRoutine = StartCoroutine(SliderRoutine(() => { ExecuteAddPiece(playerController); }));
            }

            if (playerController.holdingProp is Bullet && canShoot)
            {
                holdingProp = playerController.holdingProp;

                shotIcon.SetActive(true);

                SoundManager.Instance.PlaySFX("sfx_tank_reload", true);
                sliderRoutine = StartCoroutine(SliderRoutine(() =>
                {
                    playerController.holdingProp = null;
                    ExecuteShot();
                }));
            }
        }
    }

    private void ExecuteAddPiece(PlayerController playerController)
    {
        TankPiece piece = playerController.holdingProp as TankPiece;
        for (int i = 0; i < tankController.TankSlots.Count; i++)
        {
            var slot = tankController.TankSlots[i];
            if (slot.Id == piece.Id && team == piece.team)
            {
                TankSlotsGraphics[i].AddSlotPiece(piece);
                playerController.holdingProp = null;
            }
        }

        if (tankController.isRepaired)
        {
            canShoot = true;
        }

        SoundManager.Instance.StopSFX("sfx_tank_repair");
    }

    IEnumerator SliderRoutine(Action callback)
    {
        float count = 0;
        tankSlider.value = 0;
        tankSlider.gameObject.SetActive(true);
        isHolding = true;
        while (count < GameConstants.TIME_TO_REPAIR)
        {
            tankSlider.value = Mathf.Lerp(0f, 1f, count / GameConstants.TIME_TO_REPAIR);
            count += Time.deltaTime;

            if (!isHolding)
            {
                DisableSlider();
            }

            yield return null;
        }

        callback();

        DisableSlider();

        if (particleSystem != null)
        {
            Destroy(particleSystem.gameObject);
            particleSystem = null;
        }

        tankSlider.gameObject.SetActive(false);
    }

    private void DisableSlider()
    {
        tankSlider.gameObject.SetActive(false);
        StopCoroutine(sliderRoutine);
        sliderRoutine = null;

        repairIcon.SetActive(false);
        shotIcon.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == GameConstants.PLAYER_TAG && isHolding)
        {
            PlayerController playerController = collider.GetComponent<PlayerView>().playerController;
            if (playerController.holdingProp != null || holdingProp == playerController.holdingProp)
            {
                isHolding = false;
            }

            if (particleSystem.gameObject != null)
            {
                Destroy(particleSystem.gameObject);
                particleSystem = null;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == GameConstants.PLAYER_TAG && isHolding)
        {
            PlayerController playerController = collider.GetComponent<PlayerView>().playerController;
            if (playerController.playerTeam == team &&
                (playerController.holdingProp == null || playerController.holdingProp is DestructiveProp))
            {
                isHolding = false;
            }
        }
    }

    public void ExecuteShot(bool isIntro = false)
    {
        SoundManager.Instance.StopSFX("sfx_tank_reload");

        if (!isIntro)
        {
            SoundManager.Instance.PlayBGM(enemyTank.underAttackBgm);
        }

        TankShoot();
        TankDestruction();
        Destroy(holdingProp?.gameObject);

        if (particleSystem != null)
        {
            Destroy(particleSystem.gameObject);
            particleSystem = null;
        }
    }

    private void TankDestruction()
    {
        canShoot = false;
        for (int i = 0; i < TankSlotsGraphics.Count; i++)
        {
            TankSlotsGraphics[i].DestroySlot();
        }
    }

    public void TankShoot()
    {
        var bullet = Instantiate(BulletPrefab);
        var shotBullet = bullet.AddComponent<ShotBullet>();
        shotBullet.tank = gameObject;
        shotBullet.tankAnimator = animator;
        bullet.transform.position = ShootStartingPoint.position;

        var horizontalTween = bullet.transform.DOMoveX(EnemyTank.transform.position.x, ProjectileTravelTime)
            .SetEase(ShootAnimationCurve);
        var verticalTweenUp = bullet.transform.DOMoveY(EnemyTank.transform.position.y + ShootHeight,
            ProjectileTravelTime / 2f);
        var verticalTweenDown = bullet.transform.DOMoveY(EnemyTank.transform.position.y,
            ProjectileTravelTime / 2f);

        var verticalSequence = DOTween.Sequence();
        verticalSequence.Append(verticalTweenUp).Append(verticalTweenDown).SetEase(ShootAnimationCurve);

        var sequence = DOTween.Sequence().Insert(0f, horizontalTween).Insert(0f, verticalSequence);

        sequence.Play();
        SoundManager.Instance.PlaySFX("sfx_tank_shoot", false);
    }

    public void TankIntro()
    {
        animator.SetTrigger(intro);
    }
}
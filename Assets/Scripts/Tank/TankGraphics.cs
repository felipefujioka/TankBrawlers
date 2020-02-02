using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class TankGraphics : MonoBehaviour
{
    public List<TankSlotGraphics> TankSlotsGraphics;
    //public TankSlotGraphics tankSlotPrefab;
    public TankController tankController;
    public Team team;
    private bool canShoot, isHolding;
    private Prop holdingProp;
    public Animator animator;
    public Slider tankSlider;
    private Coroutine sliderRoutine;
    public Image lifeFill;
    public GameObject repairIcon, shotIcon;
    public static readonly int shoot = Animator.StringToHash("Shoot");
    public static readonly int reset = Animator.StringToHash("Reset");
    public static readonly int intro = Animator.StringToHash("Intro");


    private void Awake()
    {
        tankController = new TankController(this, team);

        if(team == Team.Blue)
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
                sliderRoutine = StartCoroutine(SliderRoutine(() => { ExecuteAddPiece(playerController); }));
            }

            if (playerController.holdingProp is Bullet && canShoot)
            {
                holdingProp = playerController.holdingProp;

                shotIcon.SetActive(true);

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
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == GameConstants.PLAYER_TAG && isHolding)
        {
            PlayerController playerController = collider.GetComponent<PlayerView>().playerController;
            if (playerController.playerTeam == team && (playerController.holdingProp == null || playerController.holdingProp is DestructiveProp))
            {
                isHolding = false;
            }
        }
    }

    public void ExecuteShot()
    {
        TankShoot();
        TankDestruction();
        Destroy(holdingProp?.gameObject);
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
        animator.SetTrigger(shoot);
    }

    public void TankIntro()
    {
        animator.SetTrigger(intro);
    }
}
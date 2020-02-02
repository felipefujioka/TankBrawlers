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
        for (int i = 0; i < tankController.TankSlots.Count; i++)
        {
            TankSlotsGraphics[i].SetupTankSlot(tankController.TankSlots[i]);
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
                sliderRoutine = StartCoroutine(SliderRoutine(() =>
                {
                    TankPiece piece = playerController.holdingProp as TankPiece;
                    for (int i = 0; i < tankController.TankSlots.Count; i++)
                    {
                        var slot = tankController.TankSlots[i];
                        if(slot.Id == piece.Id && team == piece.team)
                        {
                            TankSlotsGraphics[i].AddSlotPiece(piece);
                            playerController.holdingProp = null;
                            piece.cancelGravity();
                            piece.colliderInProps.enabled = false;
                        }
                    }
                    if(tankController.isRepaired)
                    {
                        canShoot = true;
                    }
                }));
            }
            
            if (playerController.holdingProp is Bullet && canShoot)
            {
                holdingProp = playerController.holdingProp;
                
                shotIcon.SetActive(true);
                
                sliderRoutine = StartCoroutine(SliderRoutine(() =>
                {
                    var bullet = playerController.holdingProp as Bullet;
                    
                    Destroy(bullet);
                    animator.SetTrigger(shoot);
                    playerController.holdingProp = null;
                }));
            }
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
}
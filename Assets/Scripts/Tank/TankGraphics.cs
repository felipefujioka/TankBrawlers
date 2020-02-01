using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TankGraphics : MonoBehaviour
{
    public List<TankSlotGraphics> TankSlotsGraphics;
    //public TankSlotGraphics tankSlotPrefab;
    public TankController tankController;
    public Team team;
    private bool canShoot, isHolding;
    public Animator animator;
    public Slider holdSlide;
    public GameObject repairIcon, shotIcon;
    public static readonly int shoot = Animator.StringToHash("Shoot");
    public static readonly int reset = Animator.StringToHash("Reset");


    private void Start()
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
            //TankSlotGraphics slot = Instantiate(tankSlotPrefab);
            TankSlotsGraphics[i].SetupTankSlot(tankController.TankSlots[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            var playerController = other.GetComponent<PlayerController>();
            if (playerController.playerTeam == team && playerController.holdingProp is TankPiece &&
                (playerController.holdingProp as TankPiece).team == team)
            {
                TankPiece piece = playerController.holdingProp as TankPiece;
                
                for (int i = 0; i < tankController.TankSlots.Count; i++)
                {
                    var slot = tankController.TankSlots[i];
                    if(slot.Id == piece.Id && team == piece.color)
                    {
                        TankSlotsGraphics[i].AddSlotPiece(piece);

                        piece.cancelGravity();

                        piece.colliderInProps.enabled = false;
                    }
                }

                if(tankController.isRepaired)
                {
                    canShoot = true;

                    //if futuro do input do jogador
                }
            }
        }

        if (other.gameObject.tag == GameConstants.BULLET_TAG && canShoot)
        {
            var bullet = other.GetComponent<Bullet>();
            //if(Input.GetButtonDown("Grab1"))

            tankController.AddBullet(bullet);

            animator.SetTrigger(shoot);

            tankController.RemoveBullet();

            bullet.cancelGravity(); 

            bullet.colliderInProps.enabled = false; 
        }
    }

    IEnumerator RepairRoutine()
    {
        float count = 0;
        holdSlide.value = 0;
        while (count < GameConstants.TIME_TO_REPAIR)
        {
            holdSlide.value = Mathf.Lerp(0f, 1f, count / GameConstants.TIME_TO_REPAIR);
            count += Time.deltaTime;

            yield return null;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.layer == GameConstants.PROPS_LAYER)
        {
            collider.enabled = true;
        }
    }
}
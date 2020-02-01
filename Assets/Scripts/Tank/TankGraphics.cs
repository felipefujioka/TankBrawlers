using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGraphics : MonoBehaviour
{
    public List<TankSlotGraphics> TankSlotsGraphics;
    //public TankSlotGraphics tankSlotPrefab;
    private TankController tankController;
    public Team color;
    private bool canShoot = false;
    public Animator animator;
    public static readonly int shoot = Animator.StringToHash("Shoot");
    public static readonly int reset = Animator.StringToHash("Reset");


    private void Start()
    {
        tankController = new TankController(this, color);
        
        if(color == Team.Blue)
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

    public void UpdateTankSlots()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == GameConstants.TANK_PIECE_TAG)
        {
            var piece = other.GetComponent<TankPiece>();
            //if(Input.GetButtonDown("Grab1"))
      
            
            for (int i = 0; i < tankController.TankSlots.Count; i++)
            {
                var slot = tankController.TankSlots[i];
                if(slot.Id == piece.Id && color == piece.color)
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

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.layer == GameConstants.PROPS_LAYER)
        {
            collider.enabled = true;
        }
    }
}
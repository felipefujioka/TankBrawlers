using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBullet : MonoBehaviour
{
    public GameObject tank;
    public Animator tankAnimator;

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == GameConstants.BULLET_TAG)
        {
            tankAnimator.SetTrigger(TankGraphics.reset);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == GameConstants.TANK_LAYER && col.gameObject != tank)
        {
            tankAnimator.SetTrigger(TankGraphics.reset);
            print("BOOM... e feijoada");
            col.gameObject.GetComponent<TankGraphics>().tankController.TakeDamage();
            SoundManager.Instance.PlaySFX("sfx_tank_damage", false);
        }
    }
}

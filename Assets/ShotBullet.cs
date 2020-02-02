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
            SoundManager.Instance.StopBGM();
            SoundManager.Instance.PlaySFX("sfx_tank_explode", false);
            //vfx
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == GameConstants.TANK_LAYER && col.gameObject != tank)
        {
            SoundManager.Instance.StopBGM();
            tankAnimator.SetTrigger(TankGraphics.reset);
            col.gameObject.GetComponent<TankGraphics>().tankController.TakeDamage();
        }
    }
}

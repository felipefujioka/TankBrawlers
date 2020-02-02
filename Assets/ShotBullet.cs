using UnityEngine;

public class ShotBullet : MonoBehaviour
{
    public GameObject tank;
    public Animator tankAnimator;

    private Vector3 lastPosition;

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(GameConstants.BULLET_TAG))
        {
            tankAnimator.SetTrigger(TankGraphics.reset);
            SoundManager.Instance.PlayBGM("bgm_gameplay");
            SoundManager.Instance.PlaySFX("sfx_tank_explode", false);
            //vfx
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == GameConstants.TANK_LAYER && col.gameObject != tank)
        {
            SoundManager.Instance.PlayBGM("bgm_gameplay");
            tankAnimator.SetTrigger(TankGraphics.reset);
            col.gameObject.GetComponent<TankGraphics>().tankController.TakeDamage();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        var direction = transform.position - lastPosition;
        var rot = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0, 0, rot * 180f / Mathf.PI - 90f);
        lastPosition = transform.position;
    }
}

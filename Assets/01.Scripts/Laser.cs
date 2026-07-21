using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour, IPoolable
{
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        if (col != null)
        {
            col.enabled = false;
        }
        StartCoroutine(LaserDelay());

        SceneManager.sceneLoaded += BulletReturn;
    }
    protected void OnDisable()
    {
        SceneManager.sceneLoaded -= BulletReturn;
    }

    protected void BulletReturn(Scene scene, LoadSceneMode mode)
    {
        ReturnPool();
    }

    private IEnumerator LaserDelay()
    {
        yield return new WaitForSeconds(1f + (20f / 60f));

        SoundManager.instance.PlaySFX(SFXType.Laser);

        if (col != null)
        {
            col.enabled = true;
        }       

        yield return new WaitForSeconds((50f / 60f));

        if (col != null)
        {
            col.enabled = false;
        }

        ReturnPool();
    }

    public void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject(ConstString.laser, this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            collision.GetComponent<PlayerController>().CollisionObject();
        }
    }
}

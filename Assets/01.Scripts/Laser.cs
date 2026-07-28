using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour, IPoolable
{
    private Collider2D col;

    [SerializeField] private float firstDelay = (1f + (20f / 60f));
    [SerializeField] private float finishDelay = ((50f / 60f));

    private WaitForSeconds wait;
    private WaitForSeconds wait2;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        wait = new WaitForSeconds(firstDelay);
        wait2 = new WaitForSeconds(finishDelay);
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
        yield return wait;

        SoundManager.instance.PlaySFX(SFXType.Laser);

        if (col != null)
        {
            col.enabled = true;
        }       

        yield return wait2;

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
            collision.GetComponent<PlayerController>().TakeDamage();
        }
    }
}

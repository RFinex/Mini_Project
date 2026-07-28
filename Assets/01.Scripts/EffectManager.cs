using UnityEngine;
using UnityEngine.SceneManagement;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    [Header("Prefabs")]
    [SerializeField] GameObject deathParticle;

    [Header("Target")]
    [SerializeField] Transform target;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        
    }

    public void Init(GameObject player)
    {
        target = player.transform;
    }

    public void ShowDeathParticle()
    {
        if (deathParticle != null)
        {
            GameObject particle = ObjectPoolManager.instance.GetObject("deathParticle");
            particle.transform.position = target.position;
            ParticleSystem ps = particle.GetComponent<ParticleSystem>();
            ps.Play();
        }
    }

    public void ShowExplodeEffect(Vector3 pos)
    {
        GameObject explode = ObjectPoolManager.instance.GetObject("explode");
        explode.transform.position = pos;
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    [SerializeField] GameObject deathParticle;
    [SerializeField] Transform target;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
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
}

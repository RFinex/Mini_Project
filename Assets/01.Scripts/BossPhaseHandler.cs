using UnityEngine;
using System.Collections.Generic;

public class BossPhaseHandler : MonoBehaviour
{
    [SerializeField] private List<BossPatternBase> patterns;
    public List<BossPatternBase> Patterns
    {
        get
        {
            return patterns;
        }
    }

    private void OnValidate()
    {
        patterns = new List<BossPatternBase>(GetComponentsInChildren<BossPatternBase>());
    }
}

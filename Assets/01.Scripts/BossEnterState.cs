using UnityEngine;

public class BossEnterState : IState<BossController>
{
    private Vector3 enterTarget;

    public void Enter(BossController obj)
    {
        enterTarget = obj.transform.position + Vector3.down * obj.MoveDis;
    }

    public void Exit(BossController obj)
    {
        SoundManager.instance.PlayBGM(BGMType.Boss);
        obj.SetBossHpBar();
        obj.Col.enabled = true;
    }

    public void Update(BossController obj)
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, enterTarget, obj.EnterSpeed * Time.deltaTime);
        if (obj.transform.position == enterTarget)
        {
            obj.ChangeState(BossState.Idle);
        }
    }
}

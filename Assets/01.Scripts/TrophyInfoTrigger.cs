using UnityEngine;

public class TrophyInfoTrigger : MonoBehaviour
{
    [SerializeField] private int trophyId;

    [SerializeField] private GameObject trophyObj;

    private bool isCollect;

    private void Start()
    {
        InitTrophyInfo();
    }

    private void OnEnable()
    {
        if (DataManager.instance != null)
        {
            DataManager.instance.trophyUpdate -= InitTrophyInfo;
            DataManager.instance.trophyUpdate += InitTrophyInfo;
        }
        
    }

    private void OnDisable()
    {
        if (DataManager.instance != null)
        {
            DataManager.instance.trophyUpdate -= InitTrophyInfo;
        }        
    }

    public void InitTrophyInfo()
    {
        Trophy trophy = DataManager.instance.GetTrophyData(trophyId);

        if (trophy == null)
        {
            Debug.Log("존재하지 않는 트로피");
            return;
        }

        isCollect = trophy.isCollect;

        if (trophyObj != null)
        {
            trophyObj.SetActive(isCollect);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            Trophy trophy = DataManager.instance.GetTrophyData(trophyId);

            if (trophy == null)
            {
                Debug.Log("존재하지 않는 트로피");
                return;
            }

            UIManager.instance.OpenTrophyInfo(trophy.id);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            UIManager.instance.CloseTrophyInfo();
        }
    }
}

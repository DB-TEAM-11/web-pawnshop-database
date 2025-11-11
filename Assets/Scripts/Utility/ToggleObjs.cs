using UnityEngine;
using UnityEngine.UI;

public enum CustomerState
{
    Deal,
    Sell,
    None
}

public class ToggleObjs : MonoBehaviour
{
    [SerializeField] private GameObject dealObjs;
    [SerializeField] private GameObject sellObjs;
    [SerializeField] private GameObject newsObjs;
    [SerializeField] private GameObject newsButton;
    [SerializeField] private GameObject newsBlackFilter;

    public void ToggleDealSellObjs()
    {
        if(SingletonManager.Instance.IsCustomerDealState == CustomerState.Deal)
        {
            dealObjs.SetActive(!dealObjs.activeSelf);
        }
        else if(SingletonManager.Instance.IsCustomerDealState == CustomerState.Sell)
        {
            dealObjs.SetActive(!dealObjs.activeSelf);
        }
    }


    public void TurnOnDealObjs()
    {
        dealObjs.SetActive(true);
    }

    public void TurnOnSellObjs()
    {
        sellObjs.SetActive(true);
    }

    public void TurnOnNewsObjs()
    {
        newsButton.SetActive(false);
        newsBlackFilter.SetActive(true);
        newsObjs.SetActive(true);
    }

    public void TurnOffDealObjs()
    {
        dealObjs.SetActive(false);
    }

    public void TurnOffSellObjs()
    {
        sellObjs.SetActive(false);
    }

    public void TurnOffNewsObjs()
    {
        newsButton.SetActive(true);
        newsBlackFilter.SetActive(false);
        newsObjs.SetActive(false);
    }

}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;
using TMPro;


namespace AYellowpaper.SerializedCollections
{
public class DealManager : MonoBehaviour
{
    [SerializeField] private GameObject dealObjs;
    [SerializeField] private GameObject deseBlackFilter;
    [SerializeField] private GameObject trayItemObj;
    [SerializeField] private GameObject customerObj;
    [SerializeField] private GameObject customerInformPanel;

    [SerializedDictionary("order","DealData")] // 얘는 데이터 저장용 dict
    public SerializedDictionary<int, DealData> dailyDealsMap = new SerializedDictionary<int, DealData>();
    
    private int flawPurchasePrice=-999;
    private int flawAppraisedPrice=-999;
    private int authPurchasePrice=-999;
    private int authAppraisedPrice=-999;
    private int gradePurchasePrice=-999;
    private int gradeAppraisedPrice=-999;

    private int totalPurchasePrice=-999;
    private int totalAppraisedPrice=-999;
    


    void Start()
    {
        dealObjs.transform.GetChild(3).GetChild(0).GetComponent<Button>().onClick.AddListener(DecideDeal);
        dealObjs.transform.GetChild(4).GetChild(0).GetComponent<Button>().onClick.AddListener(DenyDeal);
        
        // 테스트용 코드
        TextAsset jsonFile = (TextAsset)AssetDatabase.LoadAssetAtPath("Assets/Mocks/10generateDailyDeals.json", typeof(TextAsset));
        DailyDealsWrapData dailyDealsWrapData =JsonUtility.FromJson<DailyDealsWrapData>(jsonFile.text);
        InitDailyDeals(dailyDealsWrapData.dailyDeals);
    }

    public void InitDailyDeals(List<DealData> dailyDeals)
    {
        for(int i = 0; i < 3; i++)
        {
            dailyDealsMap.Add(i,dailyDeals[i]);
        }
        SetDealPanelToNewData(dailyDealsMap[0]); // 최초 거래로 설정해둠
        // 트레이 아이템 이미지 설정
        // 트레이 아이템 정보 패널 텍스트 설정
    }

    public void SetDealPanelToNewData(DealData dData)
    {
        // 고객 설정
        CustomerCatalogData cData =SingletonManager.Instance?.GetCustomerCatalog(dData.customerKey);
        customerObj.GetComponent<Image>().sprite = Resources.Load<Sprite>($"IMG_CUSTOMER/{cData.imgId}");
        // 고객 패널 설정
        customerInformPanel.transform.GetChild(1).GetComponent<TMP_Text>().text
            = $"{cData.customerName}\n[{cData.favoriteCategoryName}]";
        // 아이템 설정
        ItemCatalogData iData = SingletonManager.Instance?.GetItemCatalog(dData.itemCatalogKey);
        trayItemObj.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>($"IMG_ITEM_CATALOG/{iData.imgId}");
        //trayItemObj.transform.GetChild(1).GetComponent<Image>().SetNativeSize();
        // 아이템 패널 설정
        trayItemObj.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text
            = $"{iData.itemCatalogName}\n[{iData.categoryName}]";
        // 거래 패널 설정
/*
  "dailyDeals": [
    {
      "drcKey": 201,
      "askingPrice": 1200,
      "purchasePrice": 1200,
      "appraisedPrice": 1200,
      "itemKey": 301,
      "itemCatalogKey": 80,
      "foundGrade": 0,
      "foundFlawEa": 0,
      "isAuthenticityFound": false,
      "customerKey": 3
    },
*/
        
    }

    private void CalFlawPrice(int flawEA)
    {
        // flawPurchase;
    }
    private void CalAuthPurchasePrice()
    {
        
    }
    private void CalAuthAppraisedPrice()
    {
        
    }
    private void CalGradePurchasePrice()
    {
        
    }
    private void CalGradeAppraisedPrice()
    {
        
    }

    // private void 



    public void SendData() // 그러고 이런 것들을 버튼에 연결해가지고 이걸 실행하면 서버에서 주겠지
    {
        
    }

    private void DecideDeal()
    {
        // 딜 오브젝트 끄기
        deseBlackFilter.SetActive(false);
        dealObjs.SetActive(false);
        // 
    }

    private void DenyDeal()
    {
        // 딜 오브젝트 끄기
        deseBlackFilter.SetActive(false);
        dealObjs.SetActive(false);
        //
    }
    
    public void PopupItemInform()
    {
        trayItemObj.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void PopoffItemInform()
    {
        trayItemObj.transform.GetChild(2).gameObject.SetActive(false);        
    }
    public void PopupCustomerInform()
    {
        customerInformPanel.SetActive(true);
    }

    public void PopoffCustomerInform()
    {
        customerInformPanel.SetActive(false);        
    }

}
}
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEditor;

public class SingletonManager : MonoBehaviour
{
    private static SingletonManager instance = null;

    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Dictionary<int, ItemCatalogData> itemCatalogMap =  
                        new Dictionary<int, ItemCatalogData>();
    [SerializeField] private Dictionary<int, CustomerCatalogData> customerCatalogMap =  
                        new Dictionary<int, CustomerCatalogData>();

    private CustomerState isCustomerDealState;

    public CustomerState IsCustomerDealState
    {
        get 
        {
            return isCustomerDealState; // 속성 값을 반환
        }
        set
        {
            isCustomerDealState = value;
        }
    }

    void Awake()
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static SingletonManager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }


    public Canvas getMainCanvas()
    {
        return mainCanvas;
    }

    void Start()
    {
        // 나중에 이거 데이웨이브매니저에서 실행해야하는 거 알지?
        TextAsset jsonFile = (TextAsset)AssetDatabase.LoadAssetAtPath("Assets/Mocks/5initialCatalog.json", typeof(TextAsset));
        InitialCatalogResponse displayWrap =JsonUtility.FromJson<InitialCatalogResponse>(jsonFile.text);
        InitCatalogMaps(displayWrap);
    }


    public void InitCatalogMaps(InitialCatalogResponse catalogs)
    {
        for(int i = 0; i < catalogs.itemCatalogs.Count; i++)
        {
            itemCatalogMap.Add(catalogs.itemCatalogs[i].itemCatalogKey, catalogs.itemCatalogs[i]);
        }
        for(int i = 0; i < catalogs.customerCatalogs.Count; i++)
        {
            customerCatalogMap.Add(catalogs.customerCatalogs[i].customerKey, catalogs.customerCatalogs[i]);
        }
    }

    public ItemCatalogData GetItemCatalog(int itemCatalogKey)
    {
        return itemCatalogMap[itemCatalogKey];
    }
    public CustomerCatalogData GetCustomerCatalog(int customerKey)
    {
        return customerCatalogMap[customerKey];
    }
}

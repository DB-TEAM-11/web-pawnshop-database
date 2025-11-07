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

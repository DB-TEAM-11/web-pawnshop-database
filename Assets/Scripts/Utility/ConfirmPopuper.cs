using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConfirmPopuper : MonoBehaviour
{
    private static ConfirmPopuper instance = null;

    [SerializeField] GameObject popupUI=null;
    [SerializeField] Canvas mainCanvas=null;

    private GameObject popupInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if((popupUI = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/CHECK_POPUP.prefab", typeof(GameObject)))==null)
        {
            Debug.LogError("there's no CHECK_POPUP.prefab");
        }
        if((mainCanvas = AYellowpaper.SerializedCollections.SingletonManager.Instance.getMainCanvas()) == null)
        {
            Debug.LogError("can't get mainCanvas by SingletonManager. check ConfirmPopuper");            
        }
    }

    public void PopupCheckPanel(string log, Action action)
    {
        popupUI.transform.GetChild(3).GetComponent<TMP_Text>().text = log;
        popupInstance=Instantiate(popupUI, mainCanvas.transform);
        popupInstance.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(CloseCheckPanel);
        if(action != null)
        {
            popupInstance.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(()=>action.Invoke());
        }
    }

    void CloseCheckPanel()
    {
        Destroy(popupInstance);
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

    public static ConfirmPopuper Instance
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
}

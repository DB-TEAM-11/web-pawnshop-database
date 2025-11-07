using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPopuper : MonoBehaviour
{
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
        if((mainCanvas = SingletonManager.Instance.getMainCanvas()) == null)
        {
            Debug.LogError("can't get mainCanvas by SingletonManager. check ConfirmPopuper");            
        }
        PopupCheckPanel("안녕하세요 이건 테스트 메시지입니다, 한번 보내볼게요 뿌뿌뿡");

    }

    public void PopupCheckPanel(string log)
    {
        popupUI.transform.GetChild(3).GetComponent<TMP_Text>().text = log;
        popupInstance=Instantiate(popupUI, mainCanvas.transform);
        popupInstance.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(CloseCheckPanel);
    }

    void CloseCheckPanel()
    {
        Destroy(popupInstance);
    }
}

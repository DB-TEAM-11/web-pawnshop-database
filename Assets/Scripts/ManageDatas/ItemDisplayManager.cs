using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
namespace AYellowpaper.SerializedCollections
{
public class ItemDisplayManager : MonoBehaviour
{

    [SerializedDictionary("position Number","DisplayObject")]
    public SerializedDictionary<int,GameObject> dict = new SerializedDictionary<int, GameObject>();
    [SerializedDictionary("position key","DisplayData")]
    public SerializedDictionary<int, DisplayedItemData> itemDisplayMap = new SerializedDictionary<int, DisplayedItemData>();
    // 전시 위치 별 아이템 정보 매핑

    void Start()
    {
        TextAsset jsonFile = (TextAsset)AssetDatabase.LoadAssetAtPath("Assets/Mocks/6displayItemAll.json", typeof(TextAsset));
        ItemDisplaysWrapData displayWrap =JsonUtility.FromJson<ItemDisplaysWrapData>(jsonFile.text);
        InitDisplayedItem(displayWrap.displays);
    }

    public void InitDisplayedItem(List<DisplayedItemData> displays)
    {  
        for(int i = 0; i < displays.Count; i++)
        {
            itemDisplayMap.Add(displays[i].displayPositionKey,displays[i]);
        }

        // TODO: 아이템 오브젝트에 데이터 적용하기
    }


}
}

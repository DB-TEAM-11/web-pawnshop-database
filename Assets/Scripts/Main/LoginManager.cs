using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using TMPro;

public enum LoginState
{
    LOGIN,
    IN_LOGIN
}

public class LoginManager : MonoBehaviour
{
    [SerializeField] private GameObject LoginResisterObjs;
    [SerializeField] private GameObject worldRecordPanel;

    [SerializeField] private GameObject inLoginObjs;

    [SerializeField] private GameObject wrldRecordRowPrefab;

    private LoginState loginState = LoginState.LOGIN;

    private string playerIdCache="";

    void Start() // 게임 시작할 때
    {
        LoadWorldRecord(); // 세계 기록 미리 불러오기
    }

    
    public void RequestToLogin()
    {
        PlayerRegisterLoginRequest requestData = new PlayerRegisterLoginRequest();
        requestData.playerId = LoginResisterObjs.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text;
        requestData.password = LoginResisterObjs.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text;
        LoginResponse responseData =TransmissionManager.Instance.RequestToServer<PlayerRegisterLoginRequest,LoginResponse>(RequestType.LOGIN,requestData);
        if(responseData != default)
        {
            // 로그인 초기화
            playerIdCache =requestData.playerId; // 아이디는 캐시에 저장
            LoginResisterObjs.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "";
            LoginResisterObjs.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "";
            // 이러면 이제 세션 토큰 받았음 >> 이거 트랜스미션 매니저에 전달해야 됨
        }
        else
        {
            Debug.LogError("로그인 실패");
            // TODO: 로그인 실패 처리
        }
    }
    public void RequestToRegister()
    {
        
    }


    public void GotoInLoginState() // 로그인 버튼 누르면 발동
    {
        loginState = LoginState.IN_LOGIN;
        LoginResisterObjs.SetActive(false);
        inLoginObjs.SetActive(true);
    }


    private void LoadWorldRecord()
    {
        TextAsset jsonFile = (TextAsset)AssetDatabase.LoadAssetAtPath("Assets/Mocks/20worldRecords.json", typeof(TextAsset));
        WorldRecordResponse worldRecordsData =JsonUtility.FromJson<WorldRecordResponse>(jsonFile.text);

        GameObject conetentObj =  worldRecordPanel.transform.GetChild(2).GetChild(0).GetChild(0).gameObject;

        for(int i = 0; i < worldRecordsData.worldRecords.Count; i++)
        {
            GameObject wrldRecordRow=Instantiate(wrldRecordRowPrefab, conetentObj.transform);
            // Nickname
            wrldRecordRow.transform.GetChild(1).GetComponent<TMP_Text>().text
                = $"#{worldRecordsData.worldRecords[i].playerId}";
            wrldRecordRow.transform.GetChild(7).GetComponent<TMP_Text>().text
                = worldRecordsData.worldRecords[i].nickname;
            // Shopname
            wrldRecordRow.transform.GetChild(8).GetComponent<TMP_Text>().text
                = worldRecordsData.worldRecords[i].pawnshopName;
            // DayCount
            wrldRecordRow.transform.GetChild(9).GetComponent<TMP_Text>().text
                = $"{string.Format("{0:#,0}",worldRecordsData.worldRecords[i].clearDayCount)} 일";
            // Date
            wrldRecordRow.transform.GetChild(10).GetComponent<TMP_Text>().text
                = worldRecordsData.worldRecords[i].clearDate;
            // Profit
            wrldRecordRow.transform.GetChild(11).GetComponent<TMP_Text>().text
                = $"{string.Format("{0:#,0}",worldRecordsData.worldRecords[i].largestProfit)} G";
        }
    }

    public void OpenWorldRecord()
    {
        worldRecordPanel.SetActive(true);
    }
    public void CloseWorldRecord()
    {
        worldRecordPanel.SetActive(false);
    }

}

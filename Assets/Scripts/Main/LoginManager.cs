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
    private TMP_Text loginIdTMP;
    private TMP_Text loginPwTMP;
    private TMP_Text RegisterIdTMP;
    private TMP_Text RegisterPwTMP;

    void Start() // 게임 시작할 때
    {
        LoadWorldRecord(); // 세계 기록 미리 불러오기
        InitLocalVar(); // 변수 초기화
    }

    private void InitLocalVar()
    {
        loginIdTMP =LoginResisterObjs.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        loginPwTMP =LoginResisterObjs.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        RegisterIdTMP =LoginResisterObjs.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        RegisterPwTMP =LoginResisterObjs.transform.GetChild(1).GetChild(3).GetChild(0).GetChild(1).GetComponent<TMP_Text>();
    }

    public void RequestToLogOut(){
        // PlayerRegisterLoginRequest requestData = new PlayerRegisterLoginRequest();
        // requestData.playerId = loginIdTMP.text;
        // requestData.password = loginPwTMP.text;
        // LoginResponse responseData =TransmissionManager.Instance.RequestToServer<PlayerRegisterLoginRequest,LoginResponse>(RequestType.LOGIN,requestData);

        // responseData= new LoginResponse(); //
        // responseData.sessionToken = "ang Kimoti"; // <<<<<<<< 확인용 코드

        // if(responseData != default)
        // {
        //     // 로그인 초기화
        //     playerIdCache =requestData.playerId; // 아이디는 캐시에 저장
        //     loginIdTMP.text = "";
        //     loginPwTMP.text = "";
        //     RegisterIdTMP.text = "";
        //     RegisterPwTMP.text = "";
        //     // 트랜스미션에 세션토큰 전달
        //     TransmissionManager.Instance.SetSessionToken(responseData.sessionToken);

        //     // 로그인패널 비활성화, 인로그인패널 활성화
        //     loginState =LoginState.IN_LOGIN;
        //     LoginResisterObjs.SetActive(false);
        //     inLoginObjs.SetActive(true);

        //     string log = "로그인 성공";
        //     ConfirmPopuper.Instance?.PopupCheckPanel(log);            
        // }
        // else
        // {
        //     // 로그인 실패 처리
        //     Debug.Log("로그인 실패");
        //     string log = "로그인 실패.\nID와 PW를 잘 확인해보세요";
        //     ConfirmPopuper.Instance?.PopupCheckPanel(log);  
        // }
    }


    public void RequestToLogin()
    {
        PlayerRegisterLoginRequest requestData = new PlayerRegisterLoginRequest();
        requestData.playerId = loginIdTMP.text;
        requestData.password = loginPwTMP.text;
        LoginResponse responseData =TransmissionManager.Instance.RequestToServer<PlayerRegisterLoginRequest,LoginResponse>(RequestType.LOGIN,requestData);

        responseData= new LoginResponse(); //
        responseData.sessionToken = "ang Kimoti"; // <<<<<<<< 확인용 코드

        if(responseData != default)
        {
            // 로그인 초기화
            playerIdCache =requestData.playerId; // 아이디는 캐시에 저장
            loginIdTMP.text = "";
            loginPwTMP.text = "";
            RegisterIdTMP.text = "";
            RegisterPwTMP.text = "";
            // 트랜스미션에 세션토큰 전달
            TransmissionManager.Instance.SetSessionToken(responseData.sessionToken);

            // 로그인패널 비활성화, 인로그인패널 활성화
            loginState =LoginState.IN_LOGIN;
            LoginResisterObjs.SetActive(false);
            inLoginObjs.SetActive(true);

            string log = "로그인 성공";
            ConfirmPopuper.Instance?.PopupCheckPanel(log);            
        }
        else
        {
            // 로그인 실패 처리
            Debug.Log("로그인 실패");
            string log = "로그인 실패.\nID와 PW를 잘 확인해보세요";
            ConfirmPopuper.Instance?.PopupCheckPanel(log);  
        }
    }

    public void RequestToRegister()
    {
        PlayerRegisterLoginRequest requestData = new PlayerRegisterLoginRequest();
        requestData.playerId = RegisterIdTMP.text;
        requestData.password = RegisterPwTMP.text;
        // TODO: 해당 default가 성공인지 실패인지 알려면
        int responseCode =TransmissionManager.Instance.RequestToServer<PlayerRegisterLoginRequest,int>(RequestType.LOGIN,requestData);
        responseCode =200; // <<<<<<<<<<<<<<< 확인용 코드
        if(responseCode == 200)
        {
            loginState =LoginState.IN_LOGIN;
            string log = "회원가입 성공";
            ConfirmPopuper.Instance?.PopupCheckPanel(log);            
        }
        else if(responseCode == 400)
        {
            string log = "회원가입 실패.\nID가 중복됩니다.";
            ConfirmPopuper.Instance?.PopupCheckPanel(log);  
            // TODO: 로그인 실패 처리
        }
        else{
            Debug.LogError("회원가입 통신 실패");
            string log = "회원가입 실패.\n통신이 되지 않습니다.";
            ConfirmPopuper.Instance?.PopupCheckPanel(log);  
            // TODO: 로그인 실패 처리
        }
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
            // wrldRecordRow.transform.GetChild(10).GetComponent<TMP_Text>().text
            //     = worldRecordsData.worldRecords[i].clearDate;
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

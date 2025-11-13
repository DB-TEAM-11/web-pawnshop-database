using UnityEngine;

public enum LoginState
{
    LOGIN,
    IN_LOGIN
}

public class LoginManager : MonoBehaviour
{
    [SerializeField] private GameObject LoginResisterObjs;
    [SerializeField] private GameObject LoginObjs;
    [SerializeField] private GameObject RegisterObjs;
    [SerializeField] private GameObject WorldRecordPanel;


    
    public void LoadWorldRecord()
    {
        // 20worldRecords.json
    }

    public void OpenWorldRecord()
    {
        WorldRecordPanel.SetActive(true);
    }
    public void CloseWorldRecord()
    {
        WorldRecordPanel.SetActive(false);
    }

}

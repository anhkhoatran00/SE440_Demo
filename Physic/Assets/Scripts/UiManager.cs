using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private static UiManager _instance;
    public static UiManager Instance => _instance;
    [SerializeField]
    private GameObject infoPanel;
    [SerializeField]
    private TextMeshProUGUI txtInformation;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowInfoPanel(GameObject go, Vector3 pos)
    {
        transform.position = pos;
        txtInformation.text = go.name;
        infoPanel.GetComponent<CanvasGroup>().alpha = 1f;
    }
}

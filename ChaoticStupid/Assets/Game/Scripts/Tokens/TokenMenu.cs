using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using mudz;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using Photon.Pun;

public class TokenMenu : MonoBehaviourPun
{
    [SerializeField] GameObject ui;
    Dick dick;
    [SerializeField] GameObject settingsScreen;
    [SerializeField] TMP_Dropdown controlSelector;

    [SerializeField] int currentControllingPlayer;
    List<string> ids = new List<string>();

    bool uiToggle = false;
    bool settingsToggle = false;

    SpawnPlayers playerSpawner;

    void Start(){
        this.GetComponent<Canvas>().worldCamera = Camera.main;
        playerSpawner = FindObjectOfType<SpawnPlayers>();
    }
    

    #region toggles

    [SerializeField] float i = 0;
    [SerializeField] bool holdRunning = false;
    void Update(){
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartCoroutine(MouseHoldTime());
        }
        // Debug.Log($"After Coroutine, i = {i}");
    }
    public void ToggleUI(){
        StartCoroutine(DoIt());
    }
    IEnumerator DoIt(){
        yield return new WaitWhile(() => holdRunning);
        if(i > 0.12){yield break;}

        // Debug.Log("Clicked UI toggle");
        uiToggle = !uiToggle;

        ui.SetActive(uiToggle);
    }

    IEnumerator MouseHoldTime(){
        holdRunning = true;
        float a = Time.unscaledTime;
        yield return new WaitWhile(() => Mouse.current.leftButton.isPressed);
        float b = Time.unscaledTime;
        i = b - a;
        // Debug.Log($"{b} - {a} = {i}");
        holdRunning = false;
        yield return null;
    }

    public void ToggleSettings(){
        settingsToggle = !settingsToggle;
        settingsScreen.SetActive(settingsToggle);

        if(!settingsToggle){return;}
        GetDropdownData();
    }

    #endregion

    public void ValueChanged(){
        if(!photonView.IsMine){return;}
        currentControllingPlayer = dick.TryGetKey(
            playerSpawner.playerIds, 
            ids[controlSelector.value]
        );
    }

    void GetDropdownData(){
        foreach(string nick in playerSpawner.playerIds.Values){
            ids.Add(nick);
            // Debug.Log(nick);
        }
        controlSelector.AddOptions(ids);
        // Debug.Log(ids);
    }

}

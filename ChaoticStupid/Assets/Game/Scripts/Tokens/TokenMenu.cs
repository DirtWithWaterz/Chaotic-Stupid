using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using mudz;
using UnityEngine.InputSystem;

public class TokenMenu : MonoBehaviourPun
{
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private TMP_Dropdown controlSelector;
    [SerializeField] int currentControllingPlayer;

    private List<string> ids = new List<string>();
    Dick dick;

    private bool uiToggle = false;
    private bool settingsToggle = false;

    private SpawnPlayers playerSpawner;

    void Start()
    {
        this.GetComponent<Canvas>().worldCamera = Camera.main;
        playerSpawner = FindObjectOfType<SpawnPlayers>();
    }

    #region toggles

    private float i = 0;
    private bool holdRunning = false;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartCoroutine(MouseHoldTime());
        }
    }

    public void ToggleUI()
    {
        StartCoroutine(DoIt());
    }

    private IEnumerator DoIt()
    {
        yield return new WaitWhile(() => holdRunning);
        if (i > 0.12) { yield break; }

        uiToggle = !uiToggle;
        ui.SetActive(uiToggle);
    }

    private IEnumerator MouseHoldTime()
    {
        holdRunning = true;
        float a = Time.unscaledTime;
        yield return new WaitUntil(() => !Mouse.current.leftButton.isPressed);
        float b = Time.unscaledTime;
        i = b - a;
        holdRunning = false;
    }

    public void ToggleSettings()
    {
        settingsToggle = !settingsToggle;
        settingsScreen.SetActive(settingsToggle);

        if (!settingsToggle) { return; }
        GetDropdownData();
    }

    #endregion

    public void ValueChanged(int j)
    {
        // if (!photonView.IsMine) { return; }
        currentControllingPlayer = dick.TryGetKey(playerSpawner.playerIds, ids[j]);
    }

    private void GetDropdownData()
    {
        ids.Clear();
        foreach (string nick in playerSpawner.playerIds.Values)
        {
            if (ids.Contains(nick)) { continue; }
            ids.Add(nick);
        }
        controlSelector.ClearOptions();
        controlSelector.AddOptions(ids);
    }

}
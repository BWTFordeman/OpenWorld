using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour {
    public InputManager inputManager;

    public bool options = false;

    public DialogManager manager;
    public PlayerMovement player;

    public GameObject dialogBox;
    public GameObject optionsMenu;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        inputManager.RegisterAction(InputManager.Keys.escape, () => ToggleOptions());
    }

    public void ToggleOptions()
    {
        if (dialogBox.activeSelf)
            manager.CloseBox();
        else
        {
            options = !options;
            player.rotate = !options;
            optionsMenu.SetActive(options);
            Cursor.visible = options;
            Cursor.lockState = (options) ? CursorLockMode.None : CursorLockMode.Locked; 
        }
    }
    
}

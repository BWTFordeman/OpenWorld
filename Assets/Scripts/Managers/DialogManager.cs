using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogManager : MonoBehaviour {
    public static DialogManager manager = null;

    public InputManager inputManager;

    public GameObject dialogBox;
    public Text dialogText;

    public Button next;
    public Button close;
    public Button giveQuest;
    public Button accept;
    public Button decline;

    public string[] dialogs;
    public int currentLine;

    [SerializeField]
    private PlayerMovement player;
    

    private void Awake()
    {
        if (manager == null)
            manager = this;
        else if (manager != this)
            Destroy(gameObject);

        DontDestroyOnLoad(transform.parent);
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        dialogText.text = dialogs[0];

        close.onClick.AddListener(CloseBox);
        next.onClick.AddListener(NextLine);
	}

	
	void Update () {
        if (!(accept.gameObject.activeSelf || decline.gameObject.activeSelf))
        {
            if (currentLine < dialogs.Length - 1)
            {
                next.gameObject.SetActive(true);
                close.gameObject.SetActive(false);
            }
            else
            {
                next.gameObject.SetActive(false);
                close.gameObject.SetActive(true);
            }
        }
        else
        {
            next.gameObject.SetActive(false);
            close.gameObject.SetActive(false);
        }
    }

    public void CloseBox()
    {
        currentLine = 0;
        player.rotate = true;
        player.move = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        accept.gameObject.SetActive(false);
        decline.gameObject.SetActive(false);
        dialogBox.SetActive(false);
    }

    void NextLine()
    {
        dialogText.text = dialogs[++currentLine];
    }

    public void ShowBox(string text)
    {
        dialogText.text = text;
        dialogBox.SetActive(true);
    }

    public void ShowDialog(string[] text)
    {
        player.rotate = false;
        player.move = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        dialogs = text;
        dialogText.text = dialogs[0];
        dialogBox.SetActive(true);
    }
}

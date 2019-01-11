using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public static InputManager manager = null;

    public enum Keys
    {
        escape,
        forward,
        backward,
        left,
        right,
        jump,
        mouse1,
        mouse2,
        toggleView,
        movement,
        interract
    };

    [System.Serializable]
    public struct Actions
    {
        public List<Action> escape;
        public List<Action> forward;
        public List<Action> backward;
        public List<Action> left;
        public List<Action> right;
        public List<Action> jump;
        public List<Action> mouse0;
        public List<Action> mouse1;
        public List<Action> toggleView;
        public List<Action> movement;
        public List<Action> interract;
    };

    public struct CallbackData
    {
        public float xAxis;
        public float yAxis;

        public KeyCode key;
        public string button;
    };

    [SerializeField]
    private Actions actions;
    public CallbackData data;

    private void Awake()
    {
        if (manager == null)
            manager = this;
        else if (manager != this)
            Destroy(gameObject);

        actions.escape = new List<Action>();
        actions.forward = new List<Action>();
        actions.backward = new List<Action>();
        actions.left = new List<Action>();
        actions.right = new List<Action>();
        actions.jump = new List<Action>();
        actions.mouse0 = new List<Action>();
        actions.mouse1 = new List<Action>();
        actions.toggleView = new List<Action>();
        actions.movement = new List<Action>();
        actions.interract = new List<Action>();
    }

    public int RegisterAction(Keys key, Action action)
    {
        switch (key)
        {
            case Keys.escape:
                actions.escape.Add(action);
                return actions.escape.IndexOf(action);
            case Keys.forward:
                actions.forward.Add(action);
                return actions.forward.IndexOf(action);
            case Keys.backward:
                actions.backward.Add(action);
                return actions.backward.IndexOf(action);
            case Keys.left:
                actions.left.Add(action);
                return actions.left.IndexOf(action);
            case Keys.right:
                actions.right.Add(action);
                return actions.right.IndexOf(action);
            case Keys.jump:
                actions.jump.Add(action);
                return actions.jump.IndexOf(action);
            case Keys.mouse1:
                actions.mouse0.Add(action);
                return actions.mouse0.IndexOf(action);
            case Keys.mouse2:
                actions.mouse1.Add(action);
                return actions.mouse1.IndexOf(action);
            case Keys.toggleView:
                actions.toggleView.Add(action);
                return actions.toggleView.IndexOf(action);
            case Keys.movement:
                actions.movement.Add(action);
                return actions.movement.IndexOf(action);
            case Keys.interract:
                actions.interract.Add(action);
                return actions.interract.IndexOf(action);
            default:
                return -1;
        }
    }

    public bool RemoveAction(Keys key, Action action)
    {
        switch (key)
        {
            case Keys.escape:
                actions.escape.Remove(action);
                return true;
            case Keys.forward:
                actions.forward.Remove(action);
                return true;
            case Keys.backward:
                actions.backward.Remove(action);
                return true;
            case Keys.left:
                actions.left.Remove(action);
                return true;
            case Keys.right:
                actions.right.Remove(action);
                return true;
            case Keys.jump:
                actions.jump.Remove(action);
                return true;
            case Keys.mouse1:
                actions.mouse0.Remove(action);
                return true;
            case Keys.mouse2:
                actions.mouse1.Remove(action);
                return true;
            case Keys.toggleView:
                actions.toggleView.Remove(action);
                return true;
            case Keys.movement:
                actions.movement.Remove(action);
                return true;
            case Keys.interract:
                actions.interract.Remove(action);
                return true;
            default:
                return false;
        }
    }

    private void CreateData(KeyCode key)
    {
        data.xAxis = Input.GetAxis("Vertical");
        data.yAxis = Input.GetAxis("Horizontal");

        data.key = key;
    }
    private void CreateData(string key)
    {
        data.xAxis = Input.GetAxis("Vertical");
        data.yAxis = Input.GetAxis("Horizontal");

        data.button = key;
    }


    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            CreateData(KeyCode.W);
            foreach (Action func in actions.forward)
                func();
        }
        if (Input.GetKey(KeyCode.S))
        {
            CreateData(KeyCode.S);
            foreach (Action func in actions.backward)
                func();
        }
        if (Input.GetKey(KeyCode.A))
        {
            CreateData(KeyCode.A);
            foreach (Action func in actions.left)
                func();
        }
        if (Input.GetKey(KeyCode.D))
        {
            CreateData(KeyCode.D);
            foreach (Action func in actions.left)
                func();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CreateData(KeyCode.Escape);
            foreach (Action func in actions.escape)
                func();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateData(KeyCode.Space);
            foreach (Action func in actions.jump)
                func();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CreateData(KeyCode.Mouse0);
            foreach (Action func in actions.mouse0)
                func();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            CreateData(KeyCode.Mouse1);
            foreach (Action func in actions.mouse1)
                func();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CreateData(KeyCode.Z);
            foreach (Action func in actions.toggleView)
                func();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            CreateData(KeyCode.F);
            foreach (Action func in actions.interract)
                func();
        }
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            CreateData("Horizontal Vertical");            
            foreach (Action func in actions.movement)
                func();
        }
    }
}

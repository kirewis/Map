using UnityEngine;

public class KeyInputManager : MonoBehaviour
{
    public string moveYAxisName = "Vertical";
    public string moveXAxisName = "Horizontal";
    public KeyCode runButtonName = KeyCode.LeftShift;
    public KeyCode jumpButtonName = KeyCode.Space;
    public KeyCode getDownButtonName = KeyCode.Z;
    public KeyCode sitDownButtonName = KeyCode.C;
    public KeyCode interactionButtonName = KeyCode.F;
    public KeyCode cameraSwitchButtonName = KeyCode.V; // 1인칭, 3인칭 스위치
    public KeyCode inventoryButtonName = KeyCode.Tab;
    public string mouseXAxisName = "Mouse X";
    public string mouseYAxisName = "Mouse Y";
    public KeyCode droneLiftButtonName = KeyCode.Space;
    public KeyCode droneDropButtonName = KeyCode.C;
    public KeyCode changeShotModeButtonName = KeyCode.B;
    public int shotButton = 0;
    public KeyCode cameraRotateButton = KeyCode.C; // 카메라만 회전
    public KeyCode ReloadButton = KeyCode.R;

    public float key_moveY { get; private set; }
    public float key_moveX { get; private set; }
    public bool key_run { get; private set; }
    public bool key_jump { get; private set; }
    public bool key_getDown { get; private set; }
    public bool key_sitDown { get; private set; }
    public bool key_interaction { get; private set; }
    public bool key_cameraSwitch { get; private set; }
    public bool key_inventory { get; private set; }

    public float key_mouseX { get; private set; }
    public float key_mouseY { get; private set; }
    public bool key_droneLift { get; private set; }
    public bool key_droneDrop { get; private set; }
    public bool key_ShotMode { get; private set; }
    public bool key_Shot { get; private set; }
    public bool key_ShotDown { get; private set; }
    public bool key_Camera_Rotate { get; private set; }
    public bool key_UP_Camera_Rotate { get; private set; }
    public bool key_Reload { get; private set; }


    private static KeyInputManager sInstance;

    public static KeyInputManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObj = new GameObject("_KeyInputManager");
                sInstance = newGameObj.AddComponent<KeyInputManager>();
            }

            return sInstance;
        }
    }

    void Awake()
    {
        if (sInstance == null)
        {
            sInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Update()
    {
        key_moveY = Input.GetAxis(moveYAxisName);
        key_moveX = Input.GetAxis(moveXAxisName);
        key_run = Input.GetKey(runButtonName);
        key_jump = Input.GetKeyDown(jumpButtonName);
        key_getDown = Input.GetKeyDown(getDownButtonName);
        key_sitDown = Input.GetKeyDown(sitDownButtonName);
        key_interaction = Input.GetKeyDown(interactionButtonName);
        key_cameraSwitch = Input.GetKeyDown(cameraSwitchButtonName);
        key_inventory = Input.GetKeyDown(inventoryButtonName);
        key_mouseX = Input.GetAxis(mouseXAxisName);
        key_mouseY = Input.GetAxis(mouseYAxisName);
        key_droneLift = Input.GetKey(droneLiftButtonName);
        key_droneDrop = Input.GetKey(droneDropButtonName);
        key_ShotMode = Input.GetKeyDown(changeShotModeButtonName);
        key_Shot = Input.GetMouseButton(shotButton);
        key_ShotDown = Input.GetMouseButtonDown(shotButton);
        key_Camera_Rotate = Input.GetKey(cameraRotateButton);
        key_UP_Camera_Rotate = Input.GetKeyUp(cameraRotateButton);
        key_Reload = Input.GetKeyDown(ReloadButton);
    }
}

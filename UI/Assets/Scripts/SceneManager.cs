using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {
    public RubiksCubePrefab RCP;
    public Text txtTurnRecord;
    public Text txtNumMoves;
    public Slider SpeedSlider;
    public Text txtAnimationSpeed;
    public Toggle toggleRotateCamera;
    public bool rotateCamera = true;
    Vector3 cameraResetPos = new Vector3(4, 4, -4);

    private IEnumerator coroutine;

    void Start()
    {
        txtTurnRecord = txtTurnRecord.GetComponent<Text>();
        SpeedSlider = SpeedSlider.GetComponent<Slider>();
        txtAnimationSpeed = txtAnimationSpeed.GetComponent<Text>();
        txtNumMoves = txtNumMoves.GetComponent<Text>();
        SpeedSlider.value = RCP.rotationSpeed;
        setAnimationSpeed(RCP.rotationSpeed);
        toggleRotateCamera = toggleRotateCamera.GetComponent<Toggle>();
        toggleRotateCamera.isOn = rotateCamera;

        Camera.main.transform.position = cameraResetPos;
        Camera.main.transform.LookAt(RCP.transform.position);
    }

    public void Update()
    {
        if (rotateCamera)
            Camera.main.transform.RotateAround(Vector3.zero, Vector3.up, Time.deltaTime * 10);

        if (Input.GetKeyDown(KeyCode.H))//halt
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            RCP.resetCubePrefabPositions();
            RCP.RefreshPanels();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ScrambleCube();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            RCP.RC.printAllFaces();
            ScrambleCube();
            Debug.Log("------------------");
            RCP.RC.printAllFaces();

            string solution = "LUDFBBiFi";
            RubiksCube solCube = new RubiksCube();
            solCube.RunCustomSequence(solution);
            coroutine = RCP.animateCustomSequence(solution);
            StartCoroutine(coroutine);

            //To set the UI label with the solution.
            txtTurnRecord.text = solution;

            //To know the total number of moves and update the UI label
            txtNumMoves.text = solCube.TurnRecordTokenCount() + " Moves";

        }
    }

    public void EasyScrambleCube()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        RCP.RC.Scramble(3);
        RCP.RefreshPanels();
        txtTurnRecord.text = "";
        txtNumMoves.text = "";
    }

    public void ScrambleCube()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        RCP.RC.Scramble(50);
        RCP.RefreshPanels();
        txtTurnRecord.text = "";
        txtNumMoves.text = "";
    }

    public void setAnimationSpeed(float speed)
    {
        txtAnimationSpeed.text = "Animation Speed: " + (int)speed;
        RCP.rotationSpeed = speed;
    }

    public void setCameraRotation(bool on)
    {
        rotateCamera = on;
        Camera.main.transform.position = cameraResetPos;
        Camera.main.transform.LookAt(RCP.transform.position);
    }

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;

public class SceneManager : MonoBehaviour {
    public RubiksCubePrefab RCP;
    public RubiksCubePrefab RCP_BASE;
    public RubiksCube RCP_target;
    public Solutionn s;

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

        else if (Input.GetKeyDown(KeyCode.S))
        {
            Solve();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            EasyScrambleCube();
        }
        else if (Input.GetKeyDown(KeyCode.D)){
            ScrambleCube();
        }
    }

    public void EasyScrambleCube()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        RCP.RC.Scramble(2);
        RCP.RefreshPanels();
        txtTurnRecord.text = "";
        txtNumMoves.text = "";
    }

    public void ScrambleCube()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        RCP.RC.Scramble(5);
        RCP.RefreshPanels();
        txtTurnRecord.text = "";
        txtNumMoves.text = "";
    }

    public void Solve()
    {
      if(RCP.RC.isSolved())
      {
          Debug.Log("isSolved");
      }
      else
      {
          RubiksCube RC_target = new RubiksCube();
          RubiksCube RC_original = RCP.RC.cloneCube();
          s = new Solutionn(RC_target, RC_original, true);

          string sol = null;
          sol = s.A();

          RubiksCube solCube = new RubiksCube();
          solCube.RunCustomSequence(sol);
          coroutine = RCP.animateCustomSequence(sol);
          StartCoroutine(coroutine);

          ////To set the UI label with the solution.
          txtTurnRecord.text = sol;
          ////To know the total number of moves and update the UI label
          txtNumMoves.text = solCube.TurnRecordTokenCount() + " Moves";


          Debug.Log("Cantidad Movimientos: " + solCube.TurnRecordTokenCount());
      }
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

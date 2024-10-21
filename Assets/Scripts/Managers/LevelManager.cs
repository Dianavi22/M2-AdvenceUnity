using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Camera _camera;
    public int phase;
    private bool _phase2Done = false;
    private bool _phase3Done = false;
    public Vector3 phaseRestart;
    [SerializeField] private MeshRenderer _playerMesh;
    [SerializeField] private PostProcessVolume _glitch;
    [SerializeField] private List<TMP_Text> _txtList = new List<TMP_Text>();
    [SerializeField] private TMP_FontAsset _Startfont;
    [SerializeField] private ParticleSystem _playerMiddlePart;
  
    [Header("GameObjects")]
    [SerializeField] List<GameObject> walls1 = new List<GameObject>();
    [SerializeField] List<GameObject> walls2 = new List<GameObject>();
    [SerializeField] List<GameObject> walls3 = new List<GameObject>();
    [SerializeField] private GameObject _shpereInPlayer;
    [SerializeField] GameObject _trail;

    [Header("Materials")]
    [SerializeField] Material _level1MAT;
    [SerializeField] Material _level2MAT;
    [SerializeField] Material _level3MAT;
    [SerializeField] List<Material> _trailMaterials = new List<Material>();
    [SerializeField] List<Material> _levelMATS;
    [SerializeField] List<Material> _hooksMATS;
    [SerializeField] Material _playerMAT;

    [Header("References")]
    [SerializeField] private ShakyCame _shakyCame;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerFirstMove _playerFirstMove;
    [SerializeField] private Bumper _bumper;
    [SerializeField] private Timer _timer;

    private void Start()
    {
        _playerFirstMove = FindObjectOfType<PlayerFirstMove>();
        _bumper.enabled = false;
        _playerController.enabled = false;
        _levelMATS.Add(_level1MAT);
        _levelMATS.Add(_level2MAT);
        _levelMATS.Add(_level3MAT);
    }
    void Update()
    {
        if(phase == 0 && _playerFirstMove.isReadyToBegin)
        {
            phase = 1;
            _timer.enabled = true;
            _shakyCame.isShaking = true;
            _playerFirstMove.enabled = false;
            _bumper.enabled = true;

            _playerController.enabled = true;
            _playerMesh.material = _playerMAT;
            _shpereInPlayer.SetActive(true);
            _playerController.gameObject.GetComponent<LineRenderer>().enabled = true;
            StartCoroutine(Glitch());
            StartPhaseOne();
            ChangePhase();
            for (global::System.Int32 i = 0; i < _txtList.Count; i++)
            {
                _txtList[i].font = _Startfont;
            }

            for (global::System.Int32 i = 0; i < walls1.Count; i++)
            {
                walls1[i].gameObject.GetComponent<MeshRenderer>().material = _level1MAT;
            }
        }
        if(phase == 2 && !_phase2Done)
        {
            _phase2Done = true;
            ChangePhase();
            phaseRestart = new Vector3(23, -20, 0);

        }
        if (phase == 3 && !_phase3Done)
        {
            _phase2Done = true;
            ChangePhase();
            phaseRestart = new Vector3(37, -7, 0);

        }

    }

    private IEnumerator Glitch()
    {
        _glitch.weight = 1.0f;
        yield return new WaitForSeconds(0.4f);
        _glitch.weight = 0f;

    }

    public void StartPhaseOne()
    {
        _camera.clearFlags = CameraClearFlags.SolidColor;
    }

    public void ChangePhase() {
        _trail.GetComponent<TrailRenderer>().material = _trailMaterials[phase-1];
        _shpereInPlayer.GetComponent<MeshRenderer>().material = _levelMATS[phase - 1];
        _playerMiddlePart.GetComponent<Renderer>().material = _levelMATS[phase - 1];
        _playerController.gameObject.GetComponent<LineRenderer>().material = _hooksMATS[phase - 1];

    }

}

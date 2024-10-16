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
    [SerializeField] GameObject _trail;
    [SerializeField] List<Material> _trailMaterials = new List<Material>();
    public int phase;
    private bool _phase2Done = false;
    private bool _phase3Done = false;
    [SerializeField] List<GameObject> walls1 = new List<GameObject>();
    [SerializeField] List<GameObject> walls2 = new List<GameObject>();
    [SerializeField] List<GameObject> walls3 = new List<GameObject>();
    [SerializeField] Material _level1MAT;
    [SerializeField] Material _level2MAT;
    [SerializeField] Material _level3MAT;
    [SerializeField] Material _playerMAT;
    public Vector3 phaseRestart;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private MeshRenderer _playerMesh;
    [SerializeField] private PlayerFirstMove _playerFirstMove;
    [SerializeField] private ShakyCame _shakyCame;
    [SerializeField] private PostProcessVolume _glitch;
    [SerializeField] private List<TMP_Text> _txtList = new List<TMP_Text>();
    [SerializeField] private TMP_FontAsset _Startfont;
    private void Start()
    {
        _playerFirstMove = FindObjectOfType<PlayerFirstMove>();
    }
    void Update()
    {
        if(phase == 0 && _playerFirstMove.isReadyToBegin)
        {
            phase = 1;
            _shakyCame.isShaking = true;
            _playerFirstMove.enabled = false;
            _playerController.enabled = true;
            _playerMesh.material = _playerMAT;
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
            phaseRestart = new Vector3(23, -20, 0);

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
    }

}

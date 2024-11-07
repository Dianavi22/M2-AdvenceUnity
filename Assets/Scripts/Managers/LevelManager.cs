using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Camera _camera;
    public int phase;
    private bool _phase2Done = false;
    private bool _phase3Done = false;
    private bool _phase4Done = false;
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
    [SerializeField] List<GameObject> walls4 = new List<GameObject>();
    [SerializeField] private GameObject _shpereInPlayer;
    [SerializeField] GameObject _trail;

    [Header("Materials")]
    [SerializeField] List<Material> _trailMaterials = new List<Material>();
    [SerializeField] List<Material> _levelMATS;
    [SerializeField] List<Material> _hooksMATS;
    [SerializeField] Material _playerMAT;
    [SerializeField] Material _wallMatShader;
    [SerializeField] ParticleSystem _hitPart;
    [SerializeField] ParticleSystem _sprayPart1;
    [SerializeField] ParticleSystem _sprayPart2;
    [SerializeField] ParticleSystem _sprayPart3;
    [SerializeField] Material _skyBoxMAT;
    [SerializeField] Material _spikeMAT;

    [Header("References")]
    [SerializeField] private ShakyCame _shakyCame;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerFirstMove _playerFirstMove;
    [SerializeField] private Timer _timer;

    [SerializeField] private List<Color32> _colors;
    [SerializeField] GameObject _slider;
    [SerializeField] GameObject _fakeSlider;
    [SerializeField] GameObject _bgSlider;

    [SerializeField] List<ParticleSystem> _borderPausePart;
    [SerializeField] List<Image> _imgButtons;

    [SerializeField] private bool _isLastLevel = false;
    [SerializeField] private ParticleSystem _bgPart;
    //[SerializeField] private List<Material> _bgPartColors;
    [SerializeField] private List<Color32> _bgPartColors;

    private void Start()
    {
        
        _playerFirstMove = FindObjectOfType<PlayerFirstMove>();
        _playerController.enabled = false;
    }
    void Update()
    {
        if(phase == 0 && _playerFirstMove.isReadyToBegin)
        {
            phase = 1;
            _timer.enabled = true;
            _playerFirstMove.enabled = false;
            RenderSettings.skybox = _skyBoxMAT;
            _playerController.enabled = true;
            _playerMesh.material = _playerMAT;
            _shpereInPlayer.SetActive(true);
            _fakeSlider.SetActive(false);
            _slider.SetActive(true);
            _playerController.gameObject.GetComponent<LineRenderer>().enabled = true;
            StartCoroutine(Glitch());
            _bgPart.Play();
            ChangePhase();
            for (int i = 0; i < _txtList.Count; i++)
            {
                _txtList[i].font = _Startfont;
            }

            for (int i = 0; i < walls1.Count; i++)
            {
                walls1[i].gameObject.GetComponent<MeshRenderer>().material = _wallMatShader;
                walls1[i].GetComponentInChildren<ParticleSystem>().GetComponent<Renderer>().material = _levelMATS[phase - 1];
                walls1[i].GetComponentInChildren<ParticleSystem>().Play() ;


            }
        }
        if(phase == 2 && !_phase2Done)
        {
            _phase2Done = true;
            ChangePhase();
            phaseRestart = new Vector3(23, -20, 0);
            for (int i = 0; i < walls2.Count; i++)
            {
                walls2[i].GetComponentInChildren<ParticleSystem>().GetComponent<Renderer>().material = _levelMATS[phase - 1];
                walls2[i].GetComponentInChildren<ParticleSystem>().Play() ;

            }
            for (int i = 0; i < walls1.Count; i++)
            {
                walls1[i].GetComponentInChildren<ParticleSystem>().Stop();
            }

        }
        if (phase == 3 && !_phase3Done)
        {
            _phase3Done = true;
            ChangePhase();
            phaseRestart = new Vector3(-104, 70.4000015f, 0);
            for (int i = 0; i < walls3.Count; i++)
            {
                walls3[i].GetComponentInChildren<ParticleSystem>().GetComponent<Renderer>().material = _levelMATS[phase - 1];
                walls3[i].GetComponentInChildren<ParticleSystem>().Play();

            }
            for (int i = 0; i < walls2.Count; i++)
            {
                walls2[i].GetComponentInChildren<ParticleSystem>().Stop();
            }

        }

        if (phase == 4 && !_phase4Done)
        {
            if (!_isLastLevel) {
                _isLastLevel = true;
                _phase4Done = true;
                ChangePhase();
                phaseRestart = new Vector3(-103, 68, 0);
                for (int i = 0; i < walls4.Count; i++)
                {
                    walls4[i].GetComponentInChildren<ParticleSystem>().GetComponent<Renderer>().material = _levelMATS[phase - 1];
                    walls4[i].GetComponentInChildren<ParticleSystem>().Play();
                }
                for (int i = 0; i < walls3.Count; i++)
                {
                    walls3[i].GetComponentInChildren<ParticleSystem>().Stop();
                }

            }
            else
            {

            }
           


        }

    }

    private IEnumerator Glitch()
    {
        _glitch.weight = 1.0f;
        yield return new WaitForSeconds(0.4f);
        _glitch.weight = 0f;
       

    }


    public void ChangePhase() {
        _shakyCame.isShaking = true;
        _trail.GetComponent<TrailRenderer>().material = _trailMaterials[phase-1];
        _shpereInPlayer.GetComponent<MeshRenderer>().material = _levelMATS[phase - 1];
        _wallMatShader.SetColor("_Color", value: _levelMATS[phase - 1].color*20);
        _skyBoxMAT.SetColor("_Color", value: _colors[phase - 1]);
        _spikeMAT.SetColor("_ColorMax", value: _levelMATS[phase - 1].color * 5);
        _playerMiddlePart.GetComponent<Renderer>().material = _levelMATS[phase - 1];
        _hitPart.GetComponent<Renderer>().material = _levelMATS[phase - 1];
        _hitPart.GetComponent<ParticleSystemRenderer>().trailMaterial = _levelMATS[phase - 1];
        _playerController.gameObject.GetComponent<LineRenderer>().material = _hooksMATS[phase - 1];
        _sprayPart1.GetComponent<Renderer>().material = _levelMATS[phase - 1];
        _sprayPart2.GetComponent<Renderer>().material = _levelMATS[phase - 1];
        _sprayPart3.GetComponent<Renderer>().material = _levelMATS[phase - 1];
        _bgSlider.GetComponent<Image>().material = _levelMATS[phase - 1];
        _sprayPart1.Play();
        for (int i = 0; i < _borderPausePart.Count; i++)
        {
            _borderPausePart[i].GetComponent<Renderer>().material = _levelMATS[phase - 1];
        }
        for (int i = 0; i < _imgButtons.Count; i++)
        {
            _imgButtons[i].material = _levelMATS[phase - 1];
        }
        
          
        var particleSystem = _bgPart.GetComponent<ParticleSystem>();
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.particleCount];
        int particleCount = particleSystem.GetParticles(particles);
        Color32 newColor = _bgPartColors[phase - 1];
        for (int i = 0; i < particleCount; i++)
        {
            particles[i].startColor = newColor;
        }
        particleSystem.SetParticles(particles, particleCount);

        var mainModule = particleSystem.main;
        mainModule.startColor = new ParticleSystem.MinMaxGradient(newColor);
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [HideInInspector] public int phase;
    [HideInInspector] public Vector3 phaseRestart;
    
    [Header("GameObjectsLevel")]
    [SerializeField] List<GameObject> allPlat = new List<GameObject>();
    [SerializeField] List<GameObject> walls1 = new List<GameObject>();
    [SerializeField] List<GameObject> walls2 = new List<GameObject>();
    [SerializeField] List<GameObject> walls3 = new List<GameObject>();
    [SerializeField] List<GameObject> walls4 = new List<GameObject>();
    [SerializeField] List<GameObject> plat4 = new List<GameObject>();

    [Header("GameObjects")]
    [SerializeField] private MeshRenderer _playerMesh;
    [SerializeField] private PostProcessVolume _glitch;
    [SerializeField] List<GameObject> textToHide = new List<GameObject>();
    [SerializeField] private GameObject _shpereInPlayer;
    [SerializeField] GameObject _trail;
    [SerializeField] GameObject _goodText;
    [SerializeField] GameObject _canon;
    [SerializeField] private Camera _camera;


    [Header("Materials")]
    [SerializeField] List<Material> _trailMaterials = new List<Material>();
    [SerializeField] List<Material> _levelMATS;
    [SerializeField] List<Material> _platMat;
    [SerializeField] List<Material> _hooksMATS;
    [SerializeField] Material _playerMAT;
    [SerializeField] Material _wallMatShader;
    [SerializeField] Material _skyBoxMAT;
    [SerializeField] Material _spikeMAT;

    [Header("ParticleSystem")]
    [SerializeField] List<ParticleSystem> _borderPausePart;
    [SerializeField] private ParticleSystem _playerMiddlePart;
    [SerializeField] ParticleSystem _hitPart;
    [SerializeField] ParticleSystem _sprayPart1;
    [SerializeField] ParticleSystem _sprayPart2;
    [SerializeField] ParticleSystem _sprayPart3;
    [SerializeField] ParticleSystem _deathPart;
    [SerializeField] ParticleSystem _sliderPart;
    [SerializeField] ParticleSystem _collisionPart;
    [SerializeField] ParticleSystem _slowMoPart;
    [SerializeField] ParticleSystem _partCanon;
    [SerializeField] ParticleSystem _partInCanon;
    [SerializeField] ParticleSystem _destroyHookPart;

    [Header("References")]
    [SerializeField] private ShakyCame _shakyCame;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Tuto _tuto;
    [SerializeField] private Timer _timer;
    [SerializeField] private PlayerSetUp _playerSetUp;

    [Header("UI")]
    [SerializeField] GameObject _slider;
    [SerializeField] GameObject _fakeSlider;
    [SerializeField] List<TMP_Text> _txtToChange = new List<TMP_Text>();
    [SerializeField] private TMP_FontAsset _Startfont;
    [SerializeField] List<Image> _imgButtons;
    [SerializeField] private List<TMP_Text> _txtList = new List<TMP_Text>();
    [SerializeField] private List<TMP_Text> _txtListFlat = new List<TMP_Text>();


    [Header("Background")]
    [SerializeField] private List<Color32> _colors;
    [SerializeField] GameObject _bgSlider;
    [SerializeField] private ParticleSystem _bgPart;
    [SerializeField] private List<Color32> _bgPartColors;


    [Header("Audio")]
    [SerializeField] AudioSource _firstAudioSource;
    [SerializeField] AudioSource _audioSourceLevel1;
    [SerializeField] AudioSource _audioSourceLevel2;
    [SerializeField] AudioSource _audioSourceLevel3;
    [SerializeField] AudioSource _audioSourceLevel4;
    [SerializeField] AudioSource _audioSourceSounds;
    [SerializeField] AudioSource _audioSourceSoundsTuto;
    [SerializeField] AudioClip _changeLevelSound;

    private bool _isLastLevel = false;
    private bool _phase2Done = false;
    private bool _phase3Done = false;
    private bool _phase4Done = false;
    private bool _isStartGame = false;
    private void Start()
    {
        _playerController.enabled = false;
    }

    private void SetFirstPhase()
    {
        _audioSourceSoundsTuto.gameObject.SetActive(false);
        _firstAudioSource.gameObject.SetActive(false);
        _timer.enabled = true;
        _tuto.enabled = false;
        RenderSettings.skybox = _skyBoxMAT;
        _playerController.enabled = true;
        _playerMesh.material = _playerMAT;
        _shpereInPlayer.SetActive(true);
        _fakeSlider.SetActive(false);
        _slider.SetActive(true);
        _audioSourceSounds.volume = 1;
        _collisionPart.gameObject.SetActive(true);
        _playerController.gameObject.GetComponent<LineRenderer>().enabled = true;
        for (int i = 0; i < textToHide.Count; i++)
        {
            textToHide[i].SetActive(false);
        }
        _goodText.GetComponent<TMP_Text>().color = new UnityEngine.Color(255, 255, 255, 255);

        _isStartGame = true;
        StartCoroutine(Glitch());
        _bgPart.Play();
        _sliderPart.Play();
        _partCanon.Play();
        _canon.GetComponent<Renderer>().enabled = false;
        this.GetComponent<PauseMenu>().enabled = true;
    }
    void Update()
    {
        if (_isStartGame)
        {
            _goodText.GetComponent<TMP_Text>().color = Color.Lerp(_goodText.GetComponent<TMP_Text>().color, new Color(0, 0, 0, 0), Time.deltaTime * 2);
        }
        if (phase == 0 && _playerSetUp.isFirstPhase)
        {
            _playerSetUp.isFirstPhase = false;
            phase = 1;
            ResetAudioSources();
            _audioSourceLevel1.volume = 1;
            SetFirstPhase();
            for (int i = 0; i < _txtToChange.Count; i++)
            {
                _txtToChange[i].font = _Startfont;
            }
            ChangePhase();
            for (int i = 0; i < _txtList.Count; i++)
            {
                _txtList[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < _txtListFlat.Count; i++)
            {
                _txtListFlat[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < walls1.Count; i++)
            {
                walls1[i].gameObject.GetComponent<MeshRenderer>().material = _wallMatShader;
                walls1[i].GetComponentInChildren<ParticleSystem>().GetComponent<Renderer>().material = _levelMATS[phase - 1];
                walls1[i].GetComponentInChildren<ParticleSystem>().Play();


            }
        }
        if (phase == 2 && !_phase2Done)
        {
            _phase2Done = true;
            ResetAudioSources();
            _audioSourceLevel2.volume = 1;

            ChangePhase();

            phaseRestart = new Vector3(23, -20, 0);
            for (int i = 0; i < walls2.Count; i++)
            {
                walls2[i].GetComponentInChildren<ParticleSystem>().GetComponent<Renderer>().material = _levelMATS[phase - 1];
                walls2[i].GetComponentInChildren<ParticleSystem>().Play();

            }
            for (int i = 0; i < walls1.Count; i++)
            {
                walls1[i].GetComponentInChildren<ParticleSystem>().Stop();
            }

        }
        if (phase == 3 && !_phase3Done)
        {
            _phase3Done = true;
            phaseRestart = new Vector3(37, -7, 0);
            ResetAudioSources();
            _audioSourceLevel3.volume = 1;
            ChangePhase();

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
            if (!_isLastLevel)
            {
                _isLastLevel = true;
                ResetAudioSources();
                _audioSourceLevel4.volume = 1;
                _phase4Done = true;
                ChangePhase();
                _sliderPart.transform.position = new Vector3(_sliderPart.transform.transform.position.x, _sliderPart.transform.transform.position.y + 0.3f, _sliderPart.transform.transform.position.z);
                phaseRestart = new Vector3(-104, 70.4000015f, 0);

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

        }

    }

    private void ResetAudioSources()
    {
        _audioSourceLevel1.volume = 0;
        _audioSourceLevel2.volume = 0;
        _audioSourceLevel3.volume = 0;
        _audioSourceLevel4.volume = 0;
    }

    private IEnumerator Glitch()
    {
        _glitch.weight = 1.0f;
        yield return new WaitForSeconds(0.4f);
        _glitch.weight = 0f;
    }

    public void ChangePhase()
    {
        _playerController.StopGrapple();
        _shakyCame.ShakyCameCustom(0.3f, 0.3f);
        _audioSourceSounds.PlayOneShot(_changeLevelSound, 0.2f);
        SetDesignLevel();
        _sprayPart1.Play();
        for (int i = 0; i < _borderPausePart.Count; i++)
        {
            _borderPausePart[i].GetComponent<Renderer>().material = _levelMATS[phase - 1];
        }
        for (int i = 0; i < _imgButtons.Count; i++)
        {
            _imgButtons[i].material = _levelMATS[phase - 1];
        }
        SetColorPlateforms();
        SetBgPart();
    }

    public void SetColorPlateforms()
    {
        for (int i = 0; i < allPlat.Count; i++)
        {
            allPlat[i].GetComponent<MeshRenderer>().material = _platMat[phase - 1];
        }
    }

    #region SetLevel
    private void SetDesignLevel()
    {
        SetTrail();
        SetShader();
        SetMaterials();
        SetParticules();
    }

    private void SetTrail()
    {
        _trail.GetComponent<TrailRenderer>().material = _trailMaterials[phase - 1];
        _playerController.gameObject.GetComponent<LineRenderer>().material = _hooksMATS[phase - 1];
        _collisionPart.GetComponent<ParticleSystemRenderer>().trailMaterial = _levelMATS[phase - 1];
        _deathPart.GetComponent<ParticleSystemRenderer>().trailMaterial = _levelMATS[phase - 1];
        _hitPart.GetComponent<ParticleSystemRenderer>().trailMaterial = _levelMATS[phase - 1];
        _partInCanon.GetComponent<ParticleSystemRenderer>().trailMaterial = _levelMATS[phase - 1];
        _slowMoPart.GetComponent<ParticleSystemRenderer>().trailMaterial = _levelMATS[phase - 1];
        _destroyHookPart.GetComponent<ParticleSystemRenderer>().trailMaterial = _levelMATS[phase - 1];
    }

    private void SetParticules()
    {
        _slowMoPart.GetComponent<Renderer>().material = _levelMATS[phase - 1];
        _deathPart.GetComponent<Renderer>().material = _levelMATS[phase - 1];
        _collisionPart.GetComponent<Renderer>().material = _levelMATS[phase - 1];
        _sprayPart1.GetComponent<Renderer>().material = _levelMATS[phase - 1];
        _sprayPart2.GetComponent<Renderer>().material = _levelMATS[phase - 1];
        _sprayPart3.GetComponent<Renderer>().material = _levelMATS[phase - 1];
        _playerMiddlePart.GetComponent<Renderer>().material = _levelMATS[phase - 1];
        _hitPart.GetComponent<Renderer>().material = _levelMATS[phase - 1];
        _partCanon.GetComponent<Renderer>().material = _levelMATS[phase - 1];
        _partInCanon.GetComponent<Renderer>().material = _levelMATS[phase - 1];
        _destroyHookPart.GetComponent<Renderer>().material = _levelMATS[phase - 1];
    }

    private void SetShader()
    {
        _wallMatShader.SetColor("_Color", value: _levelMATS[phase - 1].color * 20);
        _skyBoxMAT.SetColor("_Color", value: _colors[phase - 1]);
        _spikeMAT.SetColor("_ColorMax", value: _levelMATS[phase - 1].color * 5);
    }

    private void SetMaterials()
    {
        _shpereInPlayer.GetComponent<MeshRenderer>().material = _levelMATS[phase - 1];
        _bgSlider.GetComponent<Image>().material = _levelMATS[phase - 1];
    }

    private void SetBgPart()
    {
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
    #endregion
}

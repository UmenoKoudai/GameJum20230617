using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Tooltip("GameManager�̃C���X�^���X")]
    public static GameManager instance = null;
    [Tooltip("Player1�ƃS�[���̋���")]
    [SerializeField]
    float _player1dis = 0;
    [Tooltip("Player2�ƃS�[���̋���")]
    [SerializeField]
    float _player2dis = 0;
    [Header("Player1�̖��O"),Tooltip("Player1�̖��O")]
    [SerializeField] 
    string _player1str = "Player1";
    [Header("Player2�̖��O"), Tooltip("Player2�̖��O")]
    [SerializeField] 
    string _player2str = "Player2";
    [Header("�S�[���܂ł̋���"),Tooltip("�S�[���܂ł̋���")]
    [SerializeField] 
    float _distance = 1000f;
    [Tooltip("Player����S�[���܂ł�����Slider")]
    [SerializeField] 
    Slider _player1Slider;
    [Tooltip("Player����S�[���܂ł�����Slider")]
    [SerializeField] 
    Slider _player2Slider;
    [Tooltip("�e�L�X�g��\������܂ł̃R���[�`��")]
    [SerializeField] float _wintextCoroutine = 3f;
    [Tooltip("�����A���������̃e�L�X�g")]
    [SerializeField] Text _winText;
    [Header("�������Ƃ���BGM"),Tooltip("�������Ƃ���BGM")]
    [SerializeField] 
    AudioClip _bgm;
    [Tooltip("�I�[�f�B�I")]
    [SerializeField] 
    AudioSource _audio;
    [SerializeField] 
    Animator _animator;
    [SerializeField]
    GameObject _aniObj;
    [Header("Scene�̖��O"), Tooltip("Scene�̖��O")]
    [SerializeField]
    string _sceneName;
    [SerializeField]
    Button _sceneButton;
    [Tooltip("�Q�[���̊Ǘ���state")]
    InGame _inGameState = InGame.Stay;
    public InGame InGameState => _inGameState;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        _aniObj.SetActive(false);
    }

    private void Start()
    {
        if (_sceneButton)
        {
            _sceneButton.onClick.AddListener(() => SceneChangeScripts.instance.SceneChange(_sceneName));
            _sceneButton.gameObject.SetActive(false);
        }
        StartCoroutine(StayTime());
    }

    /// <summary>�n�܂�܂ł̃J�E���g�_�E��</summary>
    /// <returns></returns>
    IEnumerator StayTime()
    {
        if (_winText)
        {
            _winText.text = "�ʒu�ɂ���";
            yield return new WaitForSeconds(2f);
            _winText.text = "��[�[��";
            yield return new WaitForSeconds(1f);
            _winText.text = "";
            var num = Random.Range(1, 11);
            yield return new WaitForSeconds(num);
            _winText.text = "�ǂ�I�I�I�I";
            if (_audio) { _audio.Play(); }
            if (_player1Slider) { _player1Slider.value = _player1dis / _distance; }
            if (_player2Slider) { _player2Slider.value = _player2dis / _distance; }
        }
        _inGameState = InGame.InGame;
        yield return new WaitForSeconds(2f);
        _winText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_inGameState == InGame.InGame)
        {
            if (_player1dis >= _distance)
            {
                Win(_player1str);
            }
            else if (_player2dis >= _distance)
            {
                Win(_player2str);
            }
        }   //��������
    }

    /// <summary>�v���C���[�̑����������̌v�Z</summary>
    /// <param name="i"></param>
    void MoveDistance(int i)
    {
        _player1dis += i;
        if (_player1Slider) { _player1Slider.value = _player1dis / _distance; }
        _player2dis += i;
        if (_player2Slider) { _player2Slider.value = _player2dis / _distance; }
    }

    /// <summary>���������̏���</summary>
    /// <param name="player"></param>
    void Win(string player)
    {
        _inGameState = InGame.Stay;
        _aniObj.SetActive(true);
        _animator.Play("WinAnimation");
        StartCoroutine(WinCoroutine(player));
    }


    /// <summary>���������̏�������Ă΂��R���[�`��</summary>
    /// <param name="player"></param>
    /// <returns></returns>
    IEnumerator WinCoroutine(string player)
    {
        _player1Slider.gameObject.SetActive(false);
        _player2Slider.gameObject.SetActive(false);
        yield return new WaitForSeconds(_wintextCoroutine);
        if (_winText)
        {
            _winText.gameObject.SetActive(true);
            _winText.text = $"{player} �̏����I";
        }
        yield return new WaitForSeconds(1.5f);
        _sceneButton.gameObject.SetActive(true);
    }

    /// <summary>�Q�[���̊Ǘ���state</summary>
    public enum InGame
    {
        Stay,
        InGame,
    }
}

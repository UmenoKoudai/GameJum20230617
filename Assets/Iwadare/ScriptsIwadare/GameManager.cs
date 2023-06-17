using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Tooltip("GameManagerのインスタンス")]
    public static GameManager instance = null;
    [Tooltip("Player1とゴールの距離")]
    [SerializeField]
    float _player1dis = 0;
    [Tooltip("Player2とゴールの距離")]
    [SerializeField]
    float _player2dis = 0;
    [Header("Player1の名前"),Tooltip("Player1の名前")]
    [SerializeField] 
    string _player1str = "Player1";
    [Header("Player2の名前"), Tooltip("Player2の名前")]
    [SerializeField] 
    string _player2str = "Player2";
    [Header("ゴールまでの距離"),Tooltip("ゴールまでの距離")]
    [SerializeField] 
    float _distance = 1000f;
    [Tooltip("Playerからゴールまでを示すSlider")]
    [SerializeField] 
    Slider _player1Slider;
    [Tooltip("Playerからゴールまでを示すSlider")]
    [SerializeField] 
    Slider _player2Slider;
    [SerializeField] GameObject _gaugeObj;
    [Tooltip("テキストを表示するまでのコルーチン")]
    [SerializeField] float _wintextCoroutine = 3f;
    [Tooltip("準備、勝った時のテキスト")]
    [SerializeField] Text _winText;
    [Header("勝ったときのBGM"),Tooltip("勝ったときのBGM")]
    [SerializeField] 
    AudioClip _bgm;
    [Tooltip("オーディオ")]
    [SerializeField] 
    AudioSource _audio;
    [SerializeField] 
    Animator _animator;
    [SerializeField]
    GameObject _aniObj;
    [Header("Sceneの名前"), Tooltip("Sceneの名前")]
    [SerializeField]
    string _sceneName;
    [SerializeField]
    Button _sceneButton;
    [SerializeField]
    string _retrySceneName;
    [SerializeField]
    Button _retryButton;
    [Tooltip("ゲームの管理のstate")]
    InGame _inGameState = InGame.Stay;
    public InGame InGameState => _inGameState;
    public float Distance => _distance;
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
        if (_sceneButton && _retryButton)
        {
            _retryButton.onClick.AddListener(() => SceneChangeScripts.instance.SceneChange(_retrySceneName));
            _sceneButton.onClick.AddListener(() => SceneChangeScripts.instance.SceneChange(_sceneName));
            _retryButton.gameObject.SetActive(false);
            _sceneButton.gameObject.SetActive(false);
        }
        StartCoroutine(StayTime());
    }

    /// <summary>始まりまでのカウントダウン</summary>
    /// <returns></returns>
    IEnumerator StayTime()
    {
        if (_winText)
        {
            _winText.text = "位置について";
            yield return new WaitForSeconds(2f);
            _winText.text = "よーーい";
            yield return new WaitForSeconds(1f);
            _winText.text = "";
            var num = Random.Range(1, 11);
            yield return new WaitForSeconds(num);
            _winText.text = "どん！！！！";
            if (_audio) { _audio.Play(); }
            if (_player1Slider) { _player1Slider.value = _player1dis / _distance; }
            if (_player2Slider) { _player2Slider.value = _player2dis / _distance; }
        }
        _inGameState = InGame.InGame;
        _gaugeObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        _winText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_inGameState == InGame.InGame)
        {
            if(_player1dis >= _distance && _player2dis >= _distance)
            {
                string winner = _player1dis > _player2dis ? "Player1" : "Player2";
                Win(winner);
            }
            else if (_player1dis >= _distance)
            {
                Win(_player1str);
            }
            else if (_player2dis >= _distance)
            {
                Win(_player2str);
            }
        }   //勝利判定
    }

    /// <summary>プレイヤーの走った距離の計算</summary>
    /// <param name="i"></param>
    public void MoveDistance(int player1, int player2)
    {
        _player1dis += player1;
        if (_player1Slider) { _player1Slider.value = _player1dis / _distance; }
        _player2dis += player2;
        if (_player2Slider) { _player2Slider.value = _player2dis / _distance; }
        _gaugeObj.SetActive(true);
    }

    /// <summary>勝った時の処理</summary>
    /// <param name="player"></param>
    void Win(string player)
    {
        _inGameState = InGame.Stay;
        _aniObj.SetActive(true);
        _animator.Play("WinAnimation");
        StartCoroutine(WinCoroutine(player));
    }


    /// <summary>勝った時の処理から呼ばれるコルーチン</summary>
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
            _winText.text = $"{player} の勝ち！";
        }
        yield return new WaitForSeconds(1.5f);
        _sceneButton.gameObject.SetActive(true);
        _retryButton.gameObject.SetActive(true);
    }

    /// <summary>ゲームの管理のstate</summary>
    public enum InGame
    {
        Stay,
        InGame,
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using EZCameraShake;

public class Snake : MonoBehaviour
{
    Vector2 direction = Vector2.right;
    List<GameObject> segment = new List<GameObject>();
    public GameObject prefabObject;
    public GameObject floatingTextPrefab;
    public GameObject floatingTextPrefab1;
    public GameObject LoosePanel;
    public UIController controller;
    public int initialSize;
    public Text counterText;
    int increase;

    [Header("Buttons")]
    public Button up, down, left, right, pause, accelerate, retry, quit;

    [Header("Sprites")]
    public Sprite upSprite, downSprite, leftSprite, rightSprite;

    [Header("Externals")]
    public Food food;
    public Food2 food2;

    private void Update()
    {
        counterText.text = increase.ToString("0");
    }

    private void Start()
    {
        up.onClick.AddListener(delegate { MoveUp(); });
        down.onClick.AddListener(delegate { MoveDown(); });
        left.onClick.AddListener(delegate { MoveLeft(); });
        right.onClick.AddListener(delegate { MoveRight(); });
        //accelerate.onClick.AddListener(delegate { accButton(); });
        pause.onClick.AddListener(delegate { PauseButton(); });
        retry.onClick.AddListener(delegate { retryButton(); });
        quit.onClick.AddListener(delegate { exitButton(); });
    }

    private void FixedUpdate()
    {
        for (int i = segment.Count-1; i > 0; i--)
        {
            segment[i].transform.position = segment[i - 1].transform.position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
            );
    }
    #region Block for button movement control.
    void MoveUp(){
        this.GetComponent<SpriteRenderer>().sprite = upSprite;
        direction = Vector2.up;
    }
    void MoveDown(){
        this.GetComponent<SpriteRenderer>().sprite = downSprite;
        direction = Vector2.down;
    }
    void MoveLeft(){
        this.GetComponent<SpriteRenderer>().sprite = leftSprite;
        direction = Vector2.left;
    }
    void MoveRight() {
        this.GetComponent<SpriteRenderer>().sprite = rightSprite;
        direction = Vector2.right;   
    }

    //void accButton()
    //{
    //    SoundManage.instance.PlaySfx("Rush");
    //}

    void retryButton()
    {
        Time.timeScale = 1;
        LoosePanel.SetActive(false);
        ResetState();
        increase = 0;
    }

    void exitButton()
    {
        controller.backToMenu();
        Time.timeScale = 1;
    }

    void PauseButton()
    {
        Time.timeScale = 0;
    }
    #endregion

    void Grow()
    {
        GameObject _gameObject = Instantiate(this.prefabObject);
        _gameObject.transform.position = segment[segment.Count - 1].transform.position;

        segment.Add(_gameObject);
    }

    void Grow2()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject game_Object = Instantiate(this.prefabObject);
            game_Object.transform.position = segment[segment.Count - 1].transform.position;
            segment.Add(game_Object);
        }       
    }

    void ResetState()
    {
        for (int i = segment.Count-1; i > 0; i--)
        {
            Destroy(segment[i].gameObject);
        }

        segment.Clear();
        segment.Add(this.gameObject);

        for (int i = 1; i < this.initialSize; i++)
        {
            segment.Add(Instantiate(this.prefabObject));
        }
        this.transform.position = Vector3.zero;

        food.RandomizePosition();
        food2.StopCoroutine(food2.wait2());
        food2.StartCoroutine(food2.wait3());     
    }

    #region floating score text
    void ShowFloatingText()
    {
        Instantiate(floatingTextPrefab, this.transform.position, Quaternion.identity, transform);
    }
    void ShowFloatingText1()
    {
        Instantiate(floatingTextPrefab1, this.transform.position, Quaternion.identity, transform);
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Obstacle")
        {
            Time.timeScale = 0;
            CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, 1f);
            SoundManage.instance.PlaySfx("Bump");
            LoosePanel.SetActive(true);
        }

        if(other.tag == "Body_Obstacle")
        {
            Time.timeScale = 0;
            LoosePanel.SetActive(true);
        }

        if (other.tag == "Food")
        {
            Grow();
            increase += 5;
            SoundManage.instance.PlaySfx("SmallEat");
            if (floatingTextPrefab)
            {
                ShowFloatingText();
            }
        }

        if (other.tag == "Food2")
        {
            Grow2();
            increase += 15;
            SoundManage.instance.PlaySfx("BigEat");
            if (floatingTextPrefab1)
            {
                ShowFloatingText1();
            }
        }  
    }
}
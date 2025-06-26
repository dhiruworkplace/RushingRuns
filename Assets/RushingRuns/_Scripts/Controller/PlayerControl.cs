using UnityEngine;
using Cinemachine;
using DG.Tweening;
using TMPro;

public class PlayerControl : Singleton<PlayerControl>
{

    // =========================== VARIABLES
    [Header("Objects")]
    [Space]
    [SerializeField] Color[] balonColors;
    public Animator playerAnim;
    public GameObject balon;
    public ParticleSystem confeti;
    [SerializeField] CinemachineVirtualCamera playerCam;

    [Header("Texts")]
    [Space]
    [SerializeField] TextMeshProUGUI plusText;
    [SerializeField] TextMeshProUGUI negativeText;
    [SerializeField] TextMeshProUGUI perfectText;
    [SerializeField] TextMeshProUGUI badText;
    [SerializeField] string[] perfectRandomText;
    [SerializeField] string[] badRandomText;

    [Header("Variables")]
    [Space]
    [SerializeField] float sliderSmoothness;
    [SerializeField] float rotationSpeed;
    [SerializeField] float maxRotation;
    private float playerSpeed = 3f;
    private float firstPos;
    private float lastPos;
    private float rotationAmount;
    // ============
    private int perfectCount;
    private int badCount;
    private int myBalonColor = 1;
    private int balonColor;  // 1 = RED  2 = GREEN 3 = BLUE
    private Vector3 scaleValue = new Vector3 (0.1f, 0.1f, 0.1f);

    // =========================== *** START

    public override void Awake()
    {
        base.Awake();
    }
    
    private void Update()
    {
        Run();
    }
    
    private void Run()
    {
        if (GameManager.Instance.isGameStarted)
        {
            transform.Translate(transform.forward * playerSpeed * Time.deltaTime, Space.World);

            if (Input.GetMouseButtonDown(0))
                    firstPos = Input.mousePosition.x;

            if (Input.GetMouseButton(0))
            {
                lastPos = Input.mousePosition.x;
                float distance = (lastPos - firstPos) / sliderSmoothness;

                rotationAmount += rotationSpeed * distance;               
                rotationAmount = Mathf.Clamp(rotationAmount, -maxRotation, maxRotation);   // Rotation Limit               
                Vector3 rotValue = new Vector3(transform.localEulerAngles.x, rotationAmount, transform.localEulerAngles.z); // Turning

                transform.localEulerAngles = rotValue; 
                firstPos = lastPos;
            }
        }
    }

    private void BalonColorChecker()
    {
        // MY BALON && BALOON COLOR MATCH
        if (balonColor == myBalonColor)
        {
            if (balon.transform.localScale.y < 2.1) // MAX BALON SCALE LIMIT
                balon.transform.localScale += scaleValue;

            transform.DOLocalMoveY(transform.localPosition.y + 0.2f, 0.6f).SetEase(Ease.Linear);
            PlusSpawner();  

            // ======= CAMERA ZOOM OUT
            if (balon.transform.localScale.y > 1.8f)
                playerCam.m_Priority = 20;
            else   
                playerCam.m_Priority = 8;    
        }
        else
        {
            if (balon.transform.localScale.y > 0.5f)
                balon.transform.localScale -= scaleValue;

            if (transform.localPosition.y > 0.2f) // MIN GROUND LIMIT
                transform.DOLocalMoveY(transform.localPosition.y - 0.2f, 0.6f).SetEase(Ease.Linear);

            NegativeSpawner();
        }

        // =======  FAKE FLY ANIMATION
        if (transform.localPosition.y > 0.2f)
            playerAnim.speed = 0.3f;
        else if (transform.localPosition.y < 0.2f)
            playerAnim.speed = 1f;

    }

    // =========================== COLLIDE && TRIGGER

    private void OnCollisionEnter(Collision other)
    {
        // PLAYER & BALON
        if (other.gameObject.CompareTag("GameController"))
        {
            balonColor = other.gameObject.GetComponent<BalonControl>().color;
            BalonColorChecker();
            Destroy(other.gameObject);
        }
        // FLOOR END & PLAYER
        if (other.gameObject.CompareTag("FloorEnd"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            float y = other.gameObject.GetComponent<FloorGates>().downValue;
            transform.DOLocalMoveY(y, 1f);
            balon.transform.DOLocalMoveY(balon.transform.localPosition.y + 5f, 5f);
            playerAnim.speed = 1f;
            playerAnim.Play("Fail");
            GameManager.Instance.GameOver();
            Debug.Log("GAME OVER");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // FINISH & PLAYER
        if (other.gameObject.CompareTag("Finish"))
        {
            confeti.Play();
            playerAnim.Play("Win");
            balon.transform.DOLocalMoveY(balon.transform.localPosition.y + 5f, 5f);
            GameManager.Instance.FinishLevel();
            
        }

        // COLOR GATES & PLAYER
        if (other.gameObject.CompareTag("Respawn"))
        {
           int x = other.gameObject.GetComponent<ColorGates>().gateColor;
           myBalonColor = x;
           balon.GetComponent<MeshRenderer>().material.DOColor(balonColors[myBalonColor - 1], 1f);
        }

        // FLOOR GATES & PLAYER
        if (other.gameObject.CompareTag("GameOverScreen"))
        {
            float y = other.gameObject.GetComponent<FloorGates>().downValue;
            transform.DOLocalMoveY(y, 0.5f);
            playerAnim.speed = 1f;
        }
    }

    // =========================== UI TEXT METHODS

    private void PerfectCounter()
    {
        perfectCount++;
        if (perfectCount == 3)
        {
            int x = Random.Range(0, 3);
            perfectText.text = perfectRandomText[x];
            perfectText.rectTransform.DOScale(1.5f, 1f).OnComplete(() =>
            {
                perfectText.rectTransform.DOScale(0, 1f);

            });
            perfectCount = 0;
        }
    }

    private void BadCounter()
    {
        badCount++;
        if (badCount == 3)
        {
            int x = Random.Range(0, 3);
            badText.text = badRandomText[x];
            badText.rectTransform.DOScale(1.5f, 1f).OnComplete(() =>
            {
                badText.rectTransform.DOScale(0, 1f);

            });
            badCount = 0;
        }
    }

    private void PlusSpawner()
    {
        plusText.rectTransform.DOScale(3, 0.5f).SetLoops(1, LoopType.Yoyo).OnComplete(() =>
        {
            plusText.rectTransform.DOScale(0, 0.5f);
        });
        PerfectCounter();
    }

    private void NegativeSpawner()
    {
        negativeText.rectTransform.DOScale(3, 0.5f).SetLoops(1, LoopType.Yoyo).OnComplete(() =>
        {
            negativeText.rectTransform.DOScale(0, 0.5f);
        });
        BadCounter();
    }

    // =========================== *** END

}
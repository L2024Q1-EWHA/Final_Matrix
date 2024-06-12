using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class MiniGameControl : MonoBehaviour
{
    [SerializeField] private GameObject _left;
    [SerializeField] private GameObject _right;

    [SerializeField] private GameObject fire;
    [SerializeField] private Button stopButton;

    [SerializeField] private GameObject alarmPanel;
    [SerializeField] private GameObject alarmPanel_Status;


    [SerializeField] private GameObject range_score80;
    [SerializeField] private GameObject range_score60;
    [SerializeField] private GameObject range_score40;

    bool movingRight = true;

    public float MovingSpeed = 100.0f;
    public bool StopMove { get; set; } = false;


    private float score80_halfWidth;
    private float score60_halfWidth;
    private float score40_halfWidth;
    private (float min, float max) score80;
    private (float min, float max) score60;
    private (float min, float max) score40;




    public void ResetFirePosition()
    {
        fire.transform.position = _left.transform.position;
    }

    void Start()
    {
        CalcRange();

        alarmPanel.SetActive(false);
        StopMove = true;
        movingRight = true;
        ResetFirePosition();
    }

    const float EPSILLON = 50.0f;
    void Update()
    {
        if (StopMove)
            return;

        if (movingRight)
        {
            Vector3 dir = _right.transform.position - fire.transform.position;

            // 목표 지점에 도착
            if (dir.magnitude < EPSILLON || dir.x < 0)
            {
                movingRight = false;
                return;
            }

            // 이동
            dir = dir.normalized;
            fire.transform.position += MovingSpeed * dir * Time.deltaTime;
            return;
        }
        else
        {
            Vector3 dir = _left.transform.position - fire.transform.position;

            // 목표 지점에 도착
            if (dir.magnitude < EPSILLON || dir.x > 0)
            {
                movingRight = true;
                return;
            }

            // 이동
            dir = dir.normalized;
            fire.transform.position += MovingSpeed * dir * Time.deltaTime;
        }


    }

    public async void StopFire()
    {
        StopMove = true;

        int score = CalcScore();
        alarmPanel_Status.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = score.ToString("D2");
        GameManager.Instance.HealthStatusUpDown(score);
        await WaitForMiliSeconds(1000);
        alarmPanel.SetActive(true);
    }
    public int CalcScore()
    {
        float firePositionX = fire.transform.position.x;
        if (firePositionX >= score80.min && firePositionX <= score80.max) return 80;
        else if (firePositionX >= score60.min && firePositionX <= score60.max) return 60;
        else if (firePositionX >= score40.min && firePositionX <= score40.max) return 40;
        else return 0;
    }

    private void CalcRange()
    {
        score80_halfWidth = range_score80.GetComponent<RectTransform>().rect.width / 2;
        score60_halfWidth = range_score60.GetComponent<RectTransform>().rect.width / 2;
        score40_halfWidth = range_score40.GetComponent<RectTransform>().rect.width / 2;

        score80 = (range_score80.transform.position.x - score80_halfWidth, range_score80.transform.position.x + score80_halfWidth);
        score60 = (range_score60.transform.position.x - score60_halfWidth, range_score60.transform.position.x + score60_halfWidth);
        score40 = (range_score40.transform.position.x - score40_halfWidth, range_score40.transform.position.x + score40_halfWidth);

    }

    private async Task WaitForMiliSeconds(int seconds)
    {
        await Task.Delay(seconds);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tags;
using TMPro;

public class BallController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _ball = new List<GameObject>();
    [SerializeField] private TMP_Text _ballCountText = null;
    [SerializeField] private GameObject _ballPrefab;


    [Header("Movement")]
    [SerializeField] private float _horizontalLimit;
    [SerializeField] private float _horizontalSpeed;
    private float _horizontal;
    [SerializeField] private float _moveSpeed;

    private int _ballCount = 0;
    private int _target = 0;
    private int gatenumber = 0;
    void Update()
    {
        BallMoveForward();
        BallHorizontalMovement();
        UpdateBallCountText();

        _ballCount = _ball.Count;
        
        Debug.Log(_target);
    }


    private void BallHorizontalMovement() {
        float _newX = 0;

        if(Input.GetMouseButton(0)) {
            _horizontal = Input.GetAxisRaw("Mouse X");
        }
        else {
            _horizontal = 0;
        }

        _newX = transform.position.x + _horizontal * _horizontalSpeed * Time.deltaTime;
        _newX = Mathf.Clamp(_newX, -_horizontalLimit, _horizontalLimit);

        transform.position = new Vector3(
            _newX,
            transform.position.y,
            transform.position.z
        );

        
    }



    private void BallMoveForward() {
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }



    private void UpdateBallCountText() {
        _ballCountText.text = _ball.Count.ToString();
    }



    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag(Tag.STACK_BALL)) {
            other.gameObject.transform.SetParent(transform);
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            other.gameObject.transform.localPosition = new Vector3(0f, 0f, _ball[_ball.Count - 1].transform.localPosition.z - 1f);
            _ball.Add(other.gameObject);
        }

        if(other.gameObject.CompareTag(Tag.GATE)) {
            gatenumber = other.gameObject.GetComponent<GateController>().GetGateNumber();
            _target = _ballCount + gatenumber;    
            if(gatenumber > 0) {
                IncreateBallCount();
            }
            else if(gatenumber < 0) {
                DecreaseBallCount();
            }
        }
    }


    private void IncreateBallCount() {
        for(int i = 0; i < gatenumber; i++) {
            GameObject _newBall = Instantiate(_ballPrefab);
            _newBall.transform.SetParent(transform);
            _newBall.GetComponent<SphereCollider>().enabled = false;
            _newBall.transform.localPosition = new Vector3(0f, 0f, _ball[_ball.Count - 1].transform.localPosition.z - 1f);
            _ball.Add(_newBall);
        }
    }


    private void DecreaseBallCount() {
        Debug.Log("Çalıştı");
        for (int i = _ballCount - 1; i >= _target; i--)
        {
            _ball[i].SetActive(false);
            _ball.RemoveAt(i);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateController : MonoBehaviour
{

    [SerializeField] private enum GateType {
        PositiveGate,
        NegativeGate
    }

    [SerializeField] private TMP_Text _gateText = null;
    [SerializeField] private GateType gateType;
    
    [SerializeField] private int _gateNumber = 0;

    public int GetGateNumber() {
        return _gateNumber;
    }

    private void Start() {
        GenerateRandomGateNumber();
    }

    private void GenerateRandomGateNumber() {
        switch(gateType) {
            case GateType.PositiveGate : _gateNumber = Random.Range(1, 15);
                                         SetGateText(); 
                                         break;
            case GateType.NegativeGate : _gateNumber = Random.Range(-15, -1);
                                         SetGateText(); 
                                         break;
        }
    }

    private void SetGateText() {
        _gateText.text = _gateNumber.ToString();
    }
}

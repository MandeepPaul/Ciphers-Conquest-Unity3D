using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HitPoints _hitPoints;
    [SerializeField] private Image _meterImage;
    [SerializeField] private Text _hpText;
    private PlayerCharacter _character;

    public PlayerCharacter Character {
        get {
            return _character;
        }
        set {
            _character = value;
        }
    }

    void Update() 
    {
        if (_character != null) 
        {
            _meterImage.fillAmount = _hitPoints.Value / _character.MaxHitPoints;
            _hpText.text = "HP:" + (_meterImage.fillAmount * 100);
        }
    }
}

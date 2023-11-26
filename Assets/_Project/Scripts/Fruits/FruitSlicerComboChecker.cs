using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Fruits
{
    public class FruitSlicerComboChecker : ITickable
    {
    private readonly GameObject _comboMultiplierRootGo;
    private readonly TMP_Text _comboMultiplierText;
    private readonly float _comboIncreaseInterval;
    private readonly int _comboMultiplierIncreaseStep;
    private float _comboTimer;
    private int _comboStep;
    private int _comboMultiplier;

    public FruitSlicerComboChecker(GameObject comboMultiplierRootGo, TMP_Text comboMultiplierText,
        float comboIncreaseInterval, int comboMultiplierIncreaseStep)
    {
        _comboMultiplierRootGo = comboMultiplierRootGo;
        _comboMultiplierText = comboMultiplierText;
        _comboIncreaseInterval = comboIncreaseInterval;
        _comboMultiplierIncreaseStep = comboMultiplierIncreaseStep;

        DropComboTimer();
        CalculateComboMultiplier(0);
    }

    public void Tick()
    {
        MoveTimer();
    }

    public void IncreaseComboStep()
    {
        SetComboStep(_comboStep + 1);
    }

    public int GetComboMultiplier()
    {
        return _comboMultiplier;
    }

    public void StopCombo()
    {
        SetComboStep(0);
    }

    private void SetComboStep(int value)
    {
        _comboStep = value;
        CalculateComboMultiplier(value);
        DropComboTimer();
    }

    private void DropComboTimer()
    {
        _comboTimer = 0;
    }

    private void CalculateComboMultiplier(int comboStep)
    {
        _comboMultiplier = 1 + comboStep / _comboMultiplierIncreaseStep;

        SetComboMultiplierText(_comboMultiplier);
        SetComboMultiplierShow(_comboMultiplier);
    }

    private void SetComboMultiplierText(int value)
    {
        _comboMultiplierText.text = $"x{value}";
    }

    private void SetComboMultiplierShow(int value)
    {
        var needShow = value > 1;
        _comboMultiplierRootGo.SetActive(needShow);
    }

    private void MoveTimer()
    {
        _comboTimer += Time.deltaTime;

        if (_comboTimer >= _comboIncreaseInterval)
        {
            StopCombo();
        }
    }
    }
}
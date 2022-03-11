using System;
using System.Globalization;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ViewModel : MonoBehaviour
{
    private enum OperationType
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    [SerializeField] private TextMeshProUGUI outputField;

    private float _aNumber;
    private float _bNumber;
    private char _operationSymbol;
    private bool _operationOnField;
    private bool _resultOnField;
    private readonly char[] _operationSymbols = {'+', '-', '*', ':'};
    
    private OperationType _currentOperation;
    private Model _model;

    private void Start()
    {
        _model = new Model(SetResult);
    }

    private void SetResult(float result)
    {
        outputField.text = result.ToString(CultureInfo.InvariantCulture);
        _operationOnField = false;
        _resultOnField = true;
        _aNumber = result;
        _bNumber = 0;
    }

    public void ClearAll()
    {
        outputField.text = "";
        _operationOnField = false;
        _resultOnField = false;
        _aNumber = 0;
        _bNumber = 0;
    }

    public void CountResult()
    {
        switch (_currentOperation)
        {
            case OperationType.Addition :
                _model.GetSumOf(_aNumber, _bNumber);
                break;
            case OperationType.Subtraction :
                _model.GetSubtractionOf(_aNumber, _bNumber);
                break;
            case OperationType.Multiplication :
                _model.GetMultiplicationOf(_aNumber, _bNumber);
                break;
            case OperationType.Division :
                _model.GetDivisionOf(_aNumber, _bNumber);
                break;
        }
    }
    
    public void ClickNumber()
    {
        if ((_resultOnField || _aNumber == 0) && !_operationOnField)
            ClearAll();
        
        var nameOfButton = EventSystem.current.currentSelectedGameObject.name;
        var clickedNumber = GetNumber(nameOfButton);
        
        if (_operationOnField && _bNumber * 10 + clickedNumber <= int.MaxValue)
            _bNumber = _bNumber * 10 + clickedNumber;
        else if (_aNumber * 10 + clickedNumber <= int.MaxValue)
            _aNumber = _aNumber * 10 + clickedNumber;
        else return;
        
        outputField.text += clickedNumber;
    }

    public void ClickOperation(int operationIndex)
    {
        if (_operationOnField || outputField.text.Length == 0)
            return;
        
        _currentOperation = (OperationType) operationIndex;
        _operationSymbol = _operationSymbols[operationIndex];
        outputField.text += _operationSymbol;
        _operationOnField = true;
    }
    
    private int GetNumber(string name)
    {
        Regex regex = new Regex("\\((\\d+)\\)");
        Match match = regex.Match(name);
        if (!match.Success)
            throw new System.Exception("Please check the number button name...");
        Group group = match.Groups[1];
        string number = group.Value;
        
        return Convert.ToInt32(number);
    }
}
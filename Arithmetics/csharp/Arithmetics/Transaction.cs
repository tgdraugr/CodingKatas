﻿namespace Arithmetics;

public class Transaction
{
    private const string Space = " ";
    
    private readonly string[] _tokens;

    public Transaction(string expression)
    {
        _tokens = expression.Split(Space);
    }

    public float? Result { get; private set; }

    public void Evaluate()
    {
        Result = EvaluateOperation();
    }

    private float EvaluateOperation()
    {
        if (_tokens.Length > 3)
        {
            var @operator = _tokens[2];
            var firstOperand = _tokens[1];
            var secondOperand = _tokens[3];
            return OperationResult(@operator, firstOperand, secondOperand);
        }
        return Constant(_tokens[1]);
    }

    private static float OperationResult(string @operator, string firstOperand, string secondOperand)
    {
        return @operator switch
        {
            "+" => Add(firstOperand, secondOperand),
            "-" => Subtract(firstOperand, secondOperand),
            "*" => Multiply(firstOperand, secondOperand),
            "/" => Divide(firstOperand, secondOperand),
            _ => ThrowInvalidOperation(@operator)
        };
    }

    private static float ThrowInvalidOperation(string @operator)
    {
        throw new InvalidRecordException(InvalidRecordException.RecordError.InvalidOperation, $"Expected one of [+, -, *, /] but got {@operator}");
    }

    private static float Divide(string firstOperand, string secondOperand)
    {
        return secondOperand != "0" ? 
            Constant(firstOperand) / Constant(secondOperand) : 
            throw new InvalidRecordException(InvalidRecordException.RecordError.DivisionByZero, "Division is not possible because denominator is 0");
    }

    private static float Multiply(string firstOperand, string secondOperand)
    {
        return Constant(firstOperand) * Constant(secondOperand);
    }

    private static float Subtract(string firstOperand, string secondOperand)
    {
        return Constant(firstOperand) - Constant(secondOperand);
    }

    private static float Add(string firstOperand, string secondOperand)
    {
        var sumOperation = new SumOperation(new Constant(firstOperand), new Constant(secondOperand));
        return sumOperation.Value;    
    }

    private static float Constant(string token)
    {
        return new Constant(token).Value;
    }
}

internal class SumOperation
{
    private readonly Constant _first;
    private readonly Constant _second;

    public SumOperation(Constant first, Constant second)
    {
        _first = first;
        _second = second;
    }

    public float Value => _first.Value + _second.Value;
}
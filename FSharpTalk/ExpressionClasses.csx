public abstract class Expression
{
    public abstract int Evaluate();
}

public class LiteralExpression : Expression
{
    private readonly int _value;
    public LiteralExpression(int value) { _value = value; }
    public override int Evaluate() => _value;
    public override string ToString() => _value.ToString();
}

public class AdditionExpression : Expression
{
    private readonly Expression _left;
    private readonly Expression _right;
    public AdditionExpression(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }
    public override int Evaluate() => _left.Evaluate() + _right.Evaluate();
    public override string ToString() => $"({_left} + {_right})";
}

public class MultiplicationExpression : Expression
{
    private readonly Expression _left;
    private readonly Expression _right;
    public MultiplicationExpression(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }
    public override int Evaluate() => _left.Evaluate() * _right.Evaluate();
    public override string ToString() => $"({_left} * {_right})";
}

// etc. for each of our expression types -- usually these would be written in separate files

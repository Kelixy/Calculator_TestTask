public class Model
{
    public delegate void SetResult(float result);

    private readonly SetResult _setResult;
    
    public Model(SetResult setResult)
    {
        _setResult = setResult;
    }
    
    public void GetSumOf(float aNum, float bNum)
    {
        _setResult(aNum + bNum);
    }

    public void GetSubtractionOf(float aNum, float bNum)
    {
        _setResult(aNum - bNum);
    }

    public void GetMultiplicationOf(float aNum, float bNum)
    {
        _setResult(aNum * bNum);
    }

    public void GetDivisionOf(float aNum, float bNum)
    {
        _setResult(aNum / bNum);
    }
}

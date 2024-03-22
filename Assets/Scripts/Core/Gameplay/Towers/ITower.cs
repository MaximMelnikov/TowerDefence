using Cysharp.Threading.Tasks;
public interface ITower
{
    public UniTask StartFiring();
    public void StopFiring();
}
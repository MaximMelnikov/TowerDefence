using Cysharp.Threading.Tasks;

namespace Core.Gameplay.MapProceduralGenerator
{
    public interface IMapGenerator
    {
        public UniTask CreateMap(MapSettings mapSettings, int seed = -1);
        public void Reset();
    }
}
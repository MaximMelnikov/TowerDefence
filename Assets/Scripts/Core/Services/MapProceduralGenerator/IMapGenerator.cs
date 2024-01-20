using Cysharp.Threading.Tasks;

namespace Core.Services.MapProceduralGenerator
{
    public interface IMapGenerator
    {
        public UniTask CreateMap(MapSettings mapSettings, int seed = -1);
        public void Reset();
    }
}
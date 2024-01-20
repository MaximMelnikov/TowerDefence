using Core.Services.MapProceduralGenerator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MapGeneratorWidget : MonoBehaviour
    {
        private IMapGenerator _mapGenerator;

        [SerializeField] private TMP_InputField Seed;
        [SerializeField] private Slider RoadLength;
        [SerializeField] private TMP_InputField MaxTowersOnRoad;
        [SerializeField] private TMP_InputField RoadsMinCount;
        [SerializeField] private TMP_InputField RoadsMaxCount;
        [SerializeField] private TMP_InputField ChanceToSpawnPropPercent;
        [SerializeField] private TMP_Text RoadLengthValue;
        

        [Inject]
        public void Construct(
            IMapGenerator mapGenerator)
        {
            _mapGenerator = mapGenerator;
        }
        
        public void Generate()
        {
            int seed = int.Parse(Seed.text);
            int roadLength = (int) RoadLength.value;
            int maxTowersOnRoad = int.Parse(MaxTowersOnRoad.text);
            int roadsMinCount = int.Parse(RoadsMinCount.text);
            int roadsMaxCount = int.Parse(RoadsMaxCount.text);
            int chanceToSpawnPropPercent = int.Parse(ChanceToSpawnPropPercent.text);
            
            var mapSettings = new MapSettings(roadLength, maxTowersOnRoad, (roadsMinCount, roadsMaxCount), chanceToSpawnPropPercent);
            _mapGenerator.CreateMap(mapSettings, seed);
        }
        
        public void Reset()
        {
            _mapGenerator.Reset();
        }
        
        public void SetRoadLength()
        {
            RoadLengthValue.text = RoadLength.value.ToString();
        }
    }
}
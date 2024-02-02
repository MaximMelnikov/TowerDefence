using Core.Gameplay.MapProceduralGenerator;
using Core.Gameplay.Monsters.MonstersFactory;
using Core.StateMachine;
using Core.StateMachine.StateMachines.States;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MapGeneratorWidget : MonoBehaviour
    {
        private IMapGenerator _mapGenerator;
        private IMonstersFactory _monstersFactory;
        private IStateMachine _projectStateMachine;

        [SerializeField] private TMP_InputField Seed;
        [SerializeField] private Slider RoadLength;
        [SerializeField] private TMP_InputField MaxTowersOnRoad;
        [SerializeField] private TMP_InputField RoadsMinCount;
        [SerializeField] private TMP_InputField RoadsMaxCount;
        [SerializeField] private TMP_InputField ChanceToSpawnPropPercent;
        [SerializeField] private TMP_Text RoadLengthValue;
        

        [Inject]
        public void Construct(
            IMapGenerator mapGenerator,
            IMonstersFactory monstersFactory,
            IStateMachine projectStateMachine)
        {
            _monstersFactory = monstersFactory;
            _mapGenerator = mapGenerator;
            _projectStateMachine = projectStateMachine;
        }

        public void Generate()
        {
            GenerateAsync();
        }
        
        public async UniTask GenerateAsync()
        {
            int seed = int.Parse(Seed.text);
            int roadLength = (int) RoadLength.value;
            int maxTowersOnRoad = int.Parse(MaxTowersOnRoad.text);
            int roadsMinCount = int.Parse(RoadsMinCount.text);
            int roadsMaxCount = int.Parse(RoadsMaxCount.text);
            int chanceToSpawnPropPercent = int.Parse(ChanceToSpawnPropPercent.text);
            
            var mapSettings = new MapSettings(roadLength, maxTowersOnRoad, (roadsMinCount, roadsMaxCount), chanceToSpawnPropPercent);
            await _mapGenerator.CreateMap(mapSettings, seed);
            _projectStateMachine.Enter<GameplayState>(true);
        }
        
        public void Reset()
        {
            _mapGenerator.Reset();
            _monstersFactory.Reset();
        }
        
        public void SetRoadLength()
        {
            RoadLengthValue.text = RoadLength.value.ToString();
        }
    }
}
namespace Core.Gameplay
{
    public struct SpawnerBalance
    {
        public int CapsuleCount;
        public int SphereCount;
        public int BoxCount;

        public int NextStageDelay;

        public SpawnerBalance(int capsuleCount, int sphereCount, int boxCount, int nextStageDelay)
        {
            CapsuleCount = capsuleCount;
            SphereCount = sphereCount;
            BoxCount = boxCount;
            NextStageDelay = nextStageDelay;
        }
    }
}
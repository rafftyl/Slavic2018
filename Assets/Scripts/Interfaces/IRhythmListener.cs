public interface IRhythmListener
{
    void MetronomeTick(int measure, int beatNumber, float intensity, bool accent, float timeToNextTick);
}
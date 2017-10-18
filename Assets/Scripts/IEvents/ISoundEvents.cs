public interface ISoundEvents
{
    void PlayingProgress(float value);
    void PausePlaying();

    void ResumePlaying();
    void StopPlaying();
}
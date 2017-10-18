public interface IPlayerEvents
{        
    void OnPlaySound(ISound sound, ISoundEvents events);
    void OnPauseSound(ISound sound);
}
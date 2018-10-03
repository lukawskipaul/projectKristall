public interface IPowerUp
{
    string PowerName { get; }

    bool IsActivated { get; set; }

    bool IsUnlocked { get; set; }
}

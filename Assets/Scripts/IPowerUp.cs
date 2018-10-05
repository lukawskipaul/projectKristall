// DO NOT USE - Old interface for powerups. Was replaced by PowerUp class
// Gonna delete this at some point
public interface IPowerUp
{
    string PowerName { get; }

    bool IsActivated { get; set; }

    bool IsUnlocked { get; set; }
}

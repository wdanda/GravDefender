
public static class Utils {

    public static bool IsPlayerLaser(Laser laser) {
        return (laser != null) && (laser.GetFiringObject().GetType() == typeof(Player));
    }
    
}
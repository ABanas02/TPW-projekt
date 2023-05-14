using System.Numerics;

public delegate void BallPositionChangedEventHandler(object sender, BallEvents e);

public class BallEvents
{
    public Vector2 Position { get; private set; }

    public BallEvents(Vector2 position)
    {
        Position = position;
    }
}

public interface INotifyBallPositionChanged
{
    event BallPositionChangedEventHandler BallPositionChanged;
}

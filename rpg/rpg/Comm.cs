using System;

public class Comm
{
    public static long Time()
    {
        DateTime dt1 = new DateTime(2017,11,7);
        TimeSpan ts = DateTime.Now - dt1;
        return (long)ts.TotalMilliseconds;
    }

    public enum Direction
    {
        UP = 4,
        DOWN = 1,
        RIGHT = 3,
        LEFT = 2,
    }


    //返回某方向的相反方向
    public static Direction opposite_direction(Direction direction)
    {
        if (direction == Direction.UP)
            return Direction.DOWN;
        else if (direction == Direction.DOWN)
            return Direction.UP;
        else if (direction == Direction.RIGHT)
            return Direction.LEFT;
        else if (direction == Direction.LEFT)
            return Direction.RIGHT;
        return Direction.DOWN;
    }

}
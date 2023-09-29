using System;

namespace Model.Commands
{
    [Serializable]
    public enum CommandNames
    {
        MoveForward,
        TurnLeft,
        TurnRight,
        Jump,
        ChangeCubeLightStatus,
        Procedure1
    }
}
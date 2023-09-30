using System;
using Model.Commands;
using UnityEngine;
using UnityEngine.Serialization;

namespace Model.Ui
{
    [Serializable]
    public class CommandSpriteModel
    {
        [FormerlySerializedAs("commandName")] [SerializeField] private CommandName commandName;
        [SerializeField] private Sprite iconSprite;

        public CommandName CommandName => commandName;
        public Sprite IconSprite => iconSprite;
    }
}
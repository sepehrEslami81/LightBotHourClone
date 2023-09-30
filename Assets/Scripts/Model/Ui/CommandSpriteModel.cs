using System;
using Model.Commands;
using UnityEngine;

namespace Model.Ui
{
    [Serializable]
    public class CommandSpriteModel
    {
        [SerializeField] private CommandName commandName;
        [SerializeField] private Sprite iconSprite;

        public Sprite IconSprite => iconSprite;
        public CommandName CommandName => commandName;
    }
}
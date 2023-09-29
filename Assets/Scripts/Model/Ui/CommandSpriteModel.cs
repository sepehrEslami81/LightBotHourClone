using System;
using Model.Commands;
using UnityEngine;

namespace Model.Ui
{
    [Serializable]
    public class CommandSpriteModel
    {
        [SerializeField] private CommandNames commandName;
        [SerializeField] private Sprite iconSprite;

        public CommandNames CommandName => commandName;
        public Sprite IconSprite => iconSprite;
    }
}
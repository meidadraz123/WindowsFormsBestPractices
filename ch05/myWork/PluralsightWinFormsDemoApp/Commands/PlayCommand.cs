﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp.Commands
{
    class PlayCommand : CommandBase
    {
        private readonly IPodcastPlayer player;

        public PlayCommand(IPodcastPlayer player)
        {
            this.player = player;
            Icon = IconResources.play_icon_32;
            ToolTip = "Play";
            ShortcutKey = Keys.Space | Keys.Control;
        }
        public override void Execute()
        {
            player.Play();
        }
    }
}

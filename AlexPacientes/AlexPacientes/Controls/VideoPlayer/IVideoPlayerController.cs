using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Controls.VideoPlayer
{
    public interface IVideoPlayerController
    {
        VideoStatus Status { set; get; }

        TimeSpan Duration { set; get; }
    }
}

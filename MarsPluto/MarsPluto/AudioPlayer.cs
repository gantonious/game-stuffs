using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Audio.OpenAL;
using OpenTK.Audio;
using System.IO;
using System.Media;

namespace MarsPluto
{

    class AudioPlayer
    {

        public AudioPlayer()
        {


        }

        public static void loadSound(string filename)
        {

            WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();

            player.URL = filename;
            player.controls.play();
        }
    }
}

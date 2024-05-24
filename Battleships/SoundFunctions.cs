using UtilityFunctions;
using System.Threading;

namespace SoundFunctions
{
    public class Sound
    {
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        Utility utility = new Utility();
        Random rnd = new Random();

        string[] goodSounds = new string[3] { "lessgo.mp3", "yeahbaby.mp3", "ohmygod.mp3" };
        string[] badSounds = new string[5] { "bruh.wav", "derp.wav", "fart.wav", "orange.wav", "troll.wav" };
        string soundFolder = "";
        bool trolled = false;
        private void Troll() // Play annoying sounds in background
        {
            soundFolder = utility.NavigateParents(Environment.CurrentDirectory, 3) + "\\Sounds\\Bad";
            while (true)
            {
                wplayer.controls.stop();
                wplayer.URL = soundFolder + "\\" + badSounds[rnd.Next(0, badSounds.Length)];
                wplayer.controls.play();

                var t = Task.Run(async delegate
                {
                    await Task.Delay(1000);
                    return 42;
                });
                t.Wait();
            }
        }
        private void BedTime() // Play lullabye in background
        {
            soundFolder = utility.NavigateParents(Environment.CurrentDirectory, 3) + "\\Songs";
            while (true)
            {
                wplayer.controls.stop();
                wplayer.URL = soundFolder + "\\RockabyeBaby.mp3";
                wplayer.controls.play();
                var t = Task.Run(async delegate
                {
                    await Task.Delay(107000);
                    return 42;
                });
                t.Wait();
            }
        }
        public void SoundEffect(string mode, Button btnMute) // Play sound effect depending on mode listed
        {
            if (trolled)
            {
                return;
            }
            if (mode == "mute")
            {
                wplayer.controls.stop();
            }
            if (btnMute.Enabled == false)
            {
                return;
            }
            if (mode == "good")
            {
                soundFolder = utility.NavigateParents(Environment.CurrentDirectory, 3) + "\\Sounds\\Good";

                wplayer.controls.stop();
                wplayer.URL = soundFolder + "\\" + goodSounds[rnd.Next(0, goodSounds.Length)];
                wplayer.controls.play();
            }
            else if (mode == "bad")
            {
                soundFolder = utility.NavigateParents(Environment.CurrentDirectory, 3) + "\\Sounds\\Bad";

                wplayer.controls.stop();
                wplayer.URL = soundFolder + "\\" + badSounds[rnd.Next(0, badSounds.Length)];
                wplayer.controls.play();
            }
            else if (mode == "placing")
            {
                soundFolder = utility.NavigateParents(Environment.CurrentDirectory, 3) + "\\Sounds\\Other";

                wplayer.controls.stop();
                wplayer.URL = soundFolder + "\\ship.wav";
                wplayer.controls.play();
            }
            else if (mode == "shoot")
            {
                soundFolder = utility.NavigateParents(Environment.CurrentDirectory, 3) + "\\Sounds\\Other";

                wplayer.controls.stop();
                wplayer.URL = soundFolder + "\\shot.mp3";
                wplayer.controls.play();
            }
            else if (mode == "bedtime")
            {
                soundFolder = utility.NavigateParents(Environment.CurrentDirectory, 3) + "\\Songs";

                wplayer.controls.stop();
                wplayer.URL = soundFolder + "\\RockabyeBaby.mp3";
                wplayer.controls.play();
            }
            else if (mode == "troll")
            {
                if (trolled == true)
                {
                    return;
                }
                trolled = true;
                var ts = new ThreadStart(Troll);
                var backgroundThread = new Thread(ts);
                backgroundThread.Start();
            }
            return;
        }
    }
}

using LibVLCSharp.Shared;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VLCinUWPTest
{
    public sealed partial class MainPage : Page, IDisposable
    {
        private LibVLC VLC = null;

        /// <summary>
        /// Constructor - initialize VLC and MediaPlayer
        /// </summary>
        public MainPage()
        {
            Core.Initialize();

            this.InitializeComponent();

            Loaded += (o, e) =>
            {
                    // init VLClib
                VLC = new LibVLC(VideoView.SwapChainOptions);
                    // init MediaPlayer
                VideoView.MediaPlayer = new MediaPlayer(VLC);
                VideoView.MediaPlayer.Playing += async (o2, e2) =>
                {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                    {
                        StatusText.Text = "Playing";
                    });
                };
                VideoView.MediaPlayer.Stopped += async (o2, e2) =>
                {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                    {
                        // cleanup media
                        VideoView.MediaPlayer?.Media?.Dispose();
                        if (VideoView.MediaPlayer != null) VideoView.MediaPlayer.Media = null;
                            // BUG: CURRENTLY THE LAST FRAME IS STILL VISIBLE
                        if (VideoView.MediaPlayer.Media == null)
                            StatusText.Text = "Stopped (Media cleared)";
                        else
                            StatusText.Text = "Stopped (Media still set)";
                    });
                };
            };
        }

        /// <summary>
        /// Destructor - cleanup resources
        /// </summary>
        ~MainPage()
        {
            Dispose();
        }

        /// <summary>
        /// Start playing the video
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if (VideoView.MediaPlayer.Media != null) return;
                // create new Media and play
            VideoView.MediaPlayer.Media = new Media(VLC, "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4", FromType.FromLocation);
            VideoView.MediaPlayer.Play();
        }

        /// <summary>
        /// Skip to the end of the video to see what happens when it ends by itself
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkipToEnd_Click(object sender, RoutedEventArgs e)
        {
            if (VideoView.MediaPlayer.Media == null) return;
            VideoView.MediaPlayer.Position = 0.99f;
        }

        /// <summary>
        /// Stop playing the video
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if (VideoView.MediaPlayer.Media == null) return;
            VideoView.MediaPlayer.Stop();
        }

        /// <summary>
        /// Cleanup resources
        /// </summary>
        public void Dispose()
        {
            VideoView.MediaPlayer?.Media?.Dispose();
            if (VideoView.MediaPlayer != null) VideoView.MediaPlayer.Media = null;

            VideoView.MediaPlayer?.Dispose();
            VideoView.MediaPlayer = null;

            VLC.Dispose();
            VLC = null;
        }
    }
}

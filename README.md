# VLCinUWPTest

This is a test project for tracking a bug in VLC for UWP. The app shows a VideoView and has simple buttons to play a fixed video(stream), skip to the end (almost) and stop playing.

# VideoLan LibVLCSharp Issue #260: Cannot clear VideoView after playing video

I want to have a situation where the VideoView is cleared from the last played video. Whatever I do, Dispose and null Media, Dispose and null Player, the last frame is still visible. When you (re)load a video this results in a glitch on screen.

Note that it's hard to see the glitch in this demo video, as it starts with a black frame. Especially when a video is used without black frames at the beginning and end it is noticeable.

An issue was added to the VideoLan LibVLCSharp repo. See [Cannot clear VideoView after playing video](https://code.videolan.org/videolan/LibVLCSharp/issues/260).

# VideoLan LibVLCSharp Issue #261: LibVLCSharp on UWP crashes with null pointer when Visibility of VideoView is set to Collapsed

When you want to hide the VideoView control initially from the UI, it's normal to use `Visibility=Collapsed` on the control. This results in a crash.

An issue was added to the VideoLan LibVLCSharp repo. See [LibVLCSharp on UWP crashes with null pointer when Visibility of VideoView is set to Collapsed](https://code.videolan.org/videolan/LibVLCSharp/issues/261).

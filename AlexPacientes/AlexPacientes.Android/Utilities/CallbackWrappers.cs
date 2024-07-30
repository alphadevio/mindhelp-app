using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AlexPacientes.Droid.Utilities
{
    public static class CallbackWrappers
    {
        public class MediaPlayer
        {
            public class OnVideoSizeChangedListener : Java.Lang.Object, Android.Media.MediaPlayer.IOnVideoSizeChangedListener
            {
                public delegate void VideoSizeChanged(Android.Media.MediaPlayer mp, int width, int height);

                public VideoSizeChanged VideoSizeChangedDelegate { get; }

                public OnVideoSizeChangedListener(VideoSizeChanged videoSizeChangedDelegate)
                {
                    VideoSizeChangedDelegate = videoSizeChangedDelegate;
                }

                public void OnVideoSizeChanged(Android.Media.MediaPlayer mp, int width, int height)
                {
                    VideoSizeChangedDelegate(mp, width, height);
                }
            }

            public class OnCompletionListener : Java.Lang.Object, Android.Media.MediaPlayer.IOnCompletionListener
            {
                public delegate void Completion(Android.Media.MediaPlayer mp);

                public Completion CompletionDelegate { get; }

                public OnCompletionListener(Completion completionDelegate)
                {
                    CompletionDelegate = completionDelegate;
                }

                public void OnCompletion(Android.Media.MediaPlayer mp)
                {
                    CompletionDelegate(mp);
                }
            }

            public class OnPreparedListener : Java.Lang.Object, Android.Media.MediaPlayer.IOnPreparedListener
            {
                public delegate void Prepared(Android.Media.MediaPlayer mp);

                public Prepared PreparedDelegate { get; }

                public OnPreparedListener(Prepared preparedDelegate)
                {
                    PreparedDelegate = preparedDelegate;
                }

                public void OnPrepared(Android.Media.MediaPlayer mp)
                {
                    PreparedDelegate(mp);
                }
            }

            public class OnErrorListener : Java.Lang.Object, Android.Media.MediaPlayer.IOnErrorListener
            {
                public delegate bool Error(Android.Media.MediaPlayer mp, MediaError what, int extra);

                public Error ErrorDelegate { get; }

                public OnErrorListener(Error errorDelegate)
                {
                    ErrorDelegate = errorDelegate;
                }

                public bool OnError(Android.Media.MediaPlayer mp, MediaError what, int extra)
                {
                    return ErrorDelegate(mp, what, extra);
                }
            }
        }

        public class View
        {
            public class OnClickListener : Java.Lang.Object, Android.Views.View.IOnClickListener
            {
                public delegate void OnClicked(Android.Views.View v);

                public OnClicked OnClickedDelegate { get; }

                public OnClickListener(OnClicked onClickedDelegate)
                {
                    OnClickedDelegate = onClickedDelegate;
                }

                public void OnClick(Android.Views.View v)
                {
                    OnClickedDelegate(v);
                }
            }
        }
    }
}
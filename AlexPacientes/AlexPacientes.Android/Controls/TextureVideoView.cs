using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Hardware;
using Android.Graphics;
using Android.Media;
using System.IO;
using Android.Content.Res;
using AlexPacientes.Droid.Utilities;
using Java.Lang;

namespace AlexPacientes.Droid.Controls
{
    public class TextureVideoView : FrameLayout, TextureView.ISurfaceTextureListener
    {
        public interface IMediaPlayerListener
        {
            void OnVideoPrepared();
            void OnVideoEnd();
        }

        private static readonly bool LOG_ON = true;
        private static readonly string TAG = nameof(TextureVideoView);

        private MediaPlayer _mediaPlayer;
        private IMediaPlayerListener _listener;

        private float _videoHeight;
        private float _videoWidth;

        private bool _isDataSourceSet;
        private bool _isViewAvailable;
        private bool _isVideoPrepared;
        private bool _isPlayCalled;

        private ScaleType _scaleType;
        private State _state;
        private TextureView _textureView;

        public enum ScaleType
        {
            CenterCrop, Top, Bottom, CenterCropRight
        }

        public enum State
        {
            Uninitialized, Play, Stop, Pause, End
        }

        public TextureVideoView(Context context) : base(context)
        {
            if (!IsInEditMode)
            {
                InitView();
            }
        }

        private void InitView()
        {
            InitPlayer();
            _textureView = new TextureView(Context);
            RemoveAllViews();
            AddView(_textureView);
            SetScaleType(ScaleType.CenterCrop);
            _textureView.SurfaceTextureListener = this;
        }

        public void SetScaleType(ScaleType scaleType)
        {
            _scaleType = scaleType;
        }

        private void UpdateTextureViewSize()
        {
            float viewWidth = Width;
            float viewHeight = Height;

            float scaleX = 1.0f;
            float scaleY = 1.0f;

            if (_videoWidth >= viewWidth && _videoHeight >= viewHeight)
            {
                scaleX = _videoWidth / viewWidth;
                scaleY = _videoHeight / viewHeight;
            }
            else if (_videoWidth <= viewWidth && _videoHeight <= viewHeight)
            {
                scaleY = viewWidth / _videoWidth;
                scaleX = viewHeight / _videoHeight;
            }
            else if (viewWidth >= _videoWidth)
            {
                scaleY = (viewWidth / _videoWidth) / (viewHeight / _videoHeight);
            }
            else if (viewHeight >= _videoHeight)
            {
                scaleX = (viewHeight / _videoHeight) / (viewWidth / _videoWidth);
            }

            // Calculate pivot points, in our case crop from center
            int pivotPointX;
            int pivotPointY;

            switch (_scaleType)
            {
                case ScaleType.Top:
                    pivotPointX = 0;
                    pivotPointY = 0;
                    break;
                case ScaleType.Bottom:
                    pivotPointX = (int)(viewWidth);
                    pivotPointY = (int)(viewHeight);
                    break;
                case ScaleType.CenterCrop:
                    pivotPointX = (int)(viewWidth / 2);
                    pivotPointY = (int)(viewHeight / 2);
                    break;
                case ScaleType.CenterCropRight:
                    pivotPointX = (int)(viewWidth / 1.5);
                    pivotPointY = (int)(viewHeight / 1.5);
                    break;

                default:
                    pivotPointX = (int)(viewWidth / 2);
                    pivotPointY = (int)(viewHeight / 2);
                    break;
            }

            Matrix matrix = new Matrix();
            matrix.SetScale(scaleX, scaleY, pivotPointX, pivotPointY);
            _textureView.SetTransform(matrix);
        }

        private void InitPlayer()
        {
            if (_mediaPlayer == null)
            {
                _mediaPlayer = new MediaPlayer();
            }
            else
            {
                _mediaPlayer.Reset();
            }
            _isVideoPrepared = false;
            _isPlayCalled = false;
            _state = State.Uninitialized;
        }

        public bool IsPlaying()
        {
            if (_mediaPlayer != null)
            {
                return _mediaPlayer.IsPlaying;
            }
            return false;
        }

        public void SetDataSource(string path)
        {
            InitPlayer();

            try
            {
                _mediaPlayer.SetDataSource(path);
                _isDataSourceSet = true;
                Prepare();
            }
            catch (IOException e)
            {
                System.Diagnostics.Debug.WriteLine("{0}: {1}", TAG, e.Message);
            }
        }

        public void SetDataSource(Context context, Android.Net.Uri uri)
        {
            InitPlayer();

            try
            {
                _mediaPlayer.SetDataSource(context, uri);
                _isDataSourceSet = true;
                Prepare();
            }
            catch (IOException e)
            {
                System.Diagnostics.Debug.WriteLine("{0}: {1}", TAG, e.Message);
            }
        }

        public void SetDataSource(AssetFileDescriptor afd)
        {
            InitPlayer();

            try
            {
                long startOffset = afd.StartOffset;
                long length = afd.Length;
                _mediaPlayer.SetDataSource(afd.FileDescriptor, startOffset, length);
                _isDataSourceSet = true;
                Prepare();
            }
            catch (IOException e)
            {
                System.Diagnostics.Debug.WriteLine("{0}: {1}", TAG, e.Message);
            }
        }

        private void Prepare()
        {
            try
            {
                _mediaPlayer.SetOnVideoSizeChangedListener(new CallbackWrappers.MediaPlayer.OnVideoSizeChangedListener(
                    (mp, width, height) => 
                    {
                        _videoWidth = width;
                        _videoHeight = height;
                        UpdateTextureViewSize();
                    }));

                _mediaPlayer.SetOnCompletionListener(new CallbackWrappers.MediaPlayer.OnCompletionListener(
                    (mp) =>
                    {
                        _state = State.End;

                        if (_listener != null)
                        {
                            _listener.OnVideoEnd();
                        }
                    }));

                _mediaPlayer.PrepareAsync();

                // Play video when the media source is ready for playback.
                _mediaPlayer.SetOnPreparedListener(new CallbackWrappers.MediaPlayer.OnPreparedListener(
                    (mp) =>
                    {
                        _isVideoPrepared = true;
                        if (_isPlayCalled && _isViewAvailable)
                        {
                            Play();
                        }

                        if (_listener != null)
                        {
                            _listener.OnVideoPrepared();
                        }
                    }));

                _mediaPlayer.SetOnErrorListener(new CallbackWrappers.MediaPlayer.OnErrorListener(
                    (mp, what, extra) =>
                    {
                        return false;
                    }));

            } catch (IllegalArgumentException e) {
                System.Diagnostics.Debug.WriteLine("{0}: {1}", TAG, e.Message);
            } catch (SecurityException e) {
                System.Diagnostics.Debug.WriteLine("{0}: {1}", TAG, e.Message);
            } catch (IllegalStateException e) {
                System.Diagnostics.Debug.WriteLine("{0}: {1}", TAG, e.Message);
            }
        }

        public void Play()
        {
            Play(0);
        }

        /// <summary>
        /// Play or resume video. Video will be played as soon as view is available and media player is prepared.
        /// If video is stopped or ended and play() method was called, video will start over.
        /// </summary>
        /// <param name="startPosition"></param>
        public void Play(int startPosition) 
        {
            if (!_isDataSourceSet)
            {
                System.Diagnostics.Debug.WriteLine("play() was called but data source was not set.");
                return;
            }

            _isPlayCalled = true;

            if (!_isVideoPrepared)
            {
                System.Diagnostics.Debug.WriteLine("play() was called but video is not prepared yet, waiting.");
                return;
            }

            if (!_isViewAvailable)
            {
                System.Diagnostics.Debug.WriteLine("play() was called but view is not available yet, waiting.");
                return;
            }

            if (_state == State.Play)
            {
                System.Diagnostics.Debug.WriteLine("play() was called but video is already playing.");
                return;
            }

            if (_state == State.Pause)
            {
                System.Diagnostics.Debug.WriteLine("play() was called but video is paused, resuming.");
                _state = State.Play;
                _mediaPlayer.Start();
                return;
            }

            _state = State.Play;
            _mediaPlayer.SeekTo(startPosition);
            _mediaPlayer.Start();
        }

        /// <summary>
        ///  Pause video. If video is already paused, stopped or ended nothing will happen.
        /// </summary>
        public void Pause()
        {
            if (_state == State.Pause)
            {
                System.Diagnostics.Debug.WriteLine("pause() was called but video already paused.");
                return;
            }

            if (_state == State.Stop)
            {
                System.Diagnostics.Debug.WriteLine("pause() was called but video already stopped.");
                return;
            }

            if (_state == State.End)
            {
                System.Diagnostics.Debug.WriteLine("pause() was called but video already ended.");
                return;
            }

            _state = State.Pause;
            if (_mediaPlayer.IsPlaying)
            {
                _mediaPlayer.Pause();
            }
        }

        /// <summary>
        /// Stop video (pause and seek to beginning). If video is already stopped or ended nothing will happen.
        /// </summary>
        public void Stop()
        {
            if (_state == State.Stop)
            {
                System.Diagnostics.Debug.WriteLine("stop() was called but video already stopped.");
                return;
            }

            if (_state == State.End)
            {
                System.Diagnostics.Debug.WriteLine("stop() was called but video already ended.");
                return;
            }

            _state = State.Stop;
            if (_mediaPlayer.IsPlaying)
            {
                _mediaPlayer.Pause();
                _mediaPlayer.SeekTo(0);
            }
        }

        public void SetLooping(bool looping)
        {
            _mediaPlayer.Looping = looping;
        }

        public void SeekTo(int milliseconds)
        {
            _mediaPlayer.SeekTo(milliseconds);
        }

        public int GetDuration()
        {
            return _mediaPlayer.Duration;
        }

        public int GetCurrentPosition()
        {
            return _mediaPlayer.CurrentPosition;
        }

        public void SetListener(IMediaPlayerListener listener)
        {
            _listener = listener;
        }

        public void OnSurfaceTextureAvailable(SurfaceTexture surfaceTexture, int width, int height)
        {
            Surface surface = new Surface(surfaceTexture);
            _mediaPlayer.SetSurface(surface);
            _isVideoPrepared = true;
            if (_isDataSourceSet && _isPlayCalled && _isVideoPrepared)
            {
                System.Diagnostics.Debug.WriteLine("View is available and play() was called.");
                _isViewAvailable = true;
                Play();
            }
        }

        public bool OnSurfaceTextureDestroyed(SurfaceTexture surface)
        {
            return false;
        }

        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
            
        }

        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {
            
        }
    }
}
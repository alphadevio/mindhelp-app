using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlexPacientes.Controls;
using AlexPacientes.Services;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Com.Opentok.Android;

[assembly: Xamarin.Forms.Dependency(typeof(AlexPacientes.Droid.Services.OpenTokService))]
namespace AlexPacientes.Droid.Services
{
    public class OpenTokService : IOpenTokService
    {
        public static readonly int VIDEO_CALL_CODE = 10;

        private string _token;

        private Session _session;
        private Publisher _publisher;
        private Subscriber _subscriber;

        private ViewGroup _publisherContainer;
        private ViewGroup _subscriberContainer;

        private Context _context;
        private PowerManager.WakeLock _wakeLock;
        public Xamarin.Forms.Command OnPartnerConnectedCommand { get; set; }
        public void Connect()
        {
            ConnectToSession();
        }

        public void EndSession()
        {
            DisconnectSession();
        }

        public void InitNewSession(string apiKey, string sessionID, string token)
        {
            InitSession(apiKey, sessionID, token);
        }

        public void SetHostContainer(StreamView streamView)
        {
            _publisherContainer = streamView.NativeView as FrameLayout;
        }

        public void SetClientContainer(StreamView streamView)
        {
            _subscriberContainer = streamView.NativeView as FrameLayout;
        }

        public void PublishAudio(bool audio)
        {
            if (_publisher != null)
                _publisher.PublishAudio = audio;
        }

        public void PublishVideo(bool video)
        {
            if (_publisher != null)
                _publisher.PublishVideo = video;
        }

        public void SwapCamera()
        {
            if (_publisher != null)
                _publisher.CycleCamera();
        }

        bool HasPermissions(string[] permissions)
        {
            foreach (var p in permissions)
            {
                if (ContextCompat.CheckSelfPermission(_context, p) != Android.Content.PM.Permission.Granted)
                    return false;
            }
            return true;
        }

        void InitSession(string apiKey, string sessionId, string token)
        {
            // Store token
            _token = token;

            // Store context
            _context = Application.Context;

            string[] permissions = { Manifest.Permission.Internet, Manifest.Permission.Camera, Manifest.Permission.RecordAudio };
            if (!HasPermissions(permissions)) return;

            // Create OpenTok session
            _session = new Session.Builder(_context, apiKey, sessionId).Build();
            // Subscribe for events
            SubscribeForSessionEvents(_session);
            // Connect session
            ConnectToSession();

            // Create wavelock
            _wakeLock = (_context.GetSystemService(Context.PowerService) as PowerManager)
                .NewWakeLock(WakeLockFlags.ScreenBright, "WaveLockPartial");
        }

        void ConnectToSession()
        {
            if (_session == null) return;
            
            _session.Connect(_token);
        }

        void DisconnectSession()
        {
            // Release wavelock
            if (_wakeLock != null && _wakeLock.IsHeld)
            {
                _wakeLock.Release();
            }

            // Delete Subscriber
            if (_subscriber != null)
            {
                _subscriber.Dispose();
                _subscriber = null;
                DesactivateStreamContainer(_subscriberContainer);
            }

            // Delete publisher
            if (_publisher != null)
            {
                UnsubscribeForPublisherEvents(_publisher);
                _publisher.Dispose();
                _publisher = null;
                DesactivateStreamContainer(_publisherContainer);
            }

            // Stop session
            if (_session != null)
            {
                UnsubscribeForSessionEvents(_session);
                _session.Disconnect();
                _session.Dispose();
                _session = null;
            }
        }

        void SubscribeForSessionEvents(Session session)
        {
            if (session == null) return;

            session.Connected += OnSessionConnected;
            session.Disconnected += OnSessionDisconnected;
            session.StreamReceived += OnSessionStreamReceived;
            session.StreamDropped += OnSessionStreamDropped;
            session.Error += OnSessionError;
        }

        void UnsubscribeForSessionEvents(Session session)
        {
            if (session == null) return;

            session.Connected -= OnSessionConnected;
            session.Disconnected -= OnSessionDisconnected;
            session.StreamReceived -= OnSessionStreamReceived;
            session.StreamDropped -= OnSessionStreamDropped;
            session.Error -= OnSessionError;
        }

        void SetPublisher()
        {
            if (_publisher != null) return;

            if (!_session.GetCapabilities().CanPublish)
            {
                System.Diagnostics.Debug.WriteLine("Cannot publish");
                return;
            }

            // Publish an stream to session
            _publisher = new Publisher.Builder(_context)
                .Resolution(Publisher.CameraCaptureResolution.High)
                .FrameRate(Publisher.CameraCaptureFrameRate.Fps15)
                .Build() as Publisher;
            SubscribeForPublisherEvents(_publisher);
            _session.Publish(_publisher);
            // Create publisher view
            _publisherContainer.AddView(_publisher.View);

            if (_publisher.View is Android.Opengl.GLSurfaceView)
                ((Android.Opengl.GLSurfaceView)_publisher.View).SetZOrderOnTop(true);

            // Init wavelock
            if (_wakeLock != null)
                _wakeLock.Acquire();
        }

        void SubscribeForPublisherEvents(Publisher publisher)
        {
            if (publisher == null) return;

            publisher.StreamCreated += OnPublisherStreamCreated;
            publisher.StreamDestroyed += OnPublisherStreamDestroyed;
            publisher.Error += OnPublisherError;
        }

        void UnsubscribeForPublisherEvents(Publisher publisher)
        {
            if (publisher == null) return;

            publisher.StreamCreated -= OnPublisherStreamCreated;
            publisher.StreamDestroyed -= OnPublisherStreamDestroyed;
            publisher.Error -= OnPublisherError;
        }

        void SubscribeStream(Stream stream)
        {
            if (_subscriber != null) return;

            // Create the susbcriber
            _subscriber = new Subscriber.Builder(_context, stream).Build() as Subscriber;
            _subscriber.SetStyle(BaseVideoRenderer.StyleVideoScale, BaseVideoRenderer.StyleVideoFill);
            _subscriber.Connected += (s, e) =>
            {
                if (this.OnPartnerConnectedCommand != null)
                    OnPartnerConnectedCommand.Execute(null);
            };
            _session.Subscribe(_subscriber);
            // Create subscriber view
            _subscriberContainer.AddView(_subscriber.View);
        }

        void UnsubscribeStream()
        {
            if (_subscriber != null)
            {
                _subscriber.Destroy();
                _subscriber = null;
                DesactivateStreamContainer(_subscriberContainer);
            }
        }

        void DesactivateStreamContainer(ViewGroup streamContainer)
        {
            streamContainer.RemoveAllViews();
        }

        #region SESSION EVENTS

        private void OnSessionConnected(object sender, Session.ConnectedEventArgs e)
        {
            SetPublisher();
        }

        private void OnSessionDisconnected(object sender, Session.DisconnectedEventArgs e)
        {
            DisconnectSession();
        }

        private void OnSessionStreamReceived(object sender, Session.StreamReceivedEventArgs e)
        {
            SubscribeStream(e.P1);
        }

        private void OnSessionStreamDropped(object sender, Session.StreamDroppedEventArgs e)
        {
            UnsubscribeStream();
        }

        private void OnSessionError(object sender, Session.ErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Session error: {0}", e.P0);
        }

        #endregion

        #region PUBLISHER EVENTS

        private void OnPublisherStreamCreated(object sender, PublisherKit.StreamCreatedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Publiser stream created");
        }

        private void OnPublisherStreamDestroyed(object sender, PublisherKit.StreamDestroyedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Publiser stream destroyed");
        }

        private void OnPublisherError(object sender, PublisherKit.ErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Publiser error: {0}", e.P1.Message);
        }

        #endregion
    }
}
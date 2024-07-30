using AlexPacientes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using OpenTok;
using System.Diagnostics;
using AlexPacientes.Controls;
using AVFoundation;

[assembly: Xamarin.Forms.Dependency(typeof(AlexPacientes.iOS.Services.OpenTokService))]
namespace AlexPacientes.iOS.Services
{
    public class OpenTokService : IOpenTokService
    {
        private string _token;

        private OTSession _session;
        private OTPublisher _publisher;
        private OTSubscriber _subscriber;

        private UIView _publisherContainer;
        private UIView _subscriberContainer;

        public Xamarin.Forms.Command OnPartnerConnectedCommand { get; set; }

        public void EndSession()
        {
            DisconnectSession();
        }

        public void InitNewSession(string apiKey, string sessionID, string token)
        {
            InitSession(apiKey, sessionID, token);
        }

        public void Connect()
        {
            ConnectToSession();
        }

        public void SetHostContainer(StreamView streamView)
        {
            _publisherContainer = streamView.NativeView as UIView;
        }

        public void SetClientContainer(StreamView streamView)
        {
            _subscriberContainer = streamView.NativeView as UIView;
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
            {
                if (_publisher.CameraPosition == AVCaptureDevicePosition.Back)
                    _publisher.CameraPosition = AVCaptureDevicePosition.Front;
                else
                    _publisher.CameraPosition = AVCaptureDevicePosition.Back;
            }
        }

        void InitSession(string apiKey, string sessionId, string token)
        {
            // Store token
            _token = token;

            // Create OpenTok session
            _session = new OTSession(apiKey, sessionId, null);
            // Subscribe for events
            SubscribeForSessionEvents(_session);
            // Connect session
            ConnectToSession();
        }

        void ConnectToSession()
        {
            if (_session == null) return;

            OTError error;
            _session.ConnectWithToken(_token, out error);
            if(error != null)
            {
                Debug.WriteLine("Error when trying to connect: {0}", error.LocalizedDescription);
            }
        }

        void DisconnectSession()
        {
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

        void SubscribeForSessionEvents(OTSession session)
        {
            if (session == null) return;

            session.DidConnect += OnSessionDidConnect;
            session.DidDisconnect += OnSessionDidDisconect;
            session.StreamCreated += OnSessionStreamCreated;
            session.StreamDestroyed += OnSessionStreamDestroyed;
            session.DidFailWithError += OnSessionError;
        }

        void UnsubscribeForSessionEvents(OTSession session)
        {
            if (session == null) return;

            session.DidConnect -= OnSessionDidConnect;
            session.DidDisconnect -= OnSessionDidDisconect;
            session.StreamCreated -= OnSessionStreamCreated;
            session.StreamDestroyed -= OnSessionStreamDestroyed;
            session.DidFailWithError -= OnSessionError;
        }

        async void SetPublisher()
        {
            if (_publisher != null) return;

            if (!_session.Capabilities.CanPublish)
            {
                Debug.WriteLine("Cannot publish");
                return;
            }

            if(!await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVAuthorizationMediaType.Video))
            {
                Debug.WriteLine("Camera permission is not granted");
                return;
            }

            // Publish an stream to session
            _publisher = new OTPublisher(null, new OTPublisherSettings 
            {
                Name = UIDevice.CurrentDevice.Name,
                CameraFrameRate = OTCameraCaptureFrameRate.OTCameraCaptureFrameRate15FPS,
                CameraResolution = OTCameraCaptureResolution.High
            });
            SubscribeForPublisherEvents(_publisher);
            OTError error;
            _session.Publish(_publisher, out error);
            // Create publisher view
            UIView streamView = _publisher.View;
            streamView.Frame = new CoreGraphics.CGRect(0, 0, _publisherContainer.Frame.Width, _publisherContainer.Frame.Height);
            _publisherContainer.AddSubview(streamView);
            
        }

        void SubscribeForPublisherEvents(OTPublisher publisher)
        {
            if (publisher == null) return;

            publisher.StreamCreated += OnPublisherStreamCreated;
            publisher.StreamDestroyed += OnPublisherStreamDestroyed;
            publisher.DidFailWithError += OnPublisherError;
        }

        void UnsubscribeForPublisherEvents(OTPublisher publisher)
        {
            if (publisher == null) return;

            publisher.StreamCreated -= OnPublisherStreamCreated;
            publisher.StreamDestroyed -= OnPublisherStreamDestroyed;
            publisher.DidFailWithError -= OnPublisherError;
        }

        void SubscribeStream(OTStream stream)
        {
            if (_subscriber != null) return;

            // Create the susbcriber
            _subscriber = new OTSubscriber(stream, null);
            _session.Subscribe(_subscriber);
            _subscriber.VideoDataReceived += (s, e) =>
            {
                if (this.OnPartnerConnectedCommand != null)
                    OnPartnerConnectedCommand.Execute(null);
            };
            // Create subscriber view
            UIView streamView = _subscriber.View;
            streamView.Frame = new CoreGraphics.CGRect(0, 0, _subscriberContainer.Frame.Width, _subscriberContainer.Frame.Height);
            _subscriberContainer.AddSubview(streamView);
        }

        void UnsubscribeStream()
        {
            if (_subscriber != null)
            {
                _subscriber.Dispose();
                _subscriber = null;
                DesactivateStreamContainer(_subscriberContainer);
            }
        }

        void DesactivateStreamContainer(UIView streamContainer)
        {
            while (streamContainer.Subviews.Length > 0)
                streamContainer.Subviews[0].RemoveFromSuperview();
        }

        #region SESSION EVENTS

        private void OnSessionDidConnect(object sender, EventArgs e)
        {
            Debug.WriteLine("Session connected");
            SetPublisher();
        }

        private void OnSessionDidDisconect(object sender, EventArgs e)
        {
            Debug.WriteLine("Session disconnected");
            DisconnectSession();
        }

        private void OnSessionStreamCreated(object sender, OTSessionDelegateStreamEventArgs e)
        {
            Debug.WriteLine("Stream created");
            SubscribeStream(e.Stream);
        }

        private void OnSessionStreamDestroyed(object sender, OTSessionDelegateStreamEventArgs e)
        {
            Debug.WriteLine("Stream destroyed");
            UnsubscribeStream();
        }

        private void OnSessionError(object sender, OTSessionDelegateErrorEventArgs e)
        {
            Debug.WriteLine("Session error: {0}", e.Error.DebugDescription);
        }

        #endregion

        #region PUBLISHER EVENTS

        private void OnPublisherStreamCreated(object sender, OTPublisherDelegateStreamEventArgs e)
        {
            Debug.WriteLine("Publiser stream created");
        }

        private void OnPublisherStreamDestroyed(object sender, OTPublisherDelegateStreamEventArgs e)
        {
            Debug.WriteLine("Publiser stream destroyed");
        }

        private void OnPublisherError(object sender, OTPublisherDelegateErrorEventArgs e)
        {
            Debug.WriteLine("Publiser error: {0}", e.Error.DebugDescription);
        }

        #endregion
    }
}
using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using AVKit;
using AVFoundation;

namespace AlexPacientes.iOS
{
    [Register("UniversalView")]
    public class SplashView : UIView
    {
        private UIImageView _logo;

        public UIButton SkipButton { get; set; }

        public SplashView()
        {
            Initialize();
        }

        public SplashView(RectangleF bounds) : base(bounds)
        {
            Initialize();
        }

        void Initialize()
        {
            _logo = new UIImageView(new UIImage("SplashLogo.png"));
            _logo.Frame = new CoreGraphics.CGRect(16, 32, 120, 100);
            AddSubview(_logo);

            SkipButton = new UIButton();
            SkipButton.SetTitle("Saltar", UIControlState.Normal);
            SkipButton.TitleLabel.TextColor = UIColor.FromRGB(36, 171, 115);
            AddSubview(SkipButton);

            BackgroundColor = UIColor.Black;
        }

        public void Layout()
        {
            //SkipButton.LeadingAnchor.ConstraintEqualTo(LeadingAnchor).Active = true;
            //SkipButton.TrailingAnchor.ConstraintEqualTo(TrailingAnchor).Active = true;
            //SkipButton.BottomAnchor.ConstraintEqualTo(BottomAnchor).Active = true;

            SkipButton.Frame = new CoreGraphics.CGRect(Frame.X, Frame.Height - 100, Frame.Width, 100);
        }
    }

    [Register("SplashViewController")]
    public class SplashViewController : UIViewController
    {
        private AVPlayerLayer _playerLayer;
        private readonly NSUrl _videoUrl = new NSUrl(NSBundle.MainBundle.PathForResource("video", "mp4"), false);
        public event EventHandler Skipped;
        private bool _loadingForms;

        public SplashViewController()
        {
            SetVideoPlayer();
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            var splash = new SplashView();
            splash.SkipButton.TouchUpInside += SkipSplash;
            View = splash;

            base.ViewDidLoad();

            // Perform any additional setup after loading the view
        }

        private void SkipSplash(object sender, EventArgs e)
        {
            if (_loadingForms) return;
            _loadingForms = true;
            _playerLayer.Player.Pause();
            Skipped?.Invoke(this, EventArgs.Empty);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            View.Layer.InsertSublayer(_playerLayer, 0);
            _playerLayer.Player.Play();
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            _playerLayer.Player.Pause();
        }

        public override void ViewDidLayoutSubviews()
        {
            _playerLayer.Frame = View.Frame;
            _playerLayer.VideoGravity = AVLayerVideoGravity.ResizeAspectFill;

            (View as SplashView).Layout();

            base.ViewDidLayoutSubviews();
        }

        private void SetVideoPlayer()
        {
            _playerLayer = new AVPlayerLayer();
            _playerLayer.Player = new AVPlayer(_videoUrl);
            NSNotificationCenter.DefaultCenter.AddObserver(AVPlayerItem.DidPlayToEndTimeNotification, name => SkipSplash(this, EventArgs.Empty), _playerLayer.Player.CurrentItem);
        }
    }
}
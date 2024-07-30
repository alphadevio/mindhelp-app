using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Constraints;
using Android.Util;
using Android.Views;
using Android.Widget;
using AlexPacientes.Droid.Controls;
using Android.Media;
using Android.Support.V4.Content;

namespace AlexPacientes.Droid
{
    [Activity(Label = "MindHelp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, NoHistory = true)]
    public class SplashActivity : Activity, TextureVideoView.IMediaPlayerListener
    {
        private TextureVideoView videoView;
        private bool loadingForms;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Window.SetFlags(WindowManagerFlags.LayoutNoLimits, WindowManagerFlags.LayoutNoLimits);

            var density = Resources.DisplayMetrics.Density;

            // Root container
            ConstraintLayout constraintLayout = new ConstraintLayout(this);
            constraintLayout.SetBackgroundColor(Android.Graphics.Color.Black);
            constraintLayout.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            ConstraintSet set = new ConstraintSet();
            set.Clone(constraintLayout);

            // Image Placeholder
            ImageView imagePlaceholder = new ImageView(this);
            imagePlaceholder.SetImageResource(Resource.Drawable.SplashPlaceholder);
            imagePlaceholder.SetScaleType(ImageView.ScaleType.CenterCrop);
            imagePlaceholder.Id = View.GenerateViewId();
            constraintLayout.AddView(imagePlaceholder);
            set.Connect(imagePlaceholder.Id, ConstraintSet.Start, ConstraintSet.ParentId, ConstraintSet.Start);
            set.Connect(imagePlaceholder.Id, ConstraintSet.Top, ConstraintSet.ParentId, ConstraintSet.Top);
            set.Connect(imagePlaceholder.Id, ConstraintSet.End, ConstraintSet.ParentId, ConstraintSet.End);
            set.Connect(imagePlaceholder.Id, ConstraintSet.Bottom, ConstraintSet.ParentId, ConstraintSet.Bottom);
            set.ApplyTo(constraintLayout);

            // Video
            videoView = new TextureVideoView(this);
            videoView.Id = View.GenerateViewId();
            constraintLayout.AddView(videoView);
            set.Connect(videoView.Id, ConstraintSet.Start, ConstraintSet.ParentId, ConstraintSet.Start);
            set.Connect(videoView.Id, ConstraintSet.Top, ConstraintSet.ParentId, ConstraintSet.Top);
            set.Connect(videoView.Id, ConstraintSet.End, ConstraintSet.ParentId, ConstraintSet.End);
            set.Connect(videoView.Id, ConstraintSet.Bottom, ConstraintSet.ParentId, ConstraintSet.Bottom);
            set.ApplyTo(constraintLayout);

            // Logo
            ImageView logo = new ImageView(this);
            logo.SetImageResource(Resource.Drawable.SplashLogo);
            logo.Id = View.GenerateViewId();
            constraintLayout.AddView(logo);
            set.Connect(logo.Id, ConstraintSet.Start, ConstraintSet.ParentId, ConstraintSet.Start, (int)Math.Round(16 * density));
            set.Connect(logo.Id, ConstraintSet.Top, ConstraintSet.ParentId, ConstraintSet.Top, (int)Math.Round(32 * density));
            set.ConstrainWidth(logo.Id, (int)Math.Round(120 * density));
            set.ConstrainHeight(logo.Id, (int)Math.Round(100 * density));
            set.ApplyTo(constraintLayout);

            // Skip label
            TextView skip = new TextView(this);
            skip.Id = View.GenerateViewId();
            skip.Text = "Saltar";
            skip.TextSize = 10 * density;
            skip.TextAlignment = TextAlignment.Center;
            skip.Gravity = GravityFlags.CenterHorizontal;
            skip.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.colorPrimary)));
            skip.SetOnClickListener(new Utilities.CallbackWrappers.View.OnClickListener(v => SkipToLogin()));
            constraintLayout.AddView(skip);
            set.Connect(skip.Id, ConstraintSet.Start, ConstraintSet.ParentId, ConstraintSet.Start);
            set.Connect(skip.Id, ConstraintSet.End, ConstraintSet.ParentId, ConstraintSet.End);
            set.Connect(skip.Id, ConstraintSet.Bottom, ConstraintSet.ParentId, ConstraintSet.Bottom, (int)Math.Round(10 * density));
            set.ConstrainWidth(skip.Id, (int)Math.Round(100 * density));
            set.ConstrainHeight(skip.Id, (int)Math.Round(50 * density));
            set.ApplyTo(constraintLayout);

            SetContentView(constraintLayout);

            PlayVideo();
        }

        private void PlayVideo()
        {
            var videoUri = Android.Net.Uri.Parse("android.resource://" + PackageName + "/" + Resource.Raw.video);
            var metrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(metrics);
            var p = videoView.LayoutParameters as ConstraintLayout.LayoutParams;
            p.Width = metrics.WidthPixels;
            p.Height = metrics.HeightPixels;
            p.LeftMargin = 0;
            // fragmentSplashScreenBinding.videoview.setLayoutParams(params);
            var dm = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(dm);
            var x = Math.Pow(dm.WidthPixels / dm.Xdpi, 3.0);
            var y = Math.Pow(dm.HeightPixels / dm.Ydpi, 3.0);
            var screenInches = Math.Sqrt(x + y);
            videoView.SetDataSource(this, videoUri);
            // Use setScaleType method to crop video
            videoView.SetScaleType(TextureVideoView.ScaleType.CenterCrop);
            videoView.SetListener(this);
        }

        private void SkipToLogin()
        {
            if (loadingForms) return;
            loadingForms = true;
            videoView.Stop();
            StartActivity(new Intent(this, typeof(MainActivity)));
            Finish();
        }

        public void OnVideoEnd()
        {
            SkipToLogin();
        }

        public void OnVideoPrepared()
        {
            videoView.Play();
        }
    }
}
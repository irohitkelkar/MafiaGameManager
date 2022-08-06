using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Mafia_GameManager.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer))]
namespace Mafia_GameManager.Droid.Renderers
{
	public class CustomPickerRenderer : PickerRenderer
	{
		Picker element;
		public CustomPickerRenderer(Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);

			element = Element;

			if (Control != null && Element != null)
				Control.Background = null;

		}

		public LayerDrawable AddPickerStyles()
		{
			ShapeDrawable border = new ShapeDrawable();
			border.Paint.Color = Android.Graphics.Color.Black;
			border.SetPadding(10, 10, 10, 10);
			border.Paint.SetStyle(Paint.Style.Stroke);

			Drawable[] layers = { border };
			LayerDrawable layerDrawable = new LayerDrawable(layers);
			layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

			return layerDrawable;
		}
	}
}

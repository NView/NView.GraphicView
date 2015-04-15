using System;

using Android.Views;

using NGraphics;
using Android.Content;
using Android.Runtime;

namespace NView.GraphicView
{
	[Preserve]
	public class GraphicView : IView
	{
		public virtual void Draw (ICanvas canvas, Rect dirtyRect)
		{
		}

		#region IView implementation
		public IDisposable BindToNative (object nativeView)
		{
			var view = ViewHelpers.GetView<NativeGraphicView> (nativeView);
			view.GraphicView = this;
			return new DisposeAction (() => {
				if (view.GraphicView == this)
					view.GraphicView = null;
			});
		}
		public Type PreferredNativeType {
			get {
				return typeof(NativeGraphicView);
			}
		}
		#endregion
	}

	public class NativeGraphicView : View
	{
		public NativeGraphicView (Context c)
			: base (c)
		{			
		}

		public GraphicView GraphicView;
		public override void Draw (global::Android.Graphics.Canvas canvas)
		{
			var c = new CanvasCanvas (canvas);
			if (GraphicView != null) {
				var r = new global::Android.Graphics.Rect ();
				canvas.GetClipBounds (r);
				GraphicView.Draw (c, new Rect (r.Left, r.Top, r.Width (), r.Height ()));
			}
		}
	}
}


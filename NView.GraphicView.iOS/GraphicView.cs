using System;

using UIKit;

using NGraphics;
using Foundation;

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

	public class NativeGraphicView : UIView
	{
		public GraphicView GraphicView;

		public override void Draw (CoreGraphics.CGRect dirtyRect)
		{
			var c = new CGContextCanvas (UIGraphics.GetCurrentContext ());
			if (GraphicView != null) {
				GraphicView.Draw (c, Conversions.GetRect (dirtyRect));
			}
		}
	}
}


using System;

using AppKit;

using NGraphics;

namespace NView.GraphicView
{
	public class GraphicView : IView
	{
		public virtual void Draw (ICanvas canvas, Rect dirtyRect)
		{
		}

		#region IView implementation
		public IDisposable BindToNative (object nativeView)
		{
			var view = PlatformHelpers.GetView<NativeGraphicView> (nativeView);
			view.GraphicView = this;
			return PlatformHelpers.CreateDisposable (() => {
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

	public class NativeGraphicView : NSView
	{
		public GraphicView GraphicView;

		public override void DrawRect (CoreGraphics.CGRect dirtyRect)
		{
			var c = new CGContextCanvas (NSGraphicsContext.CurrentContext.CGContext);
			if (GraphicView != null) {
				GraphicView.Draw (c, Conversions.GetRect (dirtyRect));
			}
		}
	}
}


using System;
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
			throw new NotImplementedException ();
		}
		public Type PreferredNativeType {
			get {
				throw new NotImplementedException ();
			}
		}
		#endregion		
	}
}


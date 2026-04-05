using System;
using Avalonia.Controls;
using Avalonia.Platform.Interop;

namespace CodeImp.DoomBuilder.Avalonia.Rendering
{
	/// <summary>
	/// Avalonia control that hosts the native OpenGL rendering surface.
	/// This is the cross-platform equivalent of the WinForms RenderTargetControl.
	/// 
	/// It uses Avalonia's NativeControlHost to embed a native window that the
	/// C++ OpenGL renderer (BuilderNative) can use for rendering.
	/// </summary>
	public class GlRenderControl : NativeControlHost
	{
		/// <summary>
		/// Gets the native window handle for use with the OpenGL context.
		/// </summary>
		public IntPtr NativeHandle { get; private set; }
	}
}

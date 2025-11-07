using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering;
using Avalonia.Threading;
using System;

namespace UltimateDoomBuilder.Avalonia.Controls
{
    /// <summary>
    /// Base class for OpenGL rendering controls in Avalonia.
    /// This provides the foundation for cross-platform OpenGL rendering
    /// similar to the original WinForms RenderTargetControl.
    /// </summary>
    public abstract class OpenGLControlBase : Control
    {
        private bool _isInitialized;
        
        static OpenGLControlBase()
        {
            AffectsRender<OpenGLControlBase>(BoundsProperty);
        }

        public OpenGLControlBase()
        {
            ClipToBounds = true;
        }

        /// <summary>
        /// Called once when the control is ready for OpenGL initialization.
        /// Override this to set up OpenGL context, shaders, buffers, etc.
        /// </summary>
        protected abstract void OnOpenGLInit();

        /// <summary>
        /// Called every frame to render OpenGL content.
        /// Override this to perform OpenGL rendering.
        /// </summary>
        protected abstract void OnOpenGLRender();

        /// <summary>
        /// Called when the control is being disposed.
        /// Override this to clean up OpenGL resources.
        /// </summary>
        protected virtual void OnOpenGLCleanup()
        {
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
            
            if (!_isInitialized)
            {
                try
                {
                    OnOpenGLInit();
                    _isInitialized = true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"OpenGL initialization failed: {ex.Message}");
                }
            }
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromVisualTree(e);
            
            if (_isInitialized)
            {
                try
                {
                    OnOpenGLCleanup();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"OpenGL cleanup failed: {ex.Message}");
                }
                finally
                {
                    _isInitialized = false;
                }
            }
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);
            
            if (!_isInitialized)
                return;

            try
            {
                OnOpenGLRender();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"OpenGL render failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Request a redraw of the control.
        /// </summary>
        public void RequestRender()
        {
            Dispatcher.UIThread.Post(InvalidateVisual, DispatcherPriority.Render);
        }
    }
}

// onotseike@hotmail.comPaula Aliu
using System;
namespace WebRTC.Unified.Core.Interfaces
{
    /// <summary>
    /// This is an Interface needed to repsent the .NET equivalent of all Native WebRTC Objects.
    /// All .NET equivalent of WebRTC objects should inherit from this interface
    /// </summary>
    public interface INativeObject : IDisposable
    {
        object NativeObject { get; }
    }
}

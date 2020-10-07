// onotseike@hotmail.comPaula Aliu
using System;
namespace WebRTC.DemoApp.Helper
{
    public static class GenerateRoom
    {
        #region Method(s)

        public static string GenerateRoomName() => Guid.NewGuid().ToString();

        #endregion
    }
}

// onotseike@hotmail.comPaula Aliu
using System;
using System.Collections.Generic;

namespace WebRTC.Unified.Core.Interfaces
{
    public interface IStatistics
    {
        string Id { get; }

        double Timestamp_us { get; }

        string Type { get; }

        Dictionary<string, object> Values { get; }
    }

    public interface IStatisticsReport
    {
        double Timestamp_us { get; }

        Dictionary<string, object> Statistics { get; }
    }
}



using System;

namespace Ftech.Lib
{
    [Serializable]
    struct JsonDateTime
    {
        public long v;
        public static implicit operator DateTime(JsonDateTime jdt)
        {
            return new DateTime(jdt.v);
        }
        public static implicit operator JsonDateTime(DateTime dt)
        {
            JsonDateTime jdt = new JsonDateTime();
            jdt.v = dt.Ticks;
            return jdt;
        }
    }

    [Serializable]
    struct JsonTimeSpan
    {
        public long v;

        public static implicit operator TimeSpan(JsonTimeSpan jts)
        {
            return new TimeSpan(jts.v);
        }
        public static implicit operator JsonTimeSpan(TimeSpan ts)
        {
            JsonTimeSpan jdt = new JsonTimeSpan();
            jdt.v = ts.Ticks;
            return jdt;
        }
    }
}
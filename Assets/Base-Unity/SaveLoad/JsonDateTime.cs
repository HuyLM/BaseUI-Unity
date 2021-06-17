

using System;

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

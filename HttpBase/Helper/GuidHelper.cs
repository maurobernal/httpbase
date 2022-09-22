using System.Diagnostics;

namespace HttpBase.Helper;

public static class GuidHelper
{
    [DebuggerStepThrough]
    public static Guid NewGuid(string guid = null)
    {
        return string.IsNullOrWhiteSpace(guid) ? Guid.NewGuid() : new Guid(guid);
    }

    [DebuggerStepThrough]
    public static Guid DefaultGuid(string guid = null)
    {
        return string.IsNullOrWhiteSpace(guid) ? new Guid("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAA") : new Guid(guid);
    }

    [DebuggerStepThrough]
    public static Guid EmptyGuid(string guid = null)
    {
        return string.IsNullOrWhiteSpace(guid) ? Guid.Empty : new Guid(guid);
    }
}

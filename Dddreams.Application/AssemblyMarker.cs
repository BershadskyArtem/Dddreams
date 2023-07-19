using System.Reflection;

namespace Dddreams.Application;

public class AssemblyMarker
{
    public static Assembly Assembly => typeof(AssemblyMarker).Assembly;
}
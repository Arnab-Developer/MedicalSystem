using System.Reflection;

namespace MedicalSystem.FrontEnds.WebMvc
{
    public static class Helper
    {
        static Helper()
        {
            ApplicationVersionNumber = string.Empty;
            var executingAssembly = Assembly.GetExecutingAssembly();
            if (executingAssembly == null) return;
            var assemblyName = executingAssembly.GetName();
            if (assemblyName == null) return;
            var assemblyVersion = assemblyName.Version;
            if (assemblyVersion == null) return;
            ApplicationVersionNumber = $" - Version {assemblyVersion.ToString(2)}";
        }

        public static string ApplicationVersionNumber { get; }
    }
}

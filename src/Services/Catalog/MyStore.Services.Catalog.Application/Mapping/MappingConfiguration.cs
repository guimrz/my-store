using AutoMapper;
using MyStore.Services.Catalog.Application.Mapping.Profiles;
using System.Reflection;

#nullable disable
namespace MyStore.Services.Catalog.Application.Mapping
{
    public static class MappingConfiguration
    {
        public static IEnumerable<Profile> GetProfiles()
        {
            return GetAssemblyProfiles(typeof(CategoryProfile).Assembly);
        }

        private static IEnumerable<Profile> GetAssemblyProfiles(Assembly assembly)
        {
            return assembly.DefinedTypes.Where(p => p.IsAssignableTo(typeof(Profile))).Select(type => Activator.CreateInstance(type) as Profile);
        }
    }
}

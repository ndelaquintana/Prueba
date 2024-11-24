using Autofac;
using System.Reflection;

namespace Carglass.TechnicalAssessment.Backend.DL;

public class Module : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterRepositories(builder);
    }

    private static void RegisterRepositories(ContainerBuilder builder)
    {
        // TODO CodeReview
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(x => !x.IsAbstract && !x.IsInterface && x.Name.EndsWith("IMRepository"))
            .AsImplementedInterfaces()
            .SingleInstance();
    }
}
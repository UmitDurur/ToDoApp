using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ToDoFrontEnd;

[Dependency(ReplaceServices = true)]
public class ToDoFrontEndBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ToDoFrontEnd";
}
